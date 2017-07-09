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
    public class FacturasProveedoresController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: FacturasProveedores/Index
        public ActionResult Index()
        {
            var facturasProveedores = db.FacturasProveedores.Include(f => f.Estados).Include(f => f.FormasEntrega).Include(f => f.FormasPago).Include(f => f.TiposFacturas);
            return PartialView(facturasProveedores.ToList());
        }

        /*
        // GET: FacturasProveedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacturasProveedores facturasProveedores = db.FacturasProveedores.Find(id);
            if (facturasProveedores == null)
            {
                return HttpNotFound();
            }
            return PartialView(facturasProveedores);
        }
        */

        // GET: FacturasProveedores/Create
        public ActionResult Create()
        {
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre");
            ViewBag.FormaEntregaId = new SelectList(db.FormasEntrega, "Id", "FormaEntrega");
            ViewBag.FormaPagoId = new SelectList(db.FormasPago, "Id", "FormaPago");
            ViewBag.TipoFacturaId = new SelectList(db.TiposFacturas, "Id", "Prefijo");
            return PartialView();
        }

        // POST: FacturasProveedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TipoFacturaId,CodFactura,FormaPagoId,FormaEntregaId,EstadoId,Estados,FormasEntrega,FormasPago,TiposFacturas,Historicos,PedidosProveedores")] FacturasProveedores facturasProveedores)
        {
            if (ModelState.IsValid)
            {
                db.FacturasProveedores.Add(facturasProveedores);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a FacturasProveedores record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", facturasProveedores.EstadoId);
            ViewBag.FormaEntregaId = new SelectList(db.FormasEntrega, "Id", "FormaEntrega", facturasProveedores.FormaEntregaId);
            ViewBag.FormaPagoId = new SelectList(db.FormasPago, "Id", "FormaPago", facturasProveedores.FormaPagoId);
            ViewBag.TipoFacturaId = new SelectList(db.TiposFacturas, "Id", "Prefijo", facturasProveedores.TipoFacturaId);
            DisplayErrorMessage();
            return PartialView(facturasProveedores);
        }

        // GET: FacturasProveedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacturasProveedores facturasProveedores = db.FacturasProveedores.Find(id);
            if (facturasProveedores == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", facturasProveedores.EstadoId);
            ViewBag.FormaEntregaId = new SelectList(db.FormasEntrega, "Id", "FormaEntrega", facturasProveedores.FormaEntregaId);
            ViewBag.FormaPagoId = new SelectList(db.FormasPago, "Id", "FormaPago", facturasProveedores.FormaPagoId);
            ViewBag.TipoFacturaId = new SelectList(db.TiposFacturas, "Id", "Prefijo", facturasProveedores.TipoFacturaId);
            return PartialView(facturasProveedores);
        }

        // POST: FacturasProveedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TipoFacturaId,CodFactura,FormaPagoId,FormaEntregaId,EstadoId,Estados,FormasEntrega,FormasPago,TiposFacturas,Historicos,PedidosProveedores")] FacturasProveedores facturasProveedores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturasProveedores).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a FacturasProveedores record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", facturasProveedores.EstadoId);
            ViewBag.FormaEntregaId = new SelectList(db.FormasEntrega, "Id", "FormaEntrega", facturasProveedores.FormaEntregaId);
            ViewBag.FormaPagoId = new SelectList(db.FormasPago, "Id", "FormaPago", facturasProveedores.FormaPagoId);
            ViewBag.TipoFacturaId = new SelectList(db.TiposFacturas, "Id", "Prefijo", facturasProveedores.TipoFacturaId);
            DisplayErrorMessage();
            return PartialView(facturasProveedores);
        }

        // GET: FacturasProveedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacturasProveedores facturasProveedores = db.FacturasProveedores.Find(id);
            if (facturasProveedores == null)
            {
                return HttpNotFound();
            }
            return PartialView(facturasProveedores);
        }

        // POST: FacturasProveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FacturasProveedores facturasProveedores = db.FacturasProveedores.Find(id);
            db.FacturasProveedores.Remove(facturasProveedores);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a FacturasProveedores record");
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
