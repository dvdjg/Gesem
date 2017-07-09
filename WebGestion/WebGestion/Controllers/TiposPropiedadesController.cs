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
    public class TiposPropiedadesController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: TiposPropiedades/Index
        public ActionResult Index()
        {
            var tiposPropiedades = db.TiposPropiedades.Include(t => t.TiposPropiedades2);
            return PartialView(tiposPropiedades.ToList());
        }

        /*
        // GET: TiposPropiedades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposPropiedades tiposPropiedades = db.TiposPropiedades.Find(id);
            if (tiposPropiedades == null)
            {
                return HttpNotFound();
            }
            return PartialView(tiposPropiedades);
        }
        */

        // GET: TiposPropiedades/Create
        public ActionResult Create()
        {
            ViewBag.PadreId = new SelectList(db.TiposPropiedades, "Id", "Nombre");
            return PartialView();
        }

        // POST: TiposPropiedades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Descripcion,PadreId,Historicos,TiposPropiedades1,TiposPropiedades2")] TiposPropiedades tiposPropiedades)
        {
            if (ModelState.IsValid)
            {
                db.TiposPropiedades.Add(tiposPropiedades);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a TiposPropiedades record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.PadreId = new SelectList(db.TiposPropiedades, "Id", "Nombre", tiposPropiedades.PadreId);
            DisplayErrorMessage();
            return PartialView(tiposPropiedades);
        }

        // GET: TiposPropiedades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposPropiedades tiposPropiedades = db.TiposPropiedades.Find(id);
            if (tiposPropiedades == null)
            {
                return HttpNotFound();
            }
            ViewBag.PadreId = new SelectList(db.TiposPropiedades, "Id", "Nombre", tiposPropiedades.PadreId);
            return PartialView(tiposPropiedades);
        }

        // POST: TiposPropiedades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Descripcion,PadreId,Historicos,TiposPropiedades1,TiposPropiedades2")] TiposPropiedades tiposPropiedades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiposPropiedades).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a TiposPropiedades record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.PadreId = new SelectList(db.TiposPropiedades, "Id", "Nombre", tiposPropiedades.PadreId);
            DisplayErrorMessage();
            return PartialView(tiposPropiedades);
        }

        // GET: TiposPropiedades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposPropiedades tiposPropiedades = db.TiposPropiedades.Find(id);
            if (tiposPropiedades == null)
            {
                return HttpNotFound();
            }
            return PartialView(tiposPropiedades);
        }

        // POST: TiposPropiedades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TiposPropiedades tiposPropiedades = db.TiposPropiedades.Find(id);
            db.TiposPropiedades.Remove(tiposPropiedades);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a TiposPropiedades record");
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
