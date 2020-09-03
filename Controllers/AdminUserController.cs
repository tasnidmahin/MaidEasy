using MaidEasy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaidEasy.Controllers
{
   
    public class AdminUserController : Controller
    {
        DemoEntities dbContext = new DemoEntities();
        // GET: AdminUser
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserList()
        {
            return View(dbContext.users.ToList());
        }
    }
}