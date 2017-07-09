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
    public class EstadosController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: Estados/Index
        public ActionResult Index()
        {
            var estados = db.Estados.Include(e => e.AspNetRoles).Include(e => e.AspNetUsers).Include(e => e.Estados2);
            return PartialView(estados.ToList());
        }

        /*
        // GET: Estados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estados estados = db.Estados.Find(id);
            if (estados == null)
            {
                return HttpNotFound();
            }
            return PartialView(estados);
        }
        */

        // GET: Estados/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.AspNetRoles, "Id", "Name");
            //ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.PadreId = new SelectList(db.Estados, "Id", "Nombre");
            return PartialView();
        }

        // POST: Estados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Descripcion,PadreId,Bienes,AspNetRoles,AspNetUsers,Cargos,Clientes,DireccionesFacturacion,Empleados,Empresas,Permisos,FacturasClientes,FacturasProveedores,Familias,FormasEntrega,FormasPago,Historicos,IRPFs,IVAs,Locales,PedidosClientes,PedidosProveedores,Personas,PresupuestosClientes,Proveedores,Estados1,Estados2")] Estados estados)
        {
            if (ModelState.IsValid)
            {
                db.Estados.Add(estados);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a Estados record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.AspNetRoles, "Id", "Name", estados.Id);
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", estados.Id);
            ViewBag.PadreId = new SelectList(db.Estados, "Id", "Nombre", estados.PadreId);
            DisplayErrorMessage();
            return PartialView(estados);
        }

        // GET: Estados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estados estados = db.Estados.Find(id);
            if (estados == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.AspNetRoles, "Id", "Name", estados.Id);
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", estados.Id);
            ViewBag.PadreId = new SelectList(db.Estados, "Id", "Nombre", estados.PadreId);
            return PartialView(estados);
        }

        // POST: Estados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Descripcion,PadreId,Bienes,AspNetRoles,AspNetUsers,Cargos,Clientes,DireccionesFacturacion,Empleados,Empresas,Permisos,FacturasClientes,FacturasProveedores,Familias,FormasEntrega,FormasPago,Historicos,IRPFs,IVAs,Locales,PedidosClientes,PedidosProveedores,Personas,PresupuestosClientes,Proveedores,Estados1,Estados2")] Estados estados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estados).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a Estados record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.AspNetRoles, "Id", "Name", estados.Id);
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", estados.Id);
            ViewBag.PadreId = new SelectList(db.Estados, "Id", "Nombre", estados.PadreId);
            DisplayErrorMessage();
            return PartialView(estados);
        }

        // GET: Estados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estados estados = db.Estados.Find(id);
            if (estados == null)
            {
                return HttpNotFound();
            }
            return PartialView(estados);
        }

        // POST: Estados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estados estados = db.Estados.Find(id);
            db.Estados.Remove(estados);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a Estados record");
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
