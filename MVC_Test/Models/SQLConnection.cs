using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Xml.XPath;

namespace MVC_Test.Models
{
    public class SQLConnection
    {
        private SqlConnection Dbconn()
        {
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            path = path + "App_Data//DBconnection.xml";

            XPathDocument doc = new XPathDocument(path);
            var navigator = doc.CreateNavigator();

            var servername = navigator.SelectSingleNode("//appsettings/servername");
            var username = navigator.SelectSingleNode("//appsettings/username");
            var password = navigator.SelectSingleNode("//appsettings/password");
            var database = navigator.SelectSingleNode("//appsettings/database");

            SqlConnection connection = new SqlConnection(string.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};MultipleActiveResultSets = True;", servername, database, username, password));
            return connection;
        }

        public List<Test> GetLists()
        {
            List<Test> lists = new List<Test>();
            SqlConnection dbconn = Dbconn();
            SqlCommand select_command = new SqlCommand("select * from Test_Table", dbconn);
            dbconn.Open();

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
    }

    public class Test
    {
        public string id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
    }

}