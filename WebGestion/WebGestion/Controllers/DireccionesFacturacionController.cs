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
    public class DireccionesFacturacionController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: DireccionesFacturacion/Index
        public ActionResult Index()
        {
            var direccionesFacturacion = db.DireccionesFacturacion.Include(d => d.Clientes).Include(d => d.Estados).Include(d => d.Localidades);
            return PartialView(direccionesFacturacion.ToList());
        }

        /*
        // GET: DireccionesFacturacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DireccionesFacturacion direccionesFacturacion = db.DireccionesFacturacion.Find(id);
            if (direccionesFacturacion == null)
            {
                return HttpNotFound();
            }
            return PartialView(direccionesFacturacion);
        }
        */

        // GET: DireccionesFacturacion/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Id");
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre");
            ViewBag.LocalidadId = new SelectList(db.Localidades, "Id", "Localidad");
            return PartialView();
        }

        // POST: DireccionesFacturacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClienteId,Activa,Nombre,Apellidos,Direccion,CP,Localidad,Provincia,Pais,LocalidadId,EstadoId,Observaciones,FechaAlta,FechaBaja,Clientes,Estados,Localidades")] DireccionesFacturacion direccionesFacturacion)
        {
            if (ModelState.IsValid)
            {
                db.DireccionesFacturacion.Add(direccionesFacturacion);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a DireccionesFacturacion record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Id", direccionesFacturacion.ClienteId);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", direccionesFacturacion.EstadoId);
            ViewBag.LocalidadId = new SelectList(db.Localidades, "Id", "Localidad", direccionesFacturacion.LocalidadId);
            DisplayErrorMessage();
            return PartialView(direccionesFacturacion);
        }

        // GET: DireccionesFacturacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DireccionesFacturacion direccionesFacturacion = db.DireccionesFacturacion.Find(id);
            if (direccionesFacturacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Id", direccionesFacturacion.ClienteId);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", direccionesFacturacion.EstadoId);
            ViewBag.LocalidadId = new SelectList(db.Localidades, "Id", "Localidad", direccionesFacturacion.LocalidadId);
            return PartialView(direccionesFacturacion);
        }

        // POST: DireccionesFacturacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClienteId,Activa,Nombre,Apellidos,Direccion,CP,Localidad,Provincia,Pais,LocalidadId,EstadoId,Observaciones,FechaAlta,FechaBaja,Clientes,Estados,Localidades")] DireccionesFacturacion direccionesFacturacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(direccionesFacturacion).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a DireccionesFacturacion record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Id", direccionesFacturacion.ClienteId);
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", direccionesFacturacion.EstadoId);
            ViewBag.LocalidadId = new SelectList(db.Localidades, "Id", "Localidad", direccionesFacturacion.LocalidadId);
            DisplayErrorMessage();
            return PartialView(direccionesFacturacion);
        }

        // GET: DireccionesFacturacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DireccionesFacturacion direccionesFacturacion = db.DireccionesFacturacion.Find(id);
            if (direccionesFacturacion == null)
            {
                return HttpNotFound();
            }
            return PartialView(direccionesFacturacion);
        }

        // POST: DireccionesFacturacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DireccionesFacturacion direccionesFacturacion = db.DireccionesFacturacion.Find(id);
            db.DireccionesFacturacion.Remove(direccionesFacturacion);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a DireccionesFacturacion record");
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
