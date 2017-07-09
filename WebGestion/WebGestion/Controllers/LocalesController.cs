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
    public class LocalesController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: Locales/Index
        public ActionResult Index()
        {
            var locales = db.Locales.Include(l => l.Empresas).Include(l => l.Estados).Include(l => l.Localidades);
            return PartialView(locales.ToList());
        }

        /*
        // GET: Locales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locales locales = db.Locales.Find(id);
            if (locales == null)
            {
                return HttpNotFound();
            }
            return PartialView(locales);
        }
        */

        // GET: Locales/Create
        public ActionResult Create()
        {
            ViewBag.EmpresaId = new SelectList(db.Empresas, "Id", "NIF");
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre");
            ViewBag.LocalidadId = new SelectList(db.Localidades, "Id", "Localidad");
            return PartialView();
        }

        // POST: Locales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmpresaId,OrdenDelLocal,Nombre,Direccion,CP,Localidad,Provincia,Pais,LocalidadId,TelefonoFijo,TelefonoMovil,TelefonoFAX,Email,Observaciones,EstadoId,FechaAlta,FechaBaja,Empleados,Empresas,Estados,Localidades,Inventario")] Locales locales)
        {
            if (ModelState.IsValid)
            {
                db.Locales.Add(locales);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a Locales record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.EmpresaId = new SelectList(db.Empresas, "Id", "NIF", locales.EmpresaId);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", locales.EstadoId);
            ViewBag.LocalidadId = new SelectList(db.Localidades, "Id", "Localidad", locales.LocalidadId);
            DisplayErrorMessage();
            return PartialView(locales);
        }

        // GET: Locales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locales locales = db.Locales.Find(id);
            if (locales == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpresaId = new SelectList(db.Empresas, "Id", "NIF", locales.EmpresaId);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", locales.EstadoId);
            ViewBag.LocalidadId = new SelectList(db.Localidades, "Id", "Localidad", locales.LocalidadId);
            return PartialView(locales);
        }

        // POST: Locales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmpresaId,OrdenDelLocal,Nombre,Direccion,CP,Localidad,Provincia,Pais,LocalidadId,TelefonoFijo,TelefonoMovil,TelefonoFAX,Email,Observaciones,EstadoId,FechaAlta,FechaBaja,Empleados,Empresas,Estados,Localidades,Inventario")] Locales locales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locales).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a Locales record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.EmpresaId = new SelectList(db.Empresas, "Id", "NIF", locales.EmpresaId);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", locales.EstadoId);
            ViewBag.LocalidadId = new SelectList(db.Localidades, "Id", "Localidad", locales.LocalidadId);
            DisplayErrorMessage();
            return PartialView(locales);
        }

        // GET: Locales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locales locales = db.Locales.Find(id);
            if (locales == null)
            {
                return HttpNotFound();
            }
            return PartialView(locales);
        }

        // POST: Locales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Locales locales = db.Locales.Find(id);
            db.Locales.Remove(locales);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a Locales record");
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
