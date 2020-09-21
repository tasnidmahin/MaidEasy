using MaidEasy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaidEasy.Controllers
{
    public class HomeController : Controller
    {
        CustomDbContext dbContext = new CustomDbContext();

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
                    sql = "UPDATE Worker SET experience = experience +1";
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


            string uType = "general";
            if (Session["username"] != null)
            {
                string sql = "SELECT type from Users where username = '" + Session["username"] + "'";
                var table = db.getData(sql);
                table.Read();
                uType = table.GetString(0);
                table.Close();
            }

            if (uType.Equals("admin") || uType.Equals("super"))
            {
                return RedirectToAction("Admin_home", "Admin");
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

        [HttpPost]
        public ActionResult EntryContactInfo()
        {
            contactu contactus = new contactu();

            contactus.Name      = Request["name"];
            contactus.Email     = Request["email"];
            contactus.Subject   = Request["subject"];
            contactus.Message   = Request["message"];
            contactus.Review    = Request["review"];

            System.Diagnostics.Debug.WriteLine("-----Contact Us-------------------------------");
            System.Diagnostics.Debug.WriteLine("Before");
            System.Diagnostics.Debug.WriteLine("-----Contact Us-------------------------------");

            if (ModelState.IsValid)
            {
                dbContext.contactus.Add(contactus);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            System.Diagnostics.Debug.WriteLine("-----Contact Us-------------------------------");
            System.Diagnostics.Debug.WriteLine(Request["name"]);
            System.Diagnostics.Debug.WriteLine("-----Contact Us-------------------------------");
            return RedirectToAction("Index");
        }
    }
}