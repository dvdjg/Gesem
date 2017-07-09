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
    public class PresupuestosClientesController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: PresupuestosClientes/Index
        public ActionResult Index()
        {
            var presupuestosClientes = db.PresupuestosClientes.Include(p => p.AspNetUsers).Include(p => p.Clientes).Include(p => p.Estados);
            return PartialView(presupuestosClientes.ToList());
        }

        /*
        // GET: PresupuestosClientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresupuestosClientes presupuestosClientes = db.PresupuestosClientes.Find(id);
            if (presupuestosClientes == null)
            {
                return HttpNotFound();
            }
            return PartialView(presupuestosClientes);
        }
        */

        // GET: PresupuestosClientes/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Id");
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre");
            return PartialView();
        }

        // POST: PresupuestosClientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodPresupuesto,ClienteId,UsuarioId,EstadoId,AspNetUsers,Clientes,Estados,PedidosClientes,PresupuestosClientesDetalleAgrupados,Historicos")] PresupuestosClientes presupuestosClientes)
        {
            if (ModelState.IsValid)
            {
                db.PresupuestosClientes.Add(presupuestosClientes);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a PresupuestosClientes record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.UsuarioId = new SelectList(db.AspNetUsers, "Id", "Email", presupuestosClientes.UsuarioId);
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Id", presupuestosClientes.ClienteId);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", presupuestosClientes.EstadoId);
            DisplayErrorMessage();
            return PartialView(presupuestosClientes);
        }

        // GET: PresupuestosClientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresupuestosClientes presupuestosClientes = db.PresupuestosClientes.Find(id);
            if (presupuestosClientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuarioId = new SelectList(db.AspNetUsers, "Id", "Email", presupuestosClientes.UsuarioId);
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Id", presupuestosClientes.ClienteId);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", presupuestosClientes.EstadoId);
            return PartialView(presupuestosClientes);
        }

        // POST: PresupuestosClientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodPresupuesto,ClienteId,UsuarioId,EstadoId,AspNetUsers,Clientes,Estados,PedidosClientes,PresupuestosClientesDetalleAgrupados,Historicos")] PresupuestosClientes presupuestosClientes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(presupuestosClientes).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a PresupuestosClientes record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.UsuarioId = new SelectList(db.AspNetUsers, "Id", "Email", presupuestosClientes.UsuarioId);
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Id", presupuestosClientes.ClienteId);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", presupuestosClientes.EstadoId);
            DisplayErrorMessage();
            return PartialView(presupuestosClientes);
        }

        // GET: PresupuestosClientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresupuestosClientes presupuestosClientes = db.PresupuestosClientes.Find(id);
            if (presupuestosClientes == null)
            {
                return HttpNotFound();
            }
            return PartialView(presupuestosClientes);
        }

        // POST: PresupuestosClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PresupuestosClientes presupuestosClientes = db.PresupuestosClientes.Find(id);
            db.PresupuestosClientes.Remove(presupuestosClientes);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a PresupuestosClientes record");
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
