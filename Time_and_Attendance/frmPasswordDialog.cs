using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Time_and_Attendance
{
    public partial class frmPasswordDialog : Form
    {
        public frmPasswordDialog()
        {
            InitializeComponent();
        }
        string connString;

        private void btnPassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == string.Empty)
            {
                lbPasswordError.Text = "Enter Password";
                txtPassword.Focus();
            }
            else
            {
                if (Path.GetExtension(Global.mdbFilePath).ToString().ToUpper() != ".ACCDB")
                    connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Global.mdbFilePath + ";Jet OLEDB:Database Password=" + txtPassword.Text;
                else
                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Global.mdbFilePath + ";Jet OLEDB:Database Password=" + txtPassword.Text;

                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connString))
                {
                    try
                    {
                        connection.Open();
                        Global.filepassword = txtPassword.Text;
                        //frmMdbTableColumnsSelect frm = new frmMdbTableColumnsSelect();
                        this.Close();
                        //frm.ShowDialog();
                    }
                    catch (Exception ee)
                    {
                        if (ee.Message == "Not a valid password.")
                        {
                            lbPasswordError.Text = "Enter correct Password";
                            txtPassword.SelectAll();
                            txtPassword.Focus();
                        }
                    }

                }
            }
        }

        private void frmPasswordDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
