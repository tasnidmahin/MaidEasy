using MaidEasy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["username"] == null) return Content("<script language='javascript' type='text/javascript'>alert('Login to continue');</script>");
            var id = Request["maid"].ToString();
            Uri MyUrl = Request.UrlReferrer;
            var lastUrl = Server.HtmlEncode(MyUrl.AbsolutePath);
            System.Diagnostics.Debug.WriteLine("--------------Maid ID--------------------------------------");
            System.Diagnostics.Debug.WriteLine(id);
            System.Diagnostics.Debug.WriteLine(MyUrl);
            System.Diagnostics.Debug.WriteLine(Server.HtmlEncode(MyUrl.Port.ToString()));
            System.Diagnostics.Debug.WriteLine(Server.HtmlEncode(MyUrl.Scheme));
            System.Diagnostics.Debug.WriteLine(Server.HtmlEncode(MyUrl.Host));
            System.Diagnostics.Debug.WriteLine(Server.HtmlEncode(MyUrl.AbsolutePath));
            System.Diagnostics.Debug.WriteLine(Server.HtmlEncode(MyUrl.LocalPath));
            System.Diagnostics.Debug.WriteLine("----------------------------------------------------");

            string sql = "SELECT Name,type,rating,experience,image from Worker where WorkerId = " + id;

            DBHelper db = DBHelper.getDB();
            var table = db.getData(sql);
            table.Read();
            string[] data = new string[6];
            data[0] = table.GetString(0);
            data[1] = table.GetString(1);
            data[2] = table.GetString(2);
            data[3] = table.GetString(3);
            data[4] = id;
            data[5] = table.GetString(4);
            table.Close();

            //sql for feedback info
            sql = "SELECT count(username) from WorkerReview where WorkerId = " + id;
            table = db.getData(sql);
            table.Read();
            int count = Int32.Parse(table.GetString(0));
            table.Close();
            sql = "SELECT username ,description ,rating from WorkerReview where WorkerId = " + id;
            string[,] feedback = new string[count, 3];
            table = db.getData(sql);

            int i = 0;
            while (table.Read())
            {
                feedback[i, 0] = table.GetString(0);
                feedback[i, 1] = table.GetString(1);
                feedback[i, 2] = table.GetString(2);
                i++;
            }
            table.Close();


            ViewData["LastUrl"] = lastUrl;
            Session["CurWorker"] = data;
            ViewData["feedback"] = feedback;
            ViewData["cnt_feed"] = count;
            return View();
        }
        public ActionResult Hire()
        {
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["username"] == null) return Content("<script language='javascript' type='text/javascript'>alert('Login to continue');</script>");
            string sql = "SELECT COUNT(WorkId) from work";
            DBHelper db = DBHelper.getDB();
            var table = db.getData(sql);
            table.Read();
            int count = Int32.Parse(table.GetString(0));
            table.Close();
            Session["work_row"] = count;

            /*System.Diagnostics.Debug.WriteLine("--------------------------------");
            System.Diagnostics.Debug.WriteLine("--------------------------------");*/

            string[,] data = new string[count, 2];

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


        private string getEndMonth(string SMonth, int conlen)
        {
            string m = "", y = "";
            for (int i = 0; i < 2; i++) m += SMonth[i];
            for (int i = 3; i < 7; i++) y += SMonth[i];

            /*System.Diagnostics.Debug.WriteLine("---------------m y-----------------");
            System.Diagnostics.Debug.WriteLine(m);
            System.Diagnostics.Debug.WriteLine(y);
            System.Diagnostics.Debug.WriteLine("--------------------------------");*/
            int mm = Int32.Parse(m);
            int yy = Int32.Parse(y);
            mm += conlen;
            if (mm > 12)
            {
                mm -= 12; yy++;
            }
            string ret = "";
            if (mm < 10) ret = "0";
            ret = ret + mm + "/" + yy;

            return ret;
        }

        private string getStatus()
        {
            int start = Int32.Parse(Session["start"].ToString());
            int end = Int32.Parse(Session["end"].ToString());
            var wData = (string[])Session["CurWorker"];
            var wID = wData[4];
            string sql = "SELECT status from Worker where WorkerId = '" + wID + "' ";
            DBHelper db = DBHelper.getDB();
            var table = db.getData(sql);
            table.Read();
            string status = table.GetString(0);
            table.Close();
            StringBuilder sb = new StringBuilder();
            sb.Append(status);

            for (int i = start; i <= end; i++)
            {
                sb[i] = '1';
            }
            status = sb.ToString();
            return status;
        }

        [HttpGet]
        public ActionResult Booking()
        {
            var wData = (string[])Session["CurWorker"];
            string workerType = wData[1];

            if (workerType[0].Equals('0'))
            {
                Session["salary"] = Request["salary"].ToString();
                Session["conLen"] = Int32.Parse(Request["con_length"].ToString());
                return RedirectToAction("BookingFull", "Maid");
            }


            var salary = Request["salary"].ToString();
            var conLen = Int32.Parse(Request["con_length"].ToString());
            int cnt = Int32.Parse(Session["work_row"].ToString());
            string worklist = "";
            //var w1 = Request["checked_value"].ToString();
            //var w2 = Request["check2"].ToString();
            //var w3 = Request["check3"].ToString();
            /* System.Diagnostics.Debug.WriteLine("--------------Booking()salary, con len, worklist------------------");
             System.Diagnostics.Debug.WriteLine(salary);
             System.Diagnostics.Debug.WriteLine(conLen);
             //System.Diagnostics.Debug.WriteLine(cnt);
             //System.Diagnostics.Debug.WriteLine(w2);
             //System.Diagnostics.Debug.WriteLine(w3);
             System.Diagnostics.Debug.WriteLine("--------------------------------");*/
            for (int i = 0; i < cnt; i++)
            {
                var nm = "box_" + i;
                System.Diagnostics.Debug.WriteLine(Request[nm].ToString());
                if (Request[nm].ToString().Equals("")) continue;
                worklist += Request[nm].ToString();
                worklist += "\n";
            }

            string SMonth = DateTime.Now.ToString("MM");
            string SYear = DateTime.Now.Year.ToString();
            SMonth = SMonth + "/" + SYear;
            string EMonth = getEndMonth(SMonth, conLen);

            System.Diagnostics.Debug.WriteLine("---------------Month and YEAR-----------------");
            System.Diagnostics.Debug.WriteLine(SMonth);
            System.Diagnostics.Debug.WriteLine(EMonth);
            System.Diagnostics.Debug.WriteLine("--------------------------------");

            var wID = wData[4];
            var uID = Int32.Parse(Session["userID"].ToString());
            int id = uID;
            var STime = Session["startTime"];
            var ETime = Session["endTime"];

            DBHelper db = DBHelper.getDB();
            string sql = "SELECT Name from Worker where WorkerId = ' " + wID + "'";
            var table = db.getData(sql);
            table.Read();
            string WName = table.GetString(0);
            table.Close();

            sql = "INSERT into contracts (WorkerId, WorkerName, UserId, StartMonth, EndMonth, StartTime, EndTime, Amount, Worklist)VALUES('" + wID + "', '" + WName + " ', ' " + uID + " ', ' " + SMonth + " ', ' " + EMonth + " ', ' " + STime + " ', ' " + ETime + " ', ' " + salary + "', '" + worklist + " ');";
            db.setData(sql);

            string status = getStatus();
            sql = "UPDATE Worker SET status  = '" + status + "' where WorkerId = '" + wID + "'";
            db.setData(sql);

            Session.Remove("work_row");
            Session.Remove("start");
            Session.Remove("end");
            Session.Remove("CurWorker");
            Session.Remove("startTime");
            Session.Remove("endTime");
            Session.Remove("SearchTimeForWorker");
            //return View("~/Views/User/hired_workers_profile.cshtml");
            return RedirectToAction("hired_workers_profile", "User");
        }


        public ActionResult BookingFull()
        {
            var wData = (string[])Session["CurWorker"];
            var salary = Session["salary"];
            var conLen = Int32.Parse(Session["conLen"].ToString());

            System.Diagnostics.Debug.WriteLine("---------------S A L A R Y-----------------");
            System.Diagnostics.Debug.WriteLine(salary);
            System.Diagnostics.Debug.WriteLine(conLen);
            System.Diagnostics.Debug.WriteLine("--------------------------------");
            string worklist = "";

            string SMonth = DateTime.Now.ToString("MM");
            string SYear = DateTime.Now.Year.ToString();
            SMonth = SMonth + "/" + SYear;
            string EMonth = getEndMonth(SMonth, conLen);

            var wID = wData[4];
            var uID = Int32.Parse(Session["userID"].ToString());
            int id = uID;
            var STime = "";
            var ETime = "";

            DBHelper db = DBHelper.getDB();
            string sql = "SELECT Name from Worker where WorkerId = ' " + wID + "'";
            var table = db.getData(sql);
            table.Read();
            string WName = table.GetString(0);
            table.Close();

            sql = "INSERT into contracts (WorkerId, WorkerName, UserId, StartMonth, EndMonth, StartTime, EndTime, Amount, Worklist)VALUES('" + wID + "', '" + WName + " ', ' " + uID + " ', ' " + SMonth + " ', ' " + EMonth + " ', ' " + STime + " ', ' " + ETime + " ', ' " + salary + "', '" + worklist + " ');";
            db.setData(sql);

            string status = "";
            for (int i = 0; i < 25; i++) status += "1";
            sql = "UPDATE Worker SET status  = '" + status + "' where WorkerId = '" + wID + "'";
            db.setData(sql);

            Session.Remove("work_row");
            Session.Remove("start");
            Session.Remove("end");
            Session.Remove("CurWorker");
            return RedirectToAction("hired_workers_profile", "User");
        }
        public ActionResult Hire_new()
        {
            return View();
        }
    }
}