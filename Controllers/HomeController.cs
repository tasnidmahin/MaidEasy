using MaidEasy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaidEasy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DBHelper db = DBHelper.getDB();
            db.DBConnection();
            string Date = DateTime.Now.ToString("dd");
            string Month = DateTime.Now.ToString("MM");

            if (Date.Equals("01"))
            {
                string sql = "SELECT updateStatus from worker LIMIT 1";
                var table = db.getData(sql);
                table.Read();
                string status = table.GetString(0);
                table.Close();
                if (status.Equals("pending"))
                {
                    string Year = DateTime.Now.ToString("yyyy");
                    Year = Month + "/" + Year;
                    sql = "UPDATE Contracts SET status = 'previous' WHERE EndMonth = '" + Year + "'";
                    db.setData(sql);
                    sql = "UPDATE Worker SET experience = experience +1 ";
                    db.setData(sql);
                    sql = "UPDATE Worker SET updateStatus  = 'updated'";
                    db.setData(sql);
                }
            }
            else if (Date.Equals("02"))
            {
                string sql = "SELECT updateStatus from worker LIMIT 1";
                var table = db.getData(sql);
                table.Read();
                string status = table.GetString(0);
                table.Close();
                if (status.Equals("updated"))
                {
                    sql = "UPDATE Worker SET updateStatus  = 'pending'";
                    db.setData(sql);
                }
            }
                return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}