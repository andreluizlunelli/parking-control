using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace parking_control.Models
{
    public class ConnectMysql
    {
        private static MySqlConnection connection = null;

        public static MySqlConnection GetInstance()
        {
            if (connection == null)
            {
                connection = new MySqlConnection(GetConnectionString());
                connection.Open();                
            }                
            return connection;
        }

        private static string GetConnectionString()
        {
            string host = "192.168.99.100";
            string nameDataBase = "meusql";
            string username = "root";
            string password = "meusql";
            return string.Format("Server={0}; database={1}; UID={2}; password={3}", host, nameDataBase, username, password);           
        }

        public static int Close()
        {
            connection.Close();
            return 1;
        }
    }

    public class NotExecuteCommandSql : Exception
    {
        public NotExecuteCommandSql(string message) : base(message)
        {
        }
    }

    public class NotFoundIDEntity : Exception
    {
        public NotFoundIDEntity(string message) : base(message)
        {
        }
    }
}