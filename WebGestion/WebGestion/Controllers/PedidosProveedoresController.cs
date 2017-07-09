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
    public class PedidosProveedoresController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: PedidosProveedores/Index
        public ActionResult Index()
        {
            var pedidosProveedores = db.PedidosProveedores.Include(p => p.Estados).Include(p => p.Proveedores);
            return PartialView(pedidosProveedores.ToList());
        }

        /*
        // GET: PedidosProveedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidosProveedores pedidosProveedores = db.PedidosProveedores.Find(id);
            if (pedidosProveedores == null)
            {
                return HttpNotFound();
            }
            return PartialView(pedidosProveedores);
        }
        */

        // GET: PedidosProveedores/Create
        public ActionResult Create()
        {
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre");
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "Id", "TelefonoFAX");
            return PartialView();
        }

        // POST: PedidosProveedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodPedido,ProveedorId,EstadoId,Estados,Proveedores,PedidosProveedoresDetalle,Historicos,FacturasProveedores")] PedidosProveedores pedidosProveedores)
        {
            if (ModelState.IsValid)
            {
                db.PedidosProveedores.Add(pedidosProveedores);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a PedidosProveedores record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", pedidosProveedores.EstadoId);
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "Id", "TelefonoFAX", pedidosProveedores.ProveedorId);
            DisplayErrorMessage();
            return PartialView(pedidosProveedores);
        }

        // GET: PedidosProveedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidosProveedores pedidosProveedores = db.PedidosProveedores.Find(id);
            if (pedidosProveedores == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", pedidosProveedores.EstadoId);
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "Id", "TelefonoFAX", pedidosProveedores.ProveedorId);
            return PartialView(pedidosProveedores);
        }

        // POST: PedidosProveedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodPedido,ProveedorId,EstadoId,Estados,Proveedores,PedidosProveedoresDetalle,Historicos,FacturasProveedores")] PedidosProveedores pedidosProveedores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedidosProveedores).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a PedidosProveedores record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", pedidosProveedores.EstadoId);
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "Id", "TelefonoFAX", pedidosProveedores.ProveedorId);
            DisplayErrorMessage();
            return PartialView(pedidosProveedores);
        }

        // GET: PedidosProveedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidosProveedores pedidosProveedores = db.PedidosProveedores.Find(id);
            if (pedidosProveedores == null)
            {
                return HttpNotFound();
            }
            return PartialView(pedidosProveedores);
        }

        // POST: PedidosProveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PedidosProveedores pedidosProveedores = db.PedidosProveedores.Find(id);
            db.PedidosProveedores.Remove(pedidosProveedores);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a PedidosProveedores record");
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
