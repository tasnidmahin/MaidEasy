using MaidEasy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaidEasy.Controllers
{
    public class AdminWorkController : Controller
    {

        CustomDbContext Db = new CustomDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        // GET: AdminWork/Details/5
        public ActionResult Details(int id)
        {
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["uType"] == null) return RedirectToAction("Index", "Home");
            return View();
        }

        // GET: AdminWork/Create
        public ActionResult Create()
        {
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["uType"] == null) return RedirectToAction("Index", "Home");
            return View();
        }

        // POST: AdminWork/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["uType"] == null) return RedirectToAction("Index", "Home");
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminWork/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["uType"] == null) return RedirectToAction("Index", "Home");
            return View();
        }

        // POST: AdminWork/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["uType"] == null) return RedirectToAction("Index", "Home");
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminWork/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["uType"] == null) return RedirectToAction("Index", "Home");
            return View();
        }

        // POST: AdminWork/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["uType"] == null) return RedirectToAction("Index", "Home");
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Worklist()
        {
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["uType"] == null) return RedirectToAction("Index", "Home");
            return View(Db.work.ToList());
        }

        [HttpPost]
        public ActionResult WorklistEdit([Bind(Include = "")] work work)
        {
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["uType"] == null) return RedirectToAction("Index", "Home");
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
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["uType"] == null) return RedirectToAction("Index", "Home");
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
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["uType"] == null) return RedirectToAction("Index", "Home");
            work work = Db.work.Find(id);
            Db.work.Remove(work);
            Db.SaveChanges();
            Session["ADMIN_WORKLIST_DELETE"] = "Admin Worklist Delete";
            return RedirectToAction("Worklist");
        }

        public ActionResult Edit_work()
        {
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["uType"] == null) return RedirectToAction("Index", "Home");
            return View();
        }
    }
}
