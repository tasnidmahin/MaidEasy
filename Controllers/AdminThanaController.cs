using MaidEasy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaidEasy.Controllers
{
    public class AdminThanaController : Controller
    {
        // GET: AdminThana
        ThanaContext Db = new ThanaContext();
        
        public ActionResult Index()
        {
            
            return View(Db.thana.ToList());
        }

        // GET: AdminThana/Details/5
        public ActionResult Details(int id)
        {
            thana thana = Db.thana.Find(id);
            if (thana == null)
            {
                return HttpNotFound();
            }
            return View(thana);
        }

        // GET: AdminThana/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminThana/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "")] thana thana  )
        {
            
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Db.thana.Add(thana);
                    Db.SaveChanges();
              
                }

                return RedirectToAction("Index");
        }

        // GET: AdminThana/Edit/5
        public ActionResult Edit(int id)
        {
            thana thana = Db.thana.Find(id);
            if (thana == null)
            {
                return HttpNotFound();
            }
            return View(thana);
        }

        // POST: AdminThana/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "")] thana thana)
        {
            if (ModelState.IsValid)
            {
                Db.Entry(thana).State = EntityState.Modified;
                Db.SaveChanges();              
            }
            return RedirectToAction("Index");
        }

        // GET: AdminThana/Delete/5
        public ActionResult Delete(int id)
        {
            thana thana = Db.thana.Find(id);
            if (thana == null)
            {
                return HttpNotFound();
            }
            return View(thana);
        }

        // POST: AdminThana/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            thana thana = Db.thana.Find(id);
            Db.thana.Remove(thana);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
