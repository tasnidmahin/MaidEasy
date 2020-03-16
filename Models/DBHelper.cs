using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaidEasy.Models
{
    public class DBHelper
    {
        public void DBConnection()
        {
            string connectionstring = "datasource = localhost; username =root; password =root; database = maideasy";
            MySqlConnection DBConnect = new MySqlConnection(connectionstring);
            try
            {
                DBConnect.Open();
                string query = "INSERT INTO Users (username , password , Name , mobile , PresentAddress , PermanentAddress ) VALUES('Mahin', 'mahin', 'Mahin', '015', 'goran', 'goran');  ";
                var cmd = new MySqlCommand(query, DBConnect);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string someStringFromColumnZero = reader.GetString(0);
                    string someStringFromColumnOne = reader.GetString(1);
                    Console.WriteLine(someStringFromColumnZero + "," + someStringFromColumnOne);
                }
                DBConnect.Close();
            }
            catch
            {

            }
        }
    }
}