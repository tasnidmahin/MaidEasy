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
        CustomDbContext dbContext = new CustomDbContext();
        // GET: AdminUser
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserList()
        {
            DemoEntities db = new DemoEntities();
            IEnumerable<MaidEasy.Models.user> us = db.users.ToList();


            IEnumerable<MaidEasy.Models.user> userList = dbContext.users.ToList();
            RegisterController regC = new RegisterController();
            foreach (var item in userList)
            {
                int id = regC.getThanaID(item.thana);
                thana thana = dbContext.thana.Find(id);
                item.thana = thana.Name;
                System.Diagnostics.Debug.WriteLine("--------------");
                System.Diagnostics.Debug.WriteLine(thana.ThanaId);
                System.Diagnostics.Debug.WriteLine(thana.Name);
                System.Diagnostics.Debug.WriteLine("--------------");
            }
            return View(userList);
        }
    }
}