using MaidEasy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MaidEasy.Controllers
{
    public class AdminWorkerController : Controller
    {
        CustomDbContext dbContext = new CustomDbContext();
        // GET: AdminWorker
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add_worker()
        {
            return View();
        }
        public ActionResult WorkerList()
        {
            
            IEnumerable<MaidEasy.Models.worker> workerList = dbContext.worker.ToList();
            foreach (var item in workerList)
            {
                item.Area = getThanaList(item.Area);
                item.type = getWorkerTypeList(item.type);
            }

            return View(workerList);
        }
        public ActionResult Edit_Worker()
        {
            
            if (Request["workerID"] != null) 
                Session["workerID"] = Request["workerID"];

            var id = Int32.Parse(Session["workerID"].ToString());
            

            worker worker = dbContext.worker.Find(id);
            var typeList = getWorkerTypeList(worker.type);
            ViewData["typeList"] = typeList;
            return View(worker);
        }

        [HttpPost]
        public ActionResult AddNewWorker(HttpPostedFileBase file)
        {
            var name = Request["name"];
            var fatherName = Request["fathername"];
            var phone = Request["Phone"];
            var presentAddress = Request["presentAddress"];
            var permanentAddress = Request["permanentAddress"];
            Session["tempImage"] = file;
            var gender = Request["gender"];

            bool temporaryType = (Request["temporaryType"] == "on") ? true : false;
            bool permanentType = (Request["permanentType"] == "on") ? true : false;
            bool babyCareType = (Request["babyCareType"] == "on") ? true : false;
            bool elderlyCareType = (Request["elderlyCareType"] == "on") ? true : false;

            var area = Request["area"];
            var thana = getThanaString(area);
            var type = getTypeString(temporaryType, permanentType, babyCareType, elderlyCareType);
            var img = "defaultmaid.png";
            //var img = Request["img"]; ;

            string Month = DateTime.Now.ToString("MM");
            string Year = DateTime.Now.ToString("yyyy");
            Year = Month + "/" + Year;

            DBHelper db = DBHelper.getDB();
            string sql = "SELECT `auto_increment` FROM INFORMATION_SCHEMA.TABLES WHERE table_name = 'worker'";
            var table = db.getData(sql);


            table.Read();
            int next = Int32.Parse(table.GetString(0));
            table.Close();


            if (Session["tempImage"] != null)
            {
                Session.Remove("tempImage");

                var filename = next.ToString() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Workers/"), filename);
                file.SaveAs(path);

                img = filename;
            }

            worker worker = new worker();
            worker.Name = name;
            worker.fatherName = fatherName;
            worker.mobile = phone;
            worker.PresentAddress = presentAddress;
            worker.PermanentAddress = permanentAddress;
            worker.gender = gender;
            worker.Area = thana;
            worker.type = type;
            worker.image = img;
            worker.joinDate = Year;
            worker.experience = 0;
            worker.updateStatus = "pending";
            worker.rating = 0.00;
            worker.status = "0000000000000000000000000";


            /*Instituitions instituitions = db.Instituitions.Find(id);
            db.Instituitions.Remove(instituitions);
            db.SaveChanges();
            
             
            if (ModelState.IsValid)
            {
                db.Entry(instituitions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

             */

            if (ModelState.IsValid)
            {
                dbContext.worker.Add(worker);
                dbContext.SaveChanges();
                return RedirectToAction("WorkerList");
            }
            //sql = " INSERT INTO worker (Name, fatherName, mobile , PresentAddress , PermanentAddress, gender, area, type, image, joinDate ) VALUES('" + name + "', '" + fatherName + " ', ' " + phone + " ', ' " + presentAddress + " ', ' " + permanentAddress + " ', ' " + gender + "', '" + thana + "', '" + type + "', '" + img + "', '" + Year + " '); ";
            
            //db.setData(sql);

            System.Diagnostics.Debug.WriteLine("-----IMAGE-------------------------------");
            System.Diagnostics.Debug.WriteLine(img);
            System.Diagnostics.Debug.WriteLine("-----IMAGE-------------------------------");

            return RedirectToAction("Add_worker", "AdminWorker");
        }

        [HttpPost]
        public ActionResult SaveWorkerData(HttpPostedFileBase file)
        {
            var name = Request["name"];
            var fathername = Request["fathername"];
            var Phone = Request["Phone"];
            var PresentAddress = Request["PresentAddress"];
            var PermanentAddress = Request["PermanentAddress"];
            //var gender = gender.Items[gender.SelectedIndex].Text;
            Session["tempImage"] = file;
            var gender = Request["gender"];
            bool temporaryType = (Request["temporaryType"] == "on") ? true : false;
            bool permanentType = (Request["permanentType"] == "on") ? true : false;
            bool babyCareType = (Request["babyCareType"] == "on") ? true : false;
            bool elderlyCareType = (Request["elderlyCareType"] == "on") ? true : false;

            var area = Request["area"];
            var thana = getThanaString(area);
            var type = getTypeString(temporaryType, permanentType, babyCareType, elderlyCareType);
            //var img = "defaultmaid.png";
            var img = Request["img"]; ;

            System.Diagnostics.Debug.WriteLine("-----TEN-------------------------------");
            System.Diagnostics.Debug.WriteLine(img);
            System.Diagnostics.Debug.WriteLine("-----Name-------------------------------");

            if (Session["tempImage"] != null)
            {
                Session.Remove("tempImage");

                var filename = Session["workerID"].ToString() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Workers/"), filename);
                file.SaveAs(path);

                img = filename;
            }

            /*string sql = "UPDATE worker set Name = '" + name + "', fatherName = '" + fathername + "', mobile = '" + Phone + "', PresentAddress = '" + PresentAddress + "', PermanentAddress = '" + PermanentAddress + "', gender = '" + gender + "', type = '" + type + "', Area = '" + thana + "', image = '" + img + "' where WorkerId = '" + Session["workerID"] + "'";
            DBHelper db = DBHelper.getDB();
            db.setData(sql);*/

            int id = Int32.Parse(Session["workerID"].ToString());
            worker worker = dbContext.worker.Find(id);
            worker.Name = name;
            worker.fatherName = fathername;
            worker.mobile = Phone;
            worker.PresentAddress = PresentAddress;
            worker.PermanentAddress = PermanentAddress;
            worker.gender = gender;
            worker.Area = thana;
            worker.type = type;
            worker.image = img;

            if (ModelState.IsValid)
            {
                dbContext.Entry(worker).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("Edit_Worker", "AdminWorker");
            }

            return RedirectToAction("Edit_Worker", "AdminWorker");
        }

        public ActionResult DeleteWorker()
        {
            /*DBHelper db = DBHelper.getDB();
            string sql = "DELETE from worker where WorkerId = '" + Session["workerID"] + "' ";
            db.setData(sql);*/

            int id = Int32.Parse(Session["workerID"].ToString());
            worker worker = dbContext.worker.Find(id);
            var img = worker.image;
            img = @"E:\Mahin\3.2\New\Lab\ISD\MaidEasy\Content\Workers\" + img; 
            dbContext.worker.Remove(worker);
            dbContext.SaveChanges();
            System.IO.File.Delete(@img);

            Session.Remove("workerID");
            return RedirectToAction("WorkerList", "AdminWorker");
        }

        private string getThanaList(string thanaString)
        {
            string ret = "", sql;
            DBHelper db = DBHelper.getDB();
            int length = thanaString.Length, i = 0, ind = 0;
            while (i < length)
            {
                if (thanaString[i] == '1')
                {
                    sql = "select Name from Thana where ThanaId = '" + (i + 1) + "' ";
                    var table = db.getData(sql);
                    table.Read();
                    string tName = table.GetString(0);
                    if (ind != 0) ret += ",\n";
                    else ind = 1;
                    ret += tName;
                    //System.Diagnostics.Debug.WriteLine(table[0]);
                    table.Close();
                }
                i++;
            }
            return ret;
        }

        private string getWorkerTypeList(string typeString)
        {
            string ret = "";
            int length = typeString.Length, i = 0, ind = 0;
            while (i < length)
            {
                if (typeString[i] == '1')
                {
                    if (ind != 0) ret += ",\n";
                    else ind = 1;
                    if (i == 0) ret += "temporary";
                    else if (i == 1) ret += "permanent";
                    else if (i == 2) ret += "babycare";
                    else ret += "elderlycare";
                }
                i++;
            }
            return ret;
        }

        private string getThanaString(string list)
        {
            string ret = "";
            String[] spearator = { "," };
            String[] areaList = list.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
            int sz = areaList.Length;
            int[] IDs = new int[sz];



            DBHelper db = DBHelper.getDB();
            for(int i=0;i<sz;i++)
            {
                string name = areaList[i];
                string Sql = "SELECT ThanaId from thana WHERE Name = '" + name + "' ";
                var Table = db.getData(Sql);
                Table.Read();
                int a = Int32.Parse(Table.GetString(0));
                IDs[i] = a;
                Table.Close();
            }



            string sql = "SELECT MAX(ThanaId) from thana";
            var table = db.getData(sql);
            table.Read();
            int cnt = Int32.Parse(table.GetString(0));
            table.Close();

            StringBuilder builder = new StringBuilder();
            for (int i = 0,j=0; i < cnt; i++)
            {
                if(j<sz && IDs[j] == i+1)
                {
                    builder.Append("1");
                    j++;
                }
                else builder.Append("0");
            }
            ret = builder.ToString();
            return ret;
        }

        private string getTypeString(bool a, bool b, bool c, bool d)
        {
            StringBuilder builder = new StringBuilder();
            if (a) builder.Append("1");
            else builder.Append("0");
            if (b) builder.Append("1");
            else builder.Append("0");
            if (c) builder.Append("1");
            else builder.Append("0");
            if (d) builder.Append("1");
            else builder.Append("0");

            string ret = builder.ToString();
            return ret;
        }

    }
}
