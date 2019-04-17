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
    public class GUARDIANsController : Controller
    {
        private OSMSEntities db = new OSMSEntities();

        // GET: GUARDIANs
        public ActionResult Index()
        {
            var gUARDIANs = db.GUARDIANs.Include(g => g.ADMIN).Include(g => g.STUDENT);
            return View(gUARDIANs.ToList());
        }

        // GET: GUARDIANs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GUARDIAN gUARDIAN = db.GUARDIANs.Find(id);
            if (gUARDIAN == null)
            {
                return HttpNotFound();
            }
            return View(gUARDIAN);
        }

        // GET: GUARDIANs/Create
        public ActionResult Create()
        {
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName");
            ViewBag.Student_ID = new SelectList(db.STUDENTs, "Student_ID", "StudentName");
            return View();
        }

        // POST: GUARDIANs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Guardian_ID,GuardianName,GuardianEmail,GuardianPassword,GuardianContactNumber,GuardianAddress,Admin_ID,Student_ID")] GUARDIAN gUARDIAN)
        {
            if (ModelState.IsValid)
            {
                db.GUARDIANs.Add(gUARDIAN);
                db.SaveChanges();
               // return RedirectToAction("Index");
            }

            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", gUARDIAN.Admin_ID);
            ViewBag.Student_ID = new SelectList(db.STUDENTs, "Student_ID", "StudentName", gUARDIAN.Student_ID);

            ModelState.Clear();
            ViewBag.Message = "successfully created";
            return View();
            //   return View(gUARDIAN);
        }

        // GET: GUARDIANs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GUARDIAN gUARDIAN = db.GUARDIANs.Find(id);
            if (gUARDIAN == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", gUARDIAN.Admin_ID);
            ViewBag.Student_ID = new SelectList(db.STUDENTs, "Student_ID", "StudentName", gUARDIAN.Student_ID);
            return View(gUARDIAN);
        }

        // POST: GUARDIANs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Guardian_ID,GuardianName,GuardianEmail,GuardianPassword,GuardianContactNumber,GuardianAddress,Admin_ID,Student_ID")] GUARDIAN gUARDIAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gUARDIAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", gUARDIAN.Admin_ID);
            ViewBag.Student_ID = new SelectList(db.STUDENTs, "Student_ID", "StudentName", gUARDIAN.Student_ID);
            return View(gUARDIAN);
        }

        // GET: GUARDIANs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GUARDIAN gUARDIAN = db.GUARDIANs.Find(id);
            if (gUARDIAN == null)
            {
                return HttpNotFound();
            }
            return View(gUARDIAN);
        }

        // POST: GUARDIANs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GUARDIAN gUARDIAN = db.GUARDIANs.Find(id);
            db.GUARDIANs.Remove(gUARDIAN);
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

        public ActionResult Login2()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login2(GUARDIAN gd)
        {
            using (OSMSEntities db = new OSMSEntities())
            {

                var loginSuccessful = false;

                try
                {
                    ModelState.Clear();
                    var usr = db.GUARDIANs.Single(u => u.GuardianEmail == gd.GuardianEmail && u.GuardianPassword == gd.GuardianPassword);
                    if (usr != null)
                    {
                        Session["GuardianEmail"] = usr.GuardianEmail.ToString();
                        Session["GuardianId"] = usr.Guardian_ID.ToString();
                        Session["MychildId"] = usr.Student_ID.ToString();
                        Session["GuardianName"] = usr.GuardianName.ToString();

                        //ViewBag.Message = Session["UserName"] + " successfully logged in";
                        loginSuccessful = true;
                        ModelState.Clear();

                        return RedirectToAction("LoggedIn2");

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

        public ActionResult LoggedIn2()
        {
            if (Session["GuardianEmail"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login2");
            }
        }

        public ActionResult GuardianDownLoadRoutine()
        {
            return RedirectToAction("DownloadClassRoutineForGuardians", "CLASSROUTINEs");
        }

        public ActionResult ViewAttendance()
        {
            return RedirectToAction("ViewAttendaceForGuardian", "ATTENDANCEs");
        }

        public ActionResult ViewAttendanceByDate(String SearchbyDate1)
        {
            Session["StudentSearchDate1"] = SearchbyDate1.ToString();
            return RedirectToAction("ViewAttendaceForGuardiansByDate", "ATTENDANCEs");
        }

        public ActionResult ViewResult()
        {
            return RedirectToAction("ViewResultForGuardians", "RESULTs");
        }

        public ActionResult ViewResultBySubject(String SearchbySubject)
        {
            Session["GuardianSearchSubject"] = SearchbySubject.ToString();
            return RedirectToAction("ViewResultForGuardiansBySubject", "RESULTs");
        }

        public ActionResult ViewProfile()
        {
            int a = Convert.ToInt32(Session["GuardianId"]);
          
            var query2 = db.GUARDIANs.SqlQuery("Select * From Guardian where Guardian_Id=" + a + "").ToList();

            return View(query2);
        }

        public ActionResult GiveReview()
        {
            return RedirectToAction("Create", "GUARDIANREVIEWs");
        }

        public ActionResult LogoutGuardian()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");

        }
    }
}
