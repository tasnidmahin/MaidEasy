using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaidEasy.Models
{
    public class DBHelper
    {


        public static int flag = 0;
        public static string connectionstring = "datasource = localhost; username =root; password =; database = maideasy";
        MySqlConnection DBConnect = new MySqlConnection(connectionstring);

        static DBHelper db = null;
        public void DBConnection()


        {
            string connectionstring = "datasource = localhost; username =root; password =; database = maideasy";
            string output = " ---- ";
            MySqlConnection DBConnect = new MySqlConnection(connectionstring);
            try
            {
                DBConnect.Open();
                string query = "INSERT INTO Users (username , password , Name , mobile , PresentAddress , PermanentAddress ) VALUES('taqi2', 'taqi', 'taqi', '015', 'ctg', 'ctg');  ";
                //string query = "SELECT username, mobile from Users";
                //string query = "UPDATE Users SET Name = 'Tasnid3' WHERE UserId = 3" ;
                //string query = "DELETE FROM Users WHERE UserId=3";

                //var cmd = new MySqlCommand(query, DBConnect);
                //MySqlDataReader reader = cmd.ExecuteReader();
                //while (reader.Read())
                //{
                /*string someStringFromColumnZero = reader.GetString(0);
                string someStringFromColumnOne = reader.GetString(1);*/
                //System.Diagnostics.Debug.WriteLine(someStringFromColumnZero + "," + someStringFromColumnOne);
                //output = output + someStringFromColumnZero + "," + someStringFromColumnOne + "-------------\n";
                //}
                //DBConnect.Close();

            }
            catch
            {

            }

            return ;
        }

        private  DBHelper()
        {
        }

        public static DBHelper getDB()
        {
            if (db == null)
            {
                return db = new DBHelper();
            }
            else return db;
        }

        public MySqlDataReader getData(string query)
        {
            DBConnection();
            var cmd = new MySqlCommand(query, DBConnect);
            MySqlDataReader reader = cmd.ExecuteReader();
            return reader;

        }


        public void setData(string query)
        {
            DBConnection();
            var cmd = new MySqlCommand(query, DBConnect);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
            return;
        }

        public void stopConnection()
        {
            flag = 0;
            DBConnect.Close();
        }


    }
}