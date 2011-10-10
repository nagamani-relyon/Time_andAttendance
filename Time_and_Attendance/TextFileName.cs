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
    public partial class TextFileName : Form
    {
        public TextFileName()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Global.filePrefix = "";
            //Global.fileSuffix = "";
            //Global.fileDateFormat = "";
            //Global.fileTypeRead = "";
            //Global.fileReadDate = "";
            //this.Close();

            txtprefix.Text = "";
            txtSuffix.Text = "";
            cmbDateFormat.SelectedIndex = 0;
            rdbnCurrent.Checked = true;
            dtpFrom.ResetText();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lbError.Text = "";
            if (cmbDateFormat.SelectedIndex != 0 && cmbDateFormat.SelectedItem.ToString()!="None(Single File)")
            {
                string dateString = DateTime.Now.ToShortDateString();
                DateTime curDate = DateTime.Parse(dateString);
                if (rdbnFrom.Checked == true)
                {
                    string dtpString = dtpFrom.Text;
                    DateTime dtpdate = DateTime.Parse(dtpString);

                    //if (curDate.ToString() == dtpdate1.ToString())
                    //{
                    //    rdbnCurrent.Checked = true;
                    //}
                    //else
                    //{
                    int result = DateTime.Compare(dtpdate, curDate);
                    //string relationship;
                    if (result < 0)
                    {
                        //relationship = "before Current Date";
                        Global.fileReadDate = dtpString;
                    }
                    else if (result == 0)
                    {
                        //relationship = "Current Date";
                        //rdbnCurrent.Checked = true;
                        Global.fileReadDate = dateString;
                    }
                    else
                    {
                        //relationship = "after Current date";
                        lbError.Text = "Selected Date is grater than today's Date. Selete Date before today's date.";
                        dtpFrom.Focus();
                        return;
                    }
                    //if (result <= 0)
                    //{
                        //Global.filePrefix = txtprefix.Text.Trim();
                        //Global.fileSuffix = txtSuffix.Text.Trim();
                        //Global.fileDateFormat = cmbDateFormat.SelectedItem.ToString();
                        //string filetype = "";
                        //if (rdbnAll.Checked == true)
                        //    filetype = "all";
                        //else if (rdbnFrom.Checked == true)
                        //    filetype = "from";
                        //else if (rdbnCurrent.Checked == true)
                        //    filetype = "cur";
                        //Global.fileTypeRead = filetype;
                        //Global.fileReadDate = dateString;
                        //return;
                    //}
                    //}

                }

                Global.filePrefix = txtprefix.Text.Trim();
                Global.fileSuffix = txtSuffix.Text.Trim();
                Global.fileDateFormat = cmbDateFormat.SelectedItem.ToString();
                string filetype = "";
                if (rdbnAll.Checked == true)
                {
                    filetype = "all";
                    Global.fileReadDate = dateString;
                }
                else if (rdbnFrom.Checked == true)
                {
                    filetype = "from";
                }
                else if (rdbnCurrent.Checked == true)
                {
                    filetype = "cur";
                    Global.fileReadDate = dateString;
                }
                Global.fileTypeRead = filetype;
                lbError.Text = "Values Saved.";
                Global.filename = "";
                return;
            }
            else if (cmbDateFormat.SelectedItem.ToString() == "None(Single File)")
            {
                lbError.Text = "Values Saved.";
                Global.filename = Path.GetFileName(txtTextFile.Text);
            }
            else if (cmbDateFormat.SelectedIndex == 0)
            {
                lbError.Text = "Select Date Format";
                cmbDateFormat.Focus();
                return;
            }
        }

        private void rdbnFrom_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbnFrom.Checked == true)
                dtpFrom.Enabled = true;
            else
                dtpFrom.Enabled = false;
        }

        private void rdbnAll_CheckedChanged(object sender, EventArgs e)
        {
            dtpFrom.Enabled = false;
        }

        private void rdbnCurrent_CheckedChanged(object sender, EventArgs e)
        {
            dtpFrom.Enabled = false;
        }

        private void TextFileName_Load(object sender, EventArgs e)
        {
            if (Global.fileTypeRead != "" && Global.fileTypeRead != "file" && Global.fileDateFormat != "" && Global.fileDateFormat != "none")
            {
                txtprefix.Text = Global.filePrefix;
                txtSuffix.Text = Global.fileSuffix;

                if (Global.fileTypeRead == "all")
                    rdbnAll.Checked = true;
                else if (Global.fileTypeRead == "cur")
                    rdbnCurrent.Checked = true;
                else if (Global.fileTypeRead == "from")
                {
                    rdbnFrom.Checked = true;
                    dtpFrom.Text = Global.fileReadDate;
                }
                
                if (Global.fileDateFormat.ToLower() == "ddmmyy")
                    cmbDateFormat.SelectedIndex = 1;
                else if (Global.fileDateFormat.ToLower() == "yymmdd")
                    cmbDateFormat.SelectedIndex = 2;
                else if (Global.fileDateFormat.ToLower() == "ddmmyyyy")
                    cmbDateFormat.SelectedIndex = 3;//
                else if (Global.fileDateFormat.ToLower() == "yyyymmdd")
                    cmbDateFormat.SelectedIndex = 4;//
                else if (Global.fileDateFormat.ToLower() == "dd/mm/yyyy")
                    cmbDateFormat.SelectedIndex = 5;//
                else if (Global.fileDateFormat.ToLower() == "yyyy/mm/dd")
                    cmbDateFormat.SelectedIndex = 6;//
                else if (Global.fileDateFormat.ToLower() == "dd/mm/yy")
                    cmbDateFormat.SelectedIndex = 7;//
                else if (Global.fileDateFormat.ToLower() == "yy/mm/dd")
                    cmbDateFormat.SelectedIndex = 8;//
                else if (Global.fileDateFormat.ToLower() == "dd-mm-yyyy")
                    cmbDateFormat.SelectedIndex = 9;//
                else if (Global.fileDateFormat.ToLower() == "yyyy-mm-dd")
                    cmbDateFormat.SelectedIndex = 10;//
                else if (Global.fileDateFormat.ToLower() == "dd-mm-yy")
                    cmbDateFormat.SelectedIndex = 11;//
                else if (Global.fileDateFormat.ToLower() == "yy-mm-dd")
                    cmbDateFormat.SelectedIndex = 12;//
                else if (Global.fileDateFormat.ToLower() == "d/m/yyyy")
                    cmbDateFormat.SelectedIndex = 13;//
                else if (Global.fileDateFormat.ToLower() == "yyyy/m/d")
                    cmbDateFormat.SelectedIndex = 14;//
                else if (Global.fileDateFormat.ToLower() == "d/m/yy")
                    cmbDateFormat.SelectedIndex = 15;//
                else if (Global.fileDateFormat.ToLower() == "yy/m/d")
                    cmbDateFormat.SelectedIndex = 16;//
                else if (Global.fileDateFormat.ToLower() == "d-m-yyyy")
                    cmbDateFormat.SelectedIndex = 17;//
                else if (Global.fileDateFormat.ToLower() == "yyyy-m-d")
                    cmbDateFormat.SelectedIndex = 18;//
                else if (Global.fileDateFormat.ToLower() == "d-m-yy")
                    cmbDateFormat.SelectedIndex = 19;//
                else if (Global.fileDateFormat.ToLower() == "yy-m-d")
                    cmbDateFormat.SelectedIndex = 20;//
                else if (Global.fileDateFormat.ToLower() == "d/mm/yyyy")
                    cmbDateFormat.SelectedIndex = 21;//
                else if (Global.fileDateFormat.ToLower() == "yyyy/mm/d")
                    cmbDateFormat.SelectedIndex = 22;//
                else if (Global.fileDateFormat.ToLower() == "d/mm/yy")
                    cmbDateFormat.SelectedIndex = 23;//
                else if (Global.fileDateFormat.ToLower() == "yy/mm/d")
                    cmbDateFormat.SelectedIndex = 24;//
                else if (Global.fileDateFormat.ToLower() == "d-mm-yyyy")
                    cmbDateFormat.SelectedIndex = 25;//
                else if (Global.fileDateFormat.ToLower() == "yyyy-mm-d")
                    cmbDateFormat.SelectedIndex = 26;//
                else if (Global.fileDateFormat.ToLower() == "d-mm-yy")
                    cmbDateFormat.SelectedIndex = 27;//
                else if (Global.fileDateFormat.ToLower() == "yy-mm-d")
                    cmbDateFormat.SelectedIndex = 28;
            }
            else if (Global.filename != "")
            {
                panel1.Enabled = false;
                grpTextfile.Enabled = true;
                txtSuffix.Enabled = false;
                txtprefix.Enabled = false;
                btnFilePath.Focus();
                cmbDateFormat.SelectedIndex = 29;
                txtTextFile.Text = Global.filename;

            }
            else 
                cmbDateFormat.SelectedIndex = 0;
        }

        private void cmbDateFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDateFormat.SelectedItem.ToString() == "None(Single File)")
            {
                panel1.Enabled = false;
                grpTextfile.Enabled = true;
                txtSuffix.Enabled = false;
                txtprefix.Enabled = false;
                if (Global.filename != "")
                    txtTextFile.Text = Global.filename;
                btnFilePath.Focus();
                
            }
            else
            {
                panel1.Enabled = true;
                grpTextfile.Enabled = false;
                txtSuffix.Enabled = true;
                txtprefix.Enabled = true;
                txtTextFile.Text = "";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFilePath_Click(object sender, EventArgs e)
        {
            fnfilebrowseText();
            if (txtTextFile.Text != "")
            {
                try
                {
                    StreamReader file = new StreamReader(txtTextFile.Text);
                    txtTextFile.Text = Path.GetFileName(txtTextFile.Text);
                }
                catch (UnauthorizedAccessException uae)
                {
                    lbError.Visible = true;
                    lbError.Text = "Give 'Read' permissions to selected file";
                }
            }
        }

        private void fnfilebrowseText()
        {
            bool blnrppext;
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            OpenFileDialog fileBrowser = new OpenFileDialog();
            fileBrowser.Title = "Select Text File";
            fileBrowser.InitialDirectory = Global.folPath;
            //fileBrowser.Filter = "Text File(*.TXT)|*.txt";
            fileBrowser.Filter = "Text Files(*.txt;*.log;*.cap;*.ttt;*.dat)|*.txt;*.log;*.cap;*.ttt;*.dat";
            //fileBrowser.DefaultExt = "*.txt";
            fileBrowser.ShowDialog(this);
            if ((fileBrowser.FileName.Length == 0) && txtTextFile.Text == "")
            {
                MessageBox.Show("Select (*.txt or *.log or *.cap or *.ttt ot *.dat) File", "Error: File Selection");
            }
            if ((fileBrowser.FileName.Length > 0) && (fileBrowser.CheckFileExists))
            {
                txtTextFile.Text = fileBrowser.FileName;
                string rppfilepath = txtTextFile.Text;
                string pathext = Path.GetExtension(rppfilepath).ToString().ToUpper();
                if (pathext != ".TXT" && pathext != ".LOG" && pathext != ".CAP" && pathext != ".TTT" && pathext != ".DAT")
                {
                    blnrppext = false;
                    if (blnrppext == false)
                    {
                        MessageBox.Show("Select Correct Text File", "Error: File Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                    }

                }
                else
                {
                }
            }
            Application.DoEvents();
            this.Cursor = Cursors.Default;
        }

    }
}
