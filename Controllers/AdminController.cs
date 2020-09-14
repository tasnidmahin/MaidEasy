using MaidEasy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaidEasy.Controllers
{
    public class AdminController : Controller
    {
        CustomDbContext Db = new CustomDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Admin_home()
        {
            /*if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["username"] == null) return Content("<script language='javascript' type='text/javascript'>alert('Login to continue');</script>");*/
            return View();
        }
        public ActionResult Worklist()
        {
            return View(Db.work.ToList());
        }

        [HttpPost]
        public ActionResult WorklistEdit([Bind(Include = "")] work work)
        {
            if (ModelState.IsValid)
            {
                Db.Entry(work).State = EntityState.Modified;
                Db.SaveChanges();
            }
            return RedirectToAction("Worklist");
        }

        [HttpPost]
        public ActionResult WorklistCreate([Bind(Include = "")] work work)
        {

            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                Db.work.Add(work);
                Db.SaveChanges();

            }

            return RedirectToAction("Worklist");
        }

        [HttpPost, ActionName("WorklistDelete")]
        public ActionResult WorklistDeleteConfirm(int id)
        {
            work work = Db.work.Find(id);
            Db.work.Remove(work);
            Db.SaveChanges();
            Session["ADMIN_WORKLIST_DELETE"] = "Admin Worklist Delete";
            return RedirectToAction("Worklist");
        }

        public ActionResult Edit_work()
        {
            return View();
        }

    }
}