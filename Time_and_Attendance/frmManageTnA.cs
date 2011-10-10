using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.Net;
using System.IO;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Collections;
using System.Data.SqlClient;

namespace Time_and_Attendance
{
    public partial class frmManageTnA : Form
    {
        public frmManageTnA()
        {
            InitializeComponent();
        }

        #region Variables

        #region files variables

        int settingsCompanyID;
        int settingsTimeInt;
        int settingsFieldLen;

        string settingsDsnName;
        string settingsUserName;
        string settingsPassword;
        string settingsDBName;
        string settingsStartDate;
        string settingsPrefixSuffix;
        string settingsReferCardEmp;
        string settingsUseRefEmp;

        string selecteComp;
        string settingsURL;

        string fileSettingsFilePath;
        string fileSettingsDateFormat;
        string fileSettingsTimeFormat;

        int fileSettingsFisFie;
        int fileSettingsFisFiePos;
        int fileSettingsFisFieLen;
        int fileSettingsSecFie;
        int fileSettingsSecFiePos;
        int fileSettingsSecFieLen;
        int fileSettingsThrFie;
        int fileSettingsThrFiePos;
        int fileSettingsThrFieLen;
        int fileSettingsFrthFie;
        int fileSettingsFrthFiePos;
        int fileSettingsFrthFieLen;
        int fileSettingsStartRow;
        int fileSettingsNoCharsPerLine;
        int fileSettingsAutoIntMin;

        #endregion

        #region SQL Variables

        string settingsSqlUrl;
        string settingsSqlPreSuf;
        string settingsSqlEmpRefCard;
        string settingsSqlServeName;
        string settingsSqlDatabase;
        string settingsSqlAuthType;
        string settingsSqlUsername;
        string settingsSqlPassword;
        
        int settingsSqlTimeInt;
        int settingsSqlFieLen;

        int sqlId = 0;
        int countRows = 1;
        int totalRec = 0;
        int recs = 0;

        #endregion

        string dataSourcePath;
        string PauseStatus = "no";
        string Pageload = "";
        string fromUpdates = "";
        

        string[,] timersDetails = new string[10, 22];
        string[,] timersTextDetails = new string[10, 24];
        string[,] timersSQLDetails = new string[10, 23];

        int mdbId;
        DateTime now;
        char fileSettingsDelimiter;

        int[] mdbComp = new int[10];
        int[] txtComp = new int[10];
        int[] SqlComp = new int[10];

        #region timer variables

        DateTime now1;
        DateTime now2;
        DateTime now3;
        DateTime now4;
        DateTime now5;
        DateTime now6;
        DateTime now7;
        DateTime now8;
        DateTime now9;
        DateTime now10;

        int TimeTimer1Flag;
        int TimeTimer2Flag;
        int TimeTimer3Flag;
        int TimeTimer4Flag;
        int TimeTimer5Flag;
        int TimeTimer6Flag;
        int TimeTimer7Flag;
        int TimeTimer8Flag;
        int TimeTimer9Flag;
        int TimeTimer10Flag;

        string comp1Type;
        string comp2Type;
        string comp3Type;
        string comp4Type;
        string comp5Type;
        string comp6Type;
        string comp7Type;
        string comp8Type;
        string comp9Type;
        string comp10Type;

        int comp1Index;
        int comp2Index;
        int comp3Index;
        int comp4Index;
        int comp5Index;
        int comp6Index;
        int comp7Index;
        int comp8Index;
        int comp9Index;
        int comp10Index;

        #endregion

        string strHost = "";
        string strDB = "";
        string strUsr = "";
        string strPWD = "";

        bool closeVal = false;
        bool fromUpload = false;

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //not used
        private void button1_Click(object sender, EventArgs e)
        {
            frmCompanyMaster frm = new frmCompanyMaster();
            frm.ShowDialog();
            fnUpdateTreeView();
        }

        private void frmManageTnA_Load(object sender, EventArgs e)
        {
            //btnFromSqlToOther_Click btnTextToOther_Click btnMdbToOther_Click
            btnFromSqlToOther.Enabled = false;
            btnFromSqlToOther.Visible = false;
            btnTextToOther.Visible = false;
            btnTextToOther.Enabled = false;
            btnMdbToOther.Enabled = false;
            btnMdbToOther.Visible = false;
            cmbFileType.SelectedIndex = 0;
            if (!fnChkSyncExe())
            {
                iToolStripMenuItem_Click(null, null);
                cmbDB.SelectedIndex = 0;//Changed
                Global.SetSQLValues = "";//Changed
                Global.SetMdbValues = "";//Changed
                lbReadComp.Text = "";
                pgbrUpload.Value = 0;
                Pageload = "yes";
                dataSourcePath = "Data Source = " + Application.StartupPath + @"\TADatabase.sdf";
                //MessageBox.Show(dataSourcePath);
                //FileInfo fi = new FileInfo(Application.StartupPath + @"\TADatabase.sdf");
                //if (fi.Exists)
                //    MessageBox.Show("Exists");

                treeView1.ImageIndex = 0;
                fnUpdateTreeView();

                //fun to start timers based file type
                fnStartMdbCompTimers();
                fnStartTextCompTimers();
                fnStartSQLCompTimers();
                fnStartMdbTimers();
                Pageload = "no";
            }
            else
            {
                MessageBox.Show("Time and Attendence Utility Running already", "Time and Attendence", MessageBoxButtons.OK);
                closeVal = true;
                Application.Exit();
            }
        }

        private void fnStartMdbCompTimers()
        {
            dataSourcePath = "Data Source = " + Application.StartupPath + @"\TADatabase.sdf";
            int cid = 0;
            SqlCeConnection sqlConnection1 = new SqlCeConnection();
            sqlConnection1.ConnectionString = dataSourcePath;
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT * FROM CompRegister WHERE CO_FILE_TYPE='mdb'";
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            SqlCeDataReader dataRead;
            dataRead = cmd.ExecuteReader();
            int i = 0;
            while (dataRead.Read())
            {
                cid = Convert.ToInt32(dataRead[0]);
                if (fnGetActiveStatus(cid))
                {
                    mdbComp[i] = cid;
                    i++;
                }
            }
            dataRead.Dispose();
            dataRead.Close();
            sqlConnection1.Dispose();
            sqlConnection1.Close();
        }

        private void fnStartTextCompTimers()
        {
            dataSourcePath = "Data Source = " + Application.StartupPath + @"\TADatabase.sdf";
            int cid = 0;
            SqlCeConnection sqlConnection1 = new SqlCeConnection();
            sqlConnection1.ConnectionString = dataSourcePath;
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT * FROM CompRegister WHERE CO_FILE_TYPE='text'";
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            SqlCeDataReader dataRead;
            dataRead = cmd.ExecuteReader();
            int i = 0;
            while (dataRead.Read())
            {
                cid = Convert.ToInt32(dataRead[0]);
                if (fnGetActiveStatus(cid))
                {
                    txtComp[i] = cid;
                    i++;
                }
            }
            dataRead.Dispose();
            dataRead.Close();
            sqlConnection1.Dispose();
            sqlConnection1.Close();
        }

        private void fnStartSQLCompTimers()
        {
            dataSourcePath = "Data Source = " + Application.StartupPath + @"\TADatabase.sdf";
            int cid = 0;
            SqlCeConnection sqlConnection1 = new SqlCeConnection();
            sqlConnection1.ConnectionString = dataSourcePath;
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT * FROM CompRegister WHERE CO_FILE_TYPE='sql'";
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            SqlCeDataReader dataRead;
            dataRead = cmd.ExecuteReader();
            int i = 0;
            while (dataRead.Read())
            {
                cid = Convert.ToInt32(dataRead[0]);
                if (fnGetActiveStatus(cid))
                {
                    SqlComp[i] = cid;
                    i++;
                }
            }
            dataRead.Dispose();
            dataRead.Close();
            sqlConnection1.Dispose();
            sqlConnection1.Close();
        }

        private bool fnGetActiveStatus(int id)
        {
            bool active = true;
            string act = "";
            SqlCeConnection sqlConnection1 = new SqlCeConnection();
            sqlConnection1.ConnectionString = dataSourcePath;

            SqlCeCommand cmd = new SqlCeCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT * FROM CompRegister WHERE CO_COMP_ID=" + id.ToString();
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            SqlCeDataReader dataRead;
            dataRead = cmd.ExecuteReader();
            dataRead.Read();
            act = dataRead[2].ToString();
            sqlConnection1.Dispose();
            sqlConnection1.Close();

            if (act == "Active")
                active = true;
            else if (act == "Not Active")
                active = false;

            return active;
        }

        private void fnStartMdbTimers()
        {
            int i = 0;
            foreach (int c in mdbComp)
            {
                if (c != 0)
                {
                    //timer1.Enabled = true;
                    int j = 0;
                    int id = 0;
                    SqlCeConnection sqlConnection2 = new SqlCeConnection();
                    sqlConnection2.ConnectionString = dataSourcePath;
                    SqlCeCommand cmd1 = new SqlCeCommand();
                    cmd1.CommandType = System.Data.CommandType.Text;
                    cmd1.CommandText = "SELECT * FROM MDB_FILE_SETTINGS WHERE MDB_COMP_ID=" + c.ToString();
                    cmd1.Connection = sqlConnection2;
                    sqlConnection2.Open();
                    SqlCeDataReader dataRead1;
                    try
                    {
                        dataRead1 = cmd1.ExecuteReader();
                        dataRead1.Read();
                        timersDetails[i, j++] = dataRead1[1].ToString();//Comp_id
                        id = Convert.ToInt32(dataRead1[1]);
                        timersDetails[i, j++] = dataRead1[2].ToString();//url
                        timersDetails[i, j++] = dataRead1[3].ToString();//file path
                        timersDetails[i, j++] = dataRead1[4].ToString();//Time interval
                        timersDetails[i, j++] = dataRead1[5].ToString();//ref as Emp
                        timersDetails[i, j++] = dataRead1[6].ToString();//card for emp
                        timersDetails[i, j++] = dataRead1[7].ToString();//prefix or suffix
                        timersDetails[i, j++] = dataRead1[8].ToString();//emp field length
                        timersDetails[i, j++] = dataRead1[9].ToString();//start Date
                    }
                    catch { }
                    sqlConnection2.Dispose();
                    sqlConnection2.Close();

                    SqlCeConnection sqlConnection1 = new SqlCeConnection();
                    sqlConnection1.ConnectionString = dataSourcePath;
                    SqlCeCommand cmd = new SqlCeCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT * FROM MDB_FILE_FIELD_SETTINGS WHERE FIE_MDB_ID=" + c.ToString();
                    cmd.Connection = sqlConnection1;

                    sqlConnection1.Open();
                    SqlCeDataReader dataRead;
                    try
                    {
                        dataRead = cmd.ExecuteReader();
                        dataRead.Read();
                        timersDetails[i, j++] = dataRead[1].ToString();//password
                        timersDetails[i, j++] = dataRead[2].ToString();//table name
                        timersDetails[i, j++] = dataRead[3].ToString();//emp field
                        timersDetails[i, j++] = dataRead[4].ToString();//device
                        timersDetails[i, j++] = dataRead[5].ToString();//date time in out
                        timersDetails[i, j++] = dataRead[6].ToString();//date time in
                        timersDetails[i, j++] = dataRead[7].ToString();//date time out
                        timersDetails[i, j++] = dataRead[8].ToString();//date
                        timersDetails[i, j++] = dataRead[9].ToString();//date in out
                        timersDetails[i, j++] = dataRead[10].ToString();//date in
                        timersDetails[i, j++] = dataRead[11].ToString();//date out
                        timersDetails[i, j++] = dataRead[12].ToString();//Last Read Index
                    }
                    catch { }
                    sqlConnection1.Dispose();
                    sqlConnection1.Close();
                    timersDetails[i, j] = "mdb";
                    //id=Convert.ToInt32(timersTextDetails[0, 0]);
                    if (fromUpload == true)
                    {
                        fnStartReadData(id, "mdb", 0);
                    }
                    i++;
                }
            }
            i = 0;
            foreach (int co in txtComp)
            {
                if (co != 0)
                {
                    int k = 0;
                    int id = 0;
                    SqlCeConnection sqlConnection3 = new SqlCeConnection();
                    sqlConnection3.ConnectionString = dataSourcePath;
                    SqlCeCommand cmd3 = new SqlCeCommand();
                    cmd3.CommandType = System.Data.CommandType.Text;
                    cmd3.CommandText = "SELECT * FROM TEXT_FILE_SETTINGS WHERE TEXT_COM_ID=" + co.ToString();
                    cmd3.Connection = sqlConnection3;
                    sqlConnection3.Open();
                    SqlCeDataReader dataRead3;
                    try
                    {
                        dataRead3 = cmd3.ExecuteReader();
                        dataRead3.Read();
                        timersTextDetails[i, k++] = dataRead3[0].ToString();//Comp_id
                        id = Convert.ToInt32(dataRead3[0]);
                        timersTextDetails[i, k++] = dataRead3[1].ToString();//url
                        timersTextDetails[i, k++] = dataRead3[2].ToString();//file path
                        timersTextDetails[i, k++] = dataRead3[3].ToString();//emp len
                        timersTextDetails[i, k++] = dataRead3[4].ToString();//emp pos
                        timersTextDetails[i, k++] = dataRead3[5].ToString();//date len
                        timersTextDetails[i, k++] = dataRead3[6].ToString();//date pos
                        timersTextDetails[i, k++] = dataRead3[7].ToString();//time len
                        timersTextDetails[i, k++] = dataRead3[8].ToString();//time pos
                        timersTextDetails[i, k++] = dataRead3[9].ToString();//dev len
                        timersTextDetails[i, k++] = dataRead3[10].ToString();//dev pos
                        timersTextDetails[i, k++] = dataRead3[11].ToString();//start row
                        timersTextDetails[i, k++] = dataRead3[12].ToString();//date format
                        timersTextDetails[i, k++] = dataRead3[13].ToString();//time format
                        timersTextDetails[i, k++] = dataRead3[14].ToString();//auto time
                        timersTextDetails[i, k++] = dataRead3[15].ToString();//emp_as
                        timersTextDetails[i, k++] = "text";//Type
                    }
                    catch { }
                    sqlConnection3.Dispose();
                    sqlConnection3.Close();

                    
                    SqlCeConnection sqlConnection1 = new SqlCeConnection();
                    sqlConnection1.ConnectionString = dataSourcePath;
                    SqlCeCommand cmd = new SqlCeCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT * FROM TEXT_FILE_NAME_FORMAT WHERE TEXT_NAME_FORMAT_ID=" + co.ToString();
                    cmd.Connection = sqlConnection1;

                    sqlConnection1.Open();
                    SqlCeDataReader dataRead;
                    try
                    {
                        dataRead = cmd.ExecuteReader();
                        dataRead.Read();
                        timersTextDetails[i, k++] = dataRead[1].ToString();//17 file name prefix 
                        timersTextDetails[i, k++] = dataRead[2].ToString();//18 filename date format
                        timersTextDetails[i, k++] = dataRead[3].ToString();//19 file name suffix
                        timersTextDetails[i, k++] = dataRead[4].ToString();//20 using file type
                        timersTextDetails[i, k++] = dataRead[5].ToString();//21 from date
                        timersTextDetails[i, k++] = dataRead[6].ToString();//22 FILE EXTENSION
                        timersTextDetails[i, k++] = dataRead[7].ToString();//23 FILE Name
                    }
                    catch { }
                    sqlConnection1.Dispose();
                    sqlConnection1.Close();

                    if (fromUpload == true)
                    {
                        fnStartReadData(id, "text", 0);
                    }
                }
                i++;
            }
            
            i = 0;
            foreach (int com in SqlComp)
            {
                if (com != 0)
                {
                    //timer1.Enabled = true;
                    int j = 0;
                    int id = 0;
                    SqlCeConnection sqlConnection2 = new SqlCeConnection();
                    sqlConnection2.ConnectionString = dataSourcePath;
                    SqlCeCommand cmd1 = new SqlCeCommand();
                    cmd1.CommandType = System.Data.CommandType.Text;
                    cmd1.CommandText = "SELECT * FROM SQL_DATA_SETTINGS WHERE SQL_COMP_ID=" + com.ToString();
                    cmd1.Connection = sqlConnection2;
                    sqlConnection2.Open();
                    SqlCeDataReader dataRead1;
                    try
                    {
                        dataRead1 = cmd1.ExecuteReader();
                        dataRead1.Read();
                        timersSQLDetails[i, j++] = dataRead1[0].ToString();//Comp_id
                        id = Convert.ToInt32(dataRead1[0]);
                        timersSQLDetails[i, j++] = dataRead1[1].ToString();//url
                        timersSQLDetails[i, j++] = dataRead1[2].ToString();//Time interval
                        timersSQLDetails[i, j++] = dataRead1[3].ToString();//emp field length
                        timersSQLDetails[i, j++] = dataRead1[4].ToString();//prefix or suffix
                        timersSQLDetails[i, j++] = dataRead1[5].ToString();//ref or Emp or Card
                    }
                    catch { }
                    sqlConnection2.Dispose();
                    sqlConnection2.Close();

                    SqlCeConnection sqlConnection1 = new SqlCeConnection();
                    sqlConnection1.ConnectionString = dataSourcePath;
                    SqlCeCommand cmd = new SqlCeCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT * FROM SQL_DATA_FIELD_SETTINGS WHERE SQL_TAB_COMP_ID=" + com.ToString();
                    cmd.Connection = sqlConnection1;

                    sqlConnection1.Open();
                    SqlCeDataReader dataRead;
                    try
                    {
                        dataRead = cmd.ExecuteReader();
                        dataRead.Read();
                        timersSQLDetails[i, j++] = dataRead[1].ToString();//servername
                        timersSQLDetails[i, j++] = dataRead[2].ToString();//Database name
                        timersSQLDetails[i, j++] = dataRead[3].ToString();//Authentication Type
                        timersSQLDetails[i, j++] = dataRead[4].ToString();//Username
                        timersSQLDetails[i, j++] = dataRead[5].ToString();//password
                        timersSQLDetails[i, j++] = dataRead[6].ToString();//table name
                        timersSQLDetails[i, j++] = dataRead[7].ToString();//emp field
                        timersSQLDetails[i, j++] = dataRead[8].ToString();//device
                        timersSQLDetails[i, j++] = dataRead[9].ToString();//date time in out
                        timersSQLDetails[i, j++] = dataRead[10].ToString();//date time in
                        timersSQLDetails[i, j++] = dataRead[11].ToString();//date time out
                        timersSQLDetails[i, j++] = dataRead[12].ToString();//date
                        timersSQLDetails[i, j++] = dataRead[13].ToString();//date in out
                        timersSQLDetails[i, j++] = dataRead[14].ToString();//date in
                        timersSQLDetails[i, j++] = dataRead[15].ToString();//date out
                        timersSQLDetails[i, j++] = dataRead[16].ToString();//Last Read Index
                    }
                    catch { }
                    sqlConnection1.Dispose();
                    sqlConnection1.Close();
                    timersSQLDetails[i, j] = "sql";
                    //id=Convert.ToInt32(timersTextDetails[0, 0]);
                    if (fromUpload == true)
                    {
                        fnStartReadData(id, "sql", 0);
                    }
                    i++;
                }
            }
            if (fromUpload != true)
            {
                fnTmer();
                fnTextTmer();
                fnSQLTmer();
            }
            //else if (fromUpload == true)
            //{
            //    fnStartReadData(Convert.ToInt32(timersTextDetails[0,0]), comp1Type, 0);
            //}
        }

        private void fnUpdateTreeView()
        {
            treeView1.Nodes.Clear();
            int rows = 0;
            try
            {
                using (SqlCeConnection c = new SqlCeConnection(dataSourcePath))
                {
                    c.Open();
                    using (SqlCeDataAdapter a = new SqlCeDataAdapter("SELECT * FROM CompRegister", c))
                    {
                        DataTable t = new DataTable();
                        a.Fill(t);
                        rows = t.Rows.Count;
                    }
                    c.Close();
                }
            }
            catch
            {
                return;
            }

            //TreeNode[] array = new TreeNode[rows];
            int i = 0;
            SqlCeConnection sqlConnection1 = new SqlCeConnection();
            sqlConnection1.ConnectionString = dataSourcePath;

            SqlCeCommand cmd = new SqlCeCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT * FROM CompRegister";
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            SqlCeDataReader dataRead;
            dataRead = cmd.ExecuteReader();
            //TreeNode node = null;
            List<TreeItem> items = new List<TreeItem>();
            while (dataRead.Read())
            {
                //node = new TreeNode(dataRead[1].ToString());
                //array[i] = node;

                items.Add(new TreeItem(dataRead[1].ToString(), 0));
                i++;
            }
            sqlConnection1.Dispose();
            sqlConnection1.Close();

            //TreeNode treeNode = new TreeNode("Company's", array);
            //treeView1.Nodes.Add(treeNode);
            PopulateTree(treeView1, items);
        }

        public static void PopulateTree(TreeView tree, ICollection<TreeItem> items)
        {
            tree.Nodes.Clear();
            List<TreeNode> roots = new List<TreeNode>();
            roots.Add(tree.Nodes.Add("Company's"));
            foreach (TreeItem item in items)
            {
                if (item.Level == roots.Count) roots.Add(roots[roots.Count - 1].LastNode);
                roots[item.Level].Nodes.Add(item.Name);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            lbError.Visible = false;
            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Text == "Company's")
            {
                fromUpdates = "yes";
                lbError.Visible = true;
                lbError.Text = "Select Company from the TreeView";
                return;
            }
            settingsCompanyID = fnGetCompanyId();
            lbError.Visible = false;
            string sel = treeView1.SelectedNode.Text;
            if (sel != "Company's" && sel != "")
            {
                settingsCompanyID = fnGetCompanyId();
                if (fnValidateSettings())
                {
                    settingsDsnName = txtDsnName.Text;
                    settingsUserName = txtUserName.Text;
                    settingsPassword = txtPassword.Text;
                    settingsDBName = txtDbName.Text;
                    if (rdbtnIdPosPre.Checked == false && rdbtnIdPosSuf.Checked == false)
                    {
                        settingsPrefixSuffix = "no";
                    }
                    else if (rdbtnIdPosSuf.Checked == true)
                    {
                        settingsPrefixSuffix = "suf";
                    }
                    else if (rdbtnIdPosPre.Checked == true)
                    {
                        settingsPrefixSuffix = "pre";
                    }
                    ///////////////////

                    if (chkRefCardEmp.Checked == false && chkUseRefEmp.Checked == false)
                    {
                        settingsReferCardEmp = "no";
                        settingsUseRefEmp = "no";
                    }
                    else if (chkRefCardEmp.Checked == true)
                    {
                        settingsReferCardEmp = "yes";
                        settingsUseRefEmp = "no";
                    }
                    else if (chkUseRefEmp.Checked == true)
                    {
                        settingsUseRefEmp = "yes";
                        settingsReferCardEmp = "no";
                    }
                    //////////////////
                    //if (chkRefCardEmp.Checked == true)
                    //    settingsReferCardEmp = "yes";
                    //else
                    //    settingsReferCardEmp = "no";

                    //if (chkUseRefEmp.Checked == true)
                    //    settingsUseRefEmp = "yes";
                    //else
                    //    settingsUseRefEmp = "no";

                    settingsStartDate = DTPStartDate.Text;

                    settingsCompanyID = fnGetCompanyId();
                    int rowVal = fnGetTASettingsRow(1);
                    if (rowVal == 0)
                    {
                        SqlCeConnection sqlConnection1 = new SqlCeConnection();
                        sqlConnection1.ConnectionString = dataSourcePath;

                        SqlCeCommand cmd = new SqlCeCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "INSERT INTO MDB_FILE_SETTINGS (MDB_ID, MDB_COMP_ID, MDB_URL, MDB_FILE_PATH, MDB_TIME_INT, MDB_REF_AS_EMP, MDB_CARD_FOR_CMP, MDB_PRE_SUF, MDB_EMP_LEN, MDB_START_DATE) VALUES(" + settingsCompanyID.ToString() + "," + settingsCompanyID.ToString() + ",'" + settingsURL + "','" + Global.mdbFilePath + "'," + settingsTimeInt.ToString() + ",'" + settingsUseRefEmp + "','" + settingsReferCardEmp + "','" + settingsPrefixSuffix + "'," + settingsFieldLen.ToString() + ",'" + settingsStartDate + "')";
                        cmd.Connection = sqlConnection1;

                        sqlConnection1.Open();
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "INSERT INTO MDB_FILE_FIELD_SETTINGS (FIE_MDB_ID, FIE_MDB_PASSWORD, FIE_MDB_TABLE_NAME, FIE_MDB_EMP, FIE_MDB_DEVICE, FIE_MDB_DATETIME_INOUT, FIE_MDB_DATETIME_IN, FIE_MDB_DATETIME_OUT, FIE_MDB_DATE, FIE_MDB_DATE_INOUT, FIE_MDB_DATE_IN, FIE_MDB_DATE_OUT, FIE_MDB_INDEX) VALUES(" + settingsCompanyID.ToString() + ",'" + Global.filepassword + "','" + Global.tableName + "','" + Global.EmpIdFieldName + "','" + Global.DeviceFieldName + "','" + Global.DateTimeInOutFieldName + "','" + Global.DateTimeInFieldName + "','" + Global.DateTimeOutFieldName + "','" + Global.DateFieldName + "','" + Global.InOutFieldName + "','" + Global.InFieldName + "','" + Global.OutFieldName + "',0)";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "UPDATE CompRegister SET CO_FILE_TYPE='mdb' WHERE CO_COMP_ID=" + settingsCompanyID.ToString();
                        cmd.ExecuteNonQuery();

                        sqlConnection1.Dispose();
                        sqlConnection1.Close();
                        lbError.Visible = true;
                        lbError.Text = "Record added";
                    }
                    else
                    {
                        SqlCeConnection sqlConnection2 = new SqlCeConnection();
                        sqlConnection2.ConnectionString = dataSourcePath;

                        SqlCeCommand cmd1 = new SqlCeCommand();

                        sqlConnection2.Open();
                        cmd1.CommandType = System.Data.CommandType.Text;
                        cmd1.CommandText = "UPDATE MDB_FILE_SETTINGS SET MDB_URL='" + settingsURL + "',MDB_TIME_INT=" + settingsTimeInt.ToString() + ",MDB_EMP_LEN=" + settingsFieldLen.ToString() + ",MDB_START_DATE='" + settingsStartDate + "',MDB_PRE_SUF='" + settingsPrefixSuffix + "',MDB_CARD_FOR_CMP='" + settingsReferCardEmp + "',MDB_REF_AS_EMP='" + settingsUseRefEmp + "',MDB_FILE_PATH='" + txtMdbFilePath.Text + "' WHERE MDB_COMP_ID=" + settingsCompanyID.ToString();
                        cmd1.Connection = sqlConnection2;
                        cmd1.ExecuteNonQuery();

                        cmd1.CommandText = "UPDATE CompRegister SET CO_FILE_TYPE='mdb' WHERE CO_COMP_ID=" + settingsCompanyID.ToString();
                        cmd1.ExecuteNonQuery();
                        if (Global.SetMdbValues == "yes")
                        {
                            cmd1.CommandText = "UPDATE MDB_FILE_FIELD_SETTINGS SET FIE_MDB_ID=" + settingsCompanyID.ToString() + ", FIE_MDB_PASSWORD='" + Global.filepassword + "', FIE_MDB_TABLE_NAME='" + Global.tableName + "', FIE_MDB_EMP='" + Global.EmpIdFieldName + "', FIE_MDB_DEVICE='" + Global.DeviceFieldName + "', FIE_MDB_DATETIME_INOUT='" + Global.DateTimeInOutFieldName + "', FIE_MDB_DATETIME_IN='" + Global.DateTimeInFieldName + "', FIE_MDB_DATETIME_OUT='" + Global.DateTimeOutFieldName + "', FIE_MDB_DATE='" + Global.DateFieldName + "', FIE_MDB_DATE_INOUT='" + Global.InOutFieldName + "', FIE_MDB_DATE_IN='" + Global.InFieldName + "', FIE_MDB_DATE_OUT='" + Global.OutFieldName + "',FIE_MDB_INDEX=0";
                            cmd1.ExecuteNonQuery();
                        }
                        sqlConnection2.Dispose();
                        sqlConnection2.Close();
                        lbError.Visible = true;
                        lbError.Text = "Record Updated";
                    }
                }
                else
                    return;
            }
            else
            {
                lbError.Visible = true;
                lbError.Text = "Select Company from the TreeView";
                fromUpdates = "yes";
                treeView1.Focus();
            }
            try
            {
                treeView1_AfterSelect(null, null);
            }
            catch { }
        }

        private int fnGetCompanyId()
        {
            string compName = selecteComp;

            if (compName != "Company's" && compName != "")
            {
                SqlCeConnection sqlConnection1 = new SqlCeConnection();
                sqlConnection1.ConnectionString = dataSourcePath;
                SqlCeCommand cmd = new SqlCeCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM CompRegister WHERE CO_COMP_NAME='" + compName + "'";
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();
                SqlCeDataReader dataRead;
                dataRead = cmd.ExecuteReader();
                while (dataRead.Read())
                {
                    settingsCompanyID = Convert.ToInt32(dataRead[0]);
                    selecteComp = dataRead[1].ToString();
                }
                sqlConnection1.Dispose();
                sqlConnection1.Close();
            }
            lblcompid.Text = "Selected Company : " + selecteComp;
            return settingsCompanyID;
        }
        //ref
        private bool fnValidateSettings()
        {
            bool valida = false;
            lbError.Visible = false;
            if (txtURL.Text == string.Empty)
            {
                lbError.Visible = true;
                lbError.Text = "Enter URL Path";
                txtURL.Focus();
            }
            else if (txtTimeInt.Text == string.Empty)
            {
                fnValidateSettingsMain(1);
                lbError.Visible = true;
                lbError.Text = "Enter Time Interval";
                txtTimeInt.Focus();
            }
            //else if (txtDsnName.Text == string.Empty)
            //{
            //    fnValidateSettingsMain(1);
            //    if (settingsFieldLen != -1)
            //    {
            //        lbError.Visible = true;
            //        lbError.Text = "Enter DSN Name";
            //        txtDsnName.Focus();
            //    }
            //    else
            //        return false;
            //}
            //else if (txtUserName.Text == string.Empty)
            //{
            //    fnValidateSettingsMain(1);
            //    lbError.Visible = true;
            //    lbError.Text = "Enter Username";
            //    txtUserName.Focus();
            //}
            //else if (txtPassword.Text == string.Empty)
            //{
            //    fnValidateSettingsMain(1);
            //    lbError.Visible = true;
            //    lbError.Text = "Enter Password";
            //    txtPassword.Focus();
            //}
            //else if (txtDbName.Text == string.Empty)
            //{
            //    fnValidateSettingsMain(1);
            //    lbError.Visible = true;
            //    lbError.Text = "Enter DB Name";
            //    txtDbName.Focus();
            //}
            else if (txtMdbFilePath.Text == string.Empty)
            {
                lbError.Visible = true;
                lbError.Text = "Select MDB File Path";
                btnMdbFileSelecter.Focus();
            }
            else if (rdbtnIdPosSuf.Checked == true || rdbtnIdPosPre.Checked == true)
            {
                if (txtFieldLen.Text == string.Empty)
                {
                    fnValidateSettingsMain(1);
                    if (settingsTimeInt != -1)
                    {
                        lbError.Visible = true;
                        lbError.Text = "Enter Field Length";
                        txtFieldLen.Focus();
                    }
                    else
                        return false;
                }
            }
            else
            {
                fnValidateSettingsMain(1);
                if (settingsFieldLen != -1 && settingsTimeInt != -1 && settingsURL != "")
                {
                    valida = true;
                }
                else
                    valida = false;
            }
            //else if (rdbtnIdPosPre.Checked == false && rdbtnIdPosSuf.Checked == false)
            //{

            //}

            //chkRefCardEmp.Checked = false;
            //chkUseRefEmp.Checked = false;
            //DTPStartDate.Value = DateTime.Today;
            return valida;
        }

        private void btnClearSettings_Click(object sender, EventArgs e)
        {
            lbServerURL.Text = "";
            txtURL.Text = "";
            txtTimeInt.Text = "";
            txtFieldLen.Text = "";
            rdbtnIdPosPre.Checked = false;
            rdbtnIdPosSuf.Checked = false;
            chkRefCardEmp.Checked = false;
            chkUseRefEmp.Checked = false;
            txtDsnName.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtDbName.Text = "";
            DTPStartDate.Value = DateTime.Today;
            txtMdbFilePath.Text = "";
        }

        private void btnClearFileSettings_Click(object sender, EventArgs e)
        {
            lbServerURL.Text = "";
            txtTextURL.Text = "";
            txtFilePath.Text = "";
            //cmbFirstField.SelectedIndex = -1;
            //cmbSecondField.SelectedIndex = -1;
            //cmbThirdField.SelectedIndex = -1;
            //cmbFourthField.SelectedIndex = -1;
            txtFisFiePos.Text = "";
            txtSecFiePos.Text = "";
            txtThrFiePos.Text = "";
            txtFrthFiePos.Text = "";
            txtFisFieLen.Text = "";
            txtSecFieLen.Text = "";
            txtThrFieLen.Text = "";
            txtFrthFieLen.Text = "";
            txtStartRow.Text = "";
            chkDelimitor.Checked = true;
            txtDelimitor.Text = "";
            txtDateFormat.Text = "";
            txtTimeFormat.Text = "";
            //chkAutoTimeInt.Checked = true;
            txtAutoTimeInt.Text = "";
        }

        private void btnReadData_Click(object sender, EventArgs e)
        {
            lbFileError.Visible = false;
            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Text == "Company's")
            {
                fromUpdates = "yes";
                lbFileError.Visible = true;
                lbFileError.Text = "Select Company from the TreeView";
                return;
            }
            string empAs = "";
            //settingsCompanyID = fnGetCompanyId();
            lbFileError.Visible = false;
            string sel = treeView1.SelectedNode.Text;
            if (sel != "Company's" && sel != "")
            {
                //MessageBox.Show("Before function");
                if (fnValidateFileSettings())
                {
                    //MessageBox.Show("After function");
                    if (rdbnEmpId.Checked == true)
                        empAs = "emp";
                    else if (rdbnRefId.Checked == true)
                        empAs = "ref";
                    else if (rdbnCardId.Checked == true)
                        empAs = "card";
                    else
                    {
                        lbFileError.Visible = true;
                        lbFileError.Text = "Select Emp Id reference ";
                    }

                    settingsCompanyID = fnGetCompanyId();
                    int row = fnGetTextSettingsRow();
                    if (row == 0)
                    {
                        try
                        {
                            SqlCeConnection sqlConnection1 = new SqlCeConnection();
                            sqlConnection1.ConnectionString = dataSourcePath;

                            SqlCeCommand cmd = new SqlCeCommand();
                            cmd.CommandType = System.Data.CommandType.Text;
                            if(txtFrthFiePos.Text.Trim()!="" && txtFrthFieLen.Text.Trim()!="")
                                cmd.CommandText = "INSERT INTO TEXT_FILE_SETTINGS (TEXT_COM_ID, TEXT_URL, TEXT_FILE_PATH, TEXT_EMP_POS, TEXT_EMP_LEN, TEXT_DATE_POS, TEXT_DATE_LEN, TEXT_TIME_POS, TEXT_TIME_LEN, TEXT_DEV_POS, TEXT_DEV_LEN, TEXT_START_ROW, TEXT_DATE_FORMAT, TEXT_TIME_FORMAT, TEXT_AUTO_TIME, TEXT_EMP_AS) VALUES(" + settingsCompanyID.ToString() + ",'" + txtTextURL.Text.Trim() + "','" + txtFilePath.Text.Trim() + "'," + fileSettingsFisFiePos.ToString() + "," + fileSettingsFisFieLen.ToString() + "," + fileSettingsSecFiePos.ToString() + "," + fileSettingsSecFieLen.ToString() + "," + fileSettingsThrFiePos.ToString() + "," + fileSettingsThrFieLen.ToString() + "," + txtFrthFiePos.Text.Trim() + "," + txtFrthFieLen.Text.Trim() + "," + fileSettingsStartRow.ToString() + ",'" + txtDateFormat.Text + "','" + txtTimeFormat.Text + "'," + fileSettingsAutoIntMin.ToString() + ",'" + empAs + "')";
                            else
                                cmd.CommandText = "INSERT INTO TEXT_FILE_SETTINGS (TEXT_COM_ID, TEXT_URL, TEXT_FILE_PATH, TEXT_EMP_POS, TEXT_EMP_LEN, TEXT_DATE_POS, TEXT_DATE_LEN, TEXT_TIME_POS, TEXT_TIME_LEN, TEXT_START_ROW, TEXT_DATE_FORMAT, TEXT_TIME_FORMAT, TEXT_AUTO_TIME, TEXT_EMP_AS) VALUES(" + settingsCompanyID.ToString() + ",'" + txtTextURL.Text.Trim() + "','" + txtFilePath.Text.Trim() + "'," + fileSettingsFisFiePos.ToString() + "," + fileSettingsFisFieLen.ToString() + "," + fileSettingsSecFiePos.ToString() + "," + fileSettingsSecFieLen.ToString() + "," + fileSettingsThrFiePos.ToString() + "," + fileSettingsThrFieLen.ToString() + "," + fileSettingsStartRow.ToString() + ",'" + txtDateFormat.Text + "','" + txtTimeFormat.Text + "'," + fileSettingsAutoIntMin.ToString() + ",'" + empAs + "')";
                            cmd.Connection = sqlConnection1;

                            sqlConnection1.Open();
                            cmd.ExecuteNonQuery();
                            if (Global.filename == "")
                            {
                                if (Global.filePrefix != "" && Global.fileSuffix != "")
                                    cmd.CommandText = "INSERT INTO TEXT_FILE_NAME_FORMAT (TEXT_NAME_FORMAT_ID, TEXT_FORMAT_PRE, TEXT_FORMAT_DATE_FORMAT, TEXT_FORMAT_SUF, TEXT_FORMAT_FILETYPE, TEXT_FORMAT_FROM_DATE, TEXT_FORMAT_FILE_EXT) VALUES(" + settingsCompanyID.ToString() + ",'" + Global.filePrefix + "','" + Global.fileDateFormat + "','" + Global.fileSuffix + "','" + Global.fileTypeRead + "','" + Global.fileReadDate + "','" + cmbFileType.SelectedItem.ToString() + "')";
                                else if (Global.filePrefix == "" && Global.fileSuffix == "")
                                    cmd.CommandText = "INSERT INTO TEXT_FILE_NAME_FORMAT (TEXT_NAME_FORMAT_ID, TEXT_FORMAT_DATE_FORMAT, TEXT_FORMAT_FILETYPE, TEXT_FORMAT_FROM_DATE, TEXT_FORMAT_FILE_EXT) VALUES(" + settingsCompanyID.ToString() + ",'" + Global.fileDateFormat + "','" + Global.fileTypeRead + "','" + Global.fileReadDate + "','" + cmbFileType.SelectedItem.ToString() + "')";
                                else if (Global.filePrefix == "")
                                    cmd.CommandText = "INSERT INTO TEXT_FILE_NAME_FORMAT (TEXT_NAME_FORMAT_ID, TEXT_FORMAT_DATE_FORMAT, TEXT_FORMAT_SUF, TEXT_FORMAT_FILETYPE, TEXT_FORMAT_FROM_DATE, TEXT_FORMAT_FILE_EXT) VALUES(" + settingsCompanyID.ToString() + ",'" + Global.fileDateFormat + "','" + Global.fileSuffix + "','" + Global.fileTypeRead + "','" + Global.fileReadDate + "','" + cmbFileType.SelectedItem.ToString() + "')";
                                else if (Global.fileSuffix == "")
                                    cmd.CommandText = "INSERT INTO TEXT_FILE_NAME_FORMAT (TEXT_NAME_FORMAT_ID, TEXT_FORMAT_PRE, TEXT_FORMAT_DATE_FORMAT, TEXT_FORMAT_FILETYPE, TEXT_FORMAT_FROM_DATE, TEXT_FORMAT_FILE_EXT) VALUES(" + settingsCompanyID.ToString() + ",'" + Global.filePrefix + "','" + Global.fileDateFormat + "','" + Global.fileTypeRead + "','" + Global.fileReadDate + "','" + cmbFileType.SelectedItem.ToString() + "')";
                            }
                            else if (Global.filename != "")
                            {
                                cmd.CommandText = "INSERT INTO TEXT_FILE_NAME_FORMAT (TEXT_NAME_FORMAT_ID, TEXT_FORMAT_DATE_FORMAT, TEXT_FORMAT_FILE_NAME, TEXT_FORMAT_FILETYPE, TEXT_FORMAT_FILE_EXT) VALUES(" + settingsCompanyID.ToString() + ", 'none', '" + Global.filename + "','file', '" + cmbFileType.SelectedItem.ToString() + "')";
                            }
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE CompRegister SET CO_FILE_TYPE='text' WHERE CO_COMP_ID=" + settingsCompanyID.ToString();
                            cmd.ExecuteNonQuery();

                            sqlConnection1.Dispose();
                            sqlConnection1.Close();
                            lbFileError.Visible = true;
                            lbFileError.Text = "Record added";
                        }
                        catch(Exception ee)
                        {
                            lbFileError.Visible = true;
                            lbFileError.Text = "Error:"+ee.Message;
                            return;
                        }
                    }
                    else
                    {
                        try
                        {
                            SqlCeConnection sqlConnection2 = new SqlCeConnection();
                            sqlConnection2.ConnectionString = dataSourcePath;

                            SqlCeCommand cmd1 = new SqlCeCommand();
                            cmd1.CommandType = System.Data.CommandType.Text;
                            if (txtFrthFiePos.Text.Trim() != "" && txtFrthFieLen.Text.Trim() != "")
                                cmd1.CommandText = "UPDATE TEXT_FILE_SETTINGS SET TEXT_URL='" + txtTextURL.Text.Trim() + "',TEXT_FILE_PATH='" + txtFilePath.Text.Trim() + "',TEXT_EMP_POS=" + fileSettingsFisFiePos.ToString() + ",TEXT_EMP_LEN=" + fileSettingsFisFieLen.ToString() + ",TEXT_DATE_POS=" + fileSettingsSecFiePos.ToString() + ",TEXT_DATE_LEN=" + fileSettingsSecFieLen.ToString() + ",TEXT_TIME_POS=" + fileSettingsThrFiePos.ToString() + ",TEXT_TIME_LEN=" + fileSettingsThrFieLen.ToString() + ",TEXT_DEV_POS=" + txtFrthFiePos.Text.Trim() + ",TEXT_DEV_LEN=" + txtFrthFieLen.Text.Trim() + ",TEXT_START_ROW=" + fileSettingsStartRow.ToString() + ", TEXT_DATE_FORMAT='" + txtDateFormat.Text + "', TEXT_TIME_FORMAT='" + txtTimeFormat.Text + "', TEXT_AUTO_TIME=" + fileSettingsAutoIntMin.ToString() + ",TEXT_EMP_AS='" + empAs + "' WHERE TEXT_COM_ID=" + settingsCompanyID.ToString();
                            else
                                cmd1.CommandText = "UPDATE TEXT_FILE_SETTINGS SET TEXT_URL='" + txtTextURL.Text.Trim() + "',TEXT_FILE_PATH='" + txtFilePath.Text.Trim() + "',TEXT_EMP_POS=" + fileSettingsFisFiePos.ToString() + ",TEXT_EMP_LEN=" + fileSettingsFisFieLen.ToString() + ",TEXT_DATE_POS=" + fileSettingsSecFiePos.ToString() + ",TEXT_DATE_LEN=" + fileSettingsSecFieLen.ToString() + ",TEXT_TIME_POS=" + fileSettingsThrFiePos.ToString() + ",TEXT_TIME_LEN=" + fileSettingsThrFieLen.ToString() + ",TEXT_START_ROW=" + fileSettingsStartRow.ToString() + ", TEXT_DATE_FORMAT='" + txtDateFormat.Text + "', TEXT_TIME_FORMAT='" + txtTimeFormat.Text + "', TEXT_AUTO_TIME=" + fileSettingsAutoIntMin.ToString() + ",TEXT_EMP_AS='" + empAs + "' WHERE TEXT_COM_ID=" + settingsCompanyID.ToString();
                            cmd1.Connection = sqlConnection2;

                            sqlConnection2.Open();
                            cmd1.ExecuteNonQuery();

                            if (Global.filename == "")
                            {
                                if (Global.fileReadDate != "" && Global.fileTypeRead != "" && Global.fileDateFormat != "")
                                {
                                    if (Global.filePrefix != "" && Global.fileSuffix != "")
                                        cmd1.CommandText = "UPDATE TEXT_FILE_NAME_FORMAT SET TEXT_FORMAT_PRE='" + Global.filePrefix + "', TEXT_FORMAT_DATE_FORMAT='" + Global.fileDateFormat + "', TEXT_FORMAT_SUF='" + Global.fileSuffix + "', TEXT_FORMAT_FILETYPE='" + Global.fileTypeRead + "', TEXT_FORMAT_FROM_DATE='" + Global.fileReadDate + "', TEXT_FORMAT_FILE_EXT='" + cmbFileType.SelectedItem.ToString() + "' WHERE TEXT_NAME_FORMAT_ID='" + settingsCompanyID.ToString() + "'";
                                    else if (Global.filePrefix == "" && Global.fileSuffix == "")
                                        cmd1.CommandText = "UPDATE TEXT_FILE_NAME_FORMAT SET TEXT_FORMAT_PRE='', TEXT_FORMAT_DATE_FORMAT='" + Global.fileDateFormat + "', TEXT_FORMAT_SUF='', TEXT_FORMAT_FILETYPE='" + Global.fileTypeRead + "', TEXT_FORMAT_FROM_DATE='" + Global.fileReadDate + "', TEXT_FORMAT_FILE_EXT='" + cmbFileType.SelectedItem.ToString() + "' WHERE TEXT_NAME_FORMAT_ID='" + settingsCompanyID.ToString() + "'";
                                    else if (Global.filePrefix == "")
                                        cmd1.CommandText = "UPDATE TEXT_FILE_NAME_FORMAT SET TEXT_FORMAT_PRE='', TEXT_FORMAT_DATE_FORMAT='" + Global.fileDateFormat + "', TEXT_FORMAT_SUF='" + Global.fileSuffix + "', TEXT_FORMAT_FILETYPE='" + Global.fileTypeRead + "', TEXT_FORMAT_FROM_DATE='" + Global.fileReadDate + "', TEXT_FORMAT_FILE_EXT='" + cmbFileType.SelectedItem.ToString() + "' WHERE TEXT_NAME_FORMAT_ID='" + settingsCompanyID.ToString() + "'";
                                    else if (Global.fileSuffix == "")
                                        cmd1.CommandText = "UPDATE TEXT_FILE_NAME_FORMAT SET TEXT_FORMAT_PRE='" + Global.filePrefix + "', TEXT_FORMAT_DATE_FORMAT='" + Global.fileDateFormat + "', TEXT_FORMAT_SUF='', TEXT_FORMAT_FILETYPE='" + Global.fileTypeRead + "', TEXT_FORMAT_FROM_DATE='" + Global.fileReadDate + "', TEXT_FORMAT_FILE_EXT='" + cmbFileType.SelectedItem.ToString() + "' WHERE TEXT_NAME_FORMAT_ID='" + settingsCompanyID.ToString() + "'";
                                    cmd1.ExecuteNonQuery();

                                }
                            }
                            else if (Global.filename != "")
                            {
                                cmd1.CommandText = "UPDATE TEXT_FILE_NAME_FORMAT SET TEXT_FORMAT_DATE_FORMAT='none', TEXT_FORMAT_FILE_NAME='" + Global.filename + "', TEXT_FORMAT_FILETYPE='file', TEXT_FORMAT_FILE_EXT='" + cmbFileType.SelectedItem.ToString() + "' WHERE TEXT_NAME_FORMAT_ID='" + settingsCompanyID.ToString() + "'";
                                cmd1.ExecuteNonQuery();
                            }

                            cmd1.CommandText = "UPDATE CompRegister SET CO_FILE_TYPE='text' WHERE CO_COMP_ID=" + settingsCompanyID.ToString();
                            cmd1.ExecuteNonQuery();

                            sqlConnection2.Dispose();
                            sqlConnection2.Close();
                            lbFileError.Visible = true;
                            lbFileError.Text = "Record Updated";
                        }
                        catch { }
                    }
                }
                else
                    return;
            }
            else
            {
                lbFileError.Visible = true;
                lbFileError.Text = "Select Company from the TreeView";
                treeView1.Focus();
            }
            try
            {
                treeView1_AfterSelect(null, null);
            }
            catch { }
        }

        private bool fnValidateFileSettings()
        {
            bool valid = false;
            lbFileError.Visible = false;
            if (txtTextURL.Text == string.Empty)
            {
                lbFileError.Visible = true;
                lbFileError.Text = "Enter URL";
                txtTextURL.Focus();
            }
            else if (txtFilePath.Text == string.Empty)
            {
                fnValidateSettingsMain(2);
                lbFileError.Visible = true;
                lbFileError.Text = "Select File Path";
                btnfileselector.Focus();
                //txtFilePath.Focus();
            }
            //else if (cmbFirstField.SelectedIndex == -1)
            //{
            //    fnValidateSettingsMain(2);
            //    lbFileError.Visible = true;
            //    lbFileError.Text = "Select Field name";
            //    cmbFirstField.Focus();
            //}
            else if (txtFisFiePos.Text == string.Empty)
            {
                fnValidateSettingsMain(2);
                    lbFileError.Visible = true;
                    lbFileError.Text = "Enter empId field starting Postion";
                    txtFisFiePos.Focus();
            }
            else if (txtFisFieLen.Text == string.Empty)
            {
                fnValidateSettingsMain(2);
                lbFileError.Visible = true;
                lbFileError.Text = "Enter field Length";
                txtFisFieLen.Focus();
                //txtFilePath.Focus();
            }
            //else if (cmbSecondField.SelectedIndex == -1)
            //{
            //    fnValidateSettingsMain(2);
            //    lbFileError.Visible = true;
            //    lbFileError.Text = "Select Field name";
            //    cmbSecondField.Focus();
            //}
            else if (txtSecFiePos.Text == string.Empty)
            {
                fnValidateSettingsMain(2);
                lbFileError.Visible = true;
                lbFileError.Text = "Enter date field starting Postion";
                txtSecFiePos.Focus();
                //txtFilePath.Focus();
            }
            else if (txtSecFieLen.Text == string.Empty)
            {
                fnValidateSettingsMain(2);
                lbFileError.Visible = true;
                lbFileError.Text = "Enter field Length";
                txtSecFieLen.Focus();
                //txtFilePath.Focus();
            }
            //else if (cmbThirdField.SelectedIndex == -1)
            //{
            //    fnValidateSettingsMain(2);
            //    lbFileError.Visible = true;
            //    lbFileError.Text = "Select Field name";
            //    cmbThirdField.Focus();
            //}
            else if (txtThrFiePos.Text == string.Empty)
            {
                fnValidateSettingsMain(2);
                lbFileError.Visible = true;
                lbFileError.Text = "Enter time field starting Postion";
                txtThrFiePos.Focus();
                //txtFilePath.Focus();
            }
            else if (txtThrFieLen.Text == string.Empty)
            {
                fnValidateSettingsMain(2);
                lbFileError.Visible = true;
                lbFileError.Text = "Enter field Length";
                txtThrFieLen.Focus();
                //txtFilePath.Focus();
            }
            //else if (cmbFourthField.SelectedIndex == -1)
            //{
            //fnValidateSettingsMain(2);
            //    lbFileError.Visible = true;
            //    lbFileError.Text = "Select Field name";
            //    cmbFourthField.Focus();
            //}
            //else if (txtFrthFiePos.Text == string.Empty)
            //{
            //fnValidateSettingsMain(2);
            //    lbFileError.Visible = true;
            //    lbFileError.Text = "Enter deviceId field starting Postion";
            //    txtFrthFiePos.Focus();
            //    //txtFilePath.Focus();
            //}
            //else if (txtFrthFieLen.Text == string.Empty)
            //{
            //fnValidateSettingsMain(2);
            //    lbFileError.Visible = true;
            //    lbFileError.Text = "Enter field Length";
            //    txtFrthFieLen.Focus();
            //    //txtFilePath.Focus();
            //}
            else if (txtStartRow.Text == string.Empty)
            {
                fnValidateSettingsMain(2);
                lbFileError.Visible = true;
                lbFileError.Text = "Enter Starting Row";
                txtStartRow.Focus();
                //txtFilePath.Focus();
            }
            //else if (txtNoCharsLine.Text == string.Empty)
            //{
            //    fnValidateSettingsMain(2);
            //    lbFileError.Visible = true;
            //    lbFileError.Text = "Enter Number of Charecters per Line";
            //    txtNoCharsLine.Focus();
            //    //txtFilePath.Focus();
            //}
            else if (txtDateFormat.Text == string.Empty)
            {
                fnValidateSettingsMain(2);
                lbFileError.Visible = true;
                lbFileError.Text = "Enter Date Format";
                txtDateFormat.Focus();
                //txtFilePath.Focus();
            }
            else if (txtTimeFormat.Text == string.Empty)
            {
                fnValidateSettingsMain(2);
                lbFileError.Visible = true;
                lbFileError.Text = "Enter Time Format";
                txtTimeFormat.Focus();
                //txtFilePath.Focus();
            }
            else
            {
                Application.DoEvents();
                fnValidateSettingsMain(2);
                if (settingsURL != "" && fileSettingsAutoIntMin!=-1)
                    valid = true;
                else
                {
                    valid = false;
                    lbFileError.Visible = true;
                    if (fileSettingsAutoIntMin == -1 && txtAutoTimeInt.Text=="")
                    {
                        lbFileError.Text = "Enter Time Interval in minutes";
                        txtAutoTimeInt.SelectAll();
                        txtAutoTimeInt.Focus();
                    }
                    else if (settingsURL == "")
                    {
                        lbFileError.Text = "Enter Valid URL Path";
                        txtTextURL.SelectAll();
                        txtTextURL.Focus();
                    }
                }
            }
            //else if (chkAutoTimeInt.Checked != true)
            //{
            //    if (txtDelimitor.Text == string.Empty)
            //    {
            //        lbFileError.Visible = true;
            //        lbFileError.Text = "Enter Auto Time Interval";
            //        txtAutoTimeInt.Focus();
            //    }
            //}
            //else if (chkDelimitor.Checked != true)
            //{
            //    if (txtDelimitor.Text == string.Empty)
            //    {
            //        lbFileError.Visible = true;
            //        lbFileError.Text = "Enter Delimiter";
            //        txtDelimitor.Focus();
            //    }
            //}
            return valid;
        }
        
        private void fnValidateSettingsMain(int i)
        {
            if (i == 1)
            {
                //for settings

                if (fnValidateURL(1))
                {
                    if (txtTimeInt.Text != "")
                    {
                        settingsTimeInt = 0;
                        try
                        {
                            settingsTimeInt = Convert.ToInt32(txtTimeInt.Text);
                        }
                        catch
                        {
                            settingsTimeInt = -1;
                            lbError.Visible = true;
                            lbError.Text = "Enter correct Time Interval";
                            txtTimeInt.SelectAll();
                            txtTimeInt.Focus();
                            return;
                        }
                        if (settingsTimeInt != -1)
                        {
                            if (rdbtnIdPosSuf.Checked == true || rdbtnIdPosPre.Checked == true)
                            {
                                if (txtFieldLen.Text != string.Empty)
                                {
                                    settingsFieldLen = 0;
                                    try
                                    {
                                        settingsFieldLen = Convert.ToInt32(txtFieldLen.Text);
                                    }
                                    catch
                                    {
                                        settingsFieldLen = -1;
                                        lbError.Visible = true;
                                        lbError.Text = "Enter correct Field Length";
                                        txtFieldLen.SelectAll();
                                        txtFieldLen.Focus();
                                        return;
                                    }
                                    //if (settingsFieldLen != -1)
                                    //{

                                    //}
                                }
                                else
                                {
                                    lbError.Visible = true;
                                    lbError.Text = "Enter Field Length";
                                    txtFieldLen.Focus();
                                }
                            }
                        }
                    }
                }
                else
                {
                    settingsURL = "";
                    lbError.Visible = true;
                    lbError.Text = "Enter Valid URL Path";
                    txtURL.SelectAll();
                    txtURL.Focus();
                }
            }
            else if (i == 2)
            {
                //for text file settings
                if (fnValidateURL(2))
                {
                    if (txtFisFiePos.Text != "")
                    {
                        fileSettingsFisFie = cmbFirstField.SelectedIndex;
                        fileSettingsFisFiePos = 0;
                        try
                        {
                            fileSettingsFisFiePos = Convert.ToInt32(txtFisFiePos.Text);
                        }
                        catch
                        {
                            fileSettingsFisFiePos = -1;
                            lbFileError.Visible = true;
                            lbFileError.Text = "Enter correct start Positon for first field";
                            txtFisFiePos.SelectAll();
                            txtFisFiePos.Focus();
                            return;
                        }
                    }
                    if (fileSettingsFisFiePos != -1)
                    {
                        if (txtFisFieLen.Text != "")
                        {
                            fileSettingsFisFieLen = 0;
                            try
                            {
                                fileSettingsFisFieLen = Convert.ToInt32(txtFisFieLen.Text);
                            }
                            catch
                            {
                                fileSettingsFisFieLen = -1;
                                lbFileError.Visible = true;
                                lbFileError.Text = "Enter correct field length";
                                txtFisFieLen.SelectAll();
                                txtFisFieLen.Focus();
                                return;
                            }
                        }
                        if (fileSettingsFisFieLen != -1)
                        {
                            if (txtSecFiePos.Text != "")
                            {
                                fileSettingsSecFie = cmbSecondField.SelectedIndex;
                                fileSettingsSecFiePos = 0;
                                try
                                {
                                    fileSettingsSecFiePos = Convert.ToInt32(txtSecFiePos.Text);
                                }
                                catch
                                {
                                    fileSettingsSecFiePos = -1;
                                    lbFileError.Visible = true;
                                    lbFileError.Text = "Enter correct start Positon for second field";
                                    txtSecFiePos.SelectAll();
                                    txtSecFiePos.Focus();
                                    return;
                                }
                            }
                            if (fileSettingsSecFiePos != -1)
                            {
                                if (txtSecFieLen.Text != "")
                                {
                                    fileSettingsSecFieLen = 0;
                                    try
                                    {
                                        fileSettingsSecFieLen = Convert.ToInt32(txtSecFieLen.Text);
                                    }
                                    catch
                                    {
                                        fileSettingsSecFieLen = -1;
                                        lbFileError.Visible = true;
                                        lbFileError.Text = "Enter correct field length";
                                        txtSecFieLen.SelectAll();
                                        txtSecFieLen.Focus();
                                        return;
                                    }
                                }
                                if (fileSettingsSecFieLen != -1)
                                {
                                    if (txtThrFiePos.Text != "")
                                    {
                                        fileSettingsThrFie = cmbThirdField.SelectedIndex;
                                        fileSettingsThrFiePos = 0;
                                        try
                                        {
                                            fileSettingsThrFiePos = Convert.ToInt32(txtThrFiePos.Text);
                                        }
                                        catch
                                        {
                                            fileSettingsThrFiePos = -1;
                                            lbFileError.Visible = true;
                                            lbFileError.Text = "Enter correct start Positon for Third field";
                                            txtThrFiePos.SelectAll();
                                            txtThrFiePos.Focus();
                                            return;
                                        }
                                    }
                                    if (fileSettingsThrFiePos != -1)
                                    {
                                        if (txtThrFieLen.Text != "")
                                        {
                                            fileSettingsThrFieLen = 0;
                                            try
                                            {
                                                fileSettingsThrFieLen = Convert.ToInt32(txtThrFieLen.Text);
                                            }
                                            catch
                                            {
                                                fileSettingsThrFieLen = -1;
                                                lbFileError.Visible = true;
                                                lbFileError.Text = "Enter correct field length";
                                                txtThrFieLen.SelectAll();
                                                txtThrFieLen.Focus();
                                                return;
                                            }
                                        }
                                        if (fileSettingsThrFieLen != -1)
                                        {
                                            if (txtStartRow.Text != "")
                                            {
                                                fileSettingsStartRow = 0;
                                                try
                                                {
                                                    fileSettingsStartRow = Convert.ToInt32(txtStartRow.Text);
                                                }
                                                catch
                                                {
                                                    fileSettingsStartRow = -1;
                                                    lbFileError.Visible = true;
                                                    lbFileError.Text = "Enter correct Start row";
                                                    txtStartRow.SelectAll();
                                                    txtStartRow.Focus();
                                                    return;
                                                }
                                            }
                                            if (fileSettingsStartRow != -1)
                                            {
                                                //if (txtNoCharsLine.Text != "")
                                                //{
                                                //    fileSettingsNoCharsPerLine = 0;
                                                //    try
                                                //    {
                                                //        fileSettingsNoCharsPerLine = Convert.ToInt32(txtNoCharsLine.Text);
                                                //    }
                                                //    catch
                                                //    {
                                                //        fileSettingsNoCharsPerLine = -1;
                                                //        lbFileError.Visible = true;
                                                //        lbFileError.Text = "Enter Number charecters correctly";
                                                //        txtNoCharsLine.SelectAll();
                                                //        txtNoCharsLine.Focus();
                                                //        return;
                                                //    }
                                                //}
                                                //if (fileSettingsNoCharsPerLine != -1)
                                                //{
                                                    //if (chkDelimitor.Checked == true)
                                                    //{
                                                    //    if (txtDelimitor.Text != "")
                                                    //    {
                                                    //        fileSettingsDelimiter = ' ';
                                                    //        try
                                                    //        {
                                                    //            fileSettingsDelimiter = Convert.ToChar(txtDelimitor.Text);
                                                    //        }
                                                    //        catch
                                                    //        {
                                                    //            fileSettingsDelimiter = 'n';
                                                    //            lbFileError.Visible = true;
                                                    //            lbFileError.Text = "Enter correct Delimiter";
                                                    //            txtDelimitor.SelectAll();
                                                    //            txtDelimitor.Focus();
                                                    //            return;
                                                    //        }
                                                    //    }
                                                    //    else
                                                    //    {
                                                    //        lbFileError.Visible = true;
                                                    //        lbFileError.Text = "Enter Delimiter";
                                                    //        txtDelimitor.SelectAll();
                                                    //        txtDelimitor.Focus();
                                                    //        return;
                                                    //    }
                                                    //}

                                                    //if (fileSettingsDelimiter != 'n')
                                                    ////{
                                                    //    if (chkAutoTimeInt.Checked == true)
                                                    //    {
                                                            if (txtAutoTimeInt.Text != "")
                                                            {
                                                                fileSettingsAutoIntMin = 0;
                                                                try
                                                                {
                                                                    fileSettingsAutoIntMin = Convert.ToInt32(txtAutoTimeInt.Text);
                                                                }
                                                                catch
                                                                {
                                                                    fileSettingsAutoIntMin = -1;
                                                                    lbFileError.Visible = true;
                                                                    lbFileError.Text = "Enter correct timer";
                                                                    txtAutoTimeInt.SelectAll();
                                                                    txtAutoTimeInt.Focus();
                                                                    return;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                fileSettingsAutoIntMin = -1;
                                                                lbFileError.Visible = true;
                                                                lbFileError.Text = "Enter Time Interval";
                                                                txtAutoTimeInt.SelectAll();
                                                                txtAutoTimeInt.Focus();
                                                                return;
                                                            }
                                                        //}
                                                    //}
                                                //}
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    settingsURL = "";
                }
            }
        }

        private bool fnValidateURL(int i)
        {
            bool valid = false;
            try
            {
                string priUrl = "";
                string strURL = "";
                if (i == 1)
                {
                    //strURL = txtURL.Text + "auto_db_read_kanaily.php";
                    priUrl = txtURL.Text;
                }
                else if (i == 2)
                {
                    //strURL = txtTextURL.Text + "auto_db_read_kanaily.php";
                    priUrl = txtTextURL.Text;
                }
                else if (i == 3)
                {
                    //strURL = lbServerURL.Text + "auto_db_read_kanaily.php";
                    priUrl = lbServerURL.Text;
                }
                else if (i == 4)
                {
                   // strURL = txtSqlUrl.Text + "auto_db_read_kanaily.php";
                    priUrl = txtSqlUrl.Text;
                }
                if (priUrl != "")
                {
                    strURL = priUrl + "auto_db_read_kanaily.php";
                    byte[] buffer = Encoding.ASCII.GetBytes("SECODE=ranjeetkumarkanaily&DBSETTINGS=autosynchronization");
                    HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(strURL);
                    WebReq.Timeout = 108000000;
                    WebReq.Method = "POST";
                    WebReq.ContentType = "application/x-www-form-urlencoded";
                    WebReq.ContentLength = buffer.Length;
                    Stream PostData = WebReq.GetRequestStream();
                    PostData.Write(buffer, 0, buffer.Length);
                    PostData.Close();
                    HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
                    Stream Answer = WebResp.GetResponseStream();
                    StreamReader _Answer = new StreamReader(Answer);
                    string msg = _Answer.ReadToEnd().ToString();
                    _Answer.Dispose();
                    msg = msg.Trim();
                    string[] arrDB = msg.Split(new char[] { ',' });
                    string username = CNull(arrDB[2]);
                    string password = CNull(arrDB[3]);
                    string dbname = CNull(arrDB[4]);
                    if (i == 3)
                    {
                        if (CNull(arrDB[1]) == "")
                        {
                            strHost = CNull(arrDB[0]);
                        }
                        else
                        {
                            strHost = CNull(arrDB[1]);
                        }
                        //Global.strHost = CNull(arrDB[1]);
                        strUsr = CNull(arrDB[2]);
                        strPWD = CNull(arrDB[3]);
                        strDB = CNull(arrDB[4]);
                    }
                    if (dbname != "")
                    {
                        valid = true;
                        if (i == 1)
                            settingsURL = txtURL.Text;
                        else if (i == 2)
                            settingsURL = txtTextURL.Text;
                        else if (i == 4)
                            settingsURL = txtSqlUrl.Text;
                    }
                    else
                    {
                        valid = false;
                    }
                    WebReq.Abort();
                    PostData.Dispose();
                    Answer.Dispose();
                }
            }
            catch (WebException web)
            {
                //MessageBox.Show("Web error : " + web.Message);
                valid = false;
            }
            catch (UriFormatException uriex)
            {
                //MessageBox.Show("uri error : " + uriex.Message);
                valid = false;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("General error : " + ex.Message);
                valid = false;
            }
            return valid;
        }

        private string CNull(string strValue)
        {
            try
            {
                if (string.IsNullOrEmpty(strValue))
                    return "";
                else
                    return strValue;
            }
            catch
            {
                return "";
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            btnFromSqlToOther.Enabled = false;
            btnFromSqlToOther.Visible = false;
            btnTextToOther.Enabled = false;
            btnTextToOther.Visible = false;
            btnMdbToOther.Enabled = false;
            btnMdbToOther.Visible = false;
            iToolStripMenuItem.Visible = true;
            selecteComp = treeView1.SelectedNode.Text;
            if (selecteComp != "Company's")
            {
                btnPauseAll_Click(null, null);
                Pageload = "";
                lblcompid.Text = "Selected Company : " + treeView1.SelectedNode.Text;

                if (fromUpdates == "")
                {
                    fnGetValuesFromDb();
                    Application.DoEvents();
                }
                fromUpdates = "";
            }
            else
            {
                fnClearErrorMsg();
                iToolStripMenuItem.Visible = true;
                lblcompid.Text = "Selected Company : None";
                selecteComp = "";
                lbMechRec.Text = "";
                lbDbRec.Text = "";
                fnClearSett();
            }
        }

        private void fnGetValuesFromDb()
        {
            btnUpdSet.Visible = false;
            btnUpSett.Visible = false;

            Global.filePrefix = "";
            Global.fileDateFormat = "";
            Global.fileSuffix = "";
            Global.fileTypeRead = "";
            Global.fileReadDate = "";
            cmbFileType.SelectedIndex = 0;
            string fileType = "";
            Global.SelUpdate = false;
            Global.selUpdateId = 0;
            SqlCeConnection sqlConnection1 = new SqlCeConnection();
            sqlConnection1.ConnectionString = dataSourcePath;
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT * FROM CompRegister WHERE CO_COMP_NAME='" + selecteComp + "'";
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            SqlCeDataReader dataRead;
            dataRead = cmd.ExecuteReader();
            while (dataRead.Read())
            {
                settingsCompanyID = Convert.ToInt32(dataRead[0]);
                Global.Indexrow = settingsCompanyID;
                fileType = dataRead[3].ToString();
            }
            dataRead.Dispose();
            dataRead.Close();
            sqlConnection1.Dispose();
            sqlConnection1.Close();
            iToolStripMenuItem.Visible = false;
            if (fileType == "mdb")
            {
                btnFromSqlToOther.Enabled = false;
                btnFromSqlToOther.Visible = false;
                btnTextToOther.Enabled = false;
                btnTextToOther.Visible = false;
                btnMdbToOther.Enabled = true;
                btnMdbToOther.Visible = false;
                SQLFileSettingsToolStripMenuItem.Visible = true;
                textFileSettingsToolStripMenuItem.Visible = true;
                mDBFileSettingsToolStripMenuItem.Visible = true;

                SQLFileSettingsToolStripMenuItem.Enabled = false;
                textFileSettingsToolStripMenuItem.Enabled = false;
                mDBFileSettingsToolStripMenuItem.Enabled = true;
                mDBFileSettingsToolStripMenuItem_Click(null, null);
                //tabFileSettings.Dispose();
                //sqlSettings.Dispose();
                //tabFileSettings.Hide();
                //sqlSettings.Hide();
                //tabSettings.Show();
                btnClearFileSettings_Click(null, null);
                SqlCeConnection sqlConnection2 = new SqlCeConnection();
                sqlConnection2.ConnectionString = dataSourcePath;
                SqlCeCommand cmd1 = new SqlCeCommand();
                cmd1.CommandType = System.Data.CommandType.Text;
                cmd1.CommandText = "SELECT * FROM MDB_FILE_SETTINGS WHERE MDB_COMP_ID=" + settingsCompanyID.ToString();
                cmd1.Connection = sqlConnection2;

                sqlConnection2.Open();
                SqlCeDataReader dataRead1;
                try
                {
                    dataRead1 = cmd1.ExecuteReader();
                    int v = 0;
                    while (dataRead1.Read())
                    {
                        v++;
                        txtURL.Text = dataRead1[2].ToString();
                        lbServerURL.Text = dataRead1[2].ToString();
                        txtMdbFilePath.Text = dataRead1[3].ToString();
                        txtTimeInt.Text = dataRead1[4].ToString();
                        txtFieldLen.Text = dataRead1[8].ToString();
                        string preSuf = dataRead1[7].ToString();
                        if (preSuf == "pre")
                            rdbtnIdPosPre.Checked = true;
                        else if (preSuf == "suf")
                            rdbtnIdPosSuf.Checked = true;

                        string refCardEmpVal = dataRead1[6].ToString();
                        if (refCardEmpVal == "yes")
                            chkRefCardEmp.Checked = true;

                        string useRefEmpVal = dataRead1[5].ToString();
                        if (useRefEmpVal == "yes")
                            chkUseRefEmp.Checked = true;

                        string dateSel = dataRead1[9].ToString();
                        DTPStartDate.Text = dateSel;

                        fnMdbRec(settingsCompanyID, dataRead1[3].ToString());
                    }
                    if (v == 0)
                    {
                        btnClearSettings_Click(null, null);
                        //btnClearFileSettings_Click(null, null);
                    }


                    dataRead1.Dispose();
                    dataRead1.Close();
                }
                catch
                {
                    //btnClearSettings_Click(null, null);
                }
                sqlConnection2.Dispose();
                sqlConnection2.Close();
                Global.SelUpdate = true;
                Global.selUpdateId = settingsCompanyID;
                btnUpdSet.Visible = true;
            }
            else if (fileType == "text")
            {

                Global.filePrefix = "";
                Global.fileDateFormat = "";
                Global.fileSuffix = "";
                Global.fileTypeRead = "";
                Global.fileReadDate = "";
                cmbFileType.SelectedIndex = 0;

                btnFromSqlToOther.Enabled = false;
                btnFromSqlToOther.Visible = false;
                btnTextToOther.Visible = false;
                btnTextToOther.Enabled = true;
                btnMdbToOther.Enabled = false;
                btnMdbToOther.Visible = false;
                SQLFileSettingsToolStripMenuItem.Enabled = false;
                textFileSettingsToolStripMenuItem.Enabled = true;
                mDBFileSettingsToolStripMenuItem.Enabled = false;
                SQLFileSettingsToolStripMenuItem.Visible = true;
                textFileSettingsToolStripMenuItem.Visible = true;
                mDBFileSettingsToolStripMenuItem.Visible = true;
                textFileSettingsToolStripMenuItem_Click(null, null);
                //tabFileSettings.Show();
                //sqlSettings.Hide();
                //tabSettings.Hide();
                btnClearSettings_Click(null, null);
                SqlCeConnection sqlConnection2 = new SqlCeConnection();
                sqlConnection2.ConnectionString = dataSourcePath;
                SqlCeCommand cmd1 = new SqlCeCommand();
                cmd1.CommandType = System.Data.CommandType.Text;
                cmd1.Connection = sqlConnection2;
                cmd1.CommandText = "SELECT * FROM TEXT_FILE_SETTINGS WHERE TEXT_COM_ID=" + settingsCompanyID.ToString();
                
                sqlConnection2.Open();
                SqlCeDataReader dataRead1;
                int v = 0;
                try
                {
                    dataRead1 = cmd1.ExecuteReader();
                    while (dataRead1.Read())
                    {
                        v++;

                        txtTextURL.Text = dataRead1[1].ToString();
                        lbServerURL.Text = dataRead1[1].ToString();
                        txtFilePath.Text = dataRead1[2].ToString();
                        txtFisFiePos.Text = dataRead1[3].ToString();
                        txtFisFieLen.Text = dataRead1[4].ToString();
                        txtSecFiePos.Text = dataRead1[5].ToString();
                        txtSecFieLen.Text = dataRead1[6].ToString();
                        txtThrFiePos.Text = dataRead1[7].ToString();
                        txtThrFieLen.Text = dataRead1[8].ToString();
                        txtFrthFiePos.Text = dataRead1[9].ToString();
                        txtFrthFieLen.Text = dataRead1[10].ToString();
                        txtStartRow.Text = dataRead1[11].ToString();

                        txtDateFormat.Text = dataRead1[12].ToString();
                        if (txtDateFormat.Text == "ddmmyy")
                            cmbDateFormat.SelectedIndex = 1;
                        else if (txtDateFormat.Text == "yymmdd")
                            cmbDateFormat.SelectedIndex = 2;
                        else if (txtDateFormat.Text == "ddmmyyyy")
                            cmbDateFormat.SelectedIndex = 3;//
                        else if (txtDateFormat.Text == "yyyymmdd")
                            cmbDateFormat.SelectedIndex = 4;//
                        else if (txtDateFormat.Text == "dd/mm/yyyy")
                            cmbDateFormat.SelectedIndex = 5;//
                        else if (txtDateFormat.Text == "yyyy/mm/dd")
                            cmbDateFormat.SelectedIndex = 6;//
                        else if (txtDateFormat.Text == "dd/mm/yy")
                            cmbDateFormat.SelectedIndex = 7;//
                        else if (txtDateFormat.Text == "yy/mm/dd")
                            cmbDateFormat.SelectedIndex = 8;//
                        else if (txtDateFormat.Text == "dd-mm-yyyy")
                            cmbDateFormat.SelectedIndex = 9;//
                        else if (txtDateFormat.Text == "yyyy-mm-dd")
                            cmbDateFormat.SelectedIndex = 10;//
                        else if (txtDateFormat.Text == "dd-mm-yy")
                            cmbDateFormat.SelectedIndex = 11;//
                        else if (txtDateFormat.Text == "yy-mm-dd")
                            cmbDateFormat.SelectedIndex = 12;//
                        else if (txtDateFormat.Text == "d/m/yyyy")
                            cmbDateFormat.SelectedIndex = 13;//
                        else if (txtDateFormat.Text == "yyyy/m/d")
                            cmbDateFormat.SelectedIndex = 14;//
                        else if (txtDateFormat.Text == "d/m/yy")
                            cmbDateFormat.SelectedIndex = 15;//
                        else if (txtDateFormat.Text == "yy/m/d")
                            cmbDateFormat.SelectedIndex = 16;//
                        else if (txtDateFormat.Text == "d-m-yyyy")
                            cmbDateFormat.SelectedIndex = 17;//
                        else if (txtDateFormat.Text == "yyyy-m-d")
                            cmbDateFormat.SelectedIndex = 18;//
                        else if (txtDateFormat.Text == "d-m-yy")
                            cmbDateFormat.SelectedIndex = 19;//
                        else if (txtDateFormat.Text == "yy-m-d")
                            cmbDateFormat.SelectedIndex = 20;//
                        else if (txtDateFormat.Text == "d/mm/yyyy")
                            cmbDateFormat.SelectedIndex = 21;//
                        else if (txtDateFormat.Text == "yyyy/mm/d")
                            cmbDateFormat.SelectedIndex = 22;//
                        else if (txtDateFormat.Text == "d/mm/yy")
                            cmbDateFormat.SelectedIndex = 23;//
                        else if (txtDateFormat.Text == "yy/mm/d")
                            cmbDateFormat.SelectedIndex = 24;//
                        else if (txtDateFormat.Text == "d-mm-yyyy")
                            cmbDateFormat.SelectedIndex = 25;//
                        else if (txtDateFormat.Text == "yyyy-mm-d")
                            cmbDateFormat.SelectedIndex = 26;//
                        else if (txtDateFormat.Text == "d-mm-yy")
                            cmbDateFormat.SelectedIndex = 27;//
                        else if (txtDateFormat.Text == "yy-mm-d")
                            cmbDateFormat.SelectedIndex = 28;
                        else
                        {
                            cmbDateFormat.SelectedIndex = 29;
                            txtDateFormat.Text = dataRead1[12].ToString();
                        }

                        txtTimeFormat.Text = dataRead1[13].ToString();
                        if (txtTimeFormat.Text == "hhmm")
                            cmbTimeFormat.SelectedIndex = 1;
                        else if (txtTimeFormat.Text == "hh:mm")
                            cmbTimeFormat.SelectedIndex = 2;
                        else if (txtTimeFormat.Text == "hhmmss")
                            cmbTimeFormat.SelectedIndex = 3;
                        else if (txtTimeFormat.Text == "hh:mm:ss")
                            cmbTimeFormat.SelectedIndex = 4;
                        else if (txtTimeFormat.Text == "h:mm:ss")
                            cmbTimeFormat.SelectedIndex = 5;
                        else if (txtTimeFormat.Text == "h:mm")
                            cmbTimeFormat.SelectedIndex = 6;
                        else
                        {
                            cmbTimeFormat.SelectedIndex = 3;
                            txtTimeFormat.Text = dataRead1[13].ToString();
                        }


                        txtAutoTimeInt.Text = dataRead1[14].ToString();
                        string str = dataRead1[15].ToString();
                        if (str == "emp")
                            rdbnEmpId.Checked = true;
                        else if (str == "ref")
                            rdbnRefId.Checked = true;
                        else if (str == "card")
                            rdbnCardId.Checked = true;

                        int rec = fnTextRec(dataRead1[2].ToString());
                        rec = (rec - Convert.ToInt32(dataRead1[11])) + 1;
                        lbMechRec.Text = rec.ToString();
                         
                    }
                    if (v == 0)
                    {
                        //btnClearSettings_Click(null, null);
                        btnClearFileSettings_Click(null, null);
                    }


                    dataRead1.Dispose();
                    dataRead1.Close();
                }
                catch
                {
                    //btnClearSettings_Click(null, null);
                }
                if (v != 0)
                {
                    btnUpSett.Visible = true;
                    cmd1.CommandText = "SELECT * FROM TEXT_FILE_NAME_FORMAT WHERE TEXT_NAME_FORMAT_ID=" + settingsCompanyID.ToString();
                    SqlCeDataReader dataRead2;
                    try
                    {
                        dataRead2 = cmd1.ExecuteReader();
                        //int v = 0;
                        while (dataRead2.Read())
                        {

                            Global.filename = dataRead2[7].ToString();
                            Global.fileDateFormat = dataRead2[2].ToString();
                            
                            Global.filePrefix = dataRead2[1].ToString();
                            Global.fileSuffix = dataRead2[3].ToString();
                            Global.fileTypeRead = dataRead2[4].ToString();
                            Global.fileReadDate = dataRead2[5].ToString();
                            string fileext = dataRead2[6].ToString();
                            if (fileext == ".txt")
                                cmbFileType.SelectedIndex = 0;
                            else if (fileext == ".log")
                                cmbFileType.SelectedIndex = 1;
                            else if (fileext == ".cap")
                                cmbFileType.SelectedIndex = 2;
                            else if (fileext == ".ttt")
                                cmbFileType.SelectedIndex = 3;
                            else if (fileext == ".dat")
                                cmbFileType.SelectedIndex = 4;
                        }
                    }
                    catch { }
                }
                sqlConnection2.Dispose();
                sqlConnection2.Close();

            }
            else if (fileType == "sql")
            {
                btnFromSqlToOther.Enabled = true;
                btnFromSqlToOther.Visible = false;
                btnTextToOther.Enabled = false;
                btnTextToOther.Visible = false;
                btnMdbToOther.Enabled = false;
                btnMdbToOther.Visible = false;
                SQLFileSettingsToolStripMenuItem.Enabled = true;
                textFileSettingsToolStripMenuItem.Enabled = false;
                mDBFileSettingsToolStripMenuItem.Enabled = false;
                SQLFileSettingsToolStripMenuItem.Visible = true;
                textFileSettingsToolStripMenuItem.Visible = true;
                mDBFileSettingsToolStripMenuItem.Visible = true;
                sQLFileSettingsToolStripMenuItem_Click(null, null);
                //tabFileSettings.Hide();
                //sqlSettings.Show();
                //tabSettings.Hide();
                Global.SelUpdate = false;
                SqlCeConnection sqlConnection2 = new SqlCeConnection();
                sqlConnection2.ConnectionString = dataSourcePath;
                SqlCeCommand cmd1 = new SqlCeCommand();
                cmd1.CommandType = System.Data.CommandType.Text;
                cmd1.CommandText = "SELECT * FROM SQL_DATA_SETTINGS WHERE SQL_COMP_ID=" + settingsCompanyID.ToString();
                cmd1.Connection = sqlConnection2;

                sqlConnection2.Open();
                SqlCeDataReader dataRead1;
                int v = 0;
                try
                {
                    dataRead1 = cmd1.ExecuteReader();
                    while (dataRead1.Read())
                    {
                        v++;
                       txtSqlUrl.Text = dataRead1[1].ToString();
                        txtSqlTime.Text = dataRead1[2].ToString();
                        txtSqlFieLen.Text = dataRead1[3].ToString();
                        string preSuf = dataRead1[4].ToString();
                        if (preSuf == "pre")
                            rdbnSqlPre.Checked = true;
                        else if (preSuf == "suf")
                            rdbnSqlSuf.Checked = true;

                        //string refCardEmpVal = dataRead1[6].ToString();
                        //if (refCardEmpVal == "yes")
                        //    chkRefCardEmp.Checked = true;

                        string useRefEmpVal = dataRead1[5].ToString();
                        if (useRefEmpVal == "emp")
                            rdbnSqlEmp.Checked = true;
                        if (useRefEmpVal == "ref")
                            rdbnSqRef.Checked = true;
                        if (useRefEmpVal == "card")
                            rdbnSqlCard.Checked = true;
                        Global.SetSQLValues = "y";
                        //fnMdbRec(settingsCompanyID, dataRead1[3].ToString());
                    }
                    if (v == 0)
                    {
                        //btnClearSettings_Click(null, null);
                        //btnClearFileSettings_Click(null, null);
                        btnSQLClear_Click(null, null);
                    }
                    dataRead1.Dispose();
                    dataRead1.Close();
                    lbMechRec.Text = "";
                    lbDbRec.Text = "";
                    Application.DoEvents();
                }
                catch
                {
                    btnSQLClear_Click(null, null);
                }
                if (v == 1)
                {
                    SqlCeDataReader dataRead2;
                    cmd1.CommandText = "SELECT * FROM SQL_DATA_FIELD_SETTINGS WHERE SQL_TAB_COMP_ID=" + settingsCompanyID.ToString();
                    try
                    {
                        dataRead2 = cmd1.ExecuteReader();
                        int v1 = 0;
                        while (dataRead2.Read())
                        {
                            v1++;

                            //fnMdbRec(settingsCompanyID, dataRead1[3].ToString());
                            txtServer.Text = dataRead2[1].ToString();
                            txtDB.Text = dataRead2[2].ToString();
                            string auth = dataRead2[3].ToString();
                            if (auth == "Windows Authentication")
                                cmbDB.SelectedIndex = 0;
                            else if (auth == "SQL Server Authentication")
                            {
                                cmbDB.SelectedIndex = 1;
                                txtUser.Text = dataRead2[4].ToString();
                                txtPass.Text = dataRead2[5].ToString();
                            }
                        }
                        if (v1 == 0)
                        {
                            btnClearSettings_Click(null, null);
                            btnClearFileSettings_Click(null, null);
                            btnSQLClear_Click(null, null);
                        }
                        dataRead2.Dispose();
                        dataRead2.Close();
                    }
                    catch
                    {
                        btnSQLClear_Click(null, null);
                    }
                    Global.SelUpdate = true;
                    Global.selUpdateId = settingsCompanyID;
                }
                sqlConnection2.Dispose();
                sqlConnection2.Close();
            }
            else
            {
                btnFromSqlToOther.Enabled = false;
                btnFromSqlToOther.Visible = false;
                btnTextToOther.Enabled = false;
                btnTextToOther.Visible = false;
                btnMdbToOther.Enabled = false;
                btnMdbToOther.Visible = false;
                btnClearSettings_Click(null, null);
                btnClearFileSettings_Click(null, null);
                btnSQLClear_Click(null, null);
                SQLFileSettingsToolStripMenuItem.Visible = true;
                textFileSettingsToolStripMenuItem.Visible = true;
                mDBFileSettingsToolStripMenuItem.Visible = true;
                SQLFileSettingsToolStripMenuItem.Enabled = true;
                textFileSettingsToolStripMenuItem.Enabled = true;
                mDBFileSettingsToolStripMenuItem.Enabled = true;
                mDBFileSettingsToolStripMenuItem_Click(null, null);
                //iToolStripMenuItem_Click(null, null);
            }
            if (fnValidateURL(3))
            {
                string strConn = "server=" + strHost + ";database=" + strDB + ";uid=" + strUsr + ";pwd=" + strPWD;
                MySqlConnection myConn = new MySqlConnection(strConn);
                try
                {
                    MySqlCommand myCommand = myConn.CreateCommand();
                    myConn.Open();

                    myCommand.CommandText = "SELECT COUNT(*) FROM kal_logdetails";
                    MySqlDataReader SQLRD;
                    SQLRD = myCommand.ExecuteReader();
                    SQLRD.Read();
                    lbDbRec.Text = SQLRD[0].ToString();
                    SQLRD.Close();
                    myConn.Close();
                }
                catch (Exception ex)
                {
                }
            }
        }

        private int fnTextRec(string file)
        {
             StreamReader fileInfo = new StreamReader(file);
             string line = null;
             int rec = 0;
             while ((line = fileInfo.ReadLine()) != null)
             {
                 rec++;
             }
             return rec;
        }

        private void fnMdbRec(int id,string file)
        {
            string fileP = file;
            string filePwd = "";
            string ftab = "";
            int ind = 0;
            string connectionString = "";
            SqlCeConnection sqlConnection3 = new SqlCeConnection();
            sqlConnection3.ConnectionString = dataSourcePath;
            SqlCeCommand cmd3 = new SqlCeCommand();
            cmd3.CommandType = System.Data.CommandType.Text;
            cmd3.CommandText = "SELECT * FROM MDB_FILE_FIELD_SETTINGS WHERE FIE_MDB_ID=" + id.ToString();
            cmd3.Connection = sqlConnection3;

            sqlConnection3.Open();
            SqlCeDataReader dataRead3;
            try
            {
                dataRead3 = cmd3.ExecuteReader();
                while (dataRead3.Read())
                {
                    filePwd = dataRead3[1].ToString();
                    ftab = dataRead3[2].ToString();
                    ind = Convert.ToInt32(dataRead3[12]);
                }

                dataRead3.Close();
            }
            catch { }
            sqlConnection3.Dispose();
            sqlConnection3.Close();


            if (filePwd != "")
            {
                if (Path.GetExtension(fileP).ToString().ToUpper() != ".ACCDB")
                    connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileP + ";Jet OLEDB:Database Password=" + filePwd + ";";
                else
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileP + ";Jet OLEDB:Database Password=" + filePwd;
            }
            else
            {
                if (Path.GetExtension(fileP).ToString().ToUpper() != ".ACCDB")
                    connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileP + ";";
                else
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileP + ";Persist Security Info=False;";
            }

            //  Open and read mdb file here
            System.Data.DataTable dataSet = new System.Data.DataTable();
            using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                connection.Open();
                System.Data.OleDb.OleDbCommand Command = new System.Data.OleDb.OleDbCommand("SELECT * FROM " + ftab, connection);
                using (System.Data.OleDb.OleDbDataAdapter dataAdapter = new System.Data.OleDb.OleDbDataAdapter(Command))
                {
                    dataAdapter.Fill(dataSet);
                }
            }
            //label15.Text = dataSet.Rows.Count.ToString(); 
            lbMechRec.Text = ((dataSet.Rows.Count)-ind).ToString(); 

            //foreach (DataRow row in dataSet.Rows)
            //{
            //    if (StartIndex == ind)
            //    {
            //        empVal = row[empF].ToString();
            //    }
            //}
        }

        private int fnGetTASettingsRow(int i)
        {
            int v = 0;
            SqlCeConnection sqlConnection2 = new SqlCeConnection();
            sqlConnection2.ConnectionString = dataSourcePath;
            SqlCeCommand cmd1 = new SqlCeCommand();
            cmd1.CommandType = System.Data.CommandType.Text;
            if (i == 1)
            {
                cmd1.CommandText = "SELECT * FROM MDB_FILE_SETTINGS WHERE MDB_COMP_ID=" + settingsCompanyID.ToString();
            }
            else if (i == 2)
            {
                cmd1.CommandText = "SELECT * FROM SQL_DATA_SETTINGS WHERE SQL_COMP_ID=" + settingsCompanyID.ToString();
            }
            cmd1.Connection = sqlConnection2;

            sqlConnection2.Open();
            SqlCeDataReader dataRead1;
            try
            {
                dataRead1 = cmd1.ExecuteReader();
                while (dataRead1.Read())
                {
                    v++;
                    if (i == 1)
                        mdbId = Convert.ToInt32(dataRead1[0]);
                    else if (i == 2)
                        sqlId = Convert.ToInt32(dataRead1[0]);
                }
                dataRead1.Close();
            }
            catch { }
            sqlConnection2.Dispose();
            sqlConnection2.Close();
            return v;
        }

        private int fnGetTextSettingsRow()
        {
            int v = 0;
            SqlCeConnection sqlConnection2 = new SqlCeConnection();
            sqlConnection2.ConnectionString = dataSourcePath;
            SqlCeCommand cmd1 = new SqlCeCommand();
            cmd1.CommandType = System.Data.CommandType.Text;
            cmd1.CommandText = "SELECT * FROM TEXT_FILE_SETTINGS WHERE TEXT_COM_ID=" + settingsCompanyID.ToString();
            cmd1.Connection = sqlConnection2;

            sqlConnection2.Open();
            SqlCeDataReader dataRead1;
            try
            {
                dataRead1 = cmd1.ExecuteReader();
                while (dataRead1.Read())
                {
                    v++;
                    //mdbId = Convert.ToInt32(dataRead1[0]);
                }
                dataRead1.Close();
            }
            catch { }
            sqlConnection2.Dispose();
            sqlConnection2.Close();
            return v;
        }

        //not used
        private void button1_Click_1(object sender, EventArgs e)
        {
            //SqlCeConnection sqlConnection1 = new SqlCeConnection();
            //sqlConnection1.ConnectionString = dataSourcePath;
            //SqlCeCommand cmd = new SqlCeCommand();
            //cmd.CommandType = System.Data.CommandType.Text;
            //cmd.CommandText = "DELETE FROM TASettings WHERE COMP_ID='" + settingsCompanyID.ToString() + "'";
            //cmd.Connection = sqlConnection1;

            //sqlConnection1.Open();
            //cmd.ExecuteNonQuery();
            //sqlConnection1.Dispose();
            //sqlConnection1.Close();
        }

        private void btnfileselector_Click(object sender, EventArgs e)
        {
            fnfilebrowseText();
            if (txtFilePath.Text != "")
            {
                try
                {
                    StreamReader file = new StreamReader(txtFilePath.Text);
                }
                catch (UnauthorizedAccessException uae)
                {
                    lbFileError.Visible = true;
                    lbFileError.Text = "Give 'Read' permissions to selected file"; 
                }
            }
        }

        private void fnfilebrowse()
        {
            bool blnrppext;
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            OpenFileDialog fileBrowser = new OpenFileDialog();
            fileBrowser.Title = "Select mdb File";
            fileBrowser.Filter = "mdb File(*.mdb, *.accdb)|*.mdb;*.accdb";
            fileBrowser.DefaultExt = "*.mdb";
            fileBrowser.ShowDialog(this);
            if ((fileBrowser.FileName.Length == 0) && txtMdbFilePath.Text == "")
            {
                MessageBox.Show("Select .mdb or .accdb File","Error: File Selection");
            }
            //else if (Global.selUpdateId != 0 && Global.SelUpdate == true)
            //{

            //}
            if ((fileBrowser.FileName.Length > 0) && (fileBrowser.CheckFileExists))
            {
                txtMdbFilePath.Text = fileBrowser.FileName;
                string rppfilepath = txtMdbFilePath.Text;
                if (Path.GetExtension(rppfilepath).ToString().ToUpper() != ".MDB" && Path.GetExtension(rppfilepath).ToString().ToUpper() != ".ACCDB")
                {
                    blnrppext = false;
                    if (blnrppext == false)
                    {
                        MessageBox.Show("Select Correct MDB File", "Error: File Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                    }

                }
                else
                {
                    Global.mdbFilePath = txtMdbFilePath.Text;
                    frmMdbTableColumnsSelect frm = new frmMdbTableColumnsSelect();
                    frm.ShowDialog();
                }
            }
            Application.DoEvents();
            this.Cursor = Cursors.Default;
        }

        private void fnfilebrowseText()
        {
            bool blnrppext;
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            OpenFileDialog fileBrowser = new OpenFileDialog();
            fileBrowser.Title = "Select Text File";
            //fileBrowser.Filter = "Text File(*.TXT)|*.txt";
            fileBrowser.Filter = "Text Files(*.txt;*.log;*.cap;*.ttt;*.dat)|*.txt;*.log;*.cap;*.ttt;*.dat";
            //fileBrowser.DefaultExt = "*.txt";
            fileBrowser.ShowDialog(this);
            if ((fileBrowser.FileName.Length == 0) && txtFilePath.Text == "")
            {
                MessageBox.Show("Select (*.txt or *.log or *.cap or *.ttt ot *.dat) File","Error: File Selection");
            }
            if ((fileBrowser.FileName.Length > 0) && (fileBrowser.CheckFileExists))
            {
                txtFilePath.Text = fileBrowser.FileName;
                string rppfilepath = txtFilePath.Text;
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

        private void btnMdbFileSelecter_Click(object sender, EventArgs e)
        {
            lbError.Visible = false;
            Global.SetMdbValues = "";
            fnfilebrowse();
            //MessageBox.Show(Global.SetMdbValues);
            if (Global.SetMdbValues == "")
            {
                if (Global.selUpdateId != 0 && Global.SelUpdate == true)
                {
                }
                else
                {
                    txtMdbFilePath.Text = "";
                    lbError.Visible = true;
                    lbError.Text = "Select Mdb File";
                    btnMdbFileSelecter.Focus();
                }
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (chkForce.Checked == true && chkForce.Enabled == true)
                Global.forceRead = true;
            else
                Global.forceRead = false;
            Application.DoEvents();
            fromUpload = false;
            pnlImportShow.Enabled = false;
            panel8.Enabled = false;
            btnCreateCompany.Enabled = false;
            btnPauseAll_Click(null, null);
            btnUpload.Enabled = false;
            lbReadComp.Visible = false;
            treeView1.Enabled = false;
            Application.DoEvents();
            if (cmbComp.SelectedIndex == 0)
            {
                //fromUpdates = "";
                lbReadComp.Visible = true;
                lbReadComp.Text = "Select Company from the ComboBox";
                btnUpload.Enabled = true;
                pnlImportShow.Enabled = true;
                panel8.Enabled = true;
                btnCreateCompany.Enabled = true;
                treeView1.Enabled = true;
                cmbComp.Focus();
                return;
            }
            string filety = "";
            if (lbServerURL.Text != "")
            {
                SqlCeConnection sqlConnection1 = new SqlCeConnection();
                sqlConnection1.ConnectionString = dataSourcePath;
                SqlCeCommand cmd = new SqlCeCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM CompRegister WHERE CO_COMP_ID=" + Global.Indexrow.ToString();
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();
                SqlCeDataReader SQLRD;
                SQLRD = cmd.ExecuteReader();
                SQLRD.Read();
                filety = SQLRD[3].ToString();
                sqlConnection1.Dispose();
                sqlConnection1.Close();

                fnSetMdbtoNull();
                fnSetTexttoNull();
                fnSetSqltoNull();

                if (filety == "mdb")
                {
                    mdbComp[0] = Global.Indexrow;
                    txtComp[0] = 0;
                    SqlComp[0] = 0;
                }
                else if (filety == "text")
                {
                    txtComp[0] = Global.Indexrow;
                    mdbComp[0] = 0;
                    SqlComp[0] = 0;
                }
                else if (filety == "sql")
                {
                    SqlComp[0] = Global.Indexrow;
                    mdbComp[0] = 0;
                    txtComp[0] = 0;
                }
                lbReadComp.Visible = true;
                lbReadComp.Text = "Reading Company" + Global.Indexrow.ToString() + " Attendence records.";
                fromUpload = true;
                fnStartMdbTimers();
                Application.DoEvents();
            }
            else
            {
                lbrecindbtxt.Text = "Select Company";
            }

            btnUpload.Enabled = true;
            fromUpload = false;
            lbrecindbtxt.Text = "Read all records";
            Application.DoEvents();
            Thread.Sleep(2000);
            //frmManageTnA_Load(null, null);
            fnStartMdbCompTimers();
            fnStartTextCompTimers();
            fnStartSQLCompTimers();
            fnStartMdbTimers();
            Application.DoEvents();
            pnlImportShow.Enabled = true;
            panel8.Enabled = true;
            btnCreateCompany.Enabled = true;
            treeView1.Enabled = true;
        }

        private void fnSetMdbtoNull()
        {
            mdbComp[0] = 0;
            mdbComp[1] = 0;
            mdbComp[2] = 0;
            mdbComp[3] = 0;
            mdbComp[4] = 0;
            mdbComp[5] = 0;
            mdbComp[6] = 0;
            mdbComp[7] = 0;
            mdbComp[8] = 0;
        }
        
        private void fnSetTexttoNull()
        {
            txtComp[0] = 0;
            txtComp[1] = 0;
            txtComp[2] = 0;
            txtComp[3] = 0;
            txtComp[4] = 0;
            txtComp[5] = 0;
            txtComp[6] = 0;
            txtComp[7] = 0;
            txtComp[8] = 0;
            txtComp[9] = 0;
        }
       
        private void fnSetSqltoNull()
        {
            SqlComp[0] = 0;
            SqlComp[1] = 0;
            SqlComp[2] = 0;
            SqlComp[3] = 0;
            SqlComp[4] = 0;
            SqlComp[5] = 0;
            SqlComp[6] = 0;
            SqlComp[7] = 0;
            SqlComp[8] = 0;
        }
      
        #region Timer starting functions

        private void fnTmer()
        {
            string ci = "";
            int min = 0;
            int i = 0;

            //timer1
            try
            {
                while (timersDetails[i, 0] != "1")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersDetails[i, 3];
                comp1Index = i;
                comp1Type = timersDetails[i, 21];
                min = Convert.ToInt32(ci);
                TimeTimer1Flag = min - 1;
                now1 = DateTime.Now;
                groupBox1.Enabled = true;
                timer1.Enabled = true;
            }

            //timer2
            i = 0;
            try
            {
                while (timersDetails[i, 0] != "2")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersDetails[i, 3];
                comp2Index = i;
                comp2Type = timersDetails[i, 21];
                min = Convert.ToInt32(ci);
                TimeTimer2Flag = min - 1;
                now2 = DateTime.Now;
                groupBox2.Enabled = true;
                timer2.Enabled = true;
            }

            //timer3
            i = 0;
            try
            {
                while (timersDetails[i, 0] != "3")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersDetails[i, 3];
                comp3Index = i;
                comp3Type = timersDetails[i, 21];
                min = Convert.ToInt32(ci);
                TimeTimer3Flag = min - 1;
                now3 = DateTime.Now;
                groupBox3.Enabled = true;
                timer3.Enabled = true;
            }

            //timer4
            i = 0;
            try
            {
                while (timersDetails[i, 0] != "4")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersDetails[i, 3];
                comp4Index = i;
                comp4Type = timersDetails[i, 21];
                min = Convert.ToInt32(ci);
                TimeTimer4Flag = min - 1;
                now4 = DateTime.Now;
                groupBox4.Enabled = true;
                timer4.Enabled = true;
            }

            //timer5
            i = 0;
            try
            {
                while (timersDetails[i, 0] != "5")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersDetails[i, 3];
                comp5Index = i;
                comp5Type = timersDetails[i, 21];
                min = Convert.ToInt32(ci);
                TimeTimer5Flag = min - 1;
                now5 = DateTime.Now;
                groupBox5.Enabled = true;
                timer5.Enabled = true;
            }

            //timer6
            i = 0;
            try
            {
                while (timersDetails[i, 0] != "6")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersDetails[i, 3];
                comp6Index = i;
                comp6Type = timersDetails[i, 21];
                min = Convert.ToInt32(ci);
                TimeTimer6Flag = min - 1;
                now6 = DateTime.Now;
                groupBox6.Enabled = true;
                timer6.Enabled = true;
            }

            //timer7
            i = 0;
            try
            {
                while (timersDetails[i, 0] != "7")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersDetails[i, 3];
                comp7Index = i;
                comp7Type = timersDetails[i, 21];
                min = Convert.ToInt32(ci);
                TimeTimer7Flag = min - 1;
                now7 = DateTime.Now;
                groupBox7.Enabled = true;
                timer7.Enabled = true;
            }

            //timer8
            i = 0;
            try
            {
                while (timersDetails[i, 0] != "8")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersDetails[i, 3];
                comp8Index = i;
                comp8Type = timersDetails[i, 21];
                min = Convert.ToInt32(ci);
                TimeTimer8Flag = min - 1;
                now8 = DateTime.Now;
                groupBox8.Enabled = true;
                timer8.Enabled = true;
            }

            //timer9
            i = 0;
            try
            {
                while (timersDetails[i, 0] != "9")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersDetails[i, 3];
                comp9Index = i;
                comp9Type = timersDetails[i, 21];
                min = Convert.ToInt32(ci);
                TimeTimer9Flag = min - 1;
                now9 = DateTime.Now;
                groupBox9.Enabled = true;
                timer9.Enabled = true;
            }

            //timer10
            i = 0;
            try
            {
                while (timersDetails[i, 0] != "10")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersDetails[i, 3];
                comp10Index = i;
                comp10Type = timersDetails[i, 21];
                min = Convert.ToInt32(ci);
                TimeTimer10Flag = min - 1;
                now10 = DateTime.Now;
                groupBox10.Enabled = true;
                timer10.Enabled = true;
            }
        }

        private void fnSQLTmer()
        {
            string ci = "";
            int min = 0;
            int i = 0;

            //timer1
            try
            {
                while (timersSQLDetails[i, 0] != "1")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersSQLDetails[i, 2];
                comp1Index = i;
                comp1Type = timersSQLDetails[i, 22];
                min = Convert.ToInt32(ci);
                TimeTimer1Flag = min - 1;
                now1 = DateTime.Now;
                groupBox1.Enabled = true;
                timer1.Enabled = true;
            }

            //timer2
            i = 0;
            try
            {
                while (timersSQLDetails[i, 0] != "2")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersSQLDetails[i, 2];
                comp2Index = i;
                comp2Type = timersSQLDetails[i, 22];
                min = Convert.ToInt32(ci);
                TimeTimer2Flag = min - 1;
                now2 = DateTime.Now;
                groupBox2.Enabled = true;
                timer2.Enabled = true;
            }

            //timer3
            i = 0;
            try
            {
                while (timersSQLDetails[i, 0] != "3")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersSQLDetails[i, 2];
                comp3Index = i;
                comp3Type = timersSQLDetails[i, 22];
                min = Convert.ToInt32(ci);
                TimeTimer3Flag = min - 1;
                now3 = DateTime.Now;
                groupBox3.Enabled = true;
                timer3.Enabled = true;
            }

            //timer4
            i = 0;
            try
            {
                while (timersSQLDetails[i, 0] != "4")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersSQLDetails[i, 2];
                comp4Index = i;
                comp4Type = timersSQLDetails[i, 22];
                min = Convert.ToInt32(ci);
                TimeTimer4Flag = min - 1;
                now4 = DateTime.Now;
                groupBox4.Enabled = true;
                timer4.Enabled = true;
            }

            //timer5
            i = 0;
            try
            {
                while (timersSQLDetails[i, 0] != "5")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersSQLDetails[i, 2];
                comp5Index = i;
                comp5Type = timersSQLDetails[i, 22];
                min = Convert.ToInt32(ci);
                TimeTimer5Flag = min - 1;
                now5 = DateTime.Now;
                groupBox5.Enabled = true;
                timer5.Enabled = true;
            }

            //timer6
            i = 0;
            try
            {
                while (timersSQLDetails[i, 0] != "6")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersSQLDetails[i, 2];
                comp6Index = i;
                comp6Type = timersSQLDetails[i, 22];
                min = Convert.ToInt32(ci);
                TimeTimer6Flag = min - 1;
                now6 = DateTime.Now;
                groupBox6.Enabled = true;
                timer6.Enabled = true;
            }

            //timer7
            i = 0;
            try
            {
                while (timersSQLDetails[i, 0] != "7")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersSQLDetails[i, 2];
                comp7Index = i;
                comp7Type = timersSQLDetails[i, 22];
                min = Convert.ToInt32(ci);
                TimeTimer7Flag = min - 1;
                now7 = DateTime.Now;
                groupBox7.Enabled = true;
                timer7.Enabled = true;
            }

            //timer8
            i = 0;
            try
            {
                while (timersSQLDetails[i, 0] != "8")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersSQLDetails[i, 2];
                comp8Index = i;
                comp8Type = timersSQLDetails[i, 22];
                min = Convert.ToInt32(ci);
                TimeTimer8Flag = min - 1;
                now8 = DateTime.Now;
                groupBox8.Enabled = true;
                timer8.Enabled = true;
            }

            //timer9
            i = 0;
            try
            {
                while (timersSQLDetails[i, 0] != "9")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersSQLDetails[i, 2];
                comp9Index = i;
                comp9Type = timersSQLDetails[i, 22];
                min = Convert.ToInt32(ci);
                TimeTimer9Flag = min - 1;
                now9 = DateTime.Now;
                groupBox9.Enabled = true;
                timer9.Enabled = true;
            }

            //timer10
            i = 0;
            try
            {
                while (timersSQLDetails[i, 0] != "10")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersSQLDetails[i, 2];
                comp10Index = i;
                comp10Type = timersSQLDetails[i, 22];
                min = Convert.ToInt32(ci);
                TimeTimer10Flag = min - 1;
                now10 = DateTime.Now;
                groupBox10.Enabled = true;
                timer10.Enabled = true;
            }
        }

        private void fnTextTmer()
        {
            string ci = "";
            int min = 0;
            int i = 0;

            //timer1
            try
            {
                while (timersTextDetails[i, 0] != "1")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersTextDetails[i, 14];
                comp1Index = i;
                comp1Type = timersTextDetails[i, 16];
                min = Convert.ToInt32(ci);
                TimeTimer1Flag = min - 1;
                now1 = DateTime.Now;
                groupBox1.Enabled = true;
                timer1.Enabled = true;
            }

            //timer2
            i = 0;
            try
            {
                while (timersTextDetails[i, 0] != "2")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersTextDetails[i, 14];
                comp2Index = i;
                comp2Type = timersTextDetails[i, 16];
                min = Convert.ToInt32(ci);
                TimeTimer2Flag = min - 1;
                now2 = DateTime.Now;
                groupBox2.Enabled = true;
                timer2.Enabled = true;
            }

            //timer3
            i = 0;
            try
            {
                while (timersTextDetails[i, 0] != "3")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersTextDetails[i, 14];
                comp3Index = i;
                comp3Type = timersTextDetails[i, 16];
                min = Convert.ToInt32(ci);
                TimeTimer3Flag = min - 1;
                now3 = DateTime.Now;
                groupBox3.Enabled = true;
                timer3.Enabled = true;
            }

            //timer4
            i = 0;
            try
            {
                while (timersTextDetails[i, 0] != "4")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersTextDetails[i, 14];
                comp4Index = i;
                comp4Type = timersTextDetails[i, 16];
                min = Convert.ToInt32(ci);
                TimeTimer4Flag = min - 1;
                now4 = DateTime.Now;
                groupBox4.Enabled = true;
                timer4.Enabled = true;
            }

            //timer5
            i = 0;
            try
            {
                while (timersTextDetails[i, 0] != "5")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersTextDetails[i, 14];
                comp5Index = i;
                comp5Type = timersTextDetails[i, 16];
                min = Convert.ToInt32(ci);
                TimeTimer5Flag = min - 1;
                now5 = DateTime.Now;
                groupBox5.Enabled = true;
                timer5.Enabled = true;
            }

            //timer6
            i = 0;
            try
            {
                while (timersTextDetails[i, 0] != "6")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersTextDetails[i, 14];
                comp6Index = i;
                comp6Type = timersTextDetails[i, 16];
                min = Convert.ToInt32(ci);
                TimeTimer6Flag = min - 1;
                now6 = DateTime.Now;
                groupBox6.Enabled = true;
                timer6.Enabled = true;
            }

            //timer7
            i = 0;
            try
            {
                while (timersTextDetails[i, 0] != "7")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersTextDetails[i, 14];
                comp7Index = i;
                comp7Type = timersTextDetails[i, 16];
                min = Convert.ToInt32(ci);
                TimeTimer7Flag = min - 1;
                now7 = DateTime.Now;
                groupBox7.Enabled = true;
                timer7.Enabled = true;
            }

            //timer8
            i = 0;
            try
            {
                while (timersTextDetails[i, 0] != "8")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersTextDetails[i, 14];
                comp8Index = i;
                comp8Type = timersTextDetails[i, 16];
                min = Convert.ToInt32(ci);
                TimeTimer8Flag = min - 1;
                now8 = DateTime.Now;
                groupBox8.Enabled = true;
                timer8.Enabled = true;
            }

            //timer9
            i = 0;
            try
            {
                while (timersTextDetails[i, 0] != "9")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersTextDetails[i, 14];
                comp9Index = i;
                comp9Type = timersTextDetails[i, 16];
                min = Convert.ToInt32(ci);
                TimeTimer9Flag = min - 1;
                now9 = DateTime.Now;
                groupBox9.Enabled = true;
                timer9.Enabled = true;
            }

            //timer10
            i = 0;
            try
            {
                while (timersTextDetails[i, 0] != "10")
                {
                    i++;
                }
            }
            catch { }
            if (i != 10)
            {
                ci = timersTextDetails[i, 14];
                comp10Index = i;
                comp10Type = timersTextDetails[i, 16];
                min = Convert.ToInt32(ci);
                TimeTimer10Flag = min - 1;
                now10 = DateTime.Now;
                groupBox10.Enabled = true;
                timer10.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime current = DateTime.Now;

            TimeSpan elapsed = current - now1;

            if ((TimeTimer1Flag + 1) == elapsed.Minutes)
            {
                timer1.Stop();
                fnStartReadData(1, comp1Type, comp1Index);
                now1 = DateTime.Now;
                timer1.Enabled = true;
                timer1.Start();
            }
            else
            {
                try
                {
                    lbTimer1.Text = ((TimeTimer1Flag) - elapsed.Minutes) + " : " + (59 - (elapsed.Seconds));
                }
                catch (StackOverflowException ee)
                {

                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            DateTime current = DateTime.Now;

            TimeSpan elapsed = current - now2;

            if ((TimeTimer2Flag + 1) == elapsed.Minutes)
            {
                timer2.Stop();
                fnStartReadData(2, comp2Type, comp2Index);
                now2 = DateTime.Now;
                timer2.Enabled = true;
                timer2.Start();
            }
            else
            {
                try
                {
                    lbTimer2.Text = ((TimeTimer2Flag) - elapsed.Minutes) + " : " + (59 - (elapsed.Seconds));
                }
                catch (StackOverflowException ee)
                {

                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            DateTime current = DateTime.Now;

            TimeSpan elapsed = current - now3;

            if ((TimeTimer3Flag + 1) == elapsed.Minutes)
            {
                timer3.Stop();
                fnStartReadData(3, comp3Type, comp3Index);
                now3 = DateTime.Now;
                timer3.Enabled = true;
                timer3.Start();
            }
            else
            {
                try
                {
                    lbTimer3.Text = ((TimeTimer3Flag) - elapsed.Minutes) + " : " + (59 - (elapsed.Seconds));
                }
                catch (StackOverflowException ee)
                {

                }
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            DateTime current = DateTime.Now;

            TimeSpan elapsed = current - now4;

            if ((TimeTimer4Flag + 1) == elapsed.Minutes)
            {
                timer4.Stop();
                fnStartReadData(4, comp4Type, comp4Index);
                now4 = DateTime.Now;
                timer4.Enabled = true;
                timer4.Start();
            }
            else
            {
                try
                {
                    lbTimer4.Text = ((TimeTimer4Flag) - elapsed.Minutes) + " : " + (59 - (elapsed.Seconds));
                }
                catch (StackOverflowException ee)
                {

                }
            }
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            DateTime current = DateTime.Now;

            TimeSpan elapsed = current - now5;

            if ((TimeTimer5Flag + 1) == elapsed.Minutes)
            {
                timer5.Stop();
                fnStartReadData(5, comp5Type, comp5Index);
                now5 = DateTime.Now;
                timer5.Enabled = true;
                timer5.Start();
            }
            else
            {
                try
                {
                    lbTimer5.Text = ((TimeTimer5Flag) - elapsed.Minutes) + " : " + (59 - (elapsed.Seconds));
                }
                catch (StackOverflowException ee)
                {

                }
            }
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            DateTime current = DateTime.Now;

            TimeSpan elapsed = current - now6;

            if ((TimeTimer6Flag + 1) == elapsed.Minutes)
            {
                timer6.Stop();
                fnStartReadData(6, comp6Type, comp6Index);
                now6 = DateTime.Now;
                timer6.Enabled = true;
                timer6.Start();
            }
            else
            {
                try
                {
                    lbTimer6.Text = ((TimeTimer6Flag) - elapsed.Minutes) + " : " + (59 - (elapsed.Seconds));
                }
                catch (StackOverflowException ee)
                {

                }
            }
        }

        private void timer7_Tick(object sender, EventArgs e)
        {
            DateTime current = DateTime.Now;

            TimeSpan elapsed = current - now7;

            if ((TimeTimer7Flag + 1) == elapsed.Minutes)
            {
                timer7.Stop();
                fnStartReadData(7, comp7Type, comp7Index);
                now7 = DateTime.Now;
                timer7.Enabled = true;
                timer7.Start();
            }
            else
            {
                try
                {
                    lbTimer7.Text = ((TimeTimer7Flag) - elapsed.Minutes) + " : " + (59 - (elapsed.Seconds));
                }
                catch (StackOverflowException ee)
                {

                }
            }
        }

        private void timer8_Tick(object sender, EventArgs e)
        {
            DateTime current = DateTime.Now;

            TimeSpan elapsed = current - now8;

            if ((TimeTimer8Flag + 1) == elapsed.Minutes)
            {
                timer8.Stop();
                fnStartReadData(8, comp8Type, comp8Index);
                now8 = DateTime.Now;
                timer8.Enabled = true;
                timer8.Start();
            }
            else
            {
                try
                {
                    lbTimer8.Text = ((TimeTimer8Flag) - elapsed.Minutes) + " : " + (59 - (elapsed.Seconds));
                }
                catch (StackOverflowException ee)
                {

                }
            }
        }

        private void timer9_Tick(object sender, EventArgs e)
        {
            DateTime current = DateTime.Now;

            TimeSpan elapsed = current - now9;

            if ((TimeTimer9Flag + 1) == elapsed.Minutes)
            {
                timer9.Stop();
                fnStartReadData(9, comp9Type, comp9Index);
                now9 = DateTime.Now;
                timer9.Enabled = true;
                timer9.Start();
            }
            else
            {
                try
                {
                    lbTimer9.Text = ((TimeTimer9Flag) - elapsed.Minutes) + " : " + (59 - (elapsed.Seconds));
                }
                catch (StackOverflowException ee)
                {

                }
            }
        }

        private void timer10_Tick(object sender, EventArgs e)
        {
            DateTime current = DateTime.Now;

            TimeSpan elapsed = current - now10;

            if ((TimeTimer10Flag + 1) == elapsed.Minutes)
            {
                timer10.Stop();
                fnStartReadData(10, comp10Type, comp10Index);
                now10 = DateTime.Now;
                timer10.Enabled = true;
                timer10.Start();
            }
            else
            {
                try
                {
                    lbTimer10.Text = ((TimeTimer10Flag) - elapsed.Minutes) + " : " + (59 - (elapsed.Seconds));
                }
                catch (StackOverflowException ee)
                {

                }
            }
        }

        #endregion

        private void fnStartReadData(int compId,string compType,int compIndex)
        {
            lbReadComp.Visible = true;
            {
                lbReadComp.Text = "Reading Attendence records of Company(Id) : " + compId.ToString() + ".";
                pgbrUpload.Value = 0;
            }

            pnlImportShow.Enabled = false;
            panel8.Enabled = false;
            btnCreateCompany.Enabled = false;
            //btnPauseAll_Click(null, null);
            btnUpload.Enabled = false;
            //lbReadComp.Visible = false;
            treeView1.Enabled = false;
            Application.DoEvents();

            if (compType == "mdb")
            {
                int comId = Convert.ToInt32(timersDetails[compIndex, 0]);
                string stDate = timersDetails[compIndex, 8];

                string cUrl = timersDetails[compIndex, 1];
                string fPath = timersDetails[compIndex, 2];
                string fpwd = timersDetails[compIndex, 9];
                string tb = timersDetails[compIndex, 10];
                string empF = timersDetails[compIndex, 11];
                string devI = timersDetails[compIndex, 12];

                string refEmp = timersDetails[compIndex, 4];
                string cardEmp = timersDetails[compIndex, 5];
                string posSuf = timersDetails[compIndex, 6];
                int StartIndex = Convert.ToInt32(timersDetails[compIndex, 20]);

                int empLen = Convert.ToInt32(timersDetails[compIndex, 7]);

                //to store date time values
                string Date = "";
                string In_Out = "";
                string In = "";
                string Out = "";
                string empVal = "";
                int devVal = 0;
                string devValue = "";

                string DateIn = "";
                string DateOut = "";

                int DateTypeCase = 0;

                string connectionString = "";
                if (fpwd != "")
                {
                    if (Path.GetExtension(fPath).ToString().ToUpper() != ".ACCDB")
                        connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fPath + ";Jet OLEDB:Database Password=" + fpwd + ";";
                    else
                        connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fPath + ";Jet OLEDB:Database Password=" + fpwd;
                }
                else
                {
                    if (Path.GetExtension(fPath).ToString().ToUpper() != ".ACCDB")
                        connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fPath + ";";
                    else
                        connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fPath + ";Persist Security Info=False;";
                }

                //  Open and read mdb file here
                System.Data.DataTable dataSet = new System.Data.DataTable();
                string que = "";
                string fieldname = "";
                if (Global.forceRead == true)
                {
                    if (timersDetails[compIndex, 13] != "")
                        fieldname = timersDetails[compIndex, 13];
                    else if (timersDetails[compIndex, 14] != "")
                        fieldname = timersDetails[compIndex, 14];
                    else if (timersDetails[compIndex, 16] != "")
                        fieldname = timersDetails[compIndex, 16];

                    que = "SELECT * FROM " + tb + " WHERE " + fieldname + " >= #" + Convert.ToDateTime(dtpForcedate.Text).ToString("yyyy-MM-dd")+"#";
                }
                else
                    que="SELECT * FROM " + tb;
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connectionString))
                {
                    connection.Open();
                    System.Data.OleDb.OleDbCommand Command = new System.Data.OleDb.OleDbCommand(que, connection);
                    using (System.Data.OleDb.OleDbDataAdapter dataAdapter = new System.Data.OleDb.OleDbDataAdapter(Command))
                    {
                        dataAdapter.Fill(dataSet);
                    }
                }
                int ind = 0;
                countRows = 1;
                int totalRec = dataSet.Rows.Count;
                if (fromUpload == true)
                    lbReadComp.Text = "Started reading " + totalRec.ToString() + " records";

                fnOnlineConnectVariables(cUrl);
                foreach (DataRow row in dataSet.Rows)
                {
                    //if (fromUpload == true)
                    //{
                        pgbrUpload.Value = ((countRows * 100) / totalRec);
                        lbReadComp.Text = (pgbrUpload.Value).ToString()+"%\n\nReading " + countRows.ToString() + " record of " + totalRec.ToString() + " records";
                    //}
                        if (StartIndex == ind || Global.forceRead == true)
                    {
                        empVal = row[empF].ToString();

                        //prefix or suffix
                        if (posSuf == "pre")
                        {
                            //value of empLen
                            empVal = empVal.Substring(0, empLen);
                        }
                        else if (posSuf == "suf")
                        {
                            empVal = empVal.Substring((empVal.Length) - empLen);
                        }
                        
                        if (refEmp == "yes")
                        {
                            //select from  mas_employee   //cUrl
                            empVal = fnGetRefCardEmp(cUrl, "ref", empVal);
                        }

                        if (cardEmp == "yes")
                        {
                            //select from mas_users
                            empVal = fnGetRefCardEmp(cUrl, "card", empVal);
                        }

                        if (cardEmp == "no" && refEmp == "no")// 
                        {
                            empVal = fnGetRefCardEmp(cUrl, "emp", empVal);
                        }
                        if (empVal != "")
                        {
                            try
                            {
                                //devVal = Convert.ToInt32(row[devI]);
                               devValue = row[devI].ToString();
                            }
                            catch {
                            }
                            if (devValue == "")
                                devValue = "0";

                            if (timersDetails[compIndex, 13] != "")
                            {

                                Date = Convert.ToDateTime(row[timersDetails[compIndex, 13]]).ToString("yyyy-MM-dd");
                                In_Out = Convert.ToDateTime(row[timersDetails[compIndex, 13]]).ToString("HH:mm:ss");
                                //string dateTime = row[timersDetails[compIndex, 13]].ToString();

                                //string[] dtf = dateTime.Split();
                                //Date = dtf[0];
                                //In_Out = dtf[1];
                                DateTypeCase = 1;
                                //get Date and in_out----------------------------------------------------------->1 record
                                In = "";
                                Out = "";
                            }
                            else if (timersDetails[compIndex, 14] != "" && timersDetails[compIndex, 15] != "")
                            {
                                //string dateTimeIn = row[timersDetails[compIndex, 14]].ToString();
                                //string[] dif = dateTimeIn.Split();
                                //DateIn = dif[0];
                                //In = dif[1];

                                //string dateTimeOut = row[timersDetails[compIndex, 15]].ToString();
                                //string[] dof = dateTimeOut.Split();
                                //DateOut = dof[0];
                                //Out = dof[1];

                                DateIn = Convert.ToDateTime(row[timersDetails[compIndex, 14]]).ToString("yyyy-MM-dd");
                                DateOut = Convert.ToDateTime(row[timersDetails[compIndex, 15]]).ToString("yyyy-MM-dd");
                                In = Convert.ToDateTime(row[timersDetails[compIndex, 14]]).ToString("HH:mm:ss");
                                Out = Convert.ToDateTime(row[timersDetails[compIndex, 15]]).ToString("HH:mm:ss");

                                DateTypeCase = 2;
                                //date, time_in and time_out---------------------------------------------------->2 records
                                In_Out = "";
                            }
                            else if (timersDetails[compIndex, 17] != "")
                            {
                                //Date = row[timersDetails[compIndex, 16]].ToString();
                                //string[] dt = Date.Split(' ');
                                //if (dt.Length > 1)
                                //{
                                //    Date = dt[0];
                                //}
                                //In_Out = row[timersDetails[compIndex, 17]].ToString();
                                //string[] tt = In_Out.Split(' ');
                                //if (tt.Length >= 2)
                                //{
                                //    In_Out = tt[1];
                                //}

                                Date = Convert.ToDateTime(row[timersDetails[compIndex, 16]]).ToString("yyyy-MM-dd");
                                In_Out = Convert.ToDateTime(row[timersDetails[compIndex, 17]]).ToString("HH:mm:ss");
                                //Date ,In_out------------------------------------------------------------------>1 record
                                DateTypeCase = 3;
                                 In = "";
                                Out = "";
                            }
                            else if (timersDetails[compIndex, 18] != "" && timersDetails[compIndex, 19] != "")
                            {
                                //Date = row[timersDetails[compIndex, 16]].ToString();
                                //string[] dt1 = Date.Split(' ');
                                //if (dt1.Length > 1)
                                //{
                                //    Date = dt1[0];
                                //}
                                //In = row[timersDetails[compIndex, 18]].ToString();

                                //string[] tt1 = In.Split(' ');
                                //if (tt1.Length >= 2)
                                //{
                                //    In = tt1[1];
                                //}
                                //Out = row[timersDetails[compIndex, 19]].ToString();
                                //string[] tt2 = Out.Split(' ');
                                //if (tt2.Length >= 2)
                                //{
                                //    Out = tt2[1];
                                //}

                                Date = Convert.ToDateTime(row[timersDetails[compIndex, 16]]).ToString("yyyy-MM-dd");
                                In = Convert.ToDateTime(row[timersDetails[compIndex, 18]]).ToString("HH:mm:ss");
                                Out = Convert.ToDateTime(row[timersDetails[compIndex, 19]]).ToString("HH:mm:ss");
                                DateTypeCase = 4;
                                //date , In, Out---------------------------------------------------------------->2 records
                                In_Out = "";
                            }
                            /*
                            try
                            {
                                string[] dat = Date.Split('/');
                                if (dat.Length == 3)
                                {
                                    // MessageBox.Show("Date : "+Date+"  "+dat[2] + "-" + dat[1] + "-" + dat[0]);
                                    Date = dat[2] + "-" + dat[1] + "-" + dat[0];
                                    // MessageBox.Show("Date : " + Date);
                                }
                                if (dat.Length <= 1)
                                {
                                    dat = Date.Split('-');
                                    if (dat.Length == 3)
                                    {
                                        if (dat[0].Length == 4)
                                        {
                                            Date = dat[0] + "-" + dat[1] + "-" + dat[2];
                                        }
                                        else
                                        {
                                            Date = dat[2] + "-" + dat[1] + "-" + dat[0];
                                        }
                                    }
                                }
                            }
                            catch (Exception ee)
                            {
                                //MessageBox.Show("Error1 " + ee.Message);
                            }
                            */
                            //save in kal_log details
                            try
                            {
                                string strConn = "server=" + strHost + ";database=" + strDB + ";uid=" + strUsr + ";pwd=" + strPWD;
                                MySqlConnection myConn = new MySqlConnection(strConn);
                                MySqlCommand myCommand = myConn.CreateCommand();
                                myConn.Open();
                                if (DateTypeCase == 1 || DateTypeCase == 3)
                                {
                                    if (fnCheckDuplicate(empVal, Date, In_Out, devValue))
                                    {
                                        myCommand.CommandText = "insert into `kal_logdetails` (EMPID, LOGDATE, LOGTIME, DEVICE_ID) values('" +
                                                    empVal + "', '" +
                                                    Date + "', '" +
                                                    In_Out + "', " +
                                                    devValue + ")";
                                        myCommand.ExecuteNonQuery();
                                    }
                                }
                                else if (DateTypeCase == 2)
                                {
                                    if (fnCheckDuplicate(empVal, DateIn, In, devValue))
                                    {
                                        myCommand.CommandText = "insert into `kal_logdetails` (EMPID, LOGDATE, LOGTIME, DEVICE_ID) values('" +
                                                    empVal + "', '" +
                                                    DateIn + "', '" +
                                                    In + "', " +
                                                    devValue + ")";
                                        myCommand.ExecuteNonQuery();
                                    }

                                    if (fnCheckDuplicate(empVal, DateOut, Out, devValue))
                                    {
                                        myCommand.CommandText = "insert into `kal_logdetails` (EMPID, LOGDATE, LOGTIME, DEVICE_ID) values('" +
                                                    empVal + "', '" +
                                                    DateOut + "', '" +
                                                    Out + "', " +
                                                    devValue + ")";
                                        myCommand.ExecuteNonQuery();
                                    }
                                }
                                else if (DateTypeCase == 4)
                                {
                                    if (fnCheckDuplicate(empVal, Date, In, devValue))
                                    {
                                        myCommand.CommandText = "insert into `kal_logdetails` (EMPID, LOGDATE, LOGTIME, DEVICE_ID) values('" +
                                                    empVal + "', '" +
                                                    Date + "', '" +
                                                    In + "', " +
                                                    devValue + ")";
                                        myCommand.ExecuteNonQuery();
                                    }
                                    if (fnCheckDuplicate(empVal, Date, Out, devValue))
                                    {
                                        myCommand.CommandText = "insert into `kal_logdetails` (EMPID, LOGDATE, LOGTIME, DEVICE_ID) values('" +
                                                    empVal + "', '" +
                                                    Date + "', '" +
                                                    Out + "', " +
                                                    devValue + ")";
                                        myCommand.ExecuteNonQuery();
                                    }
                                }

                                myCommand.CommandText = "SELECT COUNT(*) FROM `kal_logdetails`";
                                lbDbRec.Text = (Convert.ToInt32(myCommand.ExecuteScalar())).ToString();
                                lbDbRec.Update();
                                myConn.Close();
                                Application.DoEvents();
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show("Error 2 : " + ex.Message);
                            }
                        }

                        if (Global.forceRead != true)
                        {
                            ind++;
                            StartIndex++;
                            timersDetails[compIndex, 20] = StartIndex.ToString();
                            SqlCeConnection sqlConnection1 = new SqlCeConnection();
                            sqlConnection1.ConnectionString = dataSourcePath;

                            SqlCeCommand cmd = new SqlCeCommand();
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Connection = sqlConnection1;
                            sqlConnection1.Open();
                            cmd.CommandText = "UPDATE MDB_FILE_FIELD_SETTINGS SET FIE_MDB_INDEX=" + StartIndex.ToString() + " WHERE FIE_MDB_ID=" + comId.ToString();
                            cmd.ExecuteNonQuery();

                            sqlConnection1.Dispose();
                            sqlConnection1.Close();
                        }
                        Application.DoEvents();
                    }
                    else
                        ind++;

                    //if (fromUpload == true)
                        countRows++;
                }
                if (fromUpload == true)
                {
                    lbReadComp.Text = (pgbrUpload.Value).ToString() + "%    Reading " + countRows.ToString() + " record of " + totalRec.ToString() + " records";
                    fnMdbRec(comId, fPath);
                    fnValidateURL(3);
                    lbReadComp.Text = (pgbrUpload.Value).ToString() + "%    Saved " + totalRec.ToString() + " records";

                    string strConn = "server=" + strHost + ";database=" + strDB + ";uid=" + strUsr + ";pwd=" + strPWD;
                    MySqlConnection myConn = new MySqlConnection(strConn);
                    try
                    {
                        MySqlCommand myCommand = myConn.CreateCommand();
                        myConn.Open();

                        myCommand.CommandText = "SELECT COUNT(*) FROM kal_logdetails";
                        MySqlDataReader SQLRD;
                        SQLRD = myCommand.ExecuteReader();
                        SQLRD.Read();
                        lbDbRec.Text = SQLRD[0].ToString();
                        SQLRD.Close();
                        myConn.Close();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                if (Global.forceRead == true)
                {
                    chkForce.Checked = false;
                    dtpForcedate.Refresh();
                    Global.forceRead = false;
                }
            }
            else if (compType == "text")
            {
                //read from files

                int counter = 0;
                totalRec = 0;
                countRows = 1;
                int startRowInd = Convert.ToInt32(timersTextDetails[compIndex, 11]);//not required
                int compIdVal = Convert.ToInt32(timersTextDetails[compIndex, 0]);
                string nul = "";
                string line;
                string dev_date = "";
                string dateFie = "";
                string time = "";
                string timeFie = "";
                string emp_id = "";
                string dev_id = "";

                //here check which type of reading i.e, all or current of fro date
                //17->refix, 18->date format, 19->suffix, 20->file Type, 21->from date
                string fileType = timersTextDetails[compIndex, 20];
                string folderPath = timersTextDetails[compIndex, 2];
                if (fileType == "all")
                {
                    string[] filePaths = Directory.GetFiles(folderPath, "*" + timersTextDetails[compIndex, 22]);
                    int c = filePaths.GetLength(0);
                    recs = c;
                    int f = 0;
                    string filename = "";
                    if (c >= 1)
                    {
                        foreach (string filePath in filePaths)
                        {
                            f++;
                            try
                            {
                                if (Path.GetExtension(filePath).ToString().ToUpper() == timersTextDetails[compIndex, 22].ToUpper())
                                {
                                    //fileno = "Reading '" + f.ToString() + "' file among '" + c.ToString() + "' files"; ;
                                    //txtTextFile.Text = filePath;
                                    //fnTextFile();
                                    filename = filePath;
                                    FileInfo fi = new FileInfo(filename);
                                    if (fi.Exists)
                                        fnSaveFromFile(compId, compType, compIndex, filename, f);
                                }
                            }
                            catch { }
                        }
                    }

                }
                else if (fileType == "cur")
                {
                    recs = 2;
                    //DateTime curdate1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1);
                    DateTime curdate1 = DateTime.Today.AddDays(-1);
                    //string filename = folderPath + "\\" + timersTextDetails[compIndex, 17] + fnGetDateFormat(timersTextDetails[compIndex, 18]) + timersTextDetails[compIndex, 19] + timersTextDetails[compIndex, 22];
                    string filename = folderPath + "\\" + timersTextDetails[compIndex, 17] + String.Format("{0:" + timersTextDetails[compIndex, 18] + "}", curdate1) + timersTextDetails[compIndex, 19] + timersTextDetails[compIndex, 22];
                    //folderPath += "\\" + filename;
                    FileInfo fi = new FileInfo(filename);
                    if (fi.Exists)
                        fnSaveFromFile(compId, compType, compIndex, filename, 1);
                    DateTime curdate2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    //string filename = folderPath + "\\" + timersTextDetails[compIndex, 17] + fnGetDateFormat(timersTextDetails[compIndex, 18]) + timersTextDetails[compIndex, 19] + timersTextDetails[compIndex, 22];
                    filename = folderPath + "\\" + timersTextDetails[compIndex, 17] + String.Format("{0:" + timersTextDetails[compIndex, 18] + "}", curdate2) + timersTextDetails[compIndex, 19] + timersTextDetails[compIndex, 22];
                    //folderPath += "\\" + filename;
                    FileInfo fi1 = new FileInfo(filename);
                    if (fi1.Exists)
                        fnSaveFromFile(compId, compType, compIndex, filename, 2);

                }
                else if (fileType == "from")
                {
                    string filename = "";
                    string date = timersTextDetails[compIndex, 21];
                    string today = DateTime.Now.ToShortDateString();
                    DateTime fromdate = DateTime.Parse(date);
                    DateTime curdate = DateTime.Parse(today);
                    TimeSpan ts = curdate - fromdate;
                    string s = ts.ToString();
                    string[] str = s.Split('.');
                    int days = Convert.ToInt32(str[0]);
                    recs = days + 1;
                    int f = 0;
                    for (int i = days; i >= 0; i--)
                    {
                        f++;
                        //DateTime curdate1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - (i));
                        DateTime curdate1 = DateTime.Today.AddDays(-i);
                        filename = folderPath + "\\" + timersTextDetails[compIndex, 17] + String.Format("{0:" + timersTextDetails[compIndex, 18] + "}", curdate1) + timersTextDetails[compIndex, 19] + timersTextDetails[compIndex, 22];
                        //filename = String.Format("{0:" + timersTextDetails[compIndex, 18] + "}", curdate1);
                        FileInfo fi = new FileInfo(filename);
                        if (fi.Exists)
                            fnSaveFromFile(compId, compType, compIndex, filename, f);
                    }
                }
                else if (fileType == "file")
                {
                    string filename = folderPath + "\\" + timersTextDetails[compIndex, 23];
                    FileInfo fi = new FileInfo(filename);
                    recs = 1;
                    if (fi.Exists)
                        fnSaveFromFile(compId, compType, compIndex, filename, 1);
                }

                #region one Text File Format
                /*
                int totalRec = fnTextRec(timersTextDetails[compIndex, 2]);
                int countRows = 1;
                if (fromUpload == true)
                    lbReadComp.Text = "Started reading " + totalRec.ToString() + " records";

                // Read the file and display it line by line.
                StreamReader file = new StreamReader(timersTextDetails[compIndex, 2]);
                fnOnlineConnectVariables(timersTextDetails[compIndex, 1]);
                while ((line = file.ReadLine()) != null)
                {
                    //if (fromUpload == true)
                    //{
                        pgbrUpload.Value = ((countRows * 100) / totalRec);
                        lbReadComp.Text = (pgbrUpload.Value).ToString() + "%    Reading " + countRows.ToString() + " record of " + totalRec.ToString() + " records";
                    //}
                    int dayStartIndex = 0;
                    int monthStartIndex = 0;
                    int yearStartIndex = 0;
                    int yearLen = 0;

                    int hourStartIndex = 0;
                    int minStartIndex = 0;
                    int secStartIndex = 0;
                    int timeLength = 0;
                    if (startRowInd == counter)
                    {
                        emp_id = line.Substring(Convert.ToInt32(timersTextDetails[compIndex, 3]), Convert.ToInt32(timersTextDetails[compIndex, 4]));
                        dev_date = line.Substring(Convert.ToInt32(timersTextDetails[compIndex, 5]), Convert.ToInt32(timersTextDetails[compIndex, 6]));
                        time = line.Substring(Convert.ToInt32(timersTextDetails[compIndex, 7]), Convert.ToInt32(timersTextDetails[compIndex, 8]));
                        if (timersTextDetails[compIndex, 9] != "" && timersTextDetails[compIndex, 10] != "")
                        {
                            if (Convert.ToInt32(timersTextDetails[compIndex, 10]) != Convert.ToInt32(timersTextDetails[compIndex, 9]))
                            {
                                dev_id = line.Substring(Convert.ToInt32(timersTextDetails[compIndex, 9]), Convert.ToInt32(timersTextDetails[compIndex, 10]));
                            }
                        }
                        else
                            dev_id = "0";
                        string dateformat = timersTextDetails[compIndex, 12];
                        int dateformatlength = dateformat.Length;

                        dayStartIndex = (timersTextDetails[compIndex, 12].ToUpper()).IndexOf('D');
                        monthStartIndex = (timersTextDetails[compIndex, 12].ToUpper()).IndexOf('M');
                        yearStartIndex = (timersTextDetails[compIndex, 12].ToUpper()).IndexOf('Y');
                        yearLen = dev_date.Length;

                        if (dateformat.ToLower() == "dd/mm/yyyy" || dateformat.ToLower() == "dd/mm/yy" || dateformat.ToLower() == "yyyy/mm/dd" || dateformat.ToLower() == "yy/mm/dd")
                        {
                            try
                            {
                                string[] dat = dev_date.Split('/');
                                if (dat.Length == 3)
                                {
                                    if (dateformat.ToLower() == "dd/mm/yyyy" || dateformat.ToLower() == "dd/mm/yy")
                                        dateFie = dat[2] + "-" + dat[1] + "-" + dat[0];
                                    else if (dateformat.ToLower() == "yyyy/mm/dd" || dateformat.ToLower() == "yy/mm/dd")
                                        dateFie = dat[0] + "-" + dat[1] + "-" + dat[2];
                                }
                            }
                            catch (Exception ee)
                            {
                                //MessageBox.Show("Error1 " + ee.Message);
                            }
                        }
                        else if (dateformat.ToLower() == "dd-mm-yyyy" || dateformat.ToLower() == "dd-mm-yy" || dateformat.ToLower() == "yyyy-mm-dd" || dateformat.ToLower() == "yy-mm-dd")
                        {
                            try
                            {
                                string[] dat = dev_date.Split('-');
                                if (dat.Length == 3)
                                {
                                    if (dateformat.ToLower() == "dd-mm-yyyy" || dateformat.ToLower() == "dd-mm-yy")
                                        dateFie = dat[2] + "-" + dat[1] + "-" + dat[0];
                                    else if (dateformat.ToLower() == "yyyy-mm-dd" || dateformat.ToLower() == "yy-mm-dd")
                                        dateFie = dat[0] + "-" + dat[1] + "-" + dat[2];
                                }
                            }
                            catch (Exception ee)
                            {
                                //MessageBox.Show("Error1 " + ee.Message);
                            }
                        }
                        else if (dateformat.ToLower() == "d/m/yyyy" || dateformat.ToLower() == "d/m/yy" || dateformat.ToLower() == "yyyy/m/d" || dateformat.ToLower() == "yy/m/d" || dateformat.ToLower() == "d-m-yyyy" || dateformat.ToLower() == "d-m-yy" || dateformat.ToLower() == "yyyy-m-d" || dateformat.ToLower() == "yy-m-d")
                        {
                            try
                            {
                                string[] dat = dev_date.Split('/');
                                if (dat.Length == 3)
                                {
                                    if (dateformat.ToLower() == "d/m/yyyy" || dateformat.ToLower() == "d/m/yy" || dateformat.ToLower() == "d/mm/yyyy" || dateformat.ToLower() == "d/mm/yy")
                                        dateFie = dat[2] + "-" + dat[1] + "-" + dat[0];
                                    else if (dateformat.ToLower() == "yyyy/m/d" || dateformat.ToLower() == "yy/m/d" || dateformat.ToLower() == "yyyy/mm/d" || dateformat.ToLower() == "yy/mm/d")
                                        dateFie = dat[0] + "-" + dat[1] + "-" + dat[2];
                                }
                            }
                            catch (Exception ee)
                            {
                                //MessageBox.Show("Error1 " + ee.Message);
                            }
                        }
                        else if(dateformat.ToLower() == "d/mm/yyyy" || dateformat.ToLower() == "d/mm/yy" || dateformat.ToLower() == "yyyy/mm/d" || dateformat.ToLower() == "yy/mm/d" || dateformat.ToLower() == "d-mm-yyyy" || dateformat.ToLower() == "d-mm-yy" || dateformat.ToLower() == "yyyy-mm-d" || dateformat.ToLower() == "yy-mm-d")
                        {
                            try
                            {
                                string[] dat = dev_date.Split('-');
                                if (dat.Length == 3)
                                {
                                    if (dateformat.ToLower() == "d-m-yyyy" || dateformat.ToLower() == "d-m-yy" || dateformat.ToLower() == "d-mm-yyyy" || dateformat.ToLower() == "d-mm-yy")
                                        dateFie = dat[2] + "-" + dat[1] + "-" + dat[0];
                                    else if (dateformat.ToLower() == "yyyy-m-d" || dateformat.ToLower() == "yy-m-d" || dateformat.ToLower() == "yyyy-mm-d" || dateformat.ToLower() == "yy-mm-d")
                                        dateFie = dat[0] + "-" + dat[1] + "-" + dat[2];
                                }
                            }
                            catch (Exception ee)
                            {
                                //MessageBox.Show("Error1 " + ee.Message);
                            }
                        }
                        else if (dateformat.ToLower() == "ddmmyy")
                        {
                            dateFie = dev_date.Substring(4, 2) + "-" + dev_date.Substring(2, 2) + "-" + dev_date.Substring(0, 2);
                        }
                        else if (dateformat.ToLower() == "ddmmyyyy")
                        {
                            dateFie = dev_date.Substring(4, 4) + "-" + dev_date.Substring(2, 2) + "-" + dev_date.Substring(0, 2);
                        }
                        else if (dateformat.ToLower() == "yymmdd")
                        {
                            dateFie = dev_date.Substring(0, 2) + "-" + dev_date.Substring(2, 2) + "-" + dev_date.Substring(4, 2);
                        }
                        else if (dateformat.ToLower() == "yyyymmdd")
                        {
                            dateFie = dev_date.Substring(0, 4) + "-" + dev_date.Substring(4, 2) + "-" + dev_date.Substring(6, 2);
                        }

                        timeLength = time.Length;
                        string timeformat=timersTextDetails[compIndex, 13];

                        hourStartIndex = (timersTextDetails[compIndex, 13].ToUpper()).IndexOf('H');
                        minStartIndex = (timersTextDetails[compIndex, 13].ToUpper()).IndexOf('M');

                        if (timeformat.ToLower() == "hhmmss")
                        {
                            timeFie = time.Substring(0, 2) + ":" + time.Substring(2, 2) + ":" + time.Substring(4, 2);
                        }
                        else if (timeformat.ToLower() == "hh:mm:ss")
                        {
                            timeFie = time.Substring(0, 2) + ":" + time.Substring(3, 2) + ":" + time.Substring(6, 2);
                        }
                        else if (timeformat.ToLower() == "hhmm")
                        {
                            timeFie = time.Substring(0, 2) + ":" + time.Substring(2, 2);
                        }
                        else if (timeformat.ToLower() == "hh:mm")
                        {
                            timeFie = time.Substring(0, 2) + ":" + time.Substring(3, 2);
                        }
                        else if (timeformat.ToLower() == "h:mm:ss")
                        {
                            timeFie = time.Substring(0, 1) + ":" + time.Substring(2, 2) + ":" + time.Substring(5, 2);
                        }
                        else if (timeformat.ToLower() == "h:mm")
                        {
                            timeFie = time.Substring(0, 1) + ":" + time.Substring(2, 2);
                        }
                        //if (timeLength == 6)
                        //{
                        //    secStartIndex = (timersTextDetails[compIndex, 13].ToUpper()).IndexOf('S');
                        //    timeFie = time.Substring(hourStartIndex, 2) + ":" + time.Substring(minStartIndex, 2) + ":" + time.Substring(secStartIndex, 2);
                        //}
                        //else
                        //{

                        //    timeFie = time.Substring(hourStartIndex, 2) + ":" + time.Substring(minStartIndex, 2);
                        //}

                        if (timersTextDetails[compIndex, 15] == "ref")
                        {
                            emp_id = fnGetRefCardEmp(timersTextDetails[compIndex, 1], "ref", emp_id);
                        }
                        else if (timersTextDetails[compIndex, 15] == "card")
                        {
                            emp_id = fnGetRefCardEmp(timersTextDetails[compIndex, 1], "card", emp_id);
                        }
                        else if (timersTextDetails[compIndex, 15] == "emp")
                        {
                            //emp_id = fnGetRefCardEmp(timersTextDetails[compIndex, 1], "emp", emp_id);
                        }
                        if (emp_id != "")
                        {
                            string strConn = "server=" + strHost + ";database=" + strDB + ";uid=" + strUsr + ";pwd=" + strPWD;
                            MySqlConnection myConn = new MySqlConnection(strConn);
                            MySqlCommand myCommand = myConn.CreateCommand();
                            myConn.Open();
                            if (fnCheckDuplicate(emp_id, dateFie, timeFie, dev_id))
                            {
                                myCommand.CommandText = "insert into `kal_logdetails` (EMPID, LOGDATE, LOGTIME, DEVICE_ID) values('" +
                                               emp_id + "', '" +
                                               dateFie + "', '" +
                                               timeFie + "', " +
                                               dev_id + ")";
                                myCommand.ExecuteNonQuery();
                            }

                            myCommand.CommandText = "SELECT COUNT(*) FROM `kal_logdetails`";
                            lbDbRec.Text = (Convert.ToInt32(myCommand.ExecuteScalar())).ToString();
                            lbDbRec.Update();

                            myConn.Close();
                            Application.DoEvents();
                        }
                        Application.DoEvents();

                        counter++;
                        startRowInd++;
                        timersTextDetails[compIndex, 11] = startRowInd.ToString();

                        SqlCeConnection sqlConnection1 = new SqlCeConnection();
                        sqlConnection1.ConnectionString = dataSourcePath;

                        SqlCeCommand cmd = new SqlCeCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        sqlConnection1.Open();
                        cmd.CommandText = "UPDATE TEXT_FILE_SETTINGS SET TEXT_START_ROW=" + startRowInd.ToString() + " WHERE TEXT_COM_ID=" + compIdVal.ToString();
                        cmd.ExecuteNonQuery();

                        sqlConnection1.Dispose();
                        sqlConnection1.Close();
                        Application.DoEvents();
                    }
                    else
                        counter++;

                    Application.DoEvents();

                    //if (fromUpload == true)
                        countRows++;
                }
                ////////////////////////
                ////////////////////////
                ////////////////////////
                if (fromUpload == true)
                {
                    lbReadComp.Text = (pgbrUpload.Value).ToString() + "%    Reading " + countRows.ToString() + " record of " + totalRec.ToString() + " records";
                    int recs = fnTextRec(timersTextDetails[compIndex, 2]);
                    recs = (recs - startRowInd) + 1;
                    fnValidateURL(3);
                    lbReadComp.Text = (pgbrUpload.Value).ToString() + "%    Saved " + totalRec.ToString() + " records";

                    string strConn = "server=" + strHost + ";database=" + strDB + ";uid=" + strUsr + ";pwd=" + strPWD;
                    MySqlConnection myConn = new MySqlConnection(strConn);
                    try
                    {
                        MySqlCommand myCommand = myConn.CreateCommand();
                        myConn.Open();

                        myCommand.CommandText = "SELECT COUNT(*) FROM kal_logdetails";
                        MySqlDataReader SQLRD;
                        SQLRD = myCommand.ExecuteReader();
                        SQLRD.Read();
                        lbDbRec.Text = SQLRD[0].ToString();
                        SQLRD.Close();
                        myConn.Close();
                    }
                    catch (Exception ex)
                    {
                    }
                    */
                #endregion

                //file.Dispose();
                //file.Close();
                //}

            }
            else if (compType == "sql")
            {
                int comId = Convert.ToInt32(timersSQLDetails[compIndex, 0]);
                string cUrl = timersSQLDetails[compIndex, 1];
                int empLen = Convert.ToInt32(timersSQLDetails[compIndex, 3]);
                string posSuf = timersSQLDetails[compIndex, 4];
                string refEmp = timersSQLDetails[compIndex, 5];

                string servername = timersSQLDetails[compIndex, 6];
                string database = timersSQLDetails[compIndex, 7];
                string auth_type = timersSQLDetails[compIndex, 8];
                string username = timersSQLDetails[compIndex, 9];
                string fpwd = timersSQLDetails[compIndex, 10];
                string tb = timersSQLDetails[compIndex, 11];
                string empF = timersSQLDetails[compIndex, 12];
                string devI = timersSQLDetails[compIndex, 13];


                int StartIndex = Convert.ToInt32(timersSQLDetails[compIndex, 21]);


                //to store date time values
                string Date = "";
                string In_Out = "";
                string In = "";
                string Out = "";
                string empVal = "";
                int devVal = 0;
                string devValue = "";

                string DateIn = "";
                string DateOut = "";

                int DateTypeCase = 0;

                string connectionString = "";
                if (auth_type == "Windows Authentication")
                {
                    connectionString = "Integrated Security=SSPI;Data Source=" + @servername + ";Persist Security Info=False;Initial Catalog=" + database;
                    //connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fPath + ";Jet OLEDB:Database Password=" + fpwd + ";";
                }
                else if (auth_type == "SQL Server Authentication")
                {
                    connectionString = "Server=" + servername + ";Persist Security Info=True;uid=" + username + ";pwd=" + fpwd + ";Database=" + database;
                    //connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fPath + ";";
                }

                //  Open and read mdb file here
                string que = "";
                string fieldname = "";
                if (Global.forceRead == true)
                {
                    if (timersSQLDetails[compIndex, 14] != "")
                        fieldname = timersSQLDetails[compIndex, 14];
                    else if (timersSQLDetails[compIndex, 15] != "")
                        fieldname = timersSQLDetails[compIndex, 15];
                    else if (timersSQLDetails[compIndex, 17] != "")
                        fieldname = timersSQLDetails[compIndex, 17];

                    que = "SELECT * FROM " + tb + " WHERE " + fieldname + " >= '" + Convert.ToDateTime(dtpForcedate.Text).ToString("yyyy-MM-dd") + "' order by " + fieldname + " ASC";
                }
                else
                    que = "SELECT * FROM " + tb;

                System.Data.DataTable dataSet = new System.Data.DataTable();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //try
                    //{
                    connection.Open();
                    //}
                    //catch (Exception exxx)
                    //{
                    //    MessageBox.Show(exxx.Message);
                    //}
                    SqlCommand Command = new SqlCommand(que, connection);
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(Command))
                    {
                        dataAdapter.Fill(dataSet);
                    }
                    connection.Close();
                }
                int ind = 0;
                countRows = 1;
                int totalRec = dataSet.Rows.Count;
                //MessageBox.Show("Num of Records : " + totalRec.ToString());
                if (fromUpload == true)
                    lbReadComp.Text = "Started reading " + totalRec.ToString() + " records";
                fnOnlineConnectVariables(cUrl);
                //MessageBox.Show("Num of Records : " + totalRec.ToString());
                foreach (DataRow row in dataSet.Rows)
                {
                    //if (fromUpload == true)
                    //{
                    pgbrUpload.Value = ((countRows * 100) / totalRec);
                    lbReadComp.Text = (pgbrUpload.Value).ToString() + "%\n\nReading " + countRows.ToString() + " record of " + totalRec.ToString() + " records";
                    //}
                    //countRows++;
                    if (StartIndex == ind || Global.forceRead == true)
                    {
                        empVal = row[empF].ToString();
                        //MessageBox.Show(empF+" DevId : "+devVal.ToString()+" Rows count : "+dataSet.Rows.Count.ToString());
                        //prefix or suffix
                        if (empVal != "")
                        {
                            if (posSuf == "pre")
                            {
                                //value of empLen
                                empVal = empVal.Substring(0, empLen);
                            }
                            else if (posSuf == "suf")
                            {
                                //MessageBox.Show(empVal + "  Length : " + empVal.Length.ToString());
                                empVal = empVal.Substring((empVal.Length) - empLen);
                            }

                            if (refEmp == "ref")
                            {
                                //select from  mas_employee   //cUrl
                                empVal = fnGetRefCardEmp(cUrl, "ref", empVal);
                            }
                            else if (refEmp == "card")
                            {
                                //select from mas_users
                                empVal = fnGetRefCardEmp(cUrl, "card", empVal);
                            }
                            else if (refEmp == "emp")
                            {
                                empVal = fnGetRefCardEmp(cUrl, "emp", empVal);
                            }
                            if (empVal != "")
                            {
                                try
                                {
                                    //devVal = Convert.ToInt32(row[devI]);
                                    devValue = row[devI].ToString();
                                }
                                catch
                                {

                                }
                                if (devValue == "")
                                    devValue = "0";

                                if (timersSQLDetails[compIndex, 14] != "")
                                {
                                    //string dateTime = Convert.ToDateTime(row[timersSQLDetails[compIndex, 14]]).ToString("yyyy-MM-dd hh:mm:ss");

                                    //string[] dtf = dateTime.Split();
                                    //Date = dtf[0];
                                    //In_Out = dtf[1];
                                    Date = Convert.ToDateTime(row[timersSQLDetails[compIndex, 14]]).ToString("yyyy-MM-dd");
                                    In_Out = Convert.ToDateTime(row[timersSQLDetails[compIndex, 14]]).ToString("HH:mm:ss");
                                    DateTypeCase = 1;
                                    //get Date and in_out------------------------------------------------------------>1 record
                                    In = "";
                                    Out = "";
                                }
                                else if (timersSQLDetails[compIndex, 15] != "" && timersSQLDetails[compIndex, 16] != "")
                                {
                                    //string dateTimeIn = Convert.ToDateTime(row[timersSQLDetails[compIndex, 15]]).ToString("yyyy-MM-dd hh:mm:ss");
                                    //string dateTimeIn = row[timersSQLDetails[compIndex, 15]].ToString();
                                    //string[] dif = dateTimeIn.Split(' ');
                                    //DateIn = dif[0];
                                    //In = dif[1];
                                    DateIn = Convert.ToDateTime(row[timersSQLDetails[compIndex, 15]]).ToString("yyyy-MM-dd");
                                    In = Convert.ToDateTime(row[timersSQLDetails[compIndex, 15]]).ToString("HH:mm:ss");

                                    //string dateTimeOut = Convert.ToDateTime(row[timersSQLDetails[compIndex, 16]]).ToString("yyyy-MM-dd hh:mm:ss");
                                    ////string dateTimeOut = row[timersSQLDetails[compIndex, 16]].ToString();
                                    //string[] dof = dateTimeOut.Split(' ');
                                    //DateOut = dof[0];
                                    //Out = dof[1];
                                    DateOut = Convert.ToDateTime(row[timersSQLDetails[compIndex, 16]]).ToString("yyyy-MM-dd");
                                    Out = Convert.ToDateTime(row[timersSQLDetails[compIndex, 16]]).ToString("HH:mm:ss");

                                    DateTypeCase = 2;
                                    //date, time_in and time_out----------------------------------------------------->2 records
                                    In_Out = "";
                                }
                                else if (timersSQLDetails[compIndex, 18] != "")
                                {
                                    //Date = row[timersSQLDetails[compIndex, 17]].ToString();
                                    //string[] dt = Date.Split(' ');
                                    //if (dt.Length > 1)
                                    //{
                                    //    Date = dt[0];
                                    //}
                                    Date = Convert.ToDateTime(row[timersSQLDetails[compIndex, 17]]).ToString("yyyy-MM-dd");

                                    //In_Out = row[timersSQLDetails[compIndex, 18]].ToString();
                                    //string[] tt = In_Out.Split(' ');
                                    //if (tt.Length >= 2)
                                    //{
                                    //    In_Out = tt[1];
                                    //}
                                    In_Out = Convert.ToDateTime(row[timersSQLDetails[compIndex, 18]]).ToString("HH:mm:ss");
                                    //Date ,In_out
                                    DateTypeCase = 3;
                                    In = "";
                                    Out = "";
                                }
                                else if (timersSQLDetails[compIndex, 19] != "" && timersSQLDetails[compIndex, 20] != "")
                                {
                                    //Date = row[timersSQLDetails[compIndex, 17]].ToString();
                                    //string[] dt = Date.Split(' ');
                                    //if (dt.Length > 1)
                                    //{
                                    //    Date = dt[0];
                                    //}
                                    Date = Convert.ToDateTime(row[timersSQLDetails[compIndex, 17]]).ToString("yyyy-MM-dd");
                                    //In = row[timersSQLDetails[compIndex, 19]].ToString();
                                    //string[] tt1 = In.Split(' ');
                                    //if (tt1.Length >= 2)
                                    //{
                                    //    In = tt1[1];
                                    //}
                                    //Out = row[timersSQLDetails[compIndex, 20]].ToString();
                                    //string[] tt2 = Out.Split(' ');
                                    //if (tt2.Length >= 2)
                                    //{
                                    //    Out = tt2[1];
                                    //}
                                    In = Convert.ToDateTime(row[timersSQLDetails[compIndex, 19]]).ToString("HH:mm:ss");
                                    Out = Convert.ToDateTime(row[timersSQLDetails[compIndex, 20]]).ToString("HH:mm:ss");
                                    DateTypeCase = 4;
                                    //date , In, Out---------------------------------------------------------------->2 records
                                    In_Out = "";
                                }
                                /*
                                try
                                {

                                    string[] dat = Date.Split('/');
                                    if (dat.Length >= 3)
                                    {
                                        Date = dat[2] + "-" + dat[1] + "-" + dat[0];
                                    }
                                    if (dat.Length <= 1)
                                    {
                                        dat = Date.Split('-');
                                        if (dat.Length >= 3)
                                        {
                                            if (dat[0].Length == 4)
                                            {
                                                Date = dat[0] + "-" + dat[1] + "-" + dat[2];
                                            }
                                            else
                                            {
                                                Date = dat[2] + "-" + dat[1] + "-" + dat[0];
                                            }
                                        }
                                    }
                                }
                                catch { }
                                */
                                //save in kal_log details
                                //MessageBox.Show("EMP ID : " + empVal + "\nDate : " + Date+"\nINOUT : "+In_Out+"\nDev id : "+ devValue);
                                try
                                {
                                    string strConn = "server=" + strHost + ";database=" + strDB + ";uid=" + strUsr + ";pwd=" + strPWD;
                                    MySqlConnection myConn = new MySqlConnection(strConn);
                                    MySqlCommand myCommand = myConn.CreateCommand();
                                    myConn.Open();
                                    if (DateTypeCase == 1 || DateTypeCase == 3)
                                    {
                                        if (fnCheckDuplicate(empVal, Date, In_Out, devValue))
                                        {
                                            myCommand.CommandText = "insert into `kal_logdetails` (EMPID, LOGDATE, LOGTIME, DEVICE_ID) values('" +
                                                        empVal + "', '" +
                                                        Date + "', '" +
                                                        In_Out + "', " +
                                                        devValue + ")";
                                            myCommand.ExecuteNonQuery();
                                        }
                                    }
                                    else if (DateTypeCase == 2)
                                    {
                                        if (fnCheckDuplicate(empVal, DateIn, In, devValue))
                                        {
                                            myCommand.CommandText = "insert into `kal_logdetails` (EMPID, LOGDATE, LOGTIME, DEVICE_ID) values('" +
                                                        empVal + "', '" +
                                                        DateIn + "', '" +
                                                        In + "', " +
                                                        devValue + ")";
                                            myCommand.ExecuteNonQuery();
                                        }

                                        if (fnCheckDuplicate(empVal, DateOut, Out, devValue))
                                        {
                                            myCommand.CommandText = "insert into `kal_logdetails` (EMPID, LOGDATE, LOGTIME, DEVICE_ID) values('" +
                                                        empVal + "', '" +
                                                        DateOut + "', '" +
                                                        Out + "', " +
                                                        devValue + ")";
                                            myCommand.ExecuteNonQuery();
                                        }
                                    }
                                    else if (DateTypeCase == 4)
                                    {
                                        if (fnCheckDuplicate(empVal, Date, In, devValue))
                                        {
                                            myCommand.CommandText = "insert into `kal_logdetails` (EMPID, LOGDATE, LOGTIME, DEVICE_ID) values('" +
                                                        empVal + "', '" +
                                                        Date + "', '" +
                                                        In + "', " +
                                                        devValue + ")";
                                            myCommand.ExecuteNonQuery();
                                        }
                                        if (fnCheckDuplicate(empVal, Date, Out, devValue))
                                        {
                                            myCommand.CommandText = "insert into `kal_logdetails` (EMPID, LOGDATE, LOGTIME, DEVICE_ID) values('" +
                                                        empVal + "', '" +
                                                        Date + "', '" +
                                                        Out + "', " +
                                                        devValue + ")";
                                            myCommand.ExecuteNonQuery();
                                        }

                                    }
                                    myCommand.CommandText = "SELECT COUNT(*) FROM `kal_logdetails`";
                                    lbDbRec.Text = (Convert.ToInt32(myCommand.ExecuteScalar())).ToString();
                                    lbDbRec.Update();
                                    myConn.Close();
                                    Application.DoEvents();
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                        }
                        if (Global.forceRead != true)
                        {
                            ind++;
                            StartIndex++;
                            timersSQLDetails[compIndex, 21] = StartIndex.ToString();
                            SqlCeConnection sqlConnection1 = new SqlCeConnection();
                            sqlConnection1.ConnectionString = dataSourcePath;

                            SqlCeCommand cmd = new SqlCeCommand();
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Connection = sqlConnection1;
                            sqlConnection1.Open();
                            cmd.CommandText = "UPDATE SQL_DATA_FIELD_SETTINGS SET SQL_TAB_INDEX=" + StartIndex.ToString() + " WHERE SQL_TAB_COMP_ID=" + comId.ToString();
                            cmd.ExecuteNonQuery();

                            sqlConnection1.Dispose();
                            sqlConnection1.Close();
                        }
                        Application.DoEvents();
                    }
                    else
                        ind++;

                    //if (fromUpload == true)
                    countRows++;
                }
                if (fromUpload == true)
                {
                    //lbReadComp.Text = (pgbrUpload.Value).ToString() + "%    Reading " + countRows.ToString() + " record of " + totalRec.ToString() + " records";
                    ////fnMdbRec(comId, fPath);
                    //fnValidateURL(4);
                    //lbReadComp.Text = (pgbrUpload.Value).ToString() + "%    Saved " + totalRec.ToString() + " records";

                    //string strConn = "server=" + strHost + ";database=" + strDB + ";uid=" + strUsr + ";pwd=" + strPWD;
                    //MySqlConnection myConn = new MySqlConnection(strConn);
                    //try
                    //{
                    //    MySqlCommand myCommand = myConn.CreateCommand();
                    //    myConn.Open();

                    //    myCommand.CommandText = "SELECT COUNT(*) FROM kal_logdetails";
                    //    MySqlDataReader SQLRD;
                    //    SQLRD = myCommand.ExecuteReader();
                    //    SQLRD.Read();
                    //    lbDbRec.Text = SQLRD[0].ToString();
                    //    SQLRD.Close();
                    //    myConn.Close();
                    //}
                    //catch (Exception ex)
                    //{
                    //}
                }
                if (Global.forceRead == true)
                {
                    chkForce.Checked = false;
                    dtpForcedate.Refresh();
                    Global.forceRead = false;
                }
            }
            else
            {
                MessageBox.Show("setiings are not done for some companies", "Company");
            }
            Application.DoEvents();
            pnlImportShow.Enabled = true;
            panel8.Enabled = true;
            btnCreateCompany.Enabled = true;
            treeView1.Enabled = true;
            lbReadComp.Text = "Read records of Company(Id) : " + compId.ToString() + " successfully.";
            btnUpload.Enabled = true;
            lbReadComp.Visible = true;
            pgbrUpload.Value = 0;
        }

        private void fnSaveFromFile(int compId, string compType, int compIndex,string filename,int rec)
        {
            totalRec += fnTextRec(filename);
            string line = ""; 
            //string nul = "";
            string dev_date = "";
            string dateFie = "";
            string time = "";
            string timeFie = "";
            string emp_id = "";
            string dev_id = "";
            if (fromUpload == true)
                lbReadComp.Text = "Started reading " + totalRec.ToString() + " records";

            // Read the file and display it line by line.
            StreamReader file = new StreamReader(filename);
            fnOnlineConnectVariables(timersTextDetails[compIndex, 1]);
            pgbrUpload.Value = 0;
            while ((line = file.ReadLine()) != null)
            {
                try
                {
                    //if (fromUpload == true)
                    //{
                    pgbrUpload.Value = ((countRows * 100) / totalRec);
                    lbReadComp.Text = (pgbrUpload.Value).ToString() + "%\n\nReading file " + rec + " of " + recs + " files. Reading " + countRows.ToString() + " record of " + totalRec.ToString() + " records";
                    //}
                    countRows++;
                    int dayStartIndex = 0;
                    int monthStartIndex = 0;
                    int yearStartIndex = 0;
                    int yearLen = 0;

                    int hourStartIndex = 0;
                    int minStartIndex = 0;
                    //int secStartIndex = 0;
                    int timeLength = 0;
                    //if (startRowInd == counter)
                    //{
                    emp_id = line.Substring(Convert.ToInt32(timersTextDetails[compIndex, 3]), Convert.ToInt32(timersTextDetails[compIndex, 4]));
                    dev_date = line.Substring(Convert.ToInt32(timersTextDetails[compIndex, 5]), Convert.ToInt32(timersTextDetails[compIndex, 6]));
                    time = line.Substring(Convert.ToInt32(timersTextDetails[compIndex, 7]), Convert.ToInt32(timersTextDetails[compIndex, 8]));
                    if (timersTextDetails[compIndex, 9] != "" && timersTextDetails[compIndex, 10] != "")
                    {
                        if (Convert.ToInt32(timersTextDetails[compIndex, 10]) != 0)
                        {
                            dev_id = line.Substring(Convert.ToInt32(timersTextDetails[compIndex, 9]), Convert.ToInt32(timersTextDetails[compIndex, 10]));
                        }
                    }
                    else
                        dev_id = "0";

                    if (dev_id == "")
                        dev_id = "0";

                    string dateformat = timersTextDetails[compIndex, 12];
                    int dateformatlength = dateformat.Length;

                    dayStartIndex = (timersTextDetails[compIndex, 12].ToUpper()).IndexOf('D');
                    monthStartIndex = (timersTextDetails[compIndex, 12].ToUpper()).IndexOf('M');
                    yearStartIndex = (timersTextDetails[compIndex, 12].ToUpper()).IndexOf('Y');
                    yearLen = dev_date.Length;

                    if (dateformat.ToLower() == "dd/mm/yyyy" || dateformat.ToLower() == "dd/mm/yy" || dateformat.ToLower() == "yyyy/mm/dd" || dateformat.ToLower() == "yy/mm/dd")
                    {
                        try
                        {
                            string[] dat = dev_date.Split('/');
                            if (dat.Length == 3)
                            {
                                if (dateformat.ToLower() == "dd/mm/yyyy" || dateformat.ToLower() == "dd/mm/yy")
                                    dateFie = dat[2] + "-" + dat[1] + "-" + dat[0];
                                else if (dateformat.ToLower() == "yyyy/mm/dd" || dateformat.ToLower() == "yy/mm/dd")
                                    dateFie = dat[0] + "-" + dat[1] + "-" + dat[2];
                            }
                        }
                        catch (Exception ee)
                        {
                            //MessageBox.Show("Error1 " + ee.Message);
                        }
                    }
                    else if (dateformat.ToLower() == "dd-mm-yyyy" || dateformat.ToLower() == "dd-mm-yy" || dateformat.ToLower() == "yyyy-mm-dd" || dateformat.ToLower() == "yy-mm-dd")
                    {
                        try
                        {
                            string[] dat = dev_date.Split('-');
                            if (dat.Length == 3)
                            {
                                if (dateformat.ToLower() == "dd-mm-yyyy" || dateformat.ToLower() == "dd-mm-yy")
                                    dateFie = dat[2] + "-" + dat[1] + "-" + dat[0];
                                else if (dateformat.ToLower() == "yyyy-mm-dd" || dateformat.ToLower() == "yy-mm-dd")
                                    dateFie = dat[0] + "-" + dat[1] + "-" + dat[2];
                            }
                        }
                        catch (Exception ee)
                        {
                            //MessageBox.Show("Error2 " + ee.Message);
                        }
                    }
                    else if (dateformat.ToLower() == "d/m/yyyy" || dateformat.ToLower() == "d/m/yy" || dateformat.ToLower() == "yyyy/m/d" || dateformat.ToLower() == "yy/m/d" || dateformat.ToLower() == "d-m-yyyy" || dateformat.ToLower() == "d-m-yy" || dateformat.ToLower() == "yyyy-m-d" || dateformat.ToLower() == "yy-m-d")
                    {
                        try
                        {
                            string[] dat = dev_date.Split('/');
                            if (dat.Length == 3)
                            {
                                if (dateformat.ToLower() == "d/m/yyyy" || dateformat.ToLower() == "d/m/yy" || dateformat.ToLower() == "d/mm/yyyy" || dateformat.ToLower() == "d/mm/yy")
                                    dateFie = dat[2] + "-" + dat[1] + "-" + dat[0];
                                else if (dateformat.ToLower() == "yyyy/m/d" || dateformat.ToLower() == "yy/m/d" || dateformat.ToLower() == "yyyy/mm/d" || dateformat.ToLower() == "yy/mm/d")
                                    dateFie = dat[0] + "-" + dat[1] + "-" + dat[2];
                            }
                        }
                        catch (Exception ee)
                        {
                            //MessageBox.Show("Error3 " + ee.Message);
                        }
                    }
                    else if (dateformat.ToLower() == "d/mm/yyyy" || dateformat.ToLower() == "d/mm/yy" || dateformat.ToLower() == "yyyy/mm/d" || dateformat.ToLower() == "yy/mm/d" || dateformat.ToLower() == "d-mm-yyyy" || dateformat.ToLower() == "d-mm-yy" || dateformat.ToLower() == "yyyy-mm-d" || dateformat.ToLower() == "yy-mm-d")
                    {
                        try
                        {
                            string[] dat = dev_date.Split('-');
                            if (dat.Length == 3)
                            {
                                if (dateformat.ToLower() == "d-m-yyyy" || dateformat.ToLower() == "d-m-yy" || dateformat.ToLower() == "d-mm-yyyy" || dateformat.ToLower() == "d-mm-yy")
                                    dateFie = dat[2] + "-" + dat[1] + "-" + dat[0];
                                else if (dateformat.ToLower() == "yyyy-m-d" || dateformat.ToLower() == "yy-m-d" || dateformat.ToLower() == "yyyy-mm-d" || dateformat.ToLower() == "yy-mm-d")
                                    dateFie = dat[0] + "-" + dat[1] + "-" + dat[2];
                            }
                        }
                        catch (Exception ee)
                        {
                            //MessageBox.Show("Error4 " + ee.Message);
                        }
                    }
                    else if (dateformat.ToLower() == "ddmmyy")
                    {
                        dateFie = dev_date.Substring(4, 2) + "-" + dev_date.Substring(2, 2) + "-" + dev_date.Substring(0, 2);
                    }
                    else if (dateformat.ToLower() == "ddmmyyyy")
                    {
                        dateFie = dev_date.Substring(4, 4) + "-" + dev_date.Substring(2, 2) + "-" + dev_date.Substring(0, 2);
                    }
                    else if (dateformat.ToLower() == "yymmdd")
                    {
                        dateFie = dev_date.Substring(0, 2) + "-" + dev_date.Substring(2, 2) + "-" + dev_date.Substring(4, 2);
                    }
                    else if (dateformat.ToLower() == "yyyymmdd")
                    {
                        dateFie = dev_date.Substring(0, 4) + "-" + dev_date.Substring(4, 2) + "-" + dev_date.Substring(6, 2);
                    }

                    timeLength = time.Length;
                    string timeformat = timersTextDetails[compIndex, 13];

                    hourStartIndex = (timersTextDetails[compIndex, 13].ToUpper()).IndexOf('H');
                    minStartIndex = (timersTextDetails[compIndex, 13].ToUpper()).IndexOf('M');

                    if (timeformat.ToLower() == "hhmmss")
                    {
                        timeFie = time.Substring(0, 2) + ":" + time.Substring(2, 2) + ":" + time.Substring(4, 2);
                    }
                    else if (timeformat.ToLower() == "hh:mm:ss")
                    {
                        timeFie = time.Substring(0, 2) + ":" + time.Substring(3, 2) + ":" + time.Substring(6, 2);
                    }
                    else if (timeformat.ToLower() == "hhmm")
                    {
                        timeFie = time.Substring(0, 2) + ":" + time.Substring(2, 2);
                    }
                    else if (timeformat.ToLower() == "hh:mm")
                    {
                        timeFie = time.Substring(0, 2) + ":" + time.Substring(3, 2);
                    }
                    else if (timeformat.ToLower() == "h:mm:ss")
                    {
                        timeFie = time.Substring(0, 1) + ":" + time.Substring(2, 2) + ":" + time.Substring(5, 2);
                    }
                    else if (timeformat.ToLower() == "h:mm")
                    {
                        timeFie = time.Substring(0, 1) + ":" + time.Substring(2, 2);
                    }
                    //if (timeLength == 6)
                    //{
                    //    secStartIndex = (timersTextDetails[compIndex, 13].ToUpper()).IndexOf('S');
                    //    timeFie = time.Substring(hourStartIndex, 2) + ":" + time.Substring(minStartIndex, 2) + ":" + time.Substring(secStartIndex, 2);
                    //}
                    //else
                    //{

                    //    timeFie = time.Substring(hourStartIndex, 2) + ":" + time.Substring(minStartIndex, 2);
                    //}

                    if (timersTextDetails[compIndex, 15] == "ref")
                    {
                        emp_id = fnGetRefCardEmp(timersTextDetails[compIndex, 1], "ref", emp_id);
                    }
                    else if (timersTextDetails[compIndex, 15] == "card")
                    {
                        emp_id = fnGetRefCardEmp(timersTextDetails[compIndex, 1], "card", emp_id);
                    }
                    else if (timersTextDetails[compIndex, 15] == "emp")
                    {
                        emp_id = fnGetRefCardEmp(timersTextDetails[compIndex, 1], "emp", emp_id);
                    }
                    //MessageBox.Show("empid : "+emp_id+"\nLogDate : "+dateFie+"\nLogTime : "+timeFie+"\nDevid : "+dev_id);

                    if (emp_id != "")
                    {
                        try
                        {
                            string strConn = "server=" + strHost + ";database=" + strDB + ";uid=" + strUsr + ";pwd=" + strPWD;
                            MySqlConnection myConn = new MySqlConnection(strConn);
                            MySqlCommand myCommand = myConn.CreateCommand();
                            myConn.Open();
                            if (fnCheckDuplicate(emp_id, dateFie, timeFie, dev_id))
                            {
                                myCommand.CommandText = "insert into `kal_logdetails` (EMPID, LOGDATE, LOGTIME, DEVICE_ID) values('" +
                                               emp_id + "', '" +
                                               dateFie + "', '" +
                                               timeFie + "', " +
                                               dev_id + ")";
                                myCommand.ExecuteNonQuery();
                            }

                            myCommand.CommandText = "SELECT COUNT(*) FROM `kal_logdetails`";
                            lbDbRec.Text = (Convert.ToInt32(myCommand.ExecuteScalar())).ToString();
                            lbDbRec.Update();

                            myConn.Close();
                            Application.DoEvents();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    Application.DoEvents();

                    //counter++;
                    //startRowInd++;
                    //timersTextDetails[compIndex, 11] = startRowInd.ToString();

                    //SqlCeConnection sqlConnection1 = new SqlCeConnection();
                    //sqlConnection1.ConnectionString = dataSourcePath;

                    //SqlCeCommand cmd = new SqlCeCommand();
                    //cmd.CommandType = System.Data.CommandType.Text;
                    //cmd.Connection = sqlConnection1;
                    //sqlConnection1.Open();
                    //cmd.CommandText = "UPDATE TEXT_FILE_SETTINGS SET TEXT_START_ROW=" + startRowInd.ToString() + " WHERE TEXT_COM_ID=" + compIdVal.ToString();
                    //cmd.ExecuteNonQuery();

                    //sqlConnection1.Dispose();
                    //sqlConnection1.Close();
                    //Application.DoEvents();
                    //}
                    //else
                    //    counter++;

                    //if (fromUpload == true)
                    //countRows++;
                }
                catch (Exception eee)
                {
                    //MessageBox.Show(eee.Message);
                }
            }
            file.Close();
        }

        private string fnGetDateFormat(string datefromat)
        {
            string datestring = "";
            DateTime dt = DateTime.Now;
            if (datefromat == "ddmmyy")
                datestring = dt.ToString("ddMMyy");
            else if (datefromat == "yymmdd")
                datestring = dt.ToString("yyMMdd");
            else if (datefromat == "ddmmyyyy")
                datestring = dt.ToString("ddMMyyyy");
            else if (datefromat == "yyyymmdd")
                datestring = dt.ToString("yyyyMMdd");
            else if (datefromat == "dd/mm/yyyy")
                datestring = dt.ToString("dd/MM/yyyy");
            else if (datefromat == "yyyy/mm/dd")
                datestring = dt.ToString("yyyy/MM/dd");
            else if (datefromat == "dd/mm/yy")
                datestring = dt.ToString("dd/MM/yy");
            else if (datefromat == "yy/mm/dd")
                datestring = dt.ToString("yy/MM/dd");
            else if (datefromat == "dd-mm-yyyy")
                datestring = dt.ToString("dd-MM-yyyy");
            else if (datefromat == "yyyy-mm-dd")
                datestring = dt.ToString("yyyy-MM-dd");
            else if (datefromat == "dd-mm-yy")
                datestring = dt.ToString("dd-MM-yy");
            else if (datefromat == "yy-mm-dd")
                datestring = dt.ToString("yy-MM-dd");
            else if (datefromat == "d/m/yyyy")
                datestring = dt.ToString("d/M/yyyy");
            else if (datefromat == "yyyy/m/d")
                datestring = dt.ToString("yyyy/M/d");
            else if (datefromat == "d/m/yy")
                datestring = dt.ToString("d/M/yy");
            else if (datefromat == "yy/m/d")
                datestring = dt.ToString("yy/M/d");
            else if (datefromat == "d-m-yyyy")
                datestring = dt.ToString("d-M-yyyy");
            else if (datefromat == "yyyy-m-d")
                datestring = dt.ToString("yyyy-M-d");
            else if (datefromat == "d-m-yy")
                datestring = dt.ToString("d-M-yy");
            else if (datefromat == "yy-m-d")
                datestring = dt.ToString("yy-M-d");
            else if (datefromat == "d/mm/yyyy")
                datestring = dt.ToString("d/MM/yyyy");
            else if (datefromat == "yyyy/mm/d")
                datestring = dt.ToString("yyyy/MM/d");
            else if (datefromat == "d/mm/yy")
                datestring = dt.ToString("d/MM/yy");
            else if (datefromat == "yy/mm/d")
                datestring = dt.ToString("yy/MM/d");
            else if (datefromat == "d-mm-yyyy")
                datestring = dt.ToString("d-MM-yyyy");
            else if (datefromat == "yyyy-mm-d")
                datestring = dt.ToString("yyyy-MM-d");
            else if (datefromat == "d-mm-yy")
                datestring = dt.ToString("d-MM-yy");
            else if (datefromat == "yy-mm-d")
                datestring = dt.ToString("yy-MM-d");

            return datestring;
        }

        private bool fnCheckDuplicate(string emp_id,string date,string time,string dev_id)
        {
            //fnValidateURL(3);
            string strConn = "server=" + strHost + ";database=" + strDB + ";uid=" + strUsr + ";pwd=" + strPWD;
            MySqlConnection myConn = new MySqlConnection(strConn);
            MySqlCommand myCommand = myConn.CreateCommand();
            myConn.Open();
            Application.DoEvents();
            bool val = true;
            try
            {
                myCommand.CommandText = "SELECT COUNT(*) FROM `kal_logdetails` WHERE EMPID='" + emp_id + "' AND LOGDATE='" + date + "' AND LOGTIME='" + time + "' AND DEVICE_ID=" + dev_id;
                int count = Convert.ToInt32(myCommand.ExecuteScalar());
                if (count >= 1)
                    val = false;
                Application.DoEvents();
            }
            catch { }
            myConn.Close();
            return val;
        }

        private void fnOnlineConnectVariables(string url)
        {
            try
            {
                string strURL = url + "auto_db_read_kanaily.php";
                byte[] buffer = Encoding.ASCII.GetBytes("SECODE=ranjeetkumarkanaily&DBSETTINGS=autosynchronization");
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(strURL);
                WebReq.Timeout = 108000000;
                WebReq.Method = "POST";
                WebReq.ContentType = "application/x-www-form-urlencoded";
                WebReq.ContentLength = buffer.Length;
                Stream PostData = WebReq.GetRequestStream();
                PostData.Write(buffer, 0, buffer.Length);
                PostData.Close();
                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
                Stream Answer = WebResp.GetResponseStream();
                StreamReader _Answer = new StreamReader(Answer);
                string msg = _Answer.ReadToEnd().ToString();
                _Answer.Dispose();
                msg = msg.Trim();
                string[] arrDB = msg.Split(new char[] { ',' });
                //if (CNull(arrDB[1]) == "")
                //{
                //    strHost = CNull(arrDB[0]);
                //}
                //else
                //{
                if (CNull(arrDB[1]) == "")
                    MessageBox.Show("Host IP is not set in common DB file");
                else
                    strHost = CNull(arrDB[1]);
                //}
                //Global.strHost = CNull(arrDB[1]);
                strUsr = CNull(arrDB[2]);
                strPWD = CNull(arrDB[3]);
                strDB = CNull(arrDB[4]);
            }
            catch (WebException web)
            {
                //MessageBox.Show(web.Message);
            }
            catch (UriFormatException uriex)
            {
                //MessageBox.Show(uriex.Message);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private string fnGetRefCardEmp(string url, string whi,string fie)
        {
            string emp = "";
            //fnOnlineConnectVariables(url);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string strConn = "server=" + strHost + ";database=" + strDB + ";uid=" + strUsr + ";pwd=" + strPWD;
            //MessageBox.Show(strConn);
            MySqlConnection myConn = new MySqlConnection(strConn);
            try
            {
                MySqlCommand myCommand = myConn.CreateCommand();
                myConn.Open();

                if (whi == "ref")
                {
                    myCommand.CommandText = "SELECT EMPID FROM mas_employee WHERE REFNO= '" + fie + "'";
                }

                if (whi == "card")
                {
                    myCommand.CommandText = "SELECT EMPID FROM mas_users WHERE CARDID= '" + fie + "'";
                }

                if (whi == "emp")
                {
                    myCommand.CommandText = "SELECT EMPID FROM mas_users WHERE EMPID= '" + fie + "'";
                }
                MySqlDataReader SQLRD;
                SQLRD = myCommand.ExecuteReader();
                int val = 0;
                while (SQLRD.Read())
                {
                    val++;
                    emp = SQLRD["EMPID"].ToString();
                }
                SQLRD.Close();
                myConn.Close();

                if(val==0)
                    emp = "";
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error 3: " + ex.Message);
            }
            return emp;
        }

        //not used
        private void btnStart_Click(object sender, EventArgs e)
        {
            frmManageTnA_Load(null, null);
        }

        private void btnResumeAll_Click(object sender, EventArgs e)
        {
            if (groupBox1.Enabled == true)
            {
                DateTime curr = DateTime.Now;
                TimeSpan elap = curr - now1;
                if (((TimeTimer1Flag) - elap.Minutes) < 0)
                    now1 = DateTime.Now;
                timer1.Enabled = true;
            }
            if (groupBox2.Enabled == true)
            {
                DateTime curr = DateTime.Now;
                TimeSpan elap = curr - now2;
                if (((TimeTimer2Flag) - elap.Minutes) < 0)
                    now2 = DateTime.Now;
                timer2.Enabled = true;
            }
            if (groupBox3.Enabled == true)
            {
                DateTime curr = DateTime.Now;
                TimeSpan elap = curr - now3;
                if (((TimeTimer3Flag) - elap.Minutes) < 0)
                    now3 = DateTime.Now;
                timer3.Enabled = true;
            }
            if (groupBox4.Enabled == true)
            {
                DateTime curr = DateTime.Now;
                TimeSpan elap = curr - now4;
                if (((TimeTimer4Flag) - elap.Minutes) < 0)
                    now4 = DateTime.Now;
                timer4.Enabled = true;
            }
            if (groupBox5.Enabled == true)
            {
                DateTime curr = DateTime.Now;
                TimeSpan elap = curr - now5;
                if (((TimeTimer5Flag) - elap.Minutes) < 0)
                    now5 = DateTime.Now;
                timer5.Enabled = true;
            }
            if (groupBox10.Enabled == true)
            {
                DateTime curr = DateTime.Now;
                TimeSpan elap = curr - now6;
                if (((TimeTimer6Flag) - elap.Minutes) < 0)
                    now6 = DateTime.Now;
                timer6.Enabled = true;
            }
            if (groupBox9.Enabled == true)
            {
                DateTime curr = DateTime.Now;
                TimeSpan elap = curr - now7;
                if (((TimeTimer7Flag) - elap.Minutes) < 0)
                    now7 = DateTime.Now;
                timer7.Enabled = true;
            }
            if (groupBox8.Enabled == true)
            {
                DateTime curr = DateTime.Now;
                TimeSpan elap = curr - now8;
                if (((TimeTimer8Flag) - elap.Minutes) < 0)
                    now8 = DateTime.Now;
                timer8.Enabled = true;
            }
            if (groupBox7.Enabled == true)
            {
                DateTime curr = DateTime.Now;
                TimeSpan elap = curr - now9;
                if (((TimeTimer9Flag) - elap.Minutes) < 0)
                    now9 = DateTime.Now;
                timer9.Enabled = true;
            }
            if (groupBox6.Enabled == true)
            {
                DateTime curr = DateTime.Now;
                TimeSpan elap = curr - now10;
                if (((TimeTimer10Flag) - elap.Minutes) < 0)
                    now10 = DateTime.Now;
                timer10.Enabled = true;
            }
        }

        private void btnPauseAll_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            timer4.Enabled = false;
            timer5.Enabled = false;
            timer6.Enabled = false;
            timer7.Enabled = false;
            timer8.Enabled = false;
            timer9.Enabled = false;
            timer10.Enabled = false;
        }

        #region Pause Timers

        private void btnPause1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void btnPause2_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
        }

        private void btnPause3_Click(object sender, EventArgs e)
        {
            timer3.Enabled = false;
        }

        private void btnPause4_Click(object sender, EventArgs e)
        {
            timer4.Enabled = false;
        }

        private void btnPause5_Click(object sender, EventArgs e)
        {
            timer5.Enabled = false;
        }

        private void btnPause6_Click(object sender, EventArgs e)
        {
            timer6.Enabled = false;
        }

        private void btnPause7_Click(object sender, EventArgs e)
        {
            timer7.Enabled = false;
        }

        private void btnPause8_Click(object sender, EventArgs e)
        {
            timer8.Enabled = false;
        }

        private void btnPause9_Click(object sender, EventArgs e)
        {
            timer9.Enabled = false;
        }

        private void btnPause10_Click(object sender, EventArgs e)
        {
            timer10.Enabled = false;
        }

        #endregion

        #region Resume Timers

        private void btnResume1_Click(object sender, EventArgs e)
        {
            DateTime curr = DateTime.Now;
            TimeSpan elap = curr - now1;
            if (((TimeTimer1Flag) - elap.Minutes) < 0)
                now1 = DateTime.Now;
            timer1.Enabled = true;
        }

        private void btnResume2_Click(object sender, EventArgs e)
        {
            DateTime curr = DateTime.Now;
            TimeSpan elap = curr - now2;
            if (((TimeTimer2Flag) - elap.Minutes) < 0)
                now2 = DateTime.Now;
            timer2.Enabled = true;
        }

        private void btnResume3_Click(object sender, EventArgs e)
        {
            DateTime curr = DateTime.Now;
            TimeSpan elap = curr - now3;
            if (((TimeTimer3Flag) - elap.Minutes) < 0)
                now3 = DateTime.Now;
            timer3.Enabled = true;
        }

        private void btnResume4_Click(object sender, EventArgs e)
        {
            DateTime curr = DateTime.Now;
            TimeSpan elap = curr - now4;
            if (((TimeTimer4Flag) - elap.Minutes) < 0)
                now4 = DateTime.Now;
            timer4.Enabled = true;
        }

        private void btnResume5_Click(object sender, EventArgs e)
        {
            DateTime curr = DateTime.Now;
            TimeSpan elap = curr - now5;
            if (((TimeTimer5Flag) - elap.Minutes) < 0)
                now5 = DateTime.Now;
            timer5.Enabled = true;
        }

        private void btnResume6_Click(object sender, EventArgs e)
        {
            DateTime curr = DateTime.Now;
            TimeSpan elap = curr - now6;
            if (((TimeTimer6Flag) - elap.Minutes) < 0)
                now6 = DateTime.Now;
            timer6.Enabled = true;
        }

        private void btnResume7_Click(object sender, EventArgs e)
        {
            DateTime curr = DateTime.Now;
            TimeSpan elap = curr - now7;
            if (((TimeTimer7Flag) - elap.Minutes) < 0)
                now7 = DateTime.Now;
            timer7.Enabled = true;
        }

        private void btnResume8_Click(object sender, EventArgs e)
        {
            DateTime curr = DateTime.Now;
            TimeSpan elap = curr - now8;
            if (((TimeTimer8Flag) - elap.Minutes) < 0)
                now8 = DateTime.Now;
            timer8.Enabled = true;
        }

        private void btnResume9_Click(object sender, EventArgs e)
        {
            DateTime curr = DateTime.Now;
            TimeSpan elap = curr - now9;
            if (((TimeTimer9Flag) - elap.Minutes) < 0)
                now9 = DateTime.Now;
            timer9.Enabled = true;
        }

        private void btnResume10_Click(object sender, EventArgs e)
        {
            DateTime curr = DateTime.Now;
            TimeSpan elap = curr - now10;
            if (((TimeTimer10Flag) - elap.Minutes) < 0)
                now10 = DateTime.Now;
            timer10.Enabled = true;
        }

        #endregion

        private void frmManageTnA_Resize(object sender, EventArgs e)
        {
            //btnPauseAll_Click(null, null);
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(30, "Time and Attendence", "Your application has been minimized to the taskbar.", ToolTipIcon.Info);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon.Visible = true;
            }
            //btnResumeAll_Click(null, null);
        }

        private void frmManageTnA_FormClosing(object sender, FormClosingEventArgs e)
        {
            //btnPauseAll_Click(null, null);
            if (closeVal != true)
            {
                if (FormWindowState.Normal == this.WindowState || FormWindowState.Minimized == this.WindowState)
                {
                    e.Cancel = true;
                    this.WindowState = FormWindowState.Minimized;
                    notifyIcon.ShowBalloonTip(30, "Time and Attendence", "Your application has been minimized to the taskbar.", ToolTipIcon.Info);
                    this.Hide();
                }
            }
            //btnResumeAll_Click(null, null);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeVal = true;
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About frm = new About();
            frm.ShowDialog();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private bool fnChkSyncExe()
        {
            bool isRunning = false;
            //Process[] allpro = Process.GetProcesses();
            Process[] localexeByName = Process.GetProcessesByName("Time_and_Attendance");

            if (localexeByName.Length > 1)
            {
                isRunning = true;
            }
            return isRunning;
        }

        //not used
        private void button2_Click(object sender, EventArgs e)
        {
            lbFileError.Visible = false;
            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Text == "Company's")
            {
                //fromUpdates = "yes";
                lbFileError.Visible = true;
                lbFileError.Text = "Select Company from the TreeView";
                return;
            }

            if (txtTextURL.Text == "" && txtFilePath.Text == "")
            {
                if (txtURL.Text != "")
                {
                    lbFileError.Visible = true;
                    lbFileError.Text = "This Company Registered with MDB file.Cannot Read as text file";
                    return;
                }
                else
                {
                    lbFileError.Visible = true;
                    lbFileError.Text = "This Company settings not done.";
                    return;
                }
            }
            else
            {
                int cid = 0;
                int startRowInd = 1;

                string act = "";
                SqlCeConnection sqlConnection1 = new SqlCeConnection();
                sqlConnection1.ConnectionString = dataSourcePath;
                SqlCeCommand cmd = new SqlCeCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM CompRegister WHERE CO_COMP_NAME='" + treeView1.SelectedNode.Text + "'";
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();
                SqlCeDataReader dataRead;
                dataRead = cmd.ExecuteReader();
                dataRead.Read();
                cid = Convert.ToInt32(dataRead[0]);
                if (fnGetActiveStatus(cid))
                {
                    act = "yes";
                }
                dataRead.Dispose();
                dataRead.Close();

                if (act == "yes")
                {
                    string nul = "";
                    string line;
                    string dev_date = "";
                    string dateFie = "";
                    string time = "";
                    string timeFie = "";
                    string emp_id = "";
                    string dev_id = "";
                    int counter = 1;

                    cmd.CommandText = "SELECT * FROM TEXT_FILE_SETTINGS WHERE TEXT_COM_ID=" + cid.ToString();
                    SqlCeDataReader dataRead1;
                    dataRead1 = cmd.ExecuteReader();
                    dataRead1.Read();

                    startRowInd = Convert.ToInt32(dataRead1[11]);
                    int compIdVal = cid;
                    System.IO.StreamReader file = new System.IO.StreamReader(dataRead1[2].ToString());
                    while ((line = file.ReadLine()) != null)
                    {
                        bool val = true;
                        int dayStartIndex = 0;
                        int monthStartIndex = 0;
                        int yearStartIndex = 0;
                        int yearLen = 0;

                        int hourStartIndex = 0;
                        int minStartIndex = 0;
                        int secStartIndex = 0;
                        int timeLength = 0;
                        if (startRowInd == counter)
                        {
                            emp_id = line.Substring(Convert.ToInt32(dataRead1[3]), Convert.ToInt32(dataRead1[4]));
                            dev_date = line.Substring(Convert.ToInt32(dataRead1[5]), Convert.ToInt32(dataRead1[6]));
                            time = line.Substring(Convert.ToInt32(dataRead1[7]), Convert.ToInt32(dataRead1[8]));
                            if (Convert.ToInt32(dataRead1[10]) != Convert.ToInt32(dataRead1[9]))
                            {
                                dev_id = line.Substring(Convert.ToInt32(dataRead1[9]), Convert.ToInt32(dataRead1[10]));
                            }

                            dayStartIndex = (dataRead1[12].ToString().ToUpper()).IndexOf('D');
                            monthStartIndex = (dataRead1[12].ToString().ToUpper()).IndexOf('M');
                            yearStartIndex = (dataRead1[12].ToString().ToUpper()).IndexOf('Y');
                            yearLen = dev_date.Length;

                            if (yearLen == 6)
                            {
                                dateFie = dev_date.Substring(yearStartIndex, 2) + "-" + dev_date.Substring(monthStartIndex, 2) + "-" + dev_date.Substring(dayStartIndex, 2);
                            }
                            else if (yearLen == 8)
                            {
                                dateFie = dev_date.Substring(yearStartIndex, 4) + "-" + dev_date.Substring(monthStartIndex, 2) + "-" + dev_date.Substring(dayStartIndex, 2);
                            }

                            timeLength = time.Length;
                            hourStartIndex = (dataRead1[13].ToString().ToUpper()).IndexOf('H');
                            minStartIndex = (dataRead1[13].ToString().ToUpper()).IndexOf('M');
                            if (timeLength == 6)
                            {
                                secStartIndex = (dataRead1[13].ToString().ToUpper()).IndexOf('S');
                                timeFie = time.Substring(hourStartIndex, 2) + ":" + time.Substring(minStartIndex, 2) + ":" + time.Substring(secStartIndex, 2);
                            }
                            else
                            {

                                timeFie = time.Substring(hourStartIndex, 2) + ":" + time.Substring(minStartIndex, 2);
                            }
                            fnOnlineConnectVariables(dataRead1[1].ToString());
                            if ((dataRead1[15].ToString()) == "ref")
                            {
                                emp_id = fnGetRefCardEmp((dataRead1[1].ToString()), "ref", emp_id);
                            }
                            else if ((dataRead1[15].ToString()) == "card")
                            {
                                emp_id = fnGetRefCardEmp((dataRead1[1].ToString()), "card", emp_id);
                            }
                            else if ((dataRead1[15].ToString()) == "emp")
                            {
                                emp_id = fnGetRefCardEmp((dataRead1[1].ToString()), "emp", emp_id);
                            }
                            if (emp_id != "")
                            {
                                string strConn = "server=" + strHost + ";database=" + strDB + ";uid=" + strUsr + ";pwd=" + strPWD;
                                MySqlConnection myConn = new MySqlConnection(strConn);
                                MySqlCommand myCommand = myConn.CreateCommand();
                                myConn.Open();

                                if (fnCheckDuplicate(emp_id, dateFie, timeFie, dev_id))
                                {
                                    myCommand.CommandText = "insert into `kal_logdetails` (EMPID, LOGDATE, LOGTIME, DEVICE_ID) values('" +
                                                   emp_id + "', '" +
                                                   dateFie + "', '" +
                                                   timeFie + "', " +
                                                   dev_id + ")";
                                    myCommand.ExecuteNonQuery();
                                }

                                myConn.Close();
                                Application.DoEvents();
                            }

                            counter++;
                            startRowInd++;
                            fnupdateStartRow(startRowInd, cid);
                        }
                        else
                            counter++;

                        Application.DoEvents();
                    }

                    dataRead1.Dispose();
                    dataRead1.Close();

                    file.Dispose();
                    file.Close();
                }
                else
                {
                    lbFileError.Visible = true;
                    lbFileError.Text = "Company is not active.";
                }
                sqlConnection1.Dispose();
                sqlConnection1.Close();
                //if (act == "yes")
                //{
                //    SqlCeConnection sqlConnection2 = new SqlCeConnection();
                //    sqlConnection2.ConnectionString = dataSourcePath;

                //    SqlCeCommand cmd1 = new SqlCeCommand();
                //    cmd1.CommandType = System.Data.CommandType.Text;
                //    cmd1.Connection = sqlConnection2;
                //    sqlConnection2.Open();
                //    cmd1.CommandText = "UPDATE TEXT_FILE_SETTINGS SET TEXT_START_ROW=" + startRowInd + " WHERE TEXT_COM_ID=" + cid;
                //    cmd1.ExecuteNonQuery();

                //    sqlConnection2.Dispose();
                //    sqlConnection2.Close();
                //    Application.DoEvents();
                //    lbFileError.Visible = true;
                //    lbFileError.Text = "Data Read Successfully";
                //}

            }
        }

        private void fnupdateStartRow(int startRow, int cid)
        {
            Application.DoEvents();
            SqlCeConnection sqlConnection2 = new SqlCeConnection();
            sqlConnection2.ConnectionString = dataSourcePath;

            SqlCeCommand cmd1 = new SqlCeCommand();
            cmd1.CommandType = System.Data.CommandType.Text;
            cmd1.Connection = sqlConnection2;
            sqlConnection2.Open();
            cmd1.CommandText = "UPDATE TEXT_FILE_SETTINGS SET TEXT_START_ROW=" + startRow.ToString() + " WHERE TEXT_COM_ID=" + cid.ToString();
            cmd1.ExecuteNonQuery();

            sqlConnection2.Dispose();
            sqlConnection2.Close();
            Application.DoEvents();
            lbFileError.Visible = true;
            lbFileError.Text = "Data Read Successfully";
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            lbSqlError.Text = "";
            string strConn = "";

            bool validServer = true;
            bool validDB = true;

            if (txtServer.Text != "" && txtDB.Text != "")
            {
                strConn = "Integrated Security=SSPI;Data Source=" + txtServer.Text;
                //strConn = "Data Source=192.168.0.252,1433;Network Library=DBMSSOCN;";
                SqlConnection ConnEx = new SqlConnection(strConn);
                try
                {
                    ConnEx.Open();
                }
                catch(Exception ex)
                {
                    validServer = false;
                    lbSqlError.Text = "Incorrect ServerName."+ex.Message;
                    return;
                }

                strConn = strConn + ";Persist Security Info=False;Initial Catalog=" + txtDB.Text;

                //strConn = "Data Source=192.168.0.252,1433;Network Library=DBMSSOCN;Initial Catalog="+txtDB.Text;
                
                ConnEx = new SqlConnection(strConn);
                try
                {
                    ConnEx.Open();
                }
                catch (Exception ex)
                {
                    validDB = false;
                    lbSqlError.Text = "Incorrect Database Name";
                    return;
                }

                strConn = "";
                if (validServer == true && validDB == true)
                {
                    if ((cmbDB.SelectedIndex) == 0)//
                    {
                        //strConn = "Data Source=192.168.0.252,1433;Network Library=DBMSSOCN;Initial Catalog=" + txtDB.Text + "; User ID=sa;Password=sa;";
                        strConn = "Integrated Security=SSPI;Data Source=" + @txtServer.Text + ";Persist Security Info=False;Initial Catalog=" + txtDB.Text;
                    }
                    else
                    {
                        if (txtUser.Text != "" && txtPass.Text != "")
                        {
                            //strConn = "Data Source=192.168.0.252,1433;Network Library=DBMSSOCN;Initial Catalog=" + txtDB.Text + "; User ID=" + txtUser.Text + ";Password=" + txtPass.Text + ";";
                            strConn = "Server=" + @txtServer.Text + ";Persist Security Info=True;uid=" + txtUser.Text + ";pwd=" + txtPass.Text + ";Database=" + txtDB.Text;
                        }
                        else
                        {
                            if (txtUser.Text == "")
                            {
                                lbSqlError.Text = "Enter Username";
                                return;
                            }
                            else if (txtPass.Text == "")
                            {
                                lbSqlError.Text = "Enter Password";
                                return;
                            }
                        }
                    }
                    SqlConnection Conn = new SqlConnection(strConn);
                    try
                    {
                        Conn.Open();
                        Global.SQLConnStr = strConn;
                        Global.SQLServer = txtServer.Text;
                        Global.SQLDatabase = txtDB.Text;
                        lbSqlError.Text = "Connected to Database";
                        frmSqlTabColSelect frm = new frmSqlTabColSelect();
                        frm.ShowDialog();
                        if (Global.SetSQLValues != "")
                        {
                            lbSqlError.Visible = true;
                            lbSqlError.Text = "SQL Data Settings done";
                        }
                        else
                        {
                            lbSqlError.Visible = true;
                            lbSqlError.Text = "SQL Data Settings not done";
                        }

                    }
                    catch //(SqlException sqex)
                    {
                        lbSqlError.Visible = true;
                        lbSqlError.Text = "Enter Correct username/Password";
                        return;
                    }

                }

            }
            else
            {
                //txtServer.Text != "" && txtDB.Text != ""
                if (txtServer.Text == "")
                {
                    lbSqlError.Text = "Enter SERVER Name";
                    return;
                }
                else if (txtDB.Text == "")
                {
                    lbSqlError.Text = "Enter Database Name";
                    return;
                }
            }
        }

        private void cmbDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDB.SelectedIndex == 1)
            {
                groupBox13.Enabled = true;
            }
            else
                groupBox13.Enabled = false;
        }

        private void btnSQLSave_Click(object sender, EventArgs e)
        {
            lbSqlSettingsError.Visible = false;
            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Text == "Company's")
            {
                fromUpdates = "yes";
                lbSqlSettingsError.Visible = true;
                lbSqlSettingsError.Text = "Select Company from the TreeView";
                return;
            }
            settingsCompanyID = fnGetCompanyId();
            lbSqlSettingsError.Visible = false;
            string sel = treeView1.SelectedNode.Text;
            if (sel != "Company's" && sel != "")
            {
                settingsCompanyID = fnGetCompanyId();
                if (fnValidateSqlSettings())
                {
                    //MessageBox.Show("All Fields are valid");

                    //Prefix-Suffix
                    if (rdbnSqlPre.Checked == true)
                        settingsSqlPreSuf = "pre";
                    else if (rdbnSqlSuf.Checked == true)
                        settingsSqlPreSuf = "suf";
                    else
                        settingsSqlPreSuf = "no";

                    //EmpId-Ref.No-CardId
                    if (rdbnSqlEmp.Checked == true)
                        settingsSqlEmpRefCard = "emp";
                    else if (rdbnSqRef.Checked == true)
                        settingsSqlEmpRefCard = "ref";
                    else if (rdbnSqlCard.Checked == true)
                        settingsSqlEmpRefCard = "card";

                    //Enter Settings in table
                    /////////  1   //////////
                    settingsCompanyID = fnGetCompanyId();
                    settingsSqlUrl = txtSqlUrl.Text.Trim();
                    //txtSqlTime
                    //txtSqlFieLen
                    int rowVal = fnGetTASettingsRow(2);
                    if (rowVal == 0)
                    {
                        try
                        {
                            SqlCeConnection sqlConnection1 = new SqlCeConnection();
                            sqlConnection1.ConnectionString = dataSourcePath;

                            SqlCeCommand cmd = new SqlCeCommand();
                            cmd.CommandType = System.Data.CommandType.Text;
                            string s = "INSERT INTO SQL_DATA_SETTINGS (SQL_COMP_ID, SQL_URL, SQL_TIME_INT, SQL_FIE_LEN, SQL_PRE_SUF, SQL_EMP_REF_CARD) VALUES(" + settingsCompanyID.ToString() + ",'" + settingsSqlUrl + "'," + settingsSqlTimeInt.ToString() + "," + settingsSqlFieLen.ToString() + ",'" + settingsSqlPreSuf + "','" + settingsSqlEmpRefCard + "')";
                            cmd.CommandText = s;
                            //MessageBox.Show(s);
                            cmd.Connection = sqlConnection1;

                            sqlConnection1.Open();
                            cmd.ExecuteNonQuery();
                            s = "INSERT INTO SQL_DATA_FIELD_SETTINGS (SQL_TAB_COMP_ID, SQL_TAB_SERVER, SQL_TAB_DB, SQL_TAB_AUTH_TYPE, SQL_TAB_USERNAME, SQL_TAB_PASSWORD, SQL_TAB_TABLE, SQL_TAB_EMP, SQL_TAB_DEVICE, SQL_TAB_DATETIME_INOUT, SQL_TAB_DATETIME_IN, SQL_TAB_DATETIME_OUT, SQL_TAB_DATE, SQL_TAB_DATE_INOUT, SQL_TAB_DATE_IN, SQL_TAB_DATE_OUT, SQL_TAB_INDEX) VALUES(" + settingsCompanyID.ToString() + ",'" + settingsSqlServeName + "','" + settingsSqlDatabase + "','" + settingsSqlAuthType + "','" + settingsSqlUsername + "','" + settingsSqlPassword + "','" + Global.tableName + "','" + Global.EmpIdFieldName + "','" + Global.DeviceFieldName + "','" + Global.DateTimeInOutFieldName + "','" + Global.DateTimeInFieldName + "','" + Global.DateTimeOutFieldName + "','" + Global.DateFieldName + "','" + Global.InOutFieldName + "','" + Global.InFieldName + "','" + Global.OutFieldName + "',0)";
                            cmd.CommandText = s;
                            //MessageBox.Show(s);
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "UPDATE CompRegister SET CO_FILE_TYPE='sql' WHERE CO_COMP_ID=" + settingsCompanyID.ToString();
                            cmd.ExecuteNonQuery();

                            sqlConnection1.Dispose();
                            sqlConnection1.Close();
                            lbSqlSettingsError.Visible = true;
                            lbSqlSettingsError.Text = "Settings added";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        try
                        {
                        SqlCeConnection sqlConnection2 = new SqlCeConnection();
                        sqlConnection2.ConnectionString = dataSourcePath;

                        SqlCeCommand cmd1 = new SqlCeCommand();
                        
                            sqlConnection2.Open();
                            cmd1.CommandType = System.Data.CommandType.Text;
                            cmd1.CommandText = "UPDATE SQL_DATA_SETTINGS SET SQL_URL='" + settingsSqlUrl + "',SQL_TIME_INT=" + settingsSqlTimeInt.ToString() + ",SQL_FIE_LEN=" + settingsSqlFieLen.ToString() + ",SQL_PRE_SUF='" + settingsSqlPreSuf + "',SQL_EMP_REF_CARD='" + settingsSqlEmpRefCard + "' WHERE SQL_COMP_ID=" + settingsCompanyID.ToString();
                            cmd1.Connection = sqlConnection2;
                            cmd1.ExecuteNonQuery();

                            cmd1.CommandText = "UPDATE CompRegister SET CO_FILE_TYPE='sql' WHERE CO_COMP_ID=" + settingsCompanyID.ToString();
                            cmd1.ExecuteNonQuery();
                            if (Global.SetSQLValues == "yes" || Global.SetSQLValues != "y")
                            {
                                cmd1.CommandText = "UPDATE SQL_DATA_FIELD_SETTINGS SET SQL_TAB_SERVER='" + settingsSqlServeName + "', SQL_TAB_DB='" + settingsSqlDatabase + "', SQL_TAB_AUTH_TYPE='" + settingsSqlAuthType + "', SQL_TAB_USERNAME='" + settingsSqlUsername + "', SQL_TAB_PASSWORD='" + settingsSqlPassword + "', SQL_TAB_TABLE='" + Global.tableName + "', SQL_TAB_EMP='" + Global.EmpIdFieldName + "', SQL_TAB_DEVICE='" + Global.DeviceFieldName + "', SQL_TAB_DATETIME_INOUT='" + Global.DateTimeInOutFieldName + "', SQL_TAB_DATETIME_IN='" + Global.DateTimeInFieldName + "', SQL_TAB_DATETIME_OUT='" + Global.DateTimeOutFieldName + "', SQL_TAB_DATE='" + Global.DateFieldName + "', SQL_TAB_DATE_INOUT='" + Global.InOutFieldName + "', SQL_TAB_DATE_IN='" + Global.InFieldName + "', SQL_TAB_DATE_OUT='" + Global.OutFieldName + "', SQL_TAB_INDEX=0 WHERE SQL_TAB_COMP_ID=" + settingsCompanyID.ToString();
                                cmd1.ExecuteNonQuery();
                            }
                        sqlConnection2.Dispose();
                        sqlConnection2.Close();
                        lbSqlSettingsError.Visible = true;
                        lbSqlSettingsError.Text = "Settings Updated";
                        btnTextToOther.Enabled = true;
                        btnTextToOther.Visible = false;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                    return;
            }
            else
            {
                lbSqlSettingsError.Visible = true;
                lbSqlSettingsError.Text = "Select Company from the TreeView";
                fromUpdates = "yes";
                treeView1.Focus();
            }
            try
            {
                //treeView1_AfterSelect(null, null);
            }
            catch { }
            Global.SetSQLValues = "";//Changed
        }

        private bool fnValidateSqlSettings()
        {
            bool valid = true;
            lbSqlSettingsError.Visible = false;

            if (txtSqlUrl.Text == string.Empty)
            {
                lbSqlSettingsError.Visible = true;
                lbSqlSettingsError.Text = "Enter URL Path";
                txtSqlUrl.Focus();
                return false;
            }
            else if (txtSqlTime.Text == string.Empty)
            {
                fnValidateSql();
                if (settingsSqlUrl != "")
                {
                    lbSqlSettingsError.Visible = true;
                    lbSqlSettingsError.Text = "Enter Time Interval";
                    txtSqlTime.Focus();
                    return false;
                }
                else
                    return false;
            }
            else if (rdbnSqlPre.Checked == true || rdbnSqlSuf.Checked == true)
            {
                if (txtSqlFieLen.Text == string.Empty)
                {
                    fnValidateSql();
                    if (settingsSqlTimeInt != -1)
                    {
                        lbSqlSettingsError.Visible = true;
                        lbSqlSettingsError.Text = "Enter Field Length";
                        txtSqlFieLen.Focus();
                        return false;
                    }
                    else
                        return false;
                }
            }
            else if (Global.SetSQLValues == "")
            {
                fnValidateSql();
                if (settingsSqlFieLen != -1 && settingsSqlTimeInt != -1 && settingsSqlUrl != "")
                {
                    lbSqlSettingsError.Visible = true;
                    lbSqlSettingsError.Text = "Enter/Select SQL data settings";
                    btnConnect.Focus();
                    return false;
                }
                else
                    return false;
            }
            else
            {
                fnValidateSql();
                if (Global.SetSQLValues != "" && settingsSqlFieLen != -1 && settingsSqlTimeInt != -1 && settingsSqlUrl != "")
                    valid = true;
                else
                    valid = false;
            }
            //Global.SetSQLValues = "";
            return valid;
        }

        private void fnValidateSql()
        {
            if (fnValidateURL(4))
            {
                settingsSqlUrl = txtSqlUrl.Text.Trim();

                if (txtSqlTime.Text != "")
                {
                    settingsSqlTimeInt = 0;
                    try
                    {
                        settingsSqlTimeInt = Convert.ToInt32(txtSqlTime.Text);
                    }
                    catch
                    {
                        settingsSqlTimeInt = -1;
                        lbSqlSettingsError.Visible = true;
                        lbSqlSettingsError.Text = "Enter correct Time Interval";
                        txtSqlTime.SelectAll();
                        txtSqlTime.Focus();
                        return;
                    }
                    if (settingsSqlTimeInt != -1)
                    {
                        if (rdbnSqlPre.Checked == true || rdbnSqlSuf.Checked == true)
                        {
                            if (txtSqlFieLen.Text != string.Empty)
                            {
                                settingsSqlFieLen = 0;
                                try
                                {
                                    settingsSqlFieLen = Convert.ToInt32(txtSqlFieLen.Text);
                                }
                                catch
                                {
                                    settingsSqlFieLen = -1;
                                    lbSqlSettingsError.Visible = true;
                                    lbSqlSettingsError.Text = "Enter correct Field Length";
                                    txtSqlFieLen.SelectAll();
                                    txtSqlFieLen.Focus();
                                    return;
                                }
                            }
                            else
                            {
                                lbSqlSettingsError.Visible = true;
                                lbSqlSettingsError.Text = "Enter Field Length";
                                txtSqlFieLen.Focus();
                                return;
                            }
                        }

                        if (Global.SetSQLValues == "yes")
                        {
                            settingsSqlServeName = txtServer.Text.Trim();
                            settingsSqlDatabase = txtDB.Text.Trim();
                            settingsSqlAuthType = cmbDB.SelectedItem.ToString();
                            settingsSqlUsername = txtUser.Text.Trim();
                            settingsSqlPassword = txtPass.Text.Trim();
                        }
                        else
                        {
                            lbSqlSettingsError.Visible = true;
                            lbSqlSettingsError.Text = "Enter/Select Sql Data settings";
                            txtUser.Focus();
                        }
                    }
                }
            }
            else
            {
                settingsSqlUrl = "";
                lbSqlSettingsError.Visible = true;
                lbSqlSettingsError.Text = "Enter Valid URL Path";
                txtSqlUrl.SelectAll();
                txtSqlUrl.Focus();
            }
        }

        private void btnSQLClear_Click(object sender, EventArgs e)
        {
            txtSqlUrl.Text = "";
            txtSqlTime.Text = "";
            txtSqlFieLen.Text = "";
            rdbnSqlPre.Checked = false;
            rdbnSqlSuf.Checked = false;
            rdbnSqlEmp.Checked = true;
            txtServer.Text = "";
            txtDB.Text = "";
            cmbDB.SelectedIndex = 0;
            txtUser.Text = "";
            txtPass.Text = "";
        }

        private void btnUpdSet_Click(object sender, EventArgs e)
        {
            Global.mdbFilePath = txtMdbFilePath.Text;
            frmMdbTableColumnsSelect frm = new frmMdbTableColumnsSelect();
            frm.ShowDialog();
        }

        private void mDBFileSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mDBFileSettingsToolStripMenuItem.BackColor = Color.Olive;
            mDBFileSettingsToolStripMenuItem.ForeColor = Color.White;
            textFileSettingsToolStripMenuItem.BackColor = Color.Empty;
            textFileSettingsToolStripMenuItem.ForeColor = Color.Black;
            SQLFileSettingsToolStripMenuItem.BackColor = Color.Empty;
            SQLFileSettingsToolStripMenuItem.ForeColor = Color.Black;
            iToolStripMenuItem.BackColor = Color.Empty;
            iToolStripMenuItem.ForeColor = Color.Black;


            pnlMdbShow.Location = new Point(0, 0);
            pnlMdbShow.Size = new Size(419, 388);
            pnlMdbShow.Visible = true;
            pnlSqlShow.Visible = false;
            pnlTextShow.Visible = false;
            pnlImportShow.Visible = false;
        }

        private void textFileSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textFileSettingsToolStripMenuItem.BackColor = Color.Olive;
            textFileSettingsToolStripMenuItem.ForeColor = Color.White;
            mDBFileSettingsToolStripMenuItem.BackColor = Color.Empty;
            mDBFileSettingsToolStripMenuItem.ForeColor = Color.Black;
            SQLFileSettingsToolStripMenuItem.BackColor = Color.Empty;
            SQLFileSettingsToolStripMenuItem.ForeColor = Color.Black;
            iToolStripMenuItem.BackColor = Color.Empty;
            iToolStripMenuItem.ForeColor = Color.Black;

            pnlTextShow.Location = new Point(0, 0);
            pnlTextShow.Size = new Size(419, 388);
            pnlMdbShow.Visible = false;
            pnlSqlShow.Visible = false;
            pnlTextShow.Visible = true;
            pnlImportShow.Visible = false;
            cmbTimeFormat.SelectedIndex = 0;
            cmbDateFormat.SelectedIndex = 0;
        }

        private void sQLFileSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SQLFileSettingsToolStripMenuItem.BackColor = Color.Olive;
            SQLFileSettingsToolStripMenuItem.ForeColor = Color.White;
            mDBFileSettingsToolStripMenuItem.BackColor = Color.Empty;
            mDBFileSettingsToolStripMenuItem.ForeColor = Color.Black;
            textFileSettingsToolStripMenuItem.BackColor = Color.Empty;
            textFileSettingsToolStripMenuItem.ForeColor = Color.Black;
            iToolStripMenuItem.BackColor = Color.Empty;
            iToolStripMenuItem.ForeColor = Color.Black;
            pnlSqlShow.Location = new Point(0, 0);
            pnlSqlShow.Size = new Size(419, 388);
            pnlMdbShow.Visible = false;
            pnlSqlShow.Visible = true;
            pnlTextShow.Visible = false;
            pnlImportShow.Visible = false;//Enabled
        }

        private void iToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iToolStripMenuItem.BackColor = Color.Olive;
            iToolStripMenuItem.ForeColor = Color.White;
            SQLFileSettingsToolStripMenuItem.BackColor = Color.Empty;
            SQLFileSettingsToolStripMenuItem.ForeColor = Color.Black;
            mDBFileSettingsToolStripMenuItem.BackColor = Color.Empty;
            mDBFileSettingsToolStripMenuItem.ForeColor = Color.Black;
            textFileSettingsToolStripMenuItem.BackColor = Color.Empty;
            textFileSettingsToolStripMenuItem.ForeColor = Color.Black;

            pnlImportShow.Location = new Point(0, 0);
            pnlImportShow.Size = new Size(419, 388);
            pnlImportShow.Visible = true;
            pnlMdbShow.Visible = false;
            pnlSqlShow.Visible = false;
            pnlTextShow.Visible = false;

            dataSourcePath = "Data Source = " + Application.StartupPath + @"\TADatabase.sdf";
            DataTable t = new DataTable();
            cmbComp.Items.Clear();
            try
            {
                using (SqlCeConnection c = new SqlCeConnection(dataSourcePath))
                {
                    c.Open();
                    using (SqlCeDataAdapter a = new SqlCeDataAdapter("SELECT CO_COMP_NAME FROM CompRegister", c))
                    {
                        a.Fill(t);
                        //cmbComp.DataSource = t;
                    }
                    c.Close();
                }
            }
            catch { }

            int rowCount = t.Rows.Count;
            cmbComp.Items.Add("--Select Company--");
            if (rowCount > 0)
            {
                foreach (DataRow dr in t.Rows)
                {
                    cmbComp.Items.Add(dr[0].ToString());
                }
            }
            cmbComp.SelectedIndex = 0;
        }

        private void frmManageTnA_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon.Dispose();
        }

        private void btnMdbToOther_Click(object sender, EventArgs e)
        {
            if (fnConfirmChangeSettings())
            {
                dataSourcePath = "Data Source = " + Application.StartupPath + @"\TADatabase.sdf";
                SqlCeConnection sqlConnection1 = new SqlCeConnection();
                sqlConnection1.ConnectionString = dataSourcePath;
                SqlCeCommand cmd = new SqlCeCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();

                int compid = fnGetCompanyId();

                try
                {
                    cmd.CommandText = "DELETE FROM MDB_FILE_SETTINGS WHERE MDB_COMP_ID=" + compid.ToString();
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM MDB_FILE_FIELD_SETTINGS WHERE FIE_MDB_ID=" + compid.ToString();
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "UPDATE CompRegister SET CO_FILE_TYPE='' WHERE CO_COMP_ID=" + compid.ToString();
                    cmd.ExecuteNonQuery();
                }
                catch { }

                sqlConnection1.Dispose();
                sqlConnection1.Close();
                fnClearSett();
                SQLFileSettingsToolStripMenuItem.Visible = true;
                SQLFileSettingsToolStripMenuItem.Enabled = true;
                textFileSettingsToolStripMenuItem.Visible = true;
                textFileSettingsToolStripMenuItem.Enabled = true;
                mDBFileSettingsToolStripMenuItem.Visible = true;
                mDBFileSettingsToolStripMenuItem_Click(null, null);
                btnUpdSet.Visible = false;
                fnClearErrorMsg();
                iToolStripMenuItem.Visible = false;
            }
        }

        private void btnTextToOther_Click(object sender, EventArgs e)
        {
            if (fnConfirmChangeSettings())
            {
                dataSourcePath = "Data Source = " + Application.StartupPath + @"\TADatabase.sdf";
                SqlCeConnection sqlConnection1 = new SqlCeConnection();
                sqlConnection1.ConnectionString = dataSourcePath;
                SqlCeCommand cmd = new SqlCeCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();

                int compid = fnGetCompanyId();

                try
                {
                    cmd.CommandText = "DELETE FROM TEXT_FILE_SETTINGS WHERE TEXT_COM_ID=" + compid.ToString();
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "UPDATE CompRegister SET CO_FILE_TYPE='' WHERE CO_COMP_ID=" + compid.ToString();
                    cmd.ExecuteNonQuery();
                }
                catch { }

                sqlConnection1.Dispose();
                sqlConnection1.Close();
                fnClearSett();
                SQLFileSettingsToolStripMenuItem.Visible = true;
                SQLFileSettingsToolStripMenuItem.Enabled = true;
                textFileSettingsToolStripMenuItem.Visible = true;
                mDBFileSettingsToolStripMenuItem.Visible = true;
                mDBFileSettingsToolStripMenuItem.Enabled = true;
                mDBFileSettingsToolStripMenuItem_Click(null, null);
                fnClearErrorMsg();
                iToolStripMenuItem.Visible = false;
            }
        }

        private void btnFromSqlToOther_Click(object sender, EventArgs e)
        {
            if (fnConfirmChangeSettings())
            {
                dataSourcePath = "Data Source = " + Application.StartupPath + @"\TADatabase.sdf";
                SqlCeConnection sqlConnection1 = new SqlCeConnection();
                sqlConnection1.ConnectionString = dataSourcePath;
                SqlCeCommand cmd = new SqlCeCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();

                int compid = fnGetCompanyId();

                try
                {
                    cmd.CommandText = "DELETE FROM SQL_DATA_SETTINGS WHERE SQL_COMP_ID=" + compid.ToString();
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM SQL_DATA_FIELD_SETTINGS WHERE SQL_TAB_COMP_ID=" + compid.ToString();
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "UPDATE CompRegister SET CO_FILE_TYPE='' WHERE CO_COMP_ID=" + compid.ToString();
                    cmd.ExecuteNonQuery();
                }
                catch { }

                sqlConnection1.Dispose();
                sqlConnection1.Close();
                fnClearSett();
                SQLFileSettingsToolStripMenuItem.Visible = true;
                textFileSettingsToolStripMenuItem.Visible = true;
                textFileSettingsToolStripMenuItem.Enabled = true;
                mDBFileSettingsToolStripMenuItem.Visible = true;
                mDBFileSettingsToolStripMenuItem.Enabled = true;
                mDBFileSettingsToolStripMenuItem_Click(null, null);
                fnClearErrorMsg();
                iToolStripMenuItem.Visible = false;
            }
        }

        private void fnClearErrorMsg()
        {
            lbError.Text = "";
            lbFileError.Text = "";
            lbSqlError.Text = "";
            lbSqlSettingsError.Text = "";
        }

        private void fnClearSett()
        {
            btnClearSettings_Click(null, null);
            btnClearFileSettings_Click(null, null);
            btnSQLClear_Click(null, null);

            lbServerURL.Text = "";
            lbMechRec.Text = "";
            lbDbRec.Text = "";
            iToolStripMenuItem_Click(null, null);

            //SQLFileSettingsToolStripMenuItem.Enabled = true;
            //textFileSettingsToolStripMenuItem.Enabled = true;
            //mDBFileSettingsToolStripMenuItem.Enabled = true;
            SQLFileSettingsToolStripMenuItem.Visible = false;
            textFileSettingsToolStripMenuItem.Visible = false;
            mDBFileSettingsToolStripMenuItem.Visible = false;

            btnFromSqlToOther.Enabled = false;
            btnFromSqlToOther.Visible = false;
            btnTextToOther.Visible = false;
            btnTextToOther.Enabled = true;
            btnMdbToOther.Enabled = false;
            btnMdbToOther.Visible = false;
        }

        private bool fnConfirmChangeSettings()
        {
            if (MessageBox.Show("Do you want to Delete the present settings?.", "Clear Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                return true;
            else
                return false;
        }

        private void cmbComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //timer1.Stop();
            if (cmbComp.SelectedIndex != 0)
            {
                try
                {
                    string compName = cmbComp.SelectedItem.ToString();
                    int compId = 0;
                    string fileTy = "";
                    dataSourcePath = "Data Source = " + Application.StartupPath + @"\TADatabase.sdf";
                    SqlCeConnection sqlConnection1 = new SqlCeConnection();
                    sqlConnection1.ConnectionString = dataSourcePath;
                    SqlCeCommand cmd = new SqlCeCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT * FROM CompRegister WHERE CO_COMP_NAME='" + compName + "'";
                    cmd.Connection = sqlConnection1;

                    sqlConnection1.Open();
                    SqlCeDataReader dataRead;
                    dataRead = cmd.ExecuteReader();
                    dataRead.Read();
                    compId = Convert.ToInt32(dataRead[0]);
                    fileTy = dataRead[3].ToString();
                    dataRead.Dispose();
                    dataRead.Close();
                    Global.Indexrow = compId;
                    string query = "";
                    string url = "";
                    if (fileTy == "mdb")
                    {
                        query = "SELECT MDB_URL FROM MDB_FILE_SETTINGS WHERE MDB_COMP_ID=" + compId.ToString();
                        chkForce.Enabled = true;
                        dtpForcedate.Enabled = false;
                    }
                    else if (fileTy == "text")
                    {
                        query = "SELECT TEXT_URL FROM TEXT_FILE_SETTINGS WHERE TEXT_COM_ID=" + compId.ToString();
                        chkForce.Enabled = false;
                        dtpForcedate.Enabled =false;
                    }
                    else if (fileTy == "sql")
                    {
                        query = "SELECT SQL_URL FROM SQL_DATA_SETTINGS WHERE SQL_COMP_ID=" + compId.ToString();
                        chkForce.Enabled = true;
                        dtpForcedate.Enabled = false;
                    }
                    else
                        query = "";
                    if (query != "")
                    {
                        cmd.CommandText = query;
                        SqlCeDataReader dataRead1;
                        dataRead1 = cmd.ExecuteReader();
                        dataRead1.Read();
                        url = dataRead1[0].ToString();
                        dataRead1.Dispose();
                        dataRead1.Close();
                        lbServerURL.Text = url;
                        sqlConnection1.Dispose();
                        sqlConnection1.Close();
                        if (fnValidateURL(3))
                        {
                            string strConn = "server=" + strHost + ";database=" + strDB + ";uid=" + strUsr + ";pwd=" + strPWD;
                            MySqlConnection myConn = new MySqlConnection(strConn);
                            try
                            {
                                MySqlCommand myCommand = myConn.CreateCommand();
                                myConn.Open();

                                myCommand.CommandText = "SELECT COUNT(*) FROM kal_logdetails";
                                MySqlDataReader SQLRD;
                                SQLRD = myCommand.ExecuteReader();
                                SQLRD.Read();
                                lbDbRec.Text = SQLRD[0].ToString();
                                //MessageBox.Show(SQLRD[0].ToString());
                                SQLRD.Close();
                                myConn.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
                catch (Exception exx)
                {
                    MessageBox.Show(exx.Message);
                }
            }
            else
            {
                lbServerURL.Text = "";
                lbDbRec.Text = "";
            }
        }
                    
        private void cmbDateFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDateFormat.SelectedIndex != 0)
            {
                if (cmbDateFormat.SelectedItem.ToString() != "Other")
                {
                    txtDateFormat.Enabled = false;
                    txtDateFormat.Text = cmbDateFormat.SelectedItem.ToString();
                }
                else
                {
                    txtDateFormat.Enabled = true;
                    txtDateFormat.ReadOnly = false;
                    txtDateFormat.Text = "";
                }
            }
            else
                txtDateFormat.Enabled = true;
        }

        private void cmbTimeFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTimeFormat.SelectedIndex != 0)
            {
                if (cmbTimeFormat.SelectedItem.ToString() != "Other")
                {
                    txtTimeFormat.Enabled = false;
                    txtTimeFormat.Text = cmbTimeFormat.SelectedItem.ToString();
                }
                else
                {
                    txtTimeFormat.Enabled = true;
                    txtTimeFormat.ReadOnly = false;
                    txtTimeFormat.Text = "";
                }
            }
            else
                txtTimeFormat.Enabled = false;
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            lbFileError.Text = "";
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            string selected = dialog.SelectedPath;
            //txtFilePath.Text = selected;
            //txtFolderPath.Text = selected;
            if (selected != "")
            {
                Global.folPath = selected;
                TextFileName frm = new TextFileName();
                frm.ShowDialog();
                if ((Global.fileDateFormat == "" || Global.fileTypeRead == "") && Global.filename=="")
                    txtFilePath.Text = "";
                else
                    txtFilePath.Text = selected;
            }
        }

        private void btnUpSett_Click(object sender, EventArgs e)
        {
            TextFileName frm = new TextFileName();
            frm.ShowDialog();
        }

        private void lbserurltxt_Click(object sender, EventArgs e)
        {

        }

        private void lbServerURL_Click(object sender, EventArgs e)
        {

        }

        private void chkForce_CheckedChanged(object sender, EventArgs e)
        {
            if (chkForce.Checked == true)
                dtpForcedate.Enabled = true;
            else
                dtpForcedate.Enabled = false;
        }
    }
}