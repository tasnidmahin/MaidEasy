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
            string sql = "select WorkerId,Name,gender,Area,type,status,experience,rating from Worker";
            var table = db.getData(sql);
            while(table.Read())
            {

            }
            table.Close();
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

    }

}