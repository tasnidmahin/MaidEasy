using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaidEasy.Models
{
    public class UserLIstModel
    {
        public int userId { get; set; } 
        public string userName { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string thana { get; set; }
        public string type { get; set; }
        public UserLIstModel(int id)
        {
            DBHelper db = DBHelper.getDB();
            string sql = "SELECT username, Name, mobile, thana, type from users where UserId = " + id ;
            var table = db.getData(sql);
            userId = id;
            table.Read();
            userName    = table.GetString(0);
            name        = table.GetString(1);
            mobile      = table.GetString(2);
            thana       = table.GetString(3);
            type        = table.GetString(4);
            table.Close();
        }
    }
}