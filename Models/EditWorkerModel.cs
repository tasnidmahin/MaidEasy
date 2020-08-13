using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaidEasy.Models
{
    public class EditWorkerModel
    {
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Phone { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string gender { get; set; }
        public string type { get; set; }
        public string area { get; set; }
        public string image { get; set; }

        public EditWorkerModel(int id)
        {
            string sql = "SELECT Name,fatherName,mobile,PresentAddress,PermanentAddress,gender,type,Area,image from worker where WorkerId = '" + id + "'";

            DBHelper db = DBHelper.getDB();
            var table = db.getData(sql);
            table.Read();

            Name                = table.GetString(0);
            FatherName          = table.GetString(1);
            Phone               = table.GetString(2);
            PresentAddress      = table.GetString(3);
            PermanentAddress    = table.GetString(4);
            gender              = table.GetString(5);
            type                = table.GetString(6);
            area                = table.GetString(7);
            image               = table.GetString(8);
        }
    }
}