using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaidEasy.Controllers
{
    public class MaidController : Controller
    {
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
            return View();
        }
    }
}