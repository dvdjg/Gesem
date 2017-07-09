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
    public class AspNetRolesController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: AspNetRoles/Index
        public ActionResult Index()
        {
            var aspNetRoles = db.AspNetRoles.Include(a => a.Estados);
            return PartialView(aspNetRoles.ToList());
        }

        /*
        // GET: AspNetRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRoles aspNetRoles = db.AspNetRoles.Find(id);
            if (aspNetRoles == null)
            {
                return HttpNotFound();
            }
            return PartialView(aspNetRoles);
        }
        */

        // GET: AspNetRoles/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Estados, "Id", "Nombre");
            return PartialView();
        }

        // POST: AspNetRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,EstadoId,FechaAlta,FechaBaja,Estados,ApplicationGroups,AspNetUsers")] AspNetRoles aspNetRoles)
        {
            if (ModelState.IsValid)
            {
                db.AspNetRoles.Add(aspNetRoles);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a AspNetRoles record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Estados, "Id", "Nombre", aspNetRoles.Id);
            DisplayErrorMessage();
            return PartialView(aspNetRoles);
        }

        // GET: AspNetRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRoles aspNetRoles = db.AspNetRoles.Find(id);
            if (aspNetRoles == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Estados, "Id", "Nombre", aspNetRoles.Id);
            return PartialView(aspNetRoles);
        }

        // POST: AspNetRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,EstadoId,FechaAlta,FechaBaja,Estados,ApplicationGroups,AspNetUsers")] AspNetRoles aspNetRoles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetRoles).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a AspNetRoles record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Estados, "Id", "Nombre", aspNetRoles.Id);
            DisplayErrorMessage();
            return PartialView(aspNetRoles);
        }

        // GET: AspNetRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetRoles aspNetRoles = db.AspNetRoles.Find(id);
            if (aspNetRoles == null)
            {
                return HttpNotFound();
            }
            return PartialView(aspNetRoles);
        }

        // POST: AspNetRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AspNetRoles aspNetRoles = db.AspNetRoles.Find(id);
            db.AspNetRoles.Remove(aspNetRoles);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a AspNetRoles record");
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
