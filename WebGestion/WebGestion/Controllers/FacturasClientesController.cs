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
    public class FacturasClientesController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: FacturasClientes/Index
        public ActionResult Index()
        {
            var facturasClientes = db.FacturasClientes.Include(f => f.Empleados).Include(f => f.Estados).Include(f => f.IRPFs).Include(f => f.TiposFacturas);
            return PartialView(facturasClientes.ToList());
        }

        /*
        // GET: FacturasClientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacturasClientes facturasClientes = db.FacturasClientes.Find(id);
            if (facturasClientes == null)
            {
                return HttpNotFound();
            }
            return PartialView(facturasClientes);
        }
        */

        // GET: FacturasClientes/Create
        public ActionResult Create()
        {
            ViewBag.EmpleadoId = new SelectList(db.Empleados, "Id", "CodEmpleado");
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre");
            ViewBag.IRPFId = new SelectList(db.IRPFs, "Id", "Descripcion");
            ViewBag.TipoFacturaId = new SelectList(db.TiposFacturas, "Id", "Prefijo");
            return PartialView();
        }

        // POST: FacturasClientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TipoFacturaId,CodFactura,IRPFId,EmpleadoId,EstadoId,Empleados,Estados,IRPFs,TiposFacturas,Historicos,PedidosClientes")] FacturasClientes facturasClientes)
        {
            if (ModelState.IsValid)
            {
                db.FacturasClientes.Add(facturasClientes);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a FacturasClientes record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.EmpleadoId = new SelectList(db.Empleados, "Id", "CodEmpleado", facturasClientes.EmpleadoId);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", facturasClientes.EstadoId);
            ViewBag.IRPFId = new SelectList(db.IRPFs, "Id", "Descripcion", facturasClientes.IRPFId);
            ViewBag.TipoFacturaId = new SelectList(db.TiposFacturas, "Id", "Prefijo", facturasClientes.TipoFacturaId);
            DisplayErrorMessage();
            return PartialView(facturasClientes);
        }

        // GET: FacturasClientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacturasClientes facturasClientes = db.FacturasClientes.Find(id);
            if (facturasClientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpleadoId = new SelectList(db.Empleados, "Id", "CodEmpleado", facturasClientes.EmpleadoId);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", facturasClientes.EstadoId);
            ViewBag.IRPFId = new SelectList(db.IRPFs, "Id", "Descripcion", facturasClientes.IRPFId);
            ViewBag.TipoFacturaId = new SelectList(db.TiposFacturas, "Id", "Prefijo", facturasClientes.TipoFacturaId);
            return PartialView(facturasClientes);
        }

        // POST: FacturasClientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TipoFacturaId,CodFactura,IRPFId,EmpleadoId,EstadoId,Empleados,Estados,IRPFs,TiposFacturas,Historicos,PedidosClientes")] FacturasClientes facturasClientes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturasClientes).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a FacturasClientes record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.EmpleadoId = new SelectList(db.Empleados, "Id", "CodEmpleado", facturasClientes.EmpleadoId);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", facturasClientes.EstadoId);
            ViewBag.IRPFId = new SelectList(db.IRPFs, "Id", "Descripcion", facturasClientes.IRPFId);
            ViewBag.TipoFacturaId = new SelectList(db.TiposFacturas, "Id", "Prefijo", facturasClientes.TipoFacturaId);
            DisplayErrorMessage();
            return PartialView(facturasClientes);
        }

        // GET: FacturasClientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacturasClientes facturasClientes = db.FacturasClientes.Find(id);
            if (facturasClientes == null)
            {
                return HttpNotFound();
            }
            return PartialView(facturasClientes);
        }

        // POST: FacturasClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FacturasClientes facturasClientes = db.FacturasClientes.Find(id);
            db.FacturasClientes.Remove(facturasClientes);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a FacturasClientes record");
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
