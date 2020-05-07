using MaidEasy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaidEasy.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Service()
        {
            //if(Session["username"] == null) return RedirectToAction("Index", "Home");
            //if(Session["username"] == null) return Content("<script language='javascript' type='text/javascript'>alert('Login to continue');</script>");
            return View();
        }

        [HttpGet]
        public ActionResult Searching()
        {
            var type = Request["type"].ToString();
            var sortby = Request["sortby"].ToString();
            var startTime = Request["startTime"].ToString();
            //var endTime = Request["endTime"].ToString();

            if(type == "temporary")
            {
                type = "1.......";
            }

            DBHelper db = DBHelper.getDB();
            string sql = "SELECT count(WorkerId) from Worker where type = '" + type + "' ";
            //select insert(str, 3, 1, '*')
            //select SUBSTRING(@meme,2,1)
            return View("~/Views/Service/Service.cshtml");
        }
    }
}