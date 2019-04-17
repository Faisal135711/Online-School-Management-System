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
    public class PAYMENTsController : Controller
    {
        private OSMSEntities db = new OSMSEntities();

        // GET: PAYMENTs
        public ActionResult Index()
        {
            var pAYMENTs = db.PAYMENTs.Include(p => p.STUDENT);
            return View(pAYMENTs.ToList());
        }

        // GET: PAYMENTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAYMENT pAYMENT = db.PAYMENTs.Find(id);
            if (pAYMENT == null)
            {
                return HttpNotFound();
            }
            return View(pAYMENT);
        }

        // GET: PAYMENTs/Create
        public ActionResult Create()
        {
            ViewBag.Student_ID = new SelectList(db.STUDENTs, "Student_ID", "StudentName");
            return View();
        }

        // POST: PAYMENTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Payment_ID,PaymentAmount,PaymentDate,PaymentMonth")] PAYMENT pAYMENT)
        {
            if (ModelState.IsValid)
            {
                pAYMENT.Student_ID = Convert.ToInt32(Session["StudentId"]);
                db.PAYMENTs.Add(pAYMENT);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            ViewBag.Student_ID = new SelectList(db.STUDENTs, "Student_ID", "StudentName", pAYMENT.Student_ID);
            ModelState.Clear();
            ViewBag.Message = "successfully registered";
            return View();
            // return View(pAYMENT);
        }

        // GET: PAYMENTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAYMENT pAYMENT = db.PAYMENTs.Find(id);
            if (pAYMENT == null)
            {
                return HttpNotFound();
            }
            ViewBag.Student_ID = new SelectList(db.STUDENTs, "Student_ID", "StudentName", pAYMENT.Student_ID);
            return View(pAYMENT);
        }

        // POST: PAYMENTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Payment_ID,PaymentAmount,PaymentDate,PaymentMonth,Student_ID")] PAYMENT pAYMENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pAYMENT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Student_ID = new SelectList(db.STUDENTs, "Student_ID", "StudentName", pAYMENT.Student_ID);
            return View(pAYMENT);
        }

        // GET: PAYMENTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAYMENT pAYMENT = db.PAYMENTs.Find(id);
            if (pAYMENT == null)
            {
                return HttpNotFound();
            }
            return View(pAYMENT);
        }

        // POST: PAYMENTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PAYMENT pAYMENT = db.PAYMENTs.Find(id);
            db.PAYMENTs.Remove(pAYMENT);
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
