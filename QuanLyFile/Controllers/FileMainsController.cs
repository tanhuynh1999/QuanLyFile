using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace QuanLyFile.Controllers
{
    public class FileMainsController : Controller
    {
        private DATAOCREntities db = new DATAOCREntities();

        // GET: FileMains
        public ActionResult Index()
        {
            return View(db.FileMains.ToList());
        }

        // GET: FileMains/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileMain fileMain = db.FileMains.Find(id);
            if (fileMain == null)
            {
                return HttpNotFound();
            }
            return View(fileMain);
        }

        // GET: FileMains/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileMains/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "file_id,file_circular,file_form,file_startday,file_endday,file_datecreate,file_status,file_key")] FileMain fileMain)
        {
            if (ModelState.IsValid)
            {
                db.FileMains.Add(fileMain);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fileMain);
        }

        // GET: FileMains/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileMain fileMain = db.FileMains.Find(id);
            if (fileMain == null)
            {
                return HttpNotFound();
            }
            return View(fileMain);
        }

        // POST: FileMains/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "file_id,file_circular,file_form,file_startday,file_endday,file_datecreate,file_status,file_key")] FileMain fileMain)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fileMain).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fileMain);
        }

        // GET: FileMains/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileMain fileMain = db.FileMains.Find(id);
            if (fileMain == null)
            {
                return HttpNotFound();
            }
            return View(fileMain);
        }

        // POST: FileMains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FileMain fileMain = db.FileMains.Find(id);
            db.FileMains.Remove(fileMain);
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
