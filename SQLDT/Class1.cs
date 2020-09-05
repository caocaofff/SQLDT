using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SQLDT
{
    class Class1
    {

        public static string App_IP = ConfigurationManager.ConnectionStrings["Data Source"].ConnectionString;
        public static string App_DB = ConfigurationManager.ConnectionStrings["Initial Catalog"].ConnectionString;
        public static string App_USER = ConfigurationManager.ConnectionStrings["User Id"].ConnectionString;
        public static string App_PWD = ConfigurationManager.ConnectionStrings["Password"].ConnectionString;
        public static string App_Timer = ConfigurationManager.ConnectionStrings["Timer"].ConnectionString;
        public static string App_Run = ConfigurationManager.ConnectionStrings["RunEXE"].ConnectionString;
        public static string Time_Deviation = ConfigurationManager.ConnectionStrings["Deviation"].ConnectionString;

        public static string lb1 = "";
        public static string lb2 = "";
        public static DateTime lb3;
        public static string lb4 = "";
        public static string lb5 = "";
        public static string lb6 = "";
        public static DateTime? Time_change = null;

    }
}
