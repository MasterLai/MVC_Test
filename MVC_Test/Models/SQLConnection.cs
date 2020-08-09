using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Xml.XPath;

namespace MVC_Test.Models
{
    /*處理與資料庫的對接*/
    public class SQLConnection
    {
        /*連接資料庫*/
        private SqlConnection Dbconn()
        {
            /*用當前位置取得 XML 檔中的資料庫信息*/
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            path = path + "App_Data//DBconnection.xml";

            XPathDocument doc = new XPathDocument(path);
            var navigator = doc.CreateNavigator();

            /*server名稱、帳號、密碼、資料庫名*/
            var servername = navigator.SelectSingleNode("//appsettings/servername");
            var username = navigator.SelectSingleNode("//appsettings/username");
            var password = navigator.SelectSingleNode("//appsettings/password");
            var database = navigator.SelectSingleNode("//appsettings/database");

            /*sql connect指令*/
            SqlConnection connection = new SqlConnection(string.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};MultipleActiveResultSets = True;", servername, database, username, password));
            return connection;
        }

        public List<Test> GetLists()
        {
            /*存放資料庫資料的List*/
            List<Test> lists = new List<Test>();
            
            /*資料庫連接 + select 指令*/
            SqlConnection dbconn = Dbconn();
            SqlCommand select_command = new SqlCommand("select * from Test_Table", dbconn);
            dbconn.Open();

            /*讀取資料 並 新增到 宣告的 List中*/
            SqlDataReader reader = select_command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Test list = new Test
                    {
                        id = reader.GetString(reader.GetOrdinal("id")),
                        name = reader.GetString(reader.GetOrdinal("name")),
                        age = reader.GetInt32(reader.GetOrdinal("age"))
                    };

                    lists.Add(list);
                }
            }
            else
            {
                /*讀取失敗時的Lists*/
                Test list = new Test
                {
                    id = "error",
                    name = "No Date",
                    age = 0
                };
            }

            dbconn.Close();
            return lists;
        }

        /*Insert 資料到資料庫的function*/
        public string AddLists(string name, int age)
        {
            /*Insert 指令*/
            SqlConnection dbconn = Dbconn();
            SqlCommand add_command = new SqlCommand(@"INSERT INTO [dbo].[Test_Table]([id],[name],[age])VALUES(NEWID(), @name, @age)", dbconn);
            add_command.Parameters.Add(new SqlParameter("@name", name));
            add_command.Parameters.Add(new SqlParameter("@age", age));
            
            dbconn.Open();
            add_command.ExecuteNonQuery();
            dbconn.Close();

            return "success";
        }
    }

    /*資料庫資料接值*/
    public class Test
    {
        public string id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
    }

}