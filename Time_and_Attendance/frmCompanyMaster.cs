using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace Time_and_Attendance
{
    public partial class frmCompanyMaster : Form
    {
        public frmCompanyMaster()
        {
            InitializeComponent();
        }

        int row;
        string dataSourcePath;
        string newOld = "";

        private void frmCompanyMaster_Load(object sender, EventArgs e)
        {
            chkActive.Checked = true;
            dataSourcePath = "Data Source = " + Application.StartupPath + @"\TADatabase.sdf";

            dgvCompDet.DataSource = "TADatabase.sdf";
            try
            {
                using (SqlCeConnection c = new SqlCeConnection(dataSourcePath))
                {
                    c.Open();
                    using (SqlCeDataAdapter a = new SqlCeDataAdapter("SELECT CO_COMP_ID as Company_Id,CO_COMP_NAME as Company_Name, CO_ACTIVE as Active_Status, CO_FILE_TYPE as Using_File_Type, CO_DESC as Description  FROM CompRegister", c))
                    {
                        DataTable t = new DataTable();
                        a.Fill(t);
                        dgvCompDet.DataSource = t;
                        fnDeselectRow();
                    }
                    c.Close();
                }
            }
            catch { }
        }

        private void fnDeselectRow()
        {
            int rowCount = dgvCompDet.Rows.Count;
            if (rowCount >= 1)
                dgvCompDet.Rows[0].Selected = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            SqlCeConnection sqlConnection1 = new SqlCeConnection();
            sqlConnection1.ConnectionString = dataSourcePath;

            string active = "";
            if (chkActive.Checked == true)
                active = "Active";
            else
                active = "Not Active";

            if (txtCompName.Text != string.Empty && txtDescription.Text != string.Empty)
            {
                int rowCount = dgvCompDet.Rows.Count;
                if (rowCount < 10)
                {
                    rowCount++;
                    SqlCeCommand cmd = new SqlCeCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();
                    SqlCeDataReader SQLRD;
                    cmd.CommandText = "SELECT COUNT(*) FROM CompRegister WHERE CO_COMP_NAME='" + txtCompName.Text.Trim() + "'";
                    int dup = Convert.ToInt32(cmd.ExecuteScalar());
                    if (dup == 0)
                    {
                        if (newOld != "old")
                        {

                            try
                            {
                                cmd.CommandText = "INSERT INTO CompRegister VALUES(" + rowCount.ToString() + ",'" + txtCompName.Text.Trim() + "','" + active + "','','" + txtDescription.Text + "')";
                                cmd.ExecuteNonQuery();
                            }
                            catch
                            { }
                        }
                        else if (newOld == "old")
                        {
                            cmd.CommandText = "UPDATE CompRegister SET CO_COMP_NAME='" + txtCompName.Text + "',CO_ACTIVE='" + active + "',CO_DESC='" + txtDescription.Text + "' WHERE CO_COMP_ID=" + row.ToString();
                            cmd.ExecuteNonQuery();
                            newOld = "";
                        }
                        fnUpdateRows();   
                    }
                    else
                    {
                        MessageBox.Show("Company name already registred.\nEnter different name.","Error: Company Name");
                        txtCompName.SelectAll();
                        txtCompName.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("You can save only 10 Company details", "Error: Max Companies");
                    btnSave.Enabled = false;
                }             
            }
            else if(txtCompName.Text==string.Empty)
            {
                MessageBox.Show("Enter Company name", "Error: Company Name");
                txtCompName.Focus();
            }
            else if (txtDescription.Text == string.Empty)
            {
                MessageBox.Show("Enter Company Description", "Error: Company Description");
                txtDescription.Focus();
            }
            btnSave.Enabled = true;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            newOld = "new";
            txtCompName.Text = "";
            txtDescription.Text = "";
            chkActive.Checked = true;
            fnDeselectRow();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCeConnection sqlConnection1 = new SqlCeConnection();
            sqlConnection1.ConnectionString = dataSourcePath;
            if (txtCompName.Text != string.Empty && txtDescription.Text != string.Empty)
            {
                SqlCeCommand cmd = new SqlCeCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();

                cmd.CommandText = "SELECT * FROM CompRegister WHERE CO_COMP_NAME='" + txtCompName.Text + "'";
                SqlCeDataReader SQLRD;
                SQLRD = cmd.ExecuteReader();
                SQLRD.Read();
                string compid = SQLRD[0].ToString();
                SQLRD.Close();

                cmd.CommandText = "DELETE FROM CompRegister WHERE CO_COMP_ID='" + compid + "'";
                cmd.ExecuteNonQuery();
                newOld = "";
                try
                {
                    cmd.CommandText = "DELETE FROM MDB_FILE_SETTINGS WHERE MDB_COMP_ID='" + compid + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM MDB_FILE_FIELD_SETTINGS WHERE FIE_MDB_ID='" + compid + "'";
                    cmd.ExecuteNonQuery();
                }
                catch { }

                try
                {
                    cmd.CommandText = "DELETE FROM SQL_DATA_SETTINGS WHERE SQL_COMP_ID='" + compid + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM SQL_DATA_FIELD_SETTINGS WHERE SQL_DATA_FIELD_SETTINGS='" + compid + "'";
                    cmd.ExecuteNonQuery();
                }
                catch { }

                try
                {
                    cmd.CommandText = "DELETE FROM TEXT_FILE_SETTINGS WHERE TEXT_COM_ID='" + compid + "'";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM TEXT_FILE_NAME_FORMAT WHERE TEXT_NAME_FORMAT_ID='" + compid + "'";
                    cmd.ExecuteNonQuery();
                }
                catch { }
            }
            fnUpdateRowIndex();
            fnUpdateRows();
        }

        private void fnUpdateRowIndex()
        {
            int rowsCount = dgvCompDet.Rows.Count;
            if (row != (rowsCount+1))
            {
                //MessageBox.Show(row.ToString());
                fnUpdateRowsIndex2();

            }
            else
            {
                //MessageBox.Show("Last Row");
            }
        }

        private void fnUpdateRowsIndex2()
        {
            using (SqlCeConnection c = new SqlCeConnection(dataSourcePath))
            {
                c.Open();
                using (SqlCeDataAdapter a = new SqlCeDataAdapter("SELECT * FROM CompRegister WHERE CO_COMP_ID=" + (row + 1).ToString(), c))
                {
                    DataTable t = new DataTable();
                    a.Fill(t);
                    //dgvCompDet.DataSource = t;
                    //MessageBox.Show(t.Rows.Count.ToString());
                    if ((t.Rows.Count) != 0)
                    {
                        c.Close();
                        SqlCeConnection sqlConnection1 = new SqlCeConnection();
                        sqlConnection1.ConnectionString = dataSourcePath;
                        SqlCeCommand cmd = new SqlCeCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "UPDATE CompRegister SET CO_COMP_ID=" + row + " WHERE CO_COMP_ID=" + (row + 1).ToString();
                        cmd.Connection = sqlConnection1;
                        sqlConnection1.Open();
                        cmd.ExecuteNonQuery();

                        try
                        {
                            cmd.CommandText = "UPDATE MDB_FILE_SETTINGS SET MDB_ID=" + row + ",MDB_COMP_ID=" + row + " WHERE MDB_COMP_ID=" + (row + 1).ToString();
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE MDB_FILE_FIELD_SETTINGS SET FIE_MDB_ID=" + row + " WHERE FIE_MDB_ID=" + (row + 1).ToString();
                            cmd.ExecuteNonQuery();
                        }
                        catch
                        {
                        }

                        try
                        {
                            cmd.CommandText = "UPDATE SQL_DATA_SETTINGS SET SQL_COMP_ID=" + row + " WHERE SQL_COMP_ID=" + (row + 1).ToString();
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE SQL_DATA_FIELD_SETTINGS SET SQL_TAB_COMP_ID=" + row + " WHERE SQL_TAB_COMP_ID=" + (row + 1).ToString();
                            cmd.ExecuteNonQuery();
                        }
                        catch
                        {
                        }

                        try
                        {
                            cmd.CommandText = "UPDATE TEXT_FILE_SETTINGS SET TEXT_COM_ID=" + row + " WHERE TEXT_COM_ID=" + (row + 1).ToString();
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE TEXT_FILE_NAME_FORMAT SET TEXT_NAME_FORMAT_ID=" + row + " WHERE TEXT_NAME_FORMAT_ID=" + (row + 1).ToString();
                            cmd.ExecuteNonQuery();
                        }
                        catch
                        {
                        }

                        sqlConnection1.Dispose();
                        sqlConnection1.Close();
                        row++; 
                        fnUpdateRowsIndex2();
                    }
                    else
                    {
                        c.Close();
                    }
                    fnDeselectRow();
                }
            }
        }

        private void fnUpdateRows()
        {
            //dgvCompDet.Rows.Clear();
            dgvCompDet.DataSource = "TADatabase.sdf";
            dgvCompDet.ClearSelection();
            using (SqlCeConnection c = new SqlCeConnection(dataSourcePath))
            {
                c.Open();
                using (SqlCeDataAdapter a = new SqlCeDataAdapter("SELECT CO_COMP_ID as Company_Id,CO_COMP_NAME as Company_Name, CO_ACTIVE as Active_Status, CO_FILE_TYPE as Using_File_Type, CO_DESC as Description FROM CompRegister", c))
                {
                    DataTable t = new DataTable();
                    a.Fill(t);
                    dgvCompDet.DataSource = t;
                    fnDeselectRow();
                }
                c.Close();
            }
            txtCompName.Text = "";
            txtDescription.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCompDet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            newOld = "old";
            int rowindex = e.RowIndex;
            if (rowindex != -1)
            {
                Global.Indexrow = Convert.ToInt32(dgvCompDet.Rows[rowindex].Cells[0].Value);
                row = Convert.ToInt32(dgvCompDet.Rows[rowindex].Cells[0].Value);
                txtCompName.Text = dgvCompDet.Rows[rowindex].Cells[1].Value.ToString();
                txtDescription.Text = dgvCompDet.Rows[rowindex].Cells[4].Value.ToString();
                string chk = dgvCompDet.Rows[rowindex].Cells[2].Value.ToString();
                if (chk.Equals("Active"))
                {
                    chkActive.Checked = true;
                }
                else if (chk.Equals("Not Active"))
                {
                    chkActive.Checked = false;
                }
            }
        }

    }
}