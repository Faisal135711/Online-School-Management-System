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
    public class GUARDIANREVIEWsController : Controller
    {
        private OSMSEntities db = new OSMSEntities();

        // GET: GUARDIANREVIEWs
        public ActionResult Index()
        {
            var gUARDIANREVIEWs = db.GUARDIANREVIEWs.Include(g => g.GUARDIAN);
            return View(gUARDIANREVIEWs.ToList());
        }

        // GET: GUARDIANREVIEWs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GUARDIANREVIEW gUARDIANREVIEW = db.GUARDIANREVIEWs.Find(id);
            if (gUARDIANREVIEW == null)
            {
                return HttpNotFound();
            }
            return View(gUARDIANREVIEW);
        }

        // GET: GUARDIANREVIEWs/Create
        public ActionResult Create()
        {
            ViewBag.Guardian_ID = new SelectList(db.GUARDIANs, "Guardian_ID", "GuardianName");
            return View();
        }

        // POST: GUARDIANREVIEWs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Review_ID,ReviewDescription")] GUARDIANREVIEW gUARDIANREVIEW)
        {
            if (ModelState.IsValid)
            {
                gUARDIANREVIEW.Guardian_ID= Convert.ToInt32(Session["GuardianId"]);
                db.GUARDIANREVIEWs.Add(gUARDIANREVIEW);
                db.SaveChanges();
              //  return RedirectToAction("Index");
            }

            ViewBag.Guardian_ID = new SelectList(db.GUARDIANs, "Guardian_ID", "GuardianName", gUARDIANREVIEW.Guardian_ID);
            ModelState.Clear();
            ViewBag.Message = "Your review has been sent";
            return View();
           // return View(gUARDIANREVIEW);
        }

        // GET: GUARDIANREVIEWs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GUARDIANREVIEW gUARDIANREVIEW = db.GUARDIANREVIEWs.Find(id);
            if (gUARDIANREVIEW == null)
            {
                return HttpNotFound();
            }
            ViewBag.Guardian_ID = new SelectList(db.GUARDIANs, "Guardian_ID", "GuardianName", gUARDIANREVIEW.Guardian_ID);
            return View(gUARDIANREVIEW);
        }

        // POST: GUARDIANREVIEWs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Review_ID,ReviewDescription,Guardian_ID")] GUARDIANREVIEW gUARDIANREVIEW)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gUARDIANREVIEW).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Guardian_ID = new SelectList(db.GUARDIANs, "Guardian_ID", "GuardianName", gUARDIANREVIEW.Guardian_ID);
            return View(gUARDIANREVIEW);
        }

        // GET: GUARDIANREVIEWs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GUARDIANREVIEW gUARDIANREVIEW = db.GUARDIANREVIEWs.Find(id);
            if (gUARDIANREVIEW == null)
            {
                return HttpNotFound();
            }
            return View(gUARDIANREVIEW);
        }

        // POST: GUARDIANREVIEWs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GUARDIANREVIEW gUARDIANREVIEW = db.GUARDIANREVIEWs.Find(id);
            db.GUARDIANREVIEWs.Remove(gUARDIANREVIEW);
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
