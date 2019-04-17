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
    public class SUBJECTsController : Controller
    {
        private OSMSEntities db = new OSMSEntities();

        // GET: SUBJECTs
        public ActionResult Index()
        {
            var sUBJECTs = db.SUBJECTs.Include(s => s.ADMIN).Include(s => s.CLASS).Include(s => s.TEACHER);
            return View(sUBJECTs.ToList());
        }

        // GET: SUBJECTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUBJECT sUBJECT = db.SUBJECTs.Find(id);
            if (sUBJECT == null)
            {
                return HttpNotFound();
            }
            return View(sUBJECT);
        }

        // GET: SUBJECTs/Create
        public ActionResult Create()
        {
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName");
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName");
            ViewBag.Teacher_ID = new SelectList(db.TEACHERs, "Teacher_ID", "TeacherName");
            return View();
        }

        // POST: SUBJECTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Subject_ID,SubjectName,Class_ID,Teacher_ID,Admin_ID")] SUBJECT sUBJECT)
        {
            if (ModelState.IsValid)
            {
                db.SUBJECTs.Add(sUBJECT);
                db.SaveChanges();
              //  return RedirectToAction("Index");
            }

            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", sUBJECT.Admin_ID);
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName", sUBJECT.Class_ID);
            ViewBag.Teacher_ID = new SelectList(db.TEACHERs, "Teacher_ID", "TeacherName", sUBJECT.Teacher_ID);

            ModelState.Clear();
            ViewBag.Message = "successfully created";
            return View();
           // return View(sUBJECT);
        }

        // GET: SUBJECTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUBJECT sUBJECT = db.SUBJECTs.Find(id);
            if (sUBJECT == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", sUBJECT.Admin_ID);
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName", sUBJECT.Class_ID);
            ViewBag.Teacher_ID = new SelectList(db.TEACHERs, "Teacher_ID", "TeacherName", sUBJECT.Teacher_ID);
            return View(sUBJECT);
        }

        // POST: SUBJECTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Subject_ID,SubjectName,Class_ID,Teacher_ID,Admin_ID")] SUBJECT sUBJECT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUBJECT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", sUBJECT.Admin_ID);
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName", sUBJECT.Class_ID);
            ViewBag.Teacher_ID = new SelectList(db.TEACHERs, "Teacher_ID", "TeacherName", sUBJECT.Teacher_ID);
            return View(sUBJECT);
        }

        // GET: SUBJECTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUBJECT sUBJECT = db.SUBJECTs.Find(id);
            if (sUBJECT == null)
            {
                return HttpNotFound();
            }
            return View(sUBJECT);
        }

        // POST: SUBJECTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SUBJECT sUBJECT = db.SUBJECTs.Find(id);
            db.SUBJECTs.Remove(sUBJECT);
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
