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
    public class PersonasController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: Personas/Index
        public ActionResult Index()
        {
            var personas = db.Personas.Include(p => p.AspNetUsers).Include(p => p.Clientes).Include(p => p.Estados).Include(p => p.Idiomas).Include(p => p.Localidades).Include(p => p.Proveedores);
            return PartialView(personas.ToList());
        }

        /*
        // GET: Personas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personas personas = db.Personas.Find(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            return PartialView(personas);
        }
        */

        // GET: Personas/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Id = new SelectList(db.Clientes, "Id", "Id");
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre");
            ViewBag.IdiomaId = new SelectList(db.Idiomas, "Id", "Codigo");
            ViewBag.LocalidadId = new SelectList(db.Localidades, "Id", "Localidad");
            ViewBag.Id = new SelectList(db.Proveedores, "Id", "TelefonoFAX");
            return PartialView();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Apellidos,NIF,Direccion,Localidad,Provincia,Pais,CP,LocalidadId,UserId,IdiomaId,TelefonoFijo,TelefonoMovil,Email,Observaciones,EstadoId,AspNetUsers,Clientes,Estados,Idiomas,Localidades,Proveedores,Historicos")] Personas personas)
        {
            if (ModelState.IsValid)
            {
                db.Personas.Add(personas);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a Personas record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", personas.UserId);
            ViewBag.Id = new SelectList(db.Clientes, "Id", "Id", personas.Id);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", personas.EstadoId);
            ViewBag.IdiomaId = new SelectList(db.Idiomas, "Id", "Codigo", personas.IdiomaId);
            ViewBag.LocalidadId = new SelectList(db.Localidades, "Id", "Localidad", personas.LocalidadId);
            ViewBag.Id = new SelectList(db.Proveedores, "Id", "TelefonoFAX", personas.Id);
            DisplayErrorMessage();
            return PartialView(personas);
        }

        // GET: Personas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personas personas = db.Personas.Find(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", personas.UserId);
            ViewBag.Id = new SelectList(db.Clientes, "Id", "Id", personas.Id);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", personas.EstadoId);
            ViewBag.IdiomaId = new SelectList(db.Idiomas, "Id", "Codigo", personas.IdiomaId);
            ViewBag.LocalidadId = new SelectList(db.Localidades, "Id", "Localidad", personas.LocalidadId);
            ViewBag.Id = new SelectList(db.Proveedores, "Id", "TelefonoFAX", personas.Id);
            return PartialView(personas);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Apellidos,NIF,Direccion,Localidad,Provincia,Pais,CP,LocalidadId,UserId,IdiomaId,TelefonoFijo,TelefonoMovil,Email,Observaciones,EstadoId,AspNetUsers,Clientes,Estados,Idiomas,Localidades,Proveedores,Historicos")] Personas personas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personas).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a Personas record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", personas.UserId);
            ViewBag.Id = new SelectList(db.Clientes, "Id", "Id", personas.Id);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", personas.EstadoId);
            ViewBag.IdiomaId = new SelectList(db.Idiomas, "Id", "Codigo", personas.IdiomaId);
            ViewBag.LocalidadId = new SelectList(db.Localidades, "Id", "Localidad", personas.LocalidadId);
            ViewBag.Id = new SelectList(db.Proveedores, "Id", "TelefonoFAX", personas.Id);
            DisplayErrorMessage();
            return PartialView(personas);
        }

        // GET: Personas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personas personas = db.Personas.Find(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            return PartialView(personas);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personas personas = db.Personas.Find(id);
            db.Personas.Remove(personas);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a Personas record");
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
