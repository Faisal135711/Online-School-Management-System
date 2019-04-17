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
    public class RESULTsController : Controller
    {
        private OSMSEntities db = new OSMSEntities();

        // GET: RESULTs
        public ActionResult Index()
        {
            var rESULTs = db.RESULTs.Include(r => r.ADMIN).Include(r => r.CLASS).Include(r => r.SECTION).Include(r => r.STUDENT).Include(r => r.SUBJECT).Include(r => r.TEACHER);
            return View(rESULTs.ToList());
        }

        // GET: RESULTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RESULT rESULT = db.RESULTs.Find(id);
            if (rESULT == null)
            {
                return HttpNotFound();
            }
            return View(rESULT);
        }

        // GET: RESULTs/Create
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

        // POST: RESULTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Result_ID,ExamDate,MarksObtained,Grade,Student_ID,Class_ID,Section_ID,Subject_ID,Teacher_ID,Admin_ID")] RESULT rESULT)
        {
            if (ModelState.IsValid)
            {
                db.RESULTs.Add(rESULT);
                db.SaveChanges();
             //   return RedirectToAction("Index");
            }

            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", rESULT.Admin_ID);
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName", rESULT.Class_ID);
            ViewBag.Section_ID = new SelectList(db.SECTIONs, "Section_ID", "SectionName", rESULT.Section_ID);
            ViewBag.Student_ID = new SelectList(db.STUDENTs, "Student_ID", "StudentName", rESULT.Student_ID);
            ViewBag.Subject_ID = new SelectList(db.SUBJECTs, "Subject_ID", "SubjectName", rESULT.Subject_ID);
            ViewBag.Teacher_ID = new SelectList(db.TEACHERs, "Teacher_ID", "TeacherName", rESULT.Teacher_ID);

            ModelState.Clear();
            ViewBag.Message = "successfully added";
            return View();
        //    return View(rESULT);
        }






        public ActionResult Create1()
        {
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName");
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName");
            ViewBag.Section_ID = new SelectList(db.SECTIONs, "Section_ID", "SectionName");
            ViewBag.Student_ID = new SelectList(db.STUDENTs, "Student_ID", "StudentName");
            ViewBag.Subject_ID = new SelectList(db.SUBJECTs, "Subject_ID", "SubjectName");
            ViewBag.Teacher_ID = new SelectList(db.TEACHERs, "Teacher_ID", "TeacherName");
            return View();
        }

        // POST: RESULTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1([Bind(Include = "Result_ID,ExamDate,MarksObtained,Grade,Student_ID,Class_ID,Section_ID,Subject_ID,Teacher_ID,Admin_ID")] RESULT rESULT)
        {
            if (ModelState.IsValid)
            {
                db.RESULTs.Add(rESULT);
                db.SaveChanges();
                //   return RedirectToAction("Index");
            }

            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", rESULT.Admin_ID);
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName", rESULT.Class_ID);
            ViewBag.Section_ID = new SelectList(db.SECTIONs, "Section_ID", "SectionName", rESULT.Section_ID);
            ViewBag.Student_ID = new SelectList(db.STUDENTs, "Student_ID", "StudentName", rESULT.Student_ID);
            ViewBag.Subject_ID = new SelectList(db.SUBJECTs, "Subject_ID", "SubjectName", rESULT.Subject_ID);
            ViewBag.Teacher_ID = new SelectList(db.TEACHERs, "Teacher_ID", "TeacherName", rESULT.Teacher_ID);

            ModelState.Clear();
            ViewBag.Message = "successfully added";
            return View();
            //    return View(rESULT);
        }




        // GET: RESULTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RESULT rESULT = db.RESULTs.Find(id);
            if (rESULT == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", rESULT.Admin_ID);
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName", rESULT.Class_ID);
            ViewBag.Section_ID = new SelectList(db.SECTIONs, "Section_ID", "SectionName", rESULT.Section_ID);
            ViewBag.Student_ID = new SelectList(db.STUDENTs, "Student_ID", "StudentName", rESULT.Student_ID);
            ViewBag.Subject_ID = new SelectList(db.SUBJECTs, "Subject_ID", "SubjectName", rESULT.Subject_ID);
            ViewBag.Teacher_ID = new SelectList(db.TEACHERs, "Teacher_ID", "TeacherName", rESULT.Teacher_ID);
            return View(rESULT);
        }

        // POST: RESULTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Result_ID,ExamDate,MarksObtained,Grade,Student_ID,Class_ID,Section_ID,Subject_ID,Teacher_ID,Admin_ID")] RESULT rESULT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rESULT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", rESULT.Admin_ID);
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName", rESULT.Class_ID);
            ViewBag.Section_ID = new SelectList(db.SECTIONs, "Section_ID", "SectionName", rESULT.Section_ID);
            ViewBag.Student_ID = new SelectList(db.STUDENTs, "Student_ID", "StudentName", rESULT.Student_ID);
            ViewBag.Subject_ID = new SelectList(db.SUBJECTs, "Subject_ID", "SubjectName", rESULT.Subject_ID);
            ViewBag.Teacher_ID = new SelectList(db.TEACHERs, "Teacher_ID", "TeacherName", rESULT.Teacher_ID);
            return View(rESULT);
        }

        // GET: RESULTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RESULT rESULT = db.RESULTs.Find(id);
            if (rESULT == null)
            {
                return HttpNotFound();
            }
            return View(rESULT);
        }

        // POST: RESULTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RESULT rESULT = db.RESULTs.Find(id);
            db.RESULTs.Remove(rESULT);
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
        public ActionResult ViewResultForStudents()
        {


            int a;
            a = Convert.ToInt32(Session["StudentId"]);
            var query = db.RESULTs.SqlQuery("Select *from Result where Student_Id=" + a + "").ToList();

            return View(query);
            

        }


        public ActionResult ViewResultForStudentsBySubject()
        {
            int a;
            a = Convert.ToInt32(Session["StudentId"]);
            string b = Session["StudentSearchSubject"].ToString();
            var query = db.RESULTs.SqlQuery("Select *from RESULT where Subject_ID IN (  Select Subject_ID from SUBJECT where SubjectName Like'%"+b+"%') AND Student_ID="+a+"").ToList();

            return View(query);
        }

        public ActionResult ViewResultForGuardians()
        {
            int a;
            a = Convert.ToInt32(Session["MychildId"]);
            var query = db.RESULTs.SqlQuery("Select *from Result where Student_Id=" + a + "").ToList();
            return View(query);

        }

        public ActionResult ViewResultForGuardiansBySubject()
        {
            int a;
            a = Convert.ToInt32(Session["MychildId"]);
            string b = Session["GuardianSearchSubject"].ToString();
            var query = db.RESULTs.SqlQuery("Select *from RESULT where Subject_ID IN (  Select Subject_ID from SUBJECT where SubjectName Like'%" + b + "%') AND Student_ID=" + a + "").ToList();
            return View(query);
        }

        public ActionResult ViewResultForTeachers()
        {

            int a = Convert.ToInt32(Session["TeacherID"]);
            var query = db.RESULTs.SqlQuery("Select *from Result where Teacher_Id=" + a + "").ToList();
            return View(query);

        }

        public ActionResult ViewResultForTeachersBySubject()
        {
            int a = Convert.ToInt32(Session["TeacherID"]);
            string b = Session["TeacherSearchSubject"].ToString();
            var query = db.RESULTs.SqlQuery("Select *from RESULT where Subject_ID IN (  Select Subject_ID from SUBJECT where SubjectName Like'%" + b + "%') AND Teacher_ID=" + a + "").ToList();
            return View(query);
        }
    }
}
