using MaidEasy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["uType"] == null) return RedirectToAction("Index", "Home");

            DemoEntities db = new DemoEntities();
            //IEnumerable<MaidEasy.Models.user> us = db.users.ToList();


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
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["uType"] == null) return RedirectToAction("Index", "Home");

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
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["uType"] == null) return RedirectToAction("Index", "Home");

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
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["uType"] == null) return RedirectToAction("Index", "Home");

            user user = dbContext.users.Find(id);
            user.type = "blocked";

            cancelBlockedUserContracts(id);
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
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["uType"] == null) return RedirectToAction("Index", "Home");

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

        public void cancelBlockedUserContracts(int id)
        {
            var query = from c in dbContext.contracts
                        where c.UserId == id && c.status == "current"
                        select new
                        {
                            workerId = c.WorkerId,
                            startTime = c.startTime,
                            endTime = c.endTime,
                            contractId = c.Id,
                        };

            List<cancelUsercontract> workerIds = new List<cancelUsercontract>();
            List<int> contractIds = new List<int>();

            foreach (var item in query)
            {
                cancelUsercontract cuc = new cancelUsercontract();
                cuc.workerID = item.workerId;
                cuc.startTime = item.startTime;
                cuc.endTime = item.endTime;
                workerIds.Add(cuc);
                contractIds.Add(item.contractId);
            }

            foreach (var item in workerIds)
            {
                worker worker = dbContext.worker.Find(item.workerID);
                string startTime = item.startTime;
                string endTime = item.endTime;
                System.Diagnostics.Debug.WriteLine("------start and end time--------");
                System.Diagnostics.Debug.WriteLine(startTime);
                System.Diagnostics.Debug.WriteLine(endTime);
                System.Diagnostics.Debug.WriteLine("--------------");
                int start = convert(startTime);
                int end = convert(endTime);

                worker.status = setStatus(worker.status, start, end);
                if (ModelState.IsValid)
                {
                    dbContext.Entry(worker).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            foreach(var item in contractIds)
            { 
                contract contract = dbContext.contracts.Find(item);
                string Month = DateTime.Now.ToString("MM");
                string Year = DateTime.Now.ToString("yyyy");
                Year = Month + "/" + Year;

                contract.EndMonth = Year;
                contract.status = "previous";
                if (ModelState.IsValid)
                {
                    dbContext.Entry(contract).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }

            return;
        }

        private int convert(string end)
        {
            System.Diagnostics.Debug.WriteLine("--------------");
            System.Diagnostics.Debug.WriteLine(end);
            System.Diagnostics.Debug.WriteLine("--------------");
            if (end == "6:00 AM" || end == "6" || end == "6:00") return 0;
            else if (end == "6:30 AM" || end == "6:30") return 1;
            else if (end == "7:00 AM" || end == "7" || end == "7:00") return 2;
            else if (end == "7:30 AM" || end == "7:30") return 3;
            else if (end == "8:00 AM" || end == "8" || end == "8:00") return 4;
            else if (end == "8:30 AM" || end == "8:30") return 5;
            else if (end == "9:00 AM" || end == "9" || end == "9:00") return 6;
            else if (end == "9:30 AM" || end == "9:30") return 7;
            else if (end == "10:00 AM" || end == "10" || end == "10:00") return 8;
            else if (end == "10:30 AM" || end == "10:30") return 9;
            else if (end == "11:00 AM" || end == "11" || end == "11:00") return 10;
            else if (end == "11:30 AM" || end == "11:30") return 11;
            else if (end == "12:00 PM" || end == "12" || end == "12:00") return 12;
            else if (end == "12:30 PM" || end == "12:30") return 13;
            else if (end == "1:00 PM" || end == "1" || end == "1:00") return 14;
            else if (end == "1:30 PM" || end == "1:30") return 15;
            else if (end == "2:00 PM" || end == "2" || end == "2:00") return 16;
            else if (end == "2:30 PM" || end == "2:30") return 17;
            else if (end == "3:00 PM" || end == "3" || end == "3:00") return 18;
            else if (end == "3:30 PM" || end == "3:30") return 19;
            else if (end == "4:00 PM" || end == "4" || end == "4:00") return 20;
            else if (end == "4:30 PM" || end == "4:30") return 21;
            else if (end == "5:00 PM" || end == "5" || end == "5:00") return 22;
            else if (end == "5:30 PM" || end == "5:30") return 23;
            else return 24;
        }

        private string setStatus(string status, int start, int end)
        {
            System.Diagnostics.Debug.WriteLine("-----Set Status---------");
            System.Diagnostics.Debug.WriteLine(status);
            System.Diagnostics.Debug.WriteLine(start);
            System.Diagnostics.Debug.WriteLine(end);
            System.Diagnostics.Debug.WriteLine("--------------");
            StringBuilder sb = new StringBuilder();
            sb.Append(status);
            for (int i = start; i <= end; i++)
            {
                sb[i] = '0';
            }
            status = sb.ToString();
            return status;
        }

    }
}