using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Threading;
using Microsoft.Win32;

namespace SQLDT
{
    public partial class FormMain : Form
    {

        //public static FormMain fm;

        public FormMain()
        {
            InitializeComponent();
            //fm = this;
        }


        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
        

        private void FormMain_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";

            string m_IP = Class1.App_IP;
            string m_DB = Class1.App_DB;
            string m_USER = Class1.App_USER;
            string m_PWD = Class1.App_PWD;
            int m_Timer = Convert.ToInt32(Class1.App_Timer);

            
            if (m_IP !=null && m_DB !=null && m_USER !=null && m_PWD !=null )
            {

                label1.Text = "程序配置合理，开始检查网络状态。。。";
            }
            else
            {
                label1.Text = "程序配置不合理，请检查配置项。。。";
                label2.Text = "3秒后程序自动退出。。。";
                Thread.Sleep(3000);
                System.Environment.Exit(0);
            }
            this.timer1.Enabled = true;
            timer1.Interval = m_Timer*1000;
            //label2.Text = "如果程序进入假死状态，说明程序正在尝试继续连接服务器，请耐心等待。。。";
            timer1.Start();
        }

        public static void PingIP()
        {
            string m0_IP = Class1.App_IP;
            int i = 0;
            
            for (; i < 1;)
            {
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send(m0_IP, 120);//第一个参数为ip地址，第二个参数为ping的时间
                if (reply.Status == IPStatus.Success)
                {
                    i++;
                    Class1.lb2 = "可以ping通服务器，现在开始获取服务器时间。。。";
                }
                else
                {
                    i--;
                    //Class1.lb2 = "无法ping通服务器，循环开始：" + i;
                }
            }
        }

        public static void Conn()
        {
            //Console.WriteLine("静态的方法可以直接调用!但是很耗资源!");

            string m1_IP = Class1.App_IP;
            string m1_DB = Class1.App_DB;
            string m1_USER = Class1.App_USER;
            string m1_PWD = Class1.App_PWD;

            try
            {
                String m_Str = "use master select count(1) from sys.databases where name = '" + m1_DB + "'";
                string conn = "Data Source=" + m1_IP + ";Initial Catalog=" + m1_DB + ";User Id=" + m1_USER + ";Password=" + m1_PWD + ";";
                SqlConnection con = new SqlConnection();//建立数据库连接
                con.ConnectionString = conn;
                string DT = "select getdate()";
                con.Open();
                SqlCommand cmdm_Str = new SqlCommand(m_Str, con);
                int result = Convert.ToInt32(cmdm_Str.ExecuteScalar());
                if (result == 0)
                {
                    //库都没有就退出程序
                    //Thread.Sleep(3000);
                    System.Environment.Exit(0);
                }
                SqlCommand comm = new SqlCommand(DT, con);
                SqlDataReader reader = comm.ExecuteReader();
                DateTime dt;
                if (reader.Read())
                {
                    dt = (DateTime)reader[0];
                    con.Close();
                    Class1.lb3 = dt;
                }
                con.Close();

            }
                catch (Exception)
                { }
            

            
        }

        public static void Sync()
        {
            DateTime Time_now = DateTime.Now;
            TimeSpan timeSpan = Time_now.Subtract(Class1.lb3).Duration();
            int Deviation = Convert.ToInt32(Class1.Time_Deviation);
            if (timeSpan.TotalSeconds> Deviation)
            {
                //转换System.DateTime到SYSTEMTIME
                SYSTEMTIME st = new SYSTEMTIME();
                st.FromDateTime(Class1.lb3);
                //调用Win32 API设置系统时间
                Win32API.SetLocalTime(ref st);
            }


        }

        private void SystemEvents_TimeChanged(object sender, EventArgs e)
        {
            Class1.Time_change = DateTime.Now;
        }

        private void Close_App()
        {
            //Thread.Sleep(3000);
            //System.Environment.Exit(0);
            this.Close();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;
            PingIP();
            label2.Text = Class1.lb2;
            if (Class1.lb2== "可以ping通服务器，现在开始获取服务器时间。。。")
            {
                Conn();
                label3.Text = "获取到服务器时间：" + Class1.lb3;
                Sync();
                //定制事件
                SystemEvents.TimeChanged += new EventHandler(SystemEvents_TimeChanged);
                if (Class1.Time_change != null)
                {
                    label4.Text = "本地时间已修正为：" + Class1.Time_change;
                    Close_App();
                }
                else
                {
                    label4.Text = "本地时间在允许误差范围内，无需修改。。。";
                    Close_App();
                }
                
                label5.Text = "已保存本次记录，3秒后本程序自动退出。。。";
                //Close_App();
                //timer1.Enabled = true;
            }
            
        }
        
    }

}

