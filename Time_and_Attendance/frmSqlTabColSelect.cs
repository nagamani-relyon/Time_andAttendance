using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace Time_and_Attendance
{
    public partial class frmSqlTabColSelect : Form
    {
        public frmSqlTabColSelect()
        {
            InitializeComponent();
        }

        bool loaded = false;

        //fieds selections variables
        int dt = 0;
        int d = 0;
        int iot = 0;
        int it = 0;
        int ot = 0;

        private void frmSqlTabColSelect_Load(object sender, EventArgs e)
        {
            lbServer.Text = Global.SQLServer;
            lbDatabase.Text = Global.SQLDatabase;
            if (Global.SetSQLValues != "y")
                Global.SetSQLValues = "";
            fnGetTableList();
            if (Global.selUpdateId != 0 && Global.SelUpdate == true)
            {
                fnGetDBSettings();
            }
        }

        private void fnGetDBSettings()
        {
            string dataSourcePath = "Data Source = " + Application.StartupPath + @"\TADatabase.sdf";
            SqlCeConnection sqlConnection1 = new SqlCeConnection();
            sqlConnection1.ConnectionString = dataSourcePath;
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT * FROM SQL_DATA_FIELD_SETTINGS WHERE SQL_TAB_COMP_ID="+Global.selUpdateId.ToString();
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            SqlCeDataReader dataRead;
            dataRead = cmd.ExecuteReader();
            while (dataRead.Read())
            {
                int i = 0;
                foreach (string item in cboTable.Items)
                {
                    if (item == dataRead[6].ToString())
                    {
                        cboTable.SelectedIndex = i;
                        break;
                    }
                    i++;
                }
                lbServer.Text = dataRead[1].ToString();
                lbDatabase.Text = dataRead[2].ToString();
                i = 0;
                foreach (string emp in cmbEmpId.Items)
                {
                    if (emp == dataRead[7].ToString())
                    {
                        cmbEmpId.SelectedIndex = i;
                        break;
                    }
                    i++;
                }
                i = 0;
                foreach (string dev in cmbDevId.Items)
                {
                    if (dev == dataRead[8].ToString())
                    {
                        cmbDevId.SelectedIndex = i;
                        break;
                    }
                    i++;
                }
                panel2.Enabled = true;//emp
                panel9.Enabled = true;//device
                if (dataRead[9].ToString() == "" || dataRead[9].ToString() == string.Empty || dataRead[9].ToString() == null)
                {
                    if ((dataRead[10].ToString() == "" || dataRead[10].ToString() == string.Empty || dataRead[10].ToString() == null) && (dataRead[11].ToString() == "" || dataRead[11].ToString() == string.Empty || dataRead[11].ToString() == null))
                    {
                        i = 0;
                        foreach (string dev in cmbDate.Items)
                        {
                            if (dev == dataRead[12].ToString())
                            {
                                cmbDate.SelectedIndex = i;
                                break;
                            }
                            i++;
                        }
                        rdbnDateAndTime.Checked = true;
                        panel5.Enabled = true;//Date

                        if (dataRead[13].ToString() == "" || dataRead[13].ToString() == string.Empty || dataRead[13].ToString() == null)
                        {
                            i = 0;
                            foreach (string inti in cmbIn.Items)
                            {
                                if (inti == dataRead[14].ToString())
                                {
                                    cmbIn.SelectedIndex = i;
                                    break;
                                }
                                i++;
                            }
                            i = 0;
                            foreach (string inou in cmbOut.Items)
                            {
                                if (inou == dataRead[15].ToString())
                                {
                                    cmbOut.SelectedIndex = i;
                                    break;
                                }
                                i++;
                            }
                            rdbnInAndOut.Checked = true;
                            panel3.Enabled = false;//inout
                            panel4.Enabled = true;//in
                            panel7.Enabled = true;//out
                        }
                        else
                        {
                            i = 0;
                            foreach (string inout in cmbInOut.Items)
                            {
                                if (inout == dataRead[13].ToString())
                                {
                                    cmbInOut.SelectedIndex = i;
                                    break;
                                }
                                i++;
                            }
                            rdbnInOut.Checked = true;
                            panel3.Enabled = true;//inout
                            panel2.Enabled = true;//emp
                            panel9.Enabled = true;//device
                        }
                    }
                    else
                    {
                        i = 0;
                        foreach (string inti in cmbIn.Items)
                        {
                            if (inti == dataRead[10].ToString())
                            {
                                cmbIn.SelectedIndex = i;
                                break;
                            }
                            i++;
                        }
                        i = 0;
                        foreach (string tiou in cmbOut.Items)
                        {
                            if (tiou == dataRead[11].ToString())
                            {
                                cmbOut.SelectedIndex = i;
                                break;
                            }
                            i++;
                        }
                        rdbnDateTime.Checked = true;
                        rdbnInAndOut.Checked = true;

                        panel5.Enabled = false;//Date
                        panel3.Enabled = false;//inout
                        panel4.Enabled = true;//in
                        panel7.Enabled = true;//out
                    }
                }
                else
                {
                    i = 0;
                    foreach (string tiou in cmbInOut.Items)
                    {
                        if (tiou == dataRead[9].ToString())
                        {
                            cmbInOut.SelectedIndex = i;
                            break;
                        }
                        i++;
                    }
                    rdbnDateTime.Checked = true;
                    rdbnInOut.Checked = true;

                    panel4.Enabled = false;//in
                    panel5.Enabled = false;//Date
                    panel7.Enabled = false;//out
                    panel3.Enabled = true;//inout
                }
            }
            dataRead.Dispose();
            dataRead.Close();
            sqlConnection1.Dispose();
            sqlConnection1.Close();
        }

        private void fnGetTableList()
        {
            SqlConnection Conn = new SqlConnection(Global.SQLConnStr);
            try
            {
                Conn.Open();
            }
            catch (SqlException sqx)
            {
                //label8.Text = "Enter Correct username/Password";
                return;
            }
            List<string> result = new List<string>();
            SqlCommand cmd = new SqlCommand("SELECT name FROM sys.Tables", Conn);
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                result.Add(reader["name"].ToString());
            //return result.ToArray();

            string[] tables = result.ToArray();
            cboTable.SelectedIndex = -1;
            loaded = false;
            cboTable.DataSource = result;
            loaded = true;
            cboTable.SelectedIndex = -1;

            Conn.Close();
        }

        private void cboTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded == true && cboTable.SelectedIndex != -1)
            {
                cmbDate.Items.Clear();
                cmbDevId.Items.Clear();
                cmbEmpId.Items.Clear();
                cmbIn.Items.Clear();
                cmbInOut.Items.Clear();
                cmbDate.Items.Clear();
                Application.DoEvents();
                System.Data.DataTable dataSet = new System.Data.DataTable();
                SqlConnection Conn = new SqlConnection(Global.SQLConnStr);
                Conn.Open();
                List<string> result = new List<string>();
                SqlCommand cmd = new SqlCommand("SELECT * FROM " + cboTable.SelectedItem.ToString(), Conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataSet);

                for (int i = 0; i < dataSet.Columns.Count; i++)
                {
                    result.Add(dataSet.Columns[i].ColumnName);
                }


                string[] tables = result.ToArray();
                lstcol.DataSource = result;
                Conn.Close();
                Application.DoEvents();

                cmbEmpId.Items.Clear();
                cmbDate.Items.Clear();
                cmbInOut.Items.Clear();
                cmbIn.Items.Clear();
                cmbOut.Items.Clear();
                cmbDevId.Items.Clear();

                cmbEmpId.Items.Add("--Select--");
                cmbDate.Items.Add("--Select--");
                cmbInOut.Items.Add("--Select--");
                cmbIn.Items.Add("--Select--");
                cmbOut.Items.Add("--Select--");
                cmbDevId.Items.Add("--Select--");

                cmbEmpId.SelectedIndex = 0;
                cmbDate.SelectedIndex = 0;
                cmbInOut.SelectedIndex = 0;
                cmbIn.SelectedIndex = 0;
                cmbOut.SelectedIndex = 0;
                cmbDevId.SelectedIndex = 0;

                Application.DoEvents();

                foreach (string column in tables)
                {
                    cmbEmpId.Items.Add(column);
                    cmbDate.Items.Add(column);
                    cmbInOut.Items.Add(column);
                    cmbIn.Items.Add(column);
                    cmbOut.Items.Add(column);
                    cmbDevId.Items.Add(column);
                }
                Application.DoEvents();
            }
        }

        private void groupBox1_Leave(object sender, EventArgs e)
        {
            
            lbSelectError.Visible = false;
            if (rdbnDateTime.Checked == false && rdbnDateAndTime.Checked == false)
            {
                lbSelectError.Visible = true;
                lbSelectError.Text = "Select Date Time Formats";
                rdbnDateTime.Focus();
                return;
            }
            else
            {
                //int dt = 0; int d = 0; int iot = 0; int it = 0;int ot = 0;
                if (rdbnDateTime.Checked == true)
                {
                    dt = 1;
                    d = 0;
                    cmbDate.Enabled = false;
                    panel5.Enabled = false;
                    ////cmbDate.Visible = false;
                    //cmbDateTime.Enabled = true;
                    //panel6.Enabled = true;
                }
                else if (rdbnDateAndTime.Checked == true)
                {

                    dt = 0;
                    d = 1;
                    //cmbDateTime.Enabled = false;
                    //panel6.Enabled = false;
                    //cmbDateTime.Visible = false;
                    cmbDate.Enabled = true;
                    panel5.Enabled = true;
                }
            }
        }

        private void groupBox2_Leave(object sender, EventArgs e)
        {
            lbSelectError.Visible = false;
            if (rdbnInOut.Checked == false && rdbnInAndOut.Checked == false)
            {
                lbSelectError.Visible = true;
                lbSelectError.Text = "Select In_time and Out_time Formats";
                rdbnInOut.Focus();
                return;
            }
            else
            {
                if (rdbnInOut.Checked == true)
                {
                    iot = 1;
                    it = 0;
                    ot = 0;
                    panel4.Enabled = false;
                    panel7.Enabled = false;
                    cmbIn.Enabled = false;
                    cmbOut.Enabled = false;
                    panel3.Enabled = true;
                    cmbInOut.Enabled = true;
                }
                else if (rdbnInAndOut.Checked == true)
                {
                    iot = 0;
                    it = 1;
                    ot = 1;
                    panel4.Enabled = true;
                    panel7.Enabled = true;
                    cmbIn.Enabled = true;
                    cmbOut.Enabled = true;
                    panel3.Enabled = false;
                    cmbInOut.Enabled = false;
                }
            }
        }

        private void rdbnDateTime_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1_Leave(null,null);
        }

        private void rdbnDateAndTime_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1_Leave(null, null);
        }

        private void rdbnInOut_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2_Leave(null, null);
        }

        private void rdbnInAndOut_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2_Leave(null, null);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            lbSelectError.Visible = false;
            if (cboTable.SelectedIndex == -1)
            {
                lbSelectError.Visible = true;
                lbSelectError.Text = "Select Attendence Table";
                //rdbnDateTime.Focus();
                return;
            }
            else if (rdbnDateTime.Checked == false && rdbnDateAndTime.Checked == false)
            {
                lbSelectError.Visible = true;
                lbSelectError.Text = "Select Date Time Formats";
                //rdbnDateTime.Focus();
                return;
            }
            else if (rdbnInOut.Checked == false && rdbnInAndOut.Checked == false)
            {
                lbSelectError.Visible = true;
                lbSelectError.Text = "Select In_time and Out_time Formats";
                //rdbnInOut.Focus();
                return;
            }
            else
            {
                fnValidating();
            }
        }
     
        private void fnValidating()
        {
            lbSelectError.Visible = false;
            if (cmbEmpId.SelectedIndex != 0)
            {
                if (dt == 1)
                {
                    fnSelectInOut();
                }
                else if (d == 1)
                {
                    if (cmbDate.SelectedIndex != 0)
                    {
                        fnSelectInOut();
                    }
                    else
                    {
                        lbSelectError.Visible = true;
                        lbSelectError.Text = "Select Date refering Feild";
                        cmbDate.Focus();
                        return;
                    }
                }
            }
            else
            {
                lbSelectError.Visible = true;
                lbSelectError.Text = "Select Column that refers Employee Id";
                cmbEmpId.Focus();
                return;
            }
        }

        private void fnSelectInOut()
        {

            if (iot == 1)
            {
                if (cmbInOut.SelectedIndex != 0)
                {
                    fnSetValues();
                }
                else
                {
                    lbSelectError.Visible = true;
                    lbSelectError.Text = "Select In and Out time refering Feild";
                    cmbInOut.Focus();
                    return;
                }
            }
            else if (it == 1 && ot == 1)
            {
                if (cmbIn.SelectedIndex == 0 && cmbOut.SelectedIndex == 0)
                {
                    lbSelectError.Visible = true;
                    lbSelectError.Text = "Select In and Out time refering Feilds";
                    cmbIn.Focus();
                    return;
                }
                else if (cmbIn.SelectedIndex == 0 || cmbOut.SelectedIndex == 0)
                {
                    if (cmbIn.SelectedIndex == 0)
                    {
                        lbSelectError.Visible = true;
                        lbSelectError.Text = "Select In time refering Feild";
                        cmbIn.Focus();
                        return;
                    }
                    else if (cmbOut.SelectedIndex == 0)
                    {
                        lbSelectError.Visible = true;
                        lbSelectError.Text = "Select Out time refering Feild";
                        cmbOut.Focus();
                        return;
                    }
                }
                else
                {
                    fnSetValues();
                }
            }
        }

        private void fnSetValues()
        {
            fnClearGlobal();
            Global.EmpIdFieldName = cmbEmpId.SelectedItem.ToString();
            Global.tableName = cboTable.SelectedItem.ToString();
            if (dt == 1)
            {
                //Global.DateTimeFieldName = cmbDateTime.SelectedText;
                if (iot == 1)
                {
                    Global.DateTimeInOutFieldName = cmbInOut.SelectedItem.ToString();
                }
                else if (it == 1 && ot == 1)
                {
                    Global.DateTimeInFieldName = cmbIn.SelectedItem.ToString();
                    Global.DateTimeOutFieldName = cmbOut.SelectedItem.ToString();
                }
            }
            else if (d == 1)
            {
                Global.DateFieldName = cmbDate.SelectedItem.ToString();
                if (iot == 1)
                {
                    Global.InOutFieldName = cmbInOut.SelectedItem.ToString();
                }
                else if (it == 1 && ot == 1)
                {
                    Global.InFieldName = cmbIn.SelectedItem.ToString();
                    Global.OutFieldName = cmbOut.SelectedItem.ToString();
                }
            }

            Global.DeviceFieldName = cmbDevId.SelectedItem.ToString();
            Global.SetSQLValues = "yes";
            this.Close();
        }

        private void fnClearGlobal()
        {
            Global.EmpIdFieldName = "";
            Global.tableName = "";

            Global.DateTimeInOutFieldName = "";
            Global.DateTimeInFieldName = "";
            Global.DateTimeOutFieldName = "";

            Global.DateFieldName = "";
            Global.InOutFieldName = "";
            Global.InFieldName = "";
            Global.OutFieldName = "";

            Global.DeviceFieldName = "";
            if (Global.SetSQLValues != "y")
                Global.SetSQLValues = "";
        }
    }
}
