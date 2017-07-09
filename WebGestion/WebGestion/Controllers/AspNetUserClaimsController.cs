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
    public class AspNetUserClaimsController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: AspNetUserClaims/Index
        public ActionResult Index()
        {
            var aspNetUserClaims = db.AspNetUserClaims.Include(a => a.AspNetUsers);
            return PartialView(aspNetUserClaims.ToList());
        }

        /*
        // GET: AspNetUserClaims/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserClaims aspNetUserClaims = db.AspNetUserClaims.Find(id);
            if (aspNetUserClaims == null)
            {
                return HttpNotFound();
            }
            return PartialView(aspNetUserClaims);
        }
        */

        // GET: AspNetUserClaims/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return PartialView();
        }

        // POST: AspNetUserClaims/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ClaimType,ClaimValue,AspNetUsers")] AspNetUserClaims aspNetUserClaims)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUserClaims.Add(aspNetUserClaims);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a AspNetUserClaims record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserClaims.UserId);
            DisplayErrorMessage();
            return PartialView(aspNetUserClaims);
        }

        // GET: AspNetUserClaims/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserClaims aspNetUserClaims = db.AspNetUserClaims.Find(id);
            if (aspNetUserClaims == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserClaims.UserId);
            return PartialView(aspNetUserClaims);
        }

        // POST: AspNetUserClaims/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ClaimType,ClaimValue,AspNetUsers")] AspNetUserClaims aspNetUserClaims)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUserClaims).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a AspNetUserClaims record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserClaims.UserId);
            DisplayErrorMessage();
            return PartialView(aspNetUserClaims);
        }

        // GET: AspNetUserClaims/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserClaims aspNetUserClaims = db.AspNetUserClaims.Find(id);
            if (aspNetUserClaims == null)
            {
                return HttpNotFound();
            }
            return PartialView(aspNetUserClaims);
        }

        // POST: AspNetUserClaims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AspNetUserClaims aspNetUserClaims = db.AspNetUserClaims.Find(id);
            db.AspNetUserClaims.Remove(aspNetUserClaims);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a AspNetUserClaims record");
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
