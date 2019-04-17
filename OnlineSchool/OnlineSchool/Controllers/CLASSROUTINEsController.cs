using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineSchool.Models;
using OnlineSchool.Controllers;
using System.Text;
using System.IO;
using System.Web.UI.WebControls;
using Dapper;
using System.Configuration;
using System.Data.SqlClient;

namespace OnlineSchool.Controllers
{
    public class CLASSROUTINEsController : Controller
    {
        private OSMSEntities db = new OSMSEntities();

        // GET: CLASSROUTINEs
        public ActionResult Index()
        {
            var cLASSROUTINEs = db.CLASSROUTINEs.Include(c => c.ADMIN).Include(c => c.CLASS).Include(c => c.SECTION);
            return View(cLASSROUTINEs.ToList());
        }

        // GET: CLASSROUTINEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLASSROUTINE cLASSROUTINE = db.CLASSROUTINEs.Find(id);
            if (cLASSROUTINE == null)
            {
                return HttpNotFound();
            }
            return View(cLASSROUTINE);
        }

        // GET: CLASSROUTINEs/Create
        public ActionResult Create()
        {
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName");
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName");
            ViewBag.Section_ID = new SelectList(db.SECTIONs, "Section_ID", "SectionName");
            return View();
        }

        // POST: CLASSROUTINEs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file,[Bind(Include = "ClassRoutine_ID,Class_ID,Section_ID,Admin_ID")] CLASSROUTINE cLASSROUTINE)
        {

            if (file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string filepath = Path.Combine(Server.MapPath("~/CLASS ROUTINE FILES"), filename);
                file.SaveAs(filepath);
                cLASSROUTINE.ClassRoutineDirectory = filename;
            }

            if (ModelState.IsValid)
            {
                db.CLASSROUTINEs.Add(cLASSROUTINE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", cLASSROUTINE.Admin_ID);
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName", cLASSROUTINE.Class_ID);
            ViewBag.Section_ID = new SelectList(db.SECTIONs, "Section_ID", "SectionName", cLASSROUTINE.Section_ID);
            ViewBag.Message = "successfully created";

            return View(cLASSROUTINE);
        }

        // GET: CLASSROUTINEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLASSROUTINE cLASSROUTINE = db.CLASSROUTINEs.Find(id);
            if (cLASSROUTINE == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", cLASSROUTINE.Admin_ID);
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName", cLASSROUTINE.Class_ID);
            ViewBag.Section_ID = new SelectList(db.SECTIONs, "Section_ID", "SectionName", cLASSROUTINE.Section_ID);
            return View(cLASSROUTINE);
        }

        // POST: CLASSROUTINEs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassRoutine_ID,ClassRoutineDirectory,Class_ID,Section_ID,Admin_ID")] CLASSROUTINE cLASSROUTINE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cLASSROUTINE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", cLASSROUTINE.Admin_ID);
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName", cLASSROUTINE.Class_ID);
            ViewBag.Section_ID = new SelectList(db.SECTIONs, "Section_ID", "SectionName", cLASSROUTINE.Section_ID);
            return View(cLASSROUTINE);
        }

        // GET: CLASSROUTINEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLASSROUTINE cLASSROUTINE = db.CLASSROUTINEs.Find(id);
            if (cLASSROUTINE == null)
            {
                return HttpNotFound();
            }
            return View(cLASSROUTINE);
        }

        // POST: CLASSROUTINEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CLASSROUTINE cLASSROUTINE = db.CLASSROUTINEs.Find(id);
            db.CLASSROUTINEs.Remove(cLASSROUTINE);
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


        public ActionResult DownloadClassRoutineForStudents()
        {
            
            int a, b;
            a = Convert.ToInt32(Session["StudentClass"]);
            b = Convert.ToInt32(Session["StudentSection"]);

            var query = db.CLASSROUTINEs.SqlQuery("Select *from CLASSROUTINE where CLass_ID=" + a + " AND Section_ID=" + b + "").ToList();
            return View(query);
        }

        public ActionResult DownloadClassRoutineForGuardians()
        {
            int a;
            a = Convert.ToInt32(Session["MychildId"]);
           

            var query = db.CLASSROUTINEs.SqlQuery("SElect *from CLASSROUTINE where Class_ID IN (Select CLASS_Id From STUDENT where Student_ID="+a+") and Section_ID IN(Select Section_ID from STUDENT where Student_ID="+a+")").ToList();
            return View(query);
        }
    }
}
