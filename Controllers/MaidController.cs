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
            string[] data = (string[])Session["CurWorker"];
            double time = (double)Session["SearchTimeForWorker"];

            var id = data[4];
            return View("~/Views/Maid/Hire.cshtml");
        }

        [HttpGet]
        public ActionResult Hire()
        {
            var salary = Request["salary"].ToString();
            string sql = "";
            return View();
        }
    }
}