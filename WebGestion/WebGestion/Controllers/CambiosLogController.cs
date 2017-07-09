using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebGestion.Models;

namespace WebGestion.Controllers
{
    public class CambiosLogController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: CambiosLog/Index
        public ActionResult Index()
        {
            var cambiosLog = db.CambiosLog.Include(c => c.AspNetUsers);
            return PartialView(cambiosLog.ToList());
        }

        /*
        // GET: CambiosLog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CambiosLog cambiosLog = db.CambiosLog.Find(id);
            if (cambiosLog == null)
            {
                return HttpNotFound();
            }
            return PartialView(cambiosLog);
        }
        */

        // GET: CambiosLog/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return PartialView();
        }

        // POST: CambiosLog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Fecha,Query,AspNetUsers")] CambiosLog cambiosLog)
        {
            if (ModelState.IsValid)
            {
                db.CambiosLog.Add(cambiosLog);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a CambiosLog record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", cambiosLog.UserId);
            DisplayErrorMessage();
            return PartialView(cambiosLog);
        }

        // GET: CambiosLog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CambiosLog cambiosLog = db.CambiosLog.Find(id);
            if (cambiosLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", cambiosLog.UserId);
            return PartialView(cambiosLog);
        }

        // POST: CambiosLog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Fecha,Query,AspNetUsers")] CambiosLog cambiosLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cambiosLog).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a CambiosLog record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", cambiosLog.UserId);
            DisplayErrorMessage();
            return PartialView(cambiosLog);
        }

        // GET: CambiosLog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CambiosLog cambiosLog = db.CambiosLog.Find(id);
            if (cambiosLog == null)
            {
                return HttpNotFound();
            }
            return PartialView(cambiosLog);
        }

        // POST: CambiosLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CambiosLog cambiosLog = db.CambiosLog.Find(id);
            db.CambiosLog.Remove(cambiosLog);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a CambiosLog record");
            return JsonRedirectToAction("Index");
        }

        private void DisplaySuccessMessage(string msgText)
        {
            TempData["SuccessMessage"] = msgText;
        }

        private void DisplayErrorMessage()
        {
            TempData["ErrorMessage"] = "Save changes was unsuccessful.";
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
