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
        public ActionResult Worker()
        {
            return View();
        }
        public ActionResult Edit_Worker()
        {
            return View();
        }
    }
}