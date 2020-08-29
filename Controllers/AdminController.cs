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
            return View();
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
        public ActionResult UserList()
        {
            List<UserLIstModel> userListModel = new List<UserLIstModel>();
            DBHelper db = DBHelper.getDB();
            string sql = "SELECT UserId from users";
            var table = db.getData(sql);
            while(table.Read())
            {
                int id = Int32.Parse(table.GetString(0));
                UserLIstModel ULModel = new UserLIstModel(id);
                userListModel.Add(ULModel);
            }
            table.Close();
            ViewData["list"] = userListModel;
            return View();
        }

    }
}