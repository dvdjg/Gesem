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
    public class PresupuestosClientesDetalleController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: PresupuestosClientesDetalle/Index
        public ActionResult Index()
        {
            var presupuestosClientesDetalle = db.PresupuestosClientesDetalle.Include(p => p.Bienes).Include(p => p.IVAs).Include(p => p.PresupuestosClientesDetalleAgrupados);
            return PartialView(presupuestosClientesDetalle.ToList());
        }

        /*
        // GET: PresupuestosClientesDetalle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresupuestosClientesDetalle presupuestosClientesDetalle = db.PresupuestosClientesDetalle.Find(id);
            if (presupuestosClientesDetalle == null)
            {
                return HttpNotFound();
            }
            return PartialView(presupuestosClientesDetalle);
        }
        */

        // GET: PresupuestosClientesDetalle/Create
        public ActionResult Create()
        {
            ViewBag.BienId = new SelectList(db.Bienes, "Id", "CodBien");
            ViewBag.IVAId = new SelectList(db.IVAs, "Id", "Descripcion");
            ViewBag.AgrupadoId = new SelectList(db.PresupuestosClientesDetalleAgrupados, "Id", "Descripcion");
            return PartialView();
        }

        // POST: PresupuestosClientesDetalle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AgrupadoId,BienId,Cantidad,IVAId,Precio,Bienes,IVAs,PresupuestosClientesDetalleAgrupados")] PresupuestosClientesDetalle presupuestosClientesDetalle)
        {
            if (ModelState.IsValid)
            {
                db.PresupuestosClientesDetalle.Add(presupuestosClientesDetalle);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a PresupuestosClientesDetalle record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.BienId = new SelectList(db.Bienes, "Id", "CodBien", presupuestosClientesDetalle.BienId);
            ViewBag.IVAId = new SelectList(db.IVAs, "Id", "Descripcion", presupuestosClientesDetalle.IVAId);
            ViewBag.AgrupadoId = new SelectList(db.PresupuestosClientesDetalleAgrupados, "Id", "Descripcion", presupuestosClientesDetalle.AgrupadoId);
            DisplayErrorMessage();
            return PartialView(presupuestosClientesDetalle);
        }

        // GET: PresupuestosClientesDetalle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresupuestosClientesDetalle presupuestosClientesDetalle = db.PresupuestosClientesDetalle.Find(id);
            if (presupuestosClientesDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.BienId = new SelectList(db.Bienes, "Id", "CodBien", presupuestosClientesDetalle.BienId);
            ViewBag.IVAId = new SelectList(db.IVAs, "Id", "Descripcion", presupuestosClientesDetalle.IVAId);
            ViewBag.AgrupadoId = new SelectList(db.PresupuestosClientesDetalleAgrupados, "Id", "Descripcion", presupuestosClientesDetalle.AgrupadoId);
            return PartialView(presupuestosClientesDetalle);
        }

        // POST: PresupuestosClientesDetalle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AgrupadoId,BienId,Cantidad,IVAId,Precio,Bienes,IVAs,PresupuestosClientesDetalleAgrupados")] PresupuestosClientesDetalle presupuestosClientesDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(presupuestosClientesDetalle).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a PresupuestosClientesDetalle record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.BienId = new SelectList(db.Bienes, "Id", "CodBien", presupuestosClientesDetalle.BienId);
            ViewBag.IVAId = new SelectList(db.IVAs, "Id", "Descripcion", presupuestosClientesDetalle.IVAId);
            ViewBag.AgrupadoId = new SelectList(db.PresupuestosClientesDetalleAgrupados, "Id", "Descripcion", presupuestosClientesDetalle.AgrupadoId);
            DisplayErrorMessage();
            return PartialView(presupuestosClientesDetalle);
        }

        // GET: PresupuestosClientesDetalle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresupuestosClientesDetalle presupuestosClientesDetalle = db.PresupuestosClientesDetalle.Find(id);
            if (presupuestosClientesDetalle == null)
            {
                return HttpNotFound();
            }
            return PartialView(presupuestosClientesDetalle);
        }

        // POST: PresupuestosClientesDetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PresupuestosClientesDetalle presupuestosClientesDetalle = db.PresupuestosClientesDetalle.Find(id);
            db.PresupuestosClientesDetalle.Remove(presupuestosClientesDetalle);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a PresupuestosClientesDetalle record");
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
