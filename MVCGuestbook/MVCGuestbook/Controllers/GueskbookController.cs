using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCGuestbook.Models;

namespace MVCGuestbook.Controllers
{
    public class GueskbookController : Controller
    {
        private MVCGuestbookContext db = new MVCGuestbookContext();

        //
        // GET: /Gueskbook/

        public ActionResult Index()
        {
            return View(db.Gueskbooks.ToList());
        }

        //
        // GET: /Gueskbook/Details/5

        public ActionResult Details(int id = 0)
        {
            Gueskbook gueskbook = db.Gueskbooks.Find(id);
            if (gueskbook == null)
            {
                return HttpNotFound();
            }
            return View(gueskbook);
        }

        //
        // GET: /Gueskbook/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Gueskbook/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gueskbook gueskbook)
        {
            if (ModelState.IsValid)
            {
                db.Gueskbooks.Add(gueskbook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gueskbook);
        }

        //
        // GET: /Gueskbook/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Gueskbook gueskbook = db.Gueskbooks.Find(id);
            if (gueskbook == null)
            {
                return HttpNotFound();
            }
            return View(gueskbook);
        }

        //
        // POST: /Gueskbook/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Gueskbook gueskbook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gueskbook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gueskbook);
        }

        //
        // GET: /Gueskbook/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Gueskbook gueskbook = db.Gueskbooks.Find(id);
            if (gueskbook == null)
            {
                return HttpNotFound();
            }
            return View(gueskbook);
        }

        //
        // POST: /Gueskbook/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gueskbook gueskbook = db.Gueskbooks.Find(id);
            db.Gueskbooks.Remove(gueskbook);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}