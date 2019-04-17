using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineSchool.Models;

namespace OnlineSchool.Controllers
{
    public class SECTIONsController : Controller
    {
        private OSMSEntities db = new OSMSEntities();

        // GET: SECTIONs
        public ActionResult Index()
        {
            var sECTIONs = db.SECTIONs.Include(s => s.ADMIN).Include(s => s.CLASS);
            return View(sECTIONs.ToList());
        }

        // GET: SECTIONs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SECTION sECTION = db.SECTIONs.Find(id);
            if (sECTION == null)
            {
                return HttpNotFound();
            }
            return View(sECTION);
        }

        // GET: SECTIONs/Create
        public ActionResult Create()
        {
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName");
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName");
            return View();
        }

        // POST: SECTIONs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Section_ID,SectionName,TotalStudents,Admin_ID,Class_ID,SectionGroup")] SECTION sECTION)
        {
            if (ModelState.IsValid)
            {
                db.SECTIONs.Add(sECTION);
                db.SaveChanges();
               // return RedirectToAction("Index");
            }

            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", sECTION.Admin_ID);
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName", sECTION.Class_ID);
            ModelState.Clear();
            ViewBag.Message = "successfully created";
            return View();
           // return View(sECTION);
        }

        // GET: SECTIONs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SECTION sECTION = db.SECTIONs.Find(id);
            if (sECTION == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", sECTION.Admin_ID);
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName", sECTION.Class_ID);
            return View(sECTION);
        }

        // POST: SECTIONs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Section_ID,SectionName,TotalStudents,Admin_ID,Class_ID,SectionGroup")] SECTION sECTION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sECTION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", sECTION.Admin_ID);
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName", sECTION.Class_ID);
            return View(sECTION);
        }

        // GET: SECTIONs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SECTION sECTION = db.SECTIONs.Find(id);
            if (sECTION == null)
            {
                return HttpNotFound();
            }
            return View(sECTION);
        }

        // POST: SECTIONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SECTION sECTION = db.SECTIONs.Find(id);
            db.SECTIONs.Remove(sECTION);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
