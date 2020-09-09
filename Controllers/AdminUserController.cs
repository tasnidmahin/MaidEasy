using MaidEasy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public ActionResult MakeAdmin(int id)
        {
            user user = dbContext.users.Find(id);
            user.type = "admin";

            if (ModelState.IsValid)
            {
                dbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("UserList", "AdminUser");
            }

            return RedirectToAction("UserList", "AdminUser");
        }

        public ActionResult RemoveAdmin(int id)
        {
            user user = dbContext.users.Find(id);
            user.type = "general";

            if (ModelState.IsValid)
            {
                dbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("UserList", "AdminUser");
            }

            return RedirectToAction("UserList", "AdminUser");
        }

        public ActionResult BlockUser(int id)
        {
            user user = dbContext.users.Find(id);
            user.type = "blocked";

            if (ModelState.IsValid)
            {
                dbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("UserList", "AdminUser");
            }

            return RedirectToAction("UserList", "AdminUser");
        }

        public ActionResult UnblockUser(int id)
        {
            user user = dbContext.users.Find(id);
            user.type = "general";

            if (ModelState.IsValid)
            {
                dbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("UserList", "AdminUser");
            }

            return RedirectToAction("UserList", "AdminUser");
        }
    }
}