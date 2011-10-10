using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;

namespace Time_and_Attendance
{
    public partial class frmServerPath : Form
    {
        public frmServerPath()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtServerPath.Text == "")
            {
                MessageBox.Show("Enter Online SPP Path");
                txtServerPath.BorderStyle = BorderStyle.Fixed3D;
                txtServerPath.SelectAll();
                txtServerPath.Focus();
            }
            else
            {
                using (WebClient client = new WebClient())
                {
                    string s2 = null;
                    try
                    {
                        s2 = client.DownloadString(txtServerPath.Text);
                    }
                    catch (WebException web)
                    {
                        //if((web.Status.ToString()).Equals("ProtocolError"))
                        MessageBox.Show(web.Message);
                    }
                }
                //if (fnPingURL(txtServerPath.Text))
                //{
                //}
                //else
                //{
                //    MessageBox.Show("Enter valid Online SPP Path");
                //    txtServerPath.SelectAll();
                //    txtServerPath.Focus();
                //}

                //check for validity
                //if not valid Prompt to enter correct one 
                //else if valid
                     //------> Navigate to Path+"T&A_SOURCE/tm_chk_ser_path.php"
                     //------>if returned value <MSG>path</MSG>
                        //------>if old server address and newly entered are differnt then save new one and close form
            }
        }

        private bool fnPingURL(String url)
        {
            bool success = false;
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply replyMsg = ping.Send(url, 300);
                    if (replyMsg.Status == IPStatus.Success)
                    {
                        success = true;
                    }
                }
            }
            catch { }
            return success;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}