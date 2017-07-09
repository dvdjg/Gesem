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
    public class PresupuestosClientesDetalleAgrupadosController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: PresupuestosClientesDetalleAgrupados/Index
        public ActionResult Index()
        {
            var presupuestosClientesDetalleAgrupados = db.PresupuestosClientesDetalleAgrupados.Include(p => p.PresupuestosClientes);
            return PartialView(presupuestosClientesDetalleAgrupados.ToList());
        }

        /*
        // GET: PresupuestosClientesDetalleAgrupados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresupuestosClientesDetalleAgrupados presupuestosClientesDetalleAgrupados = db.PresupuestosClientesDetalleAgrupados.Find(id);
            if (presupuestosClientesDetalleAgrupados == null)
            {
                return HttpNotFound();
            }
            return PartialView(presupuestosClientesDetalleAgrupados);
        }
        */

        // GET: PresupuestosClientesDetalleAgrupados/Create
        public ActionResult Create()
        {
            ViewBag.PresupuestoId = new SelectList(db.PresupuestosClientes, "Id", "CodPresupuesto");
            return PartialView();
        }

        // POST: PresupuestosClientesDetalleAgrupados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PresupuestoId,Descripcion,Detalle,Cantidad,PresupuestosClientes,PresupuestosClientesDetalle")] PresupuestosClientesDetalleAgrupados presupuestosClientesDetalleAgrupados)
        {
            if (ModelState.IsValid)
            {
                db.PresupuestosClientesDetalleAgrupados.Add(presupuestosClientesDetalleAgrupados);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a PresupuestosClientesDetalleAgrupados record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.PresupuestoId = new SelectList(db.PresupuestosClientes, "Id", "CodPresupuesto", presupuestosClientesDetalleAgrupados.PresupuestoId);
            DisplayErrorMessage();
            return PartialView(presupuestosClientesDetalleAgrupados);
        }

        // GET: PresupuestosClientesDetalleAgrupados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresupuestosClientesDetalleAgrupados presupuestosClientesDetalleAgrupados = db.PresupuestosClientesDetalleAgrupados.Find(id);
            if (presupuestosClientesDetalleAgrupados == null)
            {
                return HttpNotFound();
            }
            ViewBag.PresupuestoId = new SelectList(db.PresupuestosClientes, "Id", "CodPresupuesto", presupuestosClientesDetalleAgrupados.PresupuestoId);
            return PartialView(presupuestosClientesDetalleAgrupados);
        }

        // POST: PresupuestosClientesDetalleAgrupados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PresupuestoId,Descripcion,Detalle,Cantidad,PresupuestosClientes,PresupuestosClientesDetalle")] PresupuestosClientesDetalleAgrupados presupuestosClientesDetalleAgrupados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(presupuestosClientesDetalleAgrupados).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a PresupuestosClientesDetalleAgrupados record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.PresupuestoId = new SelectList(db.PresupuestosClientes, "Id", "CodPresupuesto", presupuestosClientesDetalleAgrupados.PresupuestoId);
            DisplayErrorMessage();
            return PartialView(presupuestosClientesDetalleAgrupados);
        }

        // GET: PresupuestosClientesDetalleAgrupados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresupuestosClientesDetalleAgrupados presupuestosClientesDetalleAgrupados = db.PresupuestosClientesDetalleAgrupados.Find(id);
            if (presupuestosClientesDetalleAgrupados == null)
            {
                return HttpNotFound();
            }
            return PartialView(presupuestosClientesDetalleAgrupados);
        }

        // POST: PresupuestosClientesDetalleAgrupados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PresupuestosClientesDetalleAgrupados presupuestosClientesDetalleAgrupados = db.PresupuestosClientesDetalleAgrupados.Find(id);
            db.PresupuestosClientesDetalleAgrupados.Remove(presupuestosClientesDetalleAgrupados);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a PresupuestosClientesDetalleAgrupados record");
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
