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
            var month = Request["con_length"].ToString();
            //var worklist = "";
            var w1 = Request["check1"].ToString();
            var w2 = Request["check2"].ToString();
            var w3 = Request["check3"].ToString();
            System.Diagnostics.Debug.WriteLine("--------------------------------");
            System.Diagnostics.Debug.WriteLine(salary);
            System.Diagnostics.Debug.WriteLine(month);
            System.Diagnostics.Debug.WriteLine(w1);
            System.Diagnostics.Debug.WriteLine(w2);
            System.Diagnostics.Debug.WriteLine(w3);
            System.Diagnostics.Debug.WriteLine("--------------------------------");
            //string sql = "";
            return View();
        }
    }
}