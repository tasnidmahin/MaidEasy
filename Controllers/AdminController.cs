using MaidEasy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaidEasy.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Admin_home()
        {
            /*if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["username"] == null) return Content("<script language='javascript' type='text/javascript'>alert('Login to continue');</script>");*/
            return View();
        }
        public ActionResult Admin_login()
        {
            return View();
        }
        public ActionResult Add_worker()
        {
            return View();
        }
        public ActionResult WorkerList()
        {
            DBHelper db = DBHelper.getDB();
            string sql = "SELECT count(WorkerId) FROM worker";
            var table = db.getData(sql);
            table.Read();
            int count = Int32.Parse(table.GetString(0));
            table.Close();


            string[,] data = new string[count, 8];
            sql = "select WorkerId,Name,gender,Area,type,status,experience,rating from Worker";
            table = db.getData(sql);
            int i = 0;
            while (table.Read())
            {
                data[i, 0] = table.GetString(0);
                data[i, 1] = table.GetString(1);
                data[i, 2] = table.GetString(2);
                data[i, 3] = table.GetString(3);
                data[i, 4] = table.GetString(4);
                data[i, 5] = table.GetString(5);
                data[i, 6] = table.GetString(6);
                data[i, 7] = table.GetString(7);

                i++;
            }
            table.Close();
            i = 0;
            while (i<count)
            {
                data[i, 3] = getThanaList(data[i, 3]);
                data[i, 4] = getWorkerTypeList(data[i, 4]);
                i++;
            }

            System.Diagnostics.Debug.WriteLine("---------END Worker info-----------");
            Session["WorkerList"] = data;
            Session["WorkerList_Count"] = count;
            return RedirectToAction("WorkerList", "AdminWorker");
        }
        public ActionResult Edit_Worker()
        {
            return View();
        }
        public ActionResult Worklist()
        {
            return View();
        }
        public ActionResult Edit_work()
        {
            return View();
        }
        public ActionResult Thanalist()
        {
            return View();
        }
        public ActionResult Edit_thanalist()
        {
            return View();
        }
        public ActionResult Edit_admin()
        {
            return View();
        }
        public ActionResult Add_admin()
        {
            return View();
        }

        private string getThanaList(string thanaString)
        {
            string ret = "", sql;
            DBHelper db = DBHelper.getDB();
            int length = thanaString.Length, i = 0,ind = 0;
            while(i<length)
            {
                if(thanaString[i] == '1')
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
    }

}