using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaidEasy.Controllers
{
    public class AdminWorkerController : Controller
    {
        // GET: AdminWorker
        public ActionResult Index()
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
    }
}