using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Time_and_Attendance
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmManageTnA());
        }
    }

    static class Global
    {
       public static string mdbFilePath = "";
       public static string filepassword = "";
       public static string tableName = "";

       public static string EmpIdFieldName = "";
       public static string DeviceFieldName = "";

       public static string DateTimeInOutFieldName = "";
       public static string DateTimeInFieldName = "";
       public static string DateTimeOutFieldName = "";
       public static string DateFieldName = "";
       public static string InOutFieldName = "";
       public static string InFieldName = "";
       public static string OutFieldName = "";

       public static string SetMdbValues = "";

       public static int Indexrow = 0;

       public static bool forceRead = false;

        //SQL
       public static string SQLConnStr = "";
       public static string SQLDatabase = "";
       public static string SQLServer = "";
       public static string SetSQLValues = "";
       public static bool SelUpdate = false;
       public static int selUpdateId = 0;

        //Text File Format
       public static string filePrefix = "";
       public static string fileDateFormat = "";
       public static string fileSuffix = "";
       public static string fileTypeRead = "";
       public static string fileReadDate = "";
       public static string folPath = "";
       public static string filename = "";
    }
    public class TreeItem
    {
        public string Name;
        public int Level;
        public TreeItem(string name, int level)
        {
            Name = name; Level = level;
        }
    }

}