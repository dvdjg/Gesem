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
    //[Authorize(Roles = "Admin")]
    public class ApplicationGroupsController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: ApplicationGroups/Index
        public ActionResult Index()
        {
            return PartialView(db.ApplicationGroups.ToList());
        }

        /*
        // GET: ApplicationGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationGroups applicationGroups = db.ApplicationGroups.Find(id);
            if (applicationGroups == null)
            {
                return HttpNotFound();
            }
            return PartialView(applicationGroups);
        }
        */

        // GET: ApplicationGroups/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: ApplicationGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Descripcion,AspNetRoles,AspNetUsers")] ApplicationGroups applicationGroups)
        {
            if (ModelState.IsValid)
            {
                db.ApplicationGroups.Add(applicationGroups);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a ApplicationGroups record");
                return JsonRedirectToAction("Index");
            }

            DisplayErrorMessage();
            return PartialView(applicationGroups);
        }

        // GET: ApplicationGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationGroups applicationGroups = db.ApplicationGroups.Find(id);
            if (applicationGroups == null)
            {
                return HttpNotFound();
            }
            return PartialView(applicationGroups);
        }

        // POST: ApplicationGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Descripcion,AspNetRoles,AspNetUsers")] ApplicationGroups applicationGroups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationGroups).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a ApplicationGroups record");
                return JsonRedirectToAction("Index");
            }
            DisplayErrorMessage();
            return PartialView(applicationGroups);
        }

        // GET: ApplicationGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationGroups applicationGroups = db.ApplicationGroups.Find(id);
            if (applicationGroups == null)
            {
                return HttpNotFound();
            }
            return PartialView(applicationGroups);
        }

        // POST: ApplicationGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApplicationGroups applicationGroups = db.ApplicationGroups.Find(id);
            db.ApplicationGroups.Remove(applicationGroups);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a ApplicationGroups record");
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
