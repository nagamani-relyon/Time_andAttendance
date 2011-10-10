using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Time_and_Attendance
{
    public partial class TAA : Form
    {
        public TAA()
        {
            InitializeComponent();
        }

        private void TAA_Load(object sender, EventArgs e)
        {
            //if serverPath != "" then show 3rd form
            //else show frmServerPath
        }

        private void btnSrcFilePath_Click(object sender, EventArgs e)
        {
            frmServerPath frm = new frmServerPath();
            frm.ShowDialog();
            //Form dlg = new Form();
            //dlg.Text = "SPP Online Path";
            //dlg.FormBorderStyle = FormBorderStyle.FixedSingle;
            ////dlg.BackColor = Color.Azure;
            //dlg.ControlBox = true;
            //dlg.MaximizeBox = false;
            //dlg.MinimizeBox = false;
            //dlg.ShowInTaskbar = false;
            ////dlg.Icon = new Icon("1.ICO");
            //dlg.Size = new Size(280, 130);
            //Label lbtxt = new Label();
            //lbtxt.Text = "Enter SPP Online Application Path : ";
            //dlg.Controls.Add(lbtxt);
            //TextBox txtServerPath = new TextBox();
            //dlg.Controls.Add(txtServerPath);
            //lbtxt.Location = new Point(10, 10);
            //lbtxt.Width=200;
            //lbtxt.Height = 13;
            //txtServerPath.Location = new Point(10, 28);
            //txtServerPath.Width = 250;
            ////button save
            //Button btnSave = new Button();
            //btnSave.Text = "Save";
            //btnSave.Location = new Point(10, 60);
            //btnSave.DialogResult = DialogResult.OK;
            //dlg.Controls.Add(btnSave);

            ////button Cancel
            //Button btnCancel = new Button();
            //btnCancel.Text = "Cancel";
            //btnCancel.Location = new Point(185, 60);
            //btnCancel.DialogResult = DialogResult.Cancel;
            //dlg.Controls.Add(btnCancel);

            //dlg.StartPosition = FormStartPosition.CenterScreen;
            //dlg.ShowDialog();
            
            //if (dlg.DialogResult == DialogResult.OK)
            //{
            //    if (txtServerPath.Text == "")
            //    {
            //        //MessageBox.Show("Enter text");
            //        txtServerPath.Focus();
            //        txtServerPath.SelectAll();
            //        btnSrcFilePath_Click(null, null);
            //        txtServerPath.BackColor = Color.Red;
            //    }
            //}
        }

        private void btnCreCompDetails_Click(object sender, EventArgs e)
        {
            frmCompanyMaster frm = new frmCompanyMaster();
            frm.ShowDialog();
        }

        private void btnSettingsImport_Click(object sender, EventArgs e)
        {
            frmManageTnA frm = new frmManageTnA();
            frm.ShowDialog();
        }
        
    }
}