using System;
using MySql.Data.MySqlClient;
using AngelHillLibrary;

namespace AngelHillLibrary.Models
{
    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
    }
}
