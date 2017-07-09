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
    public class FamiliasController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: Familias/Index
        public ActionResult Index()
        {
            var familias = db.Familias.Include(f => f.Estados).Include(f => f.Familias2);
            return PartialView(familias.ToList());
        }

        /*
        // GET: Familias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familias familias = db.Familias.Find(id);
            if (familias == null)
            {
                return HttpNotFound();
            }
            return PartialView(familias);
        }
        */

        // GET: Familias/Create
        public ActionResult Create()
        {
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre");
            ViewBag.PadreId = new SelectList(db.Familias, "Id", "CodFamilia");
            return PartialView();
        }

        // POST: Familias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodFamilia,Familia,EstadoId,PadreId,Bienes,Estados,Familias1,Familias2,Historicos")] Familias familias)
        {
            if (ModelState.IsValid)
            {
                db.Familias.Add(familias);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a Familias record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", familias.EstadoId);
            ViewBag.PadreId = new SelectList(db.Familias, "Id", "CodFamilia", familias.PadreId);
            DisplayErrorMessage();
            return PartialView(familias);
        }

        // GET: Familias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familias familias = db.Familias.Find(id);
            if (familias == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", familias.EstadoId);
            ViewBag.PadreId = new SelectList(db.Familias, "Id", "CodFamilia", familias.PadreId);
            return PartialView(familias);
        }

        // POST: Familias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodFamilia,Familia,EstadoId,PadreId,Bienes,Estados,Familias1,Familias2,Historicos")] Familias familias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(familias).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a Familias record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", familias.EstadoId);
            ViewBag.PadreId = new SelectList(db.Familias, "Id", "CodFamilia", familias.PadreId);
            DisplayErrorMessage();
            return PartialView(familias);
        }

        // GET: Familias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familias familias = db.Familias.Find(id);
            if (familias == null)
            {
                return HttpNotFound();
            }
            return PartialView(familias);
        }

        // POST: Familias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Familias familias = db.Familias.Find(id);
            db.Familias.Remove(familias);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a Familias record");
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
