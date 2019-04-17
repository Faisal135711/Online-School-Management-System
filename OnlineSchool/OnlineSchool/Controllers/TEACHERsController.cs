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
    public class TEACHERsController : Controller
    {
        private OSMSEntities db = new OSMSEntities();

        // GET: TEACHERs
        public ActionResult Index()
        {
            var tEACHERs = db.TEACHERs.Include(t => t.ADMIN);
            return View(tEACHERs.ToList());
        }

        // GET: TEACHERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEACHER tEACHER = db.TEACHERs.Find(id);
            if (tEACHER == null)
            {
                return HttpNotFound();
            }
            return View(tEACHER);
        }

        // GET: TEACHERs/Create
        public ActionResult Create()
        {
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName");
            return View();
        }

        // POST: TEACHERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Teacher_ID,TeacherName,TeacherEmail,TeacherPassword,TeacherContactNumber,TeacherAddress,Admin_ID")] TEACHER tEACHER)
        {
            if (ModelState.IsValid)
            {
                db.TEACHERs.Add(tEACHER);
                db.SaveChanges();
              //  return RedirectToAction("Index");
            }

            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", tEACHER.Admin_ID);
            ModelState.Clear();
            ViewBag.Message = "successfully created";
            return View();
         // return View(tEACHER);
        }

        // GET: TEACHERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEACHER tEACHER = db.TEACHERs.Find(id);
            if (tEACHER == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", tEACHER.Admin_ID);
            return View(tEACHER);
        }

        // POST: TEACHERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Teacher_ID,TeacherName,TeacherEmail,TeacherPassword,TeacherContactNumber,TeacherAddress,Admin_ID")] TEACHER tEACHER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tEACHER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", tEACHER.Admin_ID);
            return View(tEACHER);
        }

        // GET: TEACHERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEACHER tEACHER = db.TEACHERs.Find(id);
            if (tEACHER == null)
            {
                return HttpNotFound();
            }
            return View(tEACHER);
        }

        // POST: TEACHERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TEACHER tEACHER = db.TEACHERs.Find(id);
            db.TEACHERs.Remove(tEACHER);
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

        public ActionResult Login3()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login3(TEACHER tc)
        {
            using (OSMSEntities db = new OSMSEntities())
            {

                var loginSuccessful = false;

                try
                {
                    ModelState.Clear();
                    var usr = db.TEACHERs.Single(u => u.TeacherEmail == tc.TeacherEmail && u.TeacherPassword == tc.TeacherPassword);
                    if (usr != null)
                    {
                        Session["TeacherEmail"] = usr.TeacherEmail.ToString();
                        Session["TeacherID"] = usr.Teacher_ID.ToString();
                        Session["TeacherName"] = usr.TeacherName.ToString();

                        //ViewBag.Message = Session["UserName"] + " successfully logged in";
                        loginSuccessful = true;
                        ModelState.Clear();

                        return RedirectToAction("LoggedIn3");

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

        public ActionResult LoggedIn3()
        {
            if (Session["TeacherEmail"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login3");
            }
        }

        public ActionResult DownloadRoutine()
        {
            return RedirectToAction("DownloadTeacherRoutine", "TEACHER_ROUTINE");
        }

        public ActionResult ViewProfile()
        {
            int a = Convert.ToInt32(Session["TeacherID"]);

            var query2 = db.TEACHERs.SqlQuery("Select * From Teacher where Teacher_Id=" + a + "").ToList();

            return View(query2);
        }

        public ActionResult ViewAttendance()
        {
            return RedirectToAction("ViewAttendaceForTeacher", "ATTENDANCEs");
        }

        public ActionResult ViewAttendanceByDate(String SearchbyDate)
        {
            Session["TeacherSearchDate"] = SearchbyDate.ToString();
            return RedirectToAction("ViewAttendaceForTeachersByDate", "ATTENDANCEs");
        }

        public ActionResult ViewResult()
        {
            return RedirectToAction("ViewResultForTeachers", "RESULTs");
        }

        public ActionResult ViewResultBySubject(String SearchbySubject)
        {
            Session["TeacherSearchSubject"] = SearchbySubject.ToString();
            return RedirectToAction("ViewResultForTeachersBySubject", "RESULTs");
        }

        public ActionResult LogoutTeacher()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");

        }
    }
}
