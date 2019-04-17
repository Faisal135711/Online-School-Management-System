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
    public class ATTENDANCEsController : Controller
    {
        private OSMSEntities db = new OSMSEntities();

        // GET: ATTENDANCEs
        public ActionResult Index()
        {
            var aTTENDANCEs = db.ATTENDANCEs.Include(a => a.ADMIN).Include(a => a.CLASS).Include(a => a.SECTION).Include(a => a.STUDENT).Include(a => a.SUBJECT).Include(a => a.TEACHER);
            return View(aTTENDANCEs.ToList());
        }

        // GET: ATTENDANCEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ATTENDANCE aTTENDANCE = db.ATTENDANCEs.Find(id);
            if (aTTENDANCE == null)
            {
                return HttpNotFound();
            }
            return View(aTTENDANCE);
        }

        // GET: ATTENDANCEs/Create
        public ActionResult Create()
        {
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName");
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName");
            ViewBag.Section_ID = new SelectList(db.SECTIONs, "Section_ID", "SectionName");
            ViewBag.Student_ID = new SelectList(db.STUDENTs, "Student_ID", "StudentName");
            ViewBag.Subject_ID = new SelectList(db.SUBJECTs, "Subject_ID", "SubjectName");
            ViewBag.Teacher_ID = new SelectList(db.TEACHERs, "Teacher_ID", "TeacherName");
            return View();
        }

        // POST: ATTENDANCEs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Attendance_ID,Attendance_Date,Attendance_Status,Student_ID,Class_ID,Section_ID,Subject_ID,Teacher_ID,Admin_ID")] ATTENDANCE aTTENDANCE)
        {
            if (ModelState.IsValid)
            {
                db.ATTENDANCEs.Add(aTTENDANCE);
                db.SaveChanges();
               // return RedirectToAction("Index");
            }


            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", aTTENDANCE.Admin_ID);
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName", aTTENDANCE.Class_ID);
            ViewBag.Section_ID = new SelectList(db.SECTIONs, "Section_ID", "SectionName", aTTENDANCE.Section_ID);
            ViewBag.Student_ID = new SelectList(db.STUDENTs, "Student_ID", "StudentName", aTTENDANCE.Student_ID);
            ViewBag.Subject_ID = new SelectList(db.SUBJECTs, "Subject_ID", "SubjectName", aTTENDANCE.Subject_ID);
            ViewBag.Teacher_ID = new SelectList(db.TEACHERs, "Teacher_ID", "TeacherName", aTTENDANCE.Teacher_ID);

            ModelState.Clear();
            ViewBag.Message = "successfully created";
            return View();
           // return View(aTTENDANCE);
        }

        // GET: ATTENDANCEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ATTENDANCE aTTENDANCE = db.ATTENDANCEs.Find(id);
            if (aTTENDANCE == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", aTTENDANCE.Admin_ID);
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName", aTTENDANCE.Class_ID);
            ViewBag.Section_ID = new SelectList(db.SECTIONs, "Section_ID", "SectionName", aTTENDANCE.Section_ID);
            ViewBag.Student_ID = new SelectList(db.STUDENTs, "Student_ID", "StudentName", aTTENDANCE.Student_ID);
            ViewBag.Subject_ID = new SelectList(db.SUBJECTs, "Subject_ID", "SubjectName", aTTENDANCE.Subject_ID);
            ViewBag.Teacher_ID = new SelectList(db.TEACHERs, "Teacher_ID", "TeacherName", aTTENDANCE.Teacher_ID);
            return View(aTTENDANCE);
        }

        // POST: ATTENDANCEs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Attendance_ID,Attendance_Date,Attendance_Status,Student_ID,Class_ID,Section_ID,Subject_ID,Teacher_ID,Admin_ID")] ATTENDANCE aTTENDANCE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aTTENDANCE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", aTTENDANCE.Admin_ID);
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName", aTTENDANCE.Class_ID);
            ViewBag.Section_ID = new SelectList(db.SECTIONs, "Section_ID", "SectionName", aTTENDANCE.Section_ID);
            ViewBag.Student_ID = new SelectList(db.STUDENTs, "Student_ID", "StudentName", aTTENDANCE.Student_ID);
            ViewBag.Subject_ID = new SelectList(db.SUBJECTs, "Subject_ID", "SubjectName", aTTENDANCE.Subject_ID);
            ViewBag.Teacher_ID = new SelectList(db.TEACHERs, "Teacher_ID", "TeacherName", aTTENDANCE.Teacher_ID);
            return View(aTTENDANCE);
        }

        // GET: ATTENDANCEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ATTENDANCE aTTENDANCE = db.ATTENDANCEs.Find(id);
            if (aTTENDANCE == null)
            {
                return HttpNotFound();
            }
            return View(aTTENDANCE);
        }

        // POST: ATTENDANCEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ATTENDANCE aTTENDANCE = db.ATTENDANCEs.Find(id);
            db.ATTENDANCEs.Remove(aTTENDANCE);
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

        public ActionResult ViewAttendaceForStudents()
        {
            int a;
            a = Convert.ToInt32(Session["StudentId"]);
            var query = db.ATTENDANCEs.SqlQuery("Select *from Attendance where Student_Id="+a+"").ToList();

            return View(query);
        }


        public ActionResult ViewAttendaceForStudentsByDate()
        {
            int a;
            a = Convert.ToInt32(Session["StudentId"]);
            string b = Session["StudentSearchDate"].ToString();
            var query = db.ATTENDANCEs.SqlQuery("Select *from Attendance where Student_Id=" + a + " AND Attendance_Date LIKE'"+b+"'").ToList();

            return View(query);
        }

        public ActionResult ViewAttendaceForGuardian()
        {
            int a;
            a = Convert.ToInt32(Session["MychildId"]);
            var query = db.ATTENDANCEs.SqlQuery("Select *from Attendance where Student_Id=" + a + "").ToList();
            return View(query);
        }

        public ActionResult ViewAttendaceForGuardiansByDate()
        {
            int a;
            a = Convert.ToInt32(Session["MychildId"]);
            string b = Session["StudentSearchDate1"].ToString();
            var query = db.ATTENDANCEs.SqlQuery("Select *from Attendance where Student_Id=" + a + " AND Attendance_Date LIKE'" + b + "'").ToList();
            return View(query);
        }

        public ActionResult ViewAttendaceForTeacher()
        {
            int a = Convert.ToInt32(Session["TeacherID"]);
            var query = db.ATTENDANCEs.SqlQuery("Select *from Attendance where Teacher_Id=" + a + "").ToList();
            return View(query);
        }

        public ActionResult ViewAttendaceForTeachersByDate()
        {
            int a = Convert.ToInt32(Session["TeacherID"]);
            string b = Session["TeacherSearchDate"].ToString();
            var query = db.ATTENDANCEs.SqlQuery("Select *from Attendance where Teacher_Id=" + a + " AND Attendance_Date LIKE'%" + b + "%'").ToList();
            return View(query);
        }
    }
}
