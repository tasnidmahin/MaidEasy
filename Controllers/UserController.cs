using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaidEasy.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult feedback()
        {
            return View();
        }

        public ActionResult user_profile()
        {
            return View();
        }

        public ActionResult hired_workers_profile()
        {
            return View();
        }
        public ActionResult Edit_profile()
        {
            return View();
        }
    }
}