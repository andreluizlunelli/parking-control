using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;

namespace parking_control.Tests.Model
{
    [TestClass]
    public class ConnectMysqlTest
    {
        [TestMethod]
        public void test01()
        {
            string host = "192.168.99.100";
            string nameDataBase = "meusql";
            string username = "root";
            string password = "meusql";
            string connStr = string.Format("Server={0}; database={1}; UID={2}; password={3}", host, nameDataBase, username, password);
            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                string sqlCommand = "SELECT * FROM VehicleEntrance"; 
                using (MySqlCommand command = new MySqlCommand(sqlCommand, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string someStringFromColumnZero = reader.GetString(0);
                        string someStringFromColumnZero1 = reader.GetString(1);
                        string someStringFromColumnZero2 = reader.GetString(2);

                        MessageBox.Show("Region = " + someStringFromColumnZero + " " + someStringFromColumnZero1 + " " + someStringFromColumnZero2);
                    }
                    
                }
            }
            

        }
    }
}
