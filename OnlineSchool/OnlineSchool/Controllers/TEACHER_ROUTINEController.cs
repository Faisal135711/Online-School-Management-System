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
    public class TEACHER_ROUTINEController : Controller
    {
        private OSMSEntities db = new OSMSEntities();

        // GET: TEACHER_ROUTINE
        public ActionResult Index()
        {
            var tEACHER_ROUTINE = db.TEACHER_ROUTINE.Include(t => t.ADMIN).Include(t => t.TEACHER);
            return View(tEACHER_ROUTINE.ToList());
        }

        // GET: TEACHER_ROUTINE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEACHER_ROUTINE tEACHER_ROUTINE = db.TEACHER_ROUTINE.Find(id);
            if (tEACHER_ROUTINE == null)
            {
                return HttpNotFound();
            }
            return View(tEACHER_ROUTINE);
        }

        // GET: TEACHER_ROUTINE/Create
        public ActionResult Create()
        {
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName");
            ViewBag.Teacher_ID = new SelectList(db.TEACHERs, "Teacher_ID", "TeacherName");
            return View();
        }

        // POST: TEACHER_ROUTINE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file,[Bind(Include = "TeacherRoutine_ID,Teacher_ID,Admin_ID")] TEACHER_ROUTINE tEACHER_ROUTINE)
        {
            if (file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string filepath = Path.Combine(Server.MapPath("~/TEACHER ROUTINE"), filename);
                file.SaveAs(filepath);
                tEACHER_ROUTINE.TeacherRoutineDirectory = filename;
            }
                if (ModelState.IsValid)
            {
                db.TEACHER_ROUTINE.Add(tEACHER_ROUTINE);
                db.SaveChanges();
             //   return RedirectToAction("Index");
            }

            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", tEACHER_ROUTINE.Admin_ID);
            ViewBag.Teacher_ID = new SelectList(db.TEACHERs, "Teacher_ID", "TeacherName", tEACHER_ROUTINE.Teacher_ID);
            ModelState.Clear();
            ViewBag.Message = "successfully created";
            return View();
           
          //  return View(tEACHER_ROUTINE);
        }

        // GET: TEACHER_ROUTINE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEACHER_ROUTINE tEACHER_ROUTINE = db.TEACHER_ROUTINE.Find(id);
            if (tEACHER_ROUTINE == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", tEACHER_ROUTINE.Admin_ID);
            ViewBag.Teacher_ID = new SelectList(db.TEACHERs, "Teacher_ID", "TeacherName", tEACHER_ROUTINE.Teacher_ID);
            return View(tEACHER_ROUTINE);
        }

        // POST: TEACHER_ROUTINE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherRoutine_ID,TeacherRoutineDirectory,Teacher_ID,Admin_ID")] TEACHER_ROUTINE tEACHER_ROUTINE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tEACHER_ROUTINE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", tEACHER_ROUTINE.Admin_ID);
            ViewBag.Teacher_ID = new SelectList(db.TEACHERs, "Teacher_ID", "TeacherName", tEACHER_ROUTINE.Teacher_ID);
            return View(tEACHER_ROUTINE);
        }

        // GET: TEACHER_ROUTINE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEACHER_ROUTINE tEACHER_ROUTINE = db.TEACHER_ROUTINE.Find(id);
            if (tEACHER_ROUTINE == null)
            {
                return HttpNotFound();
            }
            return View(tEACHER_ROUTINE);
        }

        // POST: TEACHER_ROUTINE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TEACHER_ROUTINE tEACHER_ROUTINE = db.TEACHER_ROUTINE.Find(id);
            db.TEACHER_ROUTINE.Remove(tEACHER_ROUTINE);
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

        public ActionResult DownloadTeacherRoutine()
        {
            int a;
            a = Convert.ToInt32(Session["TeacherID"]);
            var query = db.TEACHER_ROUTINE.SqlQuery("Select *from teacher_routine where Teacher_ID="+a+"").ToList();
            return View(query);
            
        }
    }
}