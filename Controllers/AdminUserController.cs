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
            IEnumerable<MaidEasy.Models.user> userList = dbContext.users.ToList();
            RegisterController regC = new RegisterController();
            foreach (var item in userList)
            {
                int id = regC.getThanaID(item.thana);
                thana thana = dbContext.thanas.Find(id);
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