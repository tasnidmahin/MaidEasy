using MaidEasy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaidEasy.Controllers
{
    public class MaidController : Controller
    {
        //
        // GET: Maid
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MaidProfile()
        {
            return View();
        }
        public ActionResult Hire()
        {
            string sql = "SELECT COUNT(WorkId) from work";
            DBHelper db = DBHelper.getDB();
            var table = db.getData(sql);
            table.Read();
            int count = Int32.Parse(table.GetString(0));
            table.Close();
            Session["work_row"] = count;

            /*System.Diagnostics.Debug.WriteLine("--------------------------------");
            System.Diagnostics.Debug.WriteLine("--------------------------------");*/

            string[,] data = new string[count , 2];

            sql = "SELECT Name,UnitPrice from work";
            table = db.getData(sql);
            int i = 0;
            while (table.Read())
            {
                data[i, 0] = table.GetString(0);
                data[i, 1] = table.GetString(1);
                i++;
            }
            table.Close();

            ViewData["workList"] = data;

            return View("~/Views/Maid/Hire.cshtml");
        }

        [HttpGet]
        public ActionResult Booking()
        {
            var salary = Request["salary"].ToString();
            var conLen = Request["con_length"].ToString();
            var worklist = "";
            //int cnt = Int32.Parse(Session["work_row"].ToString());
            //var w1 = Request["checked_value"].ToString();
            //var w2 = Request["check2"].ToString();
            //var w3 = Request["check3"].ToString();
            System.Diagnostics.Debug.WriteLine("--------------Booking()salary, con len, worklist------------------");
            System.Diagnostics.Debug.WriteLine(salary);
            System.Diagnostics.Debug.WriteLine(conLen);
            //System.Diagnostics.Debug.WriteLine(cnt);
            //System.Diagnostics.Debug.WriteLine(w2);
            //System.Diagnostics.Debug.WriteLine(w3);
            System.Diagnostics.Debug.WriteLine("--------------------------------");
            for (int i=0;i<3;i++)
            {
                var nm = "box_" + i;
                worklist += Request[nm].ToString();
                worklist += "\n";
            }

            System.Diagnostics.Debug.WriteLine(worklist);
            string Month = DateTime.Now.ToString("MM");
            var wData = (string[])Session["CurWorker"];
            var wID = wData[4];
            var uID = Session["username"];
            var hour = Session["SearchTimeForWorker"];

            //string sql = "";
            return View("~/Views/User/hired_workers_profile.cshtml");
        }
    }
}