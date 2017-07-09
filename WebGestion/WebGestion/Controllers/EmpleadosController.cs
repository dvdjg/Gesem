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
    public class EmpleadosController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: Empleados/Index
        public ActionResult Index()
        {
            var empleados = db.Empleados.Include(e => e.Personas).Include(e => e.Empleados2).Include(e => e.Estados).Include(e => e.Locales);
            return PartialView(empleados.ToList());
        }

        /*
        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return PartialView(empleados);
        }
        */

        // GET: Empleados/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.CoordinadorId = new SelectList(db.Empleados, "Id", "CodEmpleado");
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre");
            ViewBag.LocalId = new SelectList(db.Locales, "Id", "Nombre");
            return PartialView();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodEmpleado,Lugar,LocalId,CoordinadorId,EstadoId,FechaAlta,FechaBaja,AspNetUsers,Empleados1,Empleados2,Estados,Locales,FacturasClientes,Cargos")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleados);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a Empleados record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", empleados.Id);
            ViewBag.CoordinadorId = new SelectList(db.Empleados, "Id", "CodEmpleado", empleados.CoordinadorId);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", empleados.EstadoId);
            ViewBag.LocalId = new SelectList(db.Locales, "Id", "Nombre", empleados.LocalId);
            DisplayErrorMessage();
            return PartialView(empleados);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", empleados.Id);
            ViewBag.CoordinadorId = new SelectList(db.Empleados, "Id", "CodEmpleado", empleados.CoordinadorId);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", empleados.EstadoId);
            ViewBag.LocalId = new SelectList(db.Locales, "Id", "Nombre", empleados.LocalId);
            return PartialView(empleados);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodEmpleado,Lugar,LocalId,CoordinadorId,EstadoId,FechaAlta,FechaBaja,AspNetUsers,Empleados1,Empleados2,Estados,Locales,FacturasClientes,Cargos")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleados).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a Empleados record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", empleados.Id);
            ViewBag.CoordinadorId = new SelectList(db.Empleados, "Id", "CodEmpleado", empleados.CoordinadorId);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", empleados.EstadoId);
            ViewBag.LocalId = new SelectList(db.Locales, "Id", "Nombre", empleados.LocalId);
            DisplayErrorMessage();
            return PartialView(empleados);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return PartialView(empleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleados empleados = db.Empleados.Find(id);
            db.Empleados.Remove(empleados);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a Empleados record");
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
