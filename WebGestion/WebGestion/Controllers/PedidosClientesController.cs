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
    public class PedidosClientesController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: PedidosClientes/Index
        public ActionResult Index()
        {
            var pedidosClientes = db.PedidosClientes.Include(p => p.AspNetUsers).Include(p => p.Estados).Include(p => p.FormasEntrega).Include(p => p.FormasPago).Include(p => p.PresupuestosClientes);
            return PartialView(pedidosClientes.ToList());
        }

        /*
        // GET: PedidosClientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidosClientes pedidosClientes = db.PedidosClientes.Find(id);
            if (pedidosClientes == null)
            {
                return HttpNotFound();
            }
            return PartialView(pedidosClientes);
        }
        */

        // GET: PedidosClientes/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre");
            ViewBag.FormaEntregaId = new SelectList(db.FormasEntrega, "Id", "FormaEntrega");
            ViewBag.FormaPagoId = new SelectList(db.FormasPago, "Id", "FormaPago");
            ViewBag.PresupuestoId = new SelectList(db.PresupuestosClientes, "Id", "CodPresupuesto");
            return PartialView();
        }

        // POST: PedidosClientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodPedido,PresupuestoId,FormaPagoId,FormaEntregaId,UsuarioId,EstadoId,AspNetUsers,Estados,FormasEntrega,FormasPago,PresupuestosClientes,Historicos,FacturasClientes")] PedidosClientes pedidosClientes)
        {
            if (ModelState.IsValid)
            {
                db.PedidosClientes.Add(pedidosClientes);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a PedidosClientes record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.UsuarioId = new SelectList(db.AspNetUsers, "Id", "Email", pedidosClientes.UsuarioId);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", pedidosClientes.EstadoId);
            ViewBag.FormaEntregaId = new SelectList(db.FormasEntrega, "Id", "FormaEntrega", pedidosClientes.FormaEntregaId);
            ViewBag.FormaPagoId = new SelectList(db.FormasPago, "Id", "FormaPago", pedidosClientes.FormaPagoId);
            ViewBag.PresupuestoId = new SelectList(db.PresupuestosClientes, "Id", "CodPresupuesto", pedidosClientes.PresupuestoId);
            DisplayErrorMessage();
            return PartialView(pedidosClientes);
        }

        // GET: PedidosClientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidosClientes pedidosClientes = db.PedidosClientes.Find(id);
            if (pedidosClientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuarioId = new SelectList(db.AspNetUsers, "Id", "Email", pedidosClientes.UsuarioId);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", pedidosClientes.EstadoId);
            ViewBag.FormaEntregaId = new SelectList(db.FormasEntrega, "Id", "FormaEntrega", pedidosClientes.FormaEntregaId);
            ViewBag.FormaPagoId = new SelectList(db.FormasPago, "Id", "FormaPago", pedidosClientes.FormaPagoId);
            ViewBag.PresupuestoId = new SelectList(db.PresupuestosClientes, "Id", "CodPresupuesto", pedidosClientes.PresupuestoId);
            return PartialView(pedidosClientes);
        }

        // POST: PedidosClientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodPedido,PresupuestoId,FormaPagoId,FormaEntregaId,UsuarioId,EstadoId,AspNetUsers,Estados,FormasEntrega,FormasPago,PresupuestosClientes,Historicos,FacturasClientes")] PedidosClientes pedidosClientes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedidosClientes).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a PedidosClientes record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.UsuarioId = new SelectList(db.AspNetUsers, "Id", "Email", pedidosClientes.UsuarioId);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", pedidosClientes.EstadoId);
            ViewBag.FormaEntregaId = new SelectList(db.FormasEntrega, "Id", "FormaEntrega", pedidosClientes.FormaEntregaId);
            ViewBag.FormaPagoId = new SelectList(db.FormasPago, "Id", "FormaPago", pedidosClientes.FormaPagoId);
            ViewBag.PresupuestoId = new SelectList(db.PresupuestosClientes, "Id", "CodPresupuesto", pedidosClientes.PresupuestoId);
            DisplayErrorMessage();
            return PartialView(pedidosClientes);
        }

        // GET: PedidosClientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidosClientes pedidosClientes = db.PedidosClientes.Find(id);
            if (pedidosClientes == null)
            {
                return HttpNotFound();
            }
            return PartialView(pedidosClientes);
        }

        // POST: PedidosClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PedidosClientes pedidosClientes = db.PedidosClientes.Find(id);
            db.PedidosClientes.Remove(pedidosClientes);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a PedidosClientes record");
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
