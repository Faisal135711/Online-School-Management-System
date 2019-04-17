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
    public class EVENTsController : Controller
    {
        private OSMSEntities db = new OSMSEntities();

        // GET: EVENTs
        public ActionResult Index()
        {
            var eVENTs = db.EVENTs.Include(e => e.ADMIN);
            return View(eVENTs.ToList());
        }

        // GET: EVENTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENT eVENT = db.EVENTs.Find(id);
            if (eVENT == null)
            {
                return HttpNotFound();
            }
            return View(eVENT);
        }

        // GET: EVENTs/Create
        public ActionResult Create()
        {
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName");
            return View();
        }

        // POST: EVENTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, HttpPostedFileBase file1,[Bind(Include = "Event_ID,Title,Description,IssuedBY,Admin_ID")] EVENT eVENT)
        {

            if (file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string filepath = Path.Combine(Server.MapPath("~/EVENT PDF"), filename);
                file.SaveAs(filepath);
                eVENT.FileDirectory = filename;
            }

            if (file1.ContentLength > 0)
            {
                string filename1 = Path.GetFileName(file1.FileName);
                string filepath1 = Path.Combine(Server.MapPath("~/EVENT IMAGES"), filename1);
                file1.SaveAs(filepath1);
                //eVENT.FileDirectory = filename;
                eVENT.ImageDirectory = filename1;
            }



            if (ModelState.IsValid)
            {
                eVENT.AnnouncementDate= System.DateTime.Now;
                db.EVENTs.Add(eVENT);
                db.SaveChanges();
              //  return RedirectToAction("Index");
            }

            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", eVENT.Admin_ID);
            ModelState.Clear();
            ViewBag.Message = "successfully entered";
            return View();
            //return View(eVENT);
        }

        // GET: EVENTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENT eVENT = db.EVENTs.Find(id);
            if (eVENT == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", eVENT.Admin_ID);
            return View(eVENT);
        }

        // POST: EVENTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Event_ID,Title,Description,AnnouncementDate,IssuedBY,FileDirectory,ImageDirectory,Admin_ID")] EVENT eVENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eVENT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_ID = new SelectList(db.ADMINs, "Admin_ID", "AdminName", eVENT.Admin_ID);
            return View(eVENT);
        }

        // GET: EVENTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENT eVENT = db.EVENTs.Find(id);
            if (eVENT == null)
            {
                return HttpNotFound();
            }
            return View(eVENT);
        }

        // POST: EVENTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EVENT eVENT = db.EVENTs.Find(id);
            db.EVENTs.Remove(eVENT);
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
        public ActionResult SortByDate()
        {
            var q = db.EVENTs.SqlQuery("Select *from Event order by AnnouncementDate DESC").ToList();
            return View(q);
        }
    }
}
