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
    public class ADMINsController : Controller
    {
        private OSMSEntities db = new OSMSEntities();
        int ad_Id=0;

        // GET: ADMINs
        public ActionResult Index()
        {
            return View(db.ADMINs.ToList());
        }

        // GET: ADMINs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADMIN aDMIN = db.ADMINs.Find(id);
            if (aDMIN == null)
            {
                return HttpNotFound();
            }
            return View(aDMIN);
        }

        // GET: ADMINs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ADMINs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Admin_ID,AdminName,AdminEmail,AdminPassword,AdminContactNumber,AdminAddress")] ADMIN aDMIN)
        {
            if (ModelState.IsValid)
            {
                db.ADMINs.Add(aDMIN);
                db.SaveChanges();
              //  return RedirectToAction("Index");
            }
            ModelState.Clear();
            ViewBag.Message = "successfully registered";
            return View();
            // return View(aDMIN);
        }

        // GET: ADMINs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADMIN aDMIN = db.ADMINs.Find(id);
            if (aDMIN == null)
            {
                return HttpNotFound();
            }
            return View(aDMIN);
        }

        // POST: ADMINs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Admin_ID,AdminName,AdminEmail,AdminPassword,AdminContactNumber,AdminAddress")] ADMIN aDMIN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aDMIN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aDMIN);
        }

        // GET: ADMINs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADMIN aDMIN = db.ADMINs.Find(id);
            if (aDMIN == null)
            {
                return HttpNotFound();
            }
            return View(aDMIN);
        }

        // POST: ADMINs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ADMIN aDMIN = db.ADMINs.Find(id);
            db.ADMINs.Remove(aDMIN);
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

        public ActionResult Login4()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login4(ADMIN ad)
        {
            var loginSuccessful = false;
            using (OSMSEntities db = new OSMSEntities())
            {
                
                try
                {
                   
                    var usr = db.ADMINs.Single(u => u.AdminEmail == ad.AdminEmail && u.AdminPassword == ad.AdminPassword);
                    if (usr != null)
                    {
                        ad_Id = ad.Admin_ID;
                        Session["AdminId"] = usr.Admin_ID.ToString();
                       // Session["hghff"] = ad.Admin_ID.ToString();
                        Session["AdminEmail"] = usr.AdminEmail.ToString();
                        Session["AdminName"] = usr.AdminName.ToString();

                        //ViewBag.Message = Session["UserName"] + " successfully logged in";
                        loginSuccessful = true;
                  
                        //if(ad_Id!=0)
                        //return RedirectToAction("ViewProfile");
                      //  if(ad_Id==0)
                        return RedirectToAction("LoggedIn4");

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

        public ActionResult LoggedIn4()
        {
            if (Session["AdminEmail"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login4");
            }
        }

        public ActionResult AddStudent()
        {
            return RedirectToAction("Create","STUDENTs");
        }
        public ActionResult AddClass()
        {
            return RedirectToAction("Create", "CLASSes");
        }

        public ActionResult AddSection()
        {
            return RedirectToAction("Create", "SECTIONs");
        }

        public ActionResult AddGuardian()
        {
            return RedirectToAction("Create", "GUARDIANs");
        }

        public ActionResult AddTeacher()
        {
            return RedirectToAction("Create", "TEACHERs");
        }

        public ActionResult AddSubject()
        {
            return RedirectToAction("Create", "SUBJECTs");
        }

        public ActionResult AddTeacherRoutine()
        {
            return RedirectToAction("Create", "TEACHER_ROUTINE");
        }

        public ActionResult AddClassRoutine()
        {
            return RedirectToAction("Create", "CLASSROUTINEs");
        }

        public ActionResult AddEvent()
        {
            return RedirectToAction("Create", "EVENTs");
        }

        public ActionResult AddAttendance()
        {
            return RedirectToAction("Create", "ATTENDANCEs");
        }

        public ActionResult AddResult()
        {
            return RedirectToAction("Create", "RESULTs");
        }

        public ActionResult ViewProfile()
        {
            //int a = Convert.ToInt32(Session["AdminId112"]);
            string s ;
            s=Session["AdminEmail"].ToString();
            //a = 1;
            //a = Convert.ToInt32(Session["hghff"]);
            var query2 = db.ADMINs.SqlQuery("Select * From ADMIN where AdminEmail='" +s+"'").ToList();

            return View(query2);
        }

        public ActionResult LogoutAdmin()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");

        }

        public ActionResult AllList()
        {
            return View();
        }

        //SHow ProfileDetails Korte Hobe Sd er Moto
        //Paarle Upload pRofile pic tao korte hobe
    }
}
