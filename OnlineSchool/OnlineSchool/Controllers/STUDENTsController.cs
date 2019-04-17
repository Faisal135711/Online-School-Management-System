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
    public class STUDENTsController : Controller
    {
        private OSMSEntities db = new OSMSEntities();

        // GET: STUDENTs
        public ActionResult Index()
        {
            var sTUDENTs = db.STUDENTs.Include(s => s.ADMIN).Include(s => s.CLASS).Include(s => s.SECTION);
            return View(sTUDENTs.ToList());
        }

        // GET: STUDENTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STUDENT sTUDENT = db.STUDENTs.Find(id);
            if (sTUDENT == null)
            {
                return HttpNotFound();
            }
            return View(sTUDENT);
        }

        // GET: STUDENTs/Create
        public ActionResult Create()
        {
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName");
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName");
            ViewBag.Section_ID = new SelectList(db.SECTIONs, "Section_ID", "SectionName");
            return View();
        }

        // POST: STUDENTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Student_ID,StudentName,StudentEmail,StudentPassword,StudentContactNumber,StudentAddress,Admin_ID,Class_ID,Section_ID")] STUDENT sTUDENT)
        {
            if (ModelState.IsValid)
            {
                db.STUDENTs.Add(sTUDENT);
                db.SaveChanges();
             //   return RedirectToAction("Index");
            }

            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", sTUDENT.Admin_ID);
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName", sTUDENT.Class_ID);
            ViewBag.Section_ID = new SelectList(db.SECTIONs, "Section_ID", "SectionName", sTUDENT.Section_ID);

            ModelState.Clear();
            ViewBag.Message = "successfully added";
            return View();
         //   return View(sTUDENT);
        }

        // GET: STUDENTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STUDENT sTUDENT = db.STUDENTs.Find(id);
            if (sTUDENT == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", sTUDENT.Admin_ID);
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName", sTUDENT.Class_ID);
            ViewBag.Section_ID = new SelectList(db.SECTIONs, "Section_ID", "SectionName", sTUDENT.Section_ID);
            return View(sTUDENT);
        }

        // POST: STUDENTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Student_ID,StudentName,StudentEmail,StudentPassword,StudentContactNumber,StudentAddress,Admin_ID,Class_ID,Section_ID")] STUDENT sTUDENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sTUDENT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", sTUDENT.Admin_ID);
            ViewBag.Class_ID = new SelectList(db.CLASSes, "Class_ID", "ClassName", sTUDENT.Class_ID);
            ViewBag.Section_ID = new SelectList(db.SECTIONs, "Section_ID", "SectionName", sTUDENT.Section_ID);
            return View(sTUDENT);
        }

        // GET: STUDENTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STUDENT sTUDENT = db.STUDENTs.Find(id);
            if (sTUDENT == null)
            {
                return HttpNotFound();
            }
            return View(sTUDENT);
        }

        // POST: STUDENTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            STUDENT sTUDENT = db.STUDENTs.Find(id);
            db.STUDENTs.Remove(sTUDENT);
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

        //login

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(STUDENT st)
        {
            using (OSMSEntities db = new OSMSEntities())
            {

                var loginSuccessful = false;

                try
                {
                    ModelState.Clear();
                    var usr = db.STUDENTs.Single(u => u.StudentEmail == st.StudentEmail && u.StudentPassword == st.StudentPassword);
                    if (usr != null)
                    {
                        Session["StudentEmail"] = usr.StudentEmail.ToString();
                        Session["StudentClass"] = usr.Class_ID.ToString();
                        Session["StudentSection"] = usr.Section_ID.ToString();
                        Session["StudentId"] = usr.Student_ID.ToString();
                        Session["StudentName"] = usr.StudentName.ToString();
                        
                        //ViewBag.Message = Session["UserName"] + " successfully logged in";
                        loginSuccessful = true;
                        ModelState.Clear();

                        return RedirectToAction("LoggedIn");

                    }

                }

                catch (InvalidOperationException)
                {

                }

                if (!loginSuccessful)
                {
                    ModelState.AddModelError("", "Incorrect Combination");
                    // 
                }

            }

            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["StudentEmail"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult DownloadClassRoutine()
        {
            return RedirectToAction("DownloadClassRoutineForStudents", "CLASSROUTINEs");
          
        }

        public ActionResult ViewAttendance()
        {
            return RedirectToAction("ViewAttendaceForStudents", "ATTENDANCEs");
        }

        public ActionResult ViewAttendanceByDate(String SearchbyDate)
        {
            Session["StudentSearchDate"] = SearchbyDate.ToString();
            return RedirectToAction("ViewAttendaceForStudentsByDate", "ATTENDANCEs");
        }

        public ActionResult ViewResult()
        {
            return RedirectToAction("ViewResultForStudents", "RESULTs");
        }
        public ActionResult ViewResultBySubject(String SearchbySubject)
        {
            Session["StudentSearchSubject"] = SearchbySubject.ToString();
            return RedirectToAction("ViewResultForStudentsBySubject", "RESULTs");
        }


        public ActionResult PayOnlineFee()
        {
            return RedirectToAction("Create", "PAYMENTs");
        }

        public ActionResult ViewProfile()
        {
            int a = Convert.ToInt32(Session["StudentId"]);

            var query2 = db.STUDENTs.SqlQuery("Select * From Student where Student_Id=" + a + "").ToList();

            return View(query2);
        }

        public ActionResult LogoutStudent()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");

        }
    }
}
