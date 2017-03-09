using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using parking_control.Models;

namespace parking_control.Tests.Model
{
    [TestClass]
    public class ConnectMysqlTest
    {        
        public void poc()
        {
            string host = "192.168.99.100";
            string nameDataBase = "meusql";
            string username = "root";
            string password = "meusql";
            string connStr = string.Format("Server={0}; database={1}; UID={2}; password={3}", host, nameDataBase, username, password);
            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                string SQLCommand = "SELECT * FROM VehicleEntrance"; 
                using (MySqlCommand command = new MySqlCommand(SQLCommand, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string someStringFromColumnZero  = reader.GetString(0);
                        string someStringFromColumnZero1 = reader.GetString(1);
                        string someStringFromColumnZero2 = reader.GetString(2);

                        MessageBox.Show("Region = " + someStringFromColumnZero + " " + someStringFromColumnZero1 + " " + someStringFromColumnZero2);
                    }                    
                }
            }            
        }

        [TestMethod]
        public void test01()
        {
            MySqlConnection conn = ConnectMysql.GetInstance();
            Assert.IsNotNull(conn);
            Assert.AreEqual(conn, ConnectMysql.GetInstance());
        }

        [TestMethod]
        public void test02()
        {
            ConnectMysql.GetInstance();
            int r = ConnectMysql.Close();
            Assert.AreEqual(1, r);
        }
    }

}
