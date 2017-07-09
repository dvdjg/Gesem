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
    public class HistoricosController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: Historicos/Index
        public ActionResult Index()
        {
            var historicos = db.Historicos.Include(h => h.Estados).Include(h => h.TiposPropiedades);
            return PartialView(historicos.ToList());
        }

        /*
        // GET: Historicos/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historicos historicos = db.Historicos.Find(id);
            if (historicos == null)
            {
                return HttpNotFound();
            }
            return PartialView(historicos);
        }
        */

        // GET: Historicos/Create
        public ActionResult Create()
        {
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre");
            ViewBag.TipoId = new SelectList(db.TiposPropiedades, "Id", "Nombre");
            return PartialView();
        }

        // POST: Historicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,UsuarioId,TipoId,Descripcion,Archivo,EstadoId,Estados,TiposPropiedades,Bienes,FacturasClientes,FacturasProveedores,Familias,PedidosClientes,PedidosProveedores,Personas,PresupuestosClientes")] Historicos historicos)
        {
            if (ModelState.IsValid)
            {
                db.Historicos.Add(historicos);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a Historicos record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", historicos.EstadoId);
            ViewBag.TipoId = new SelectList(db.TiposPropiedades, "Id", "Nombre", historicos.TipoId);
            DisplayErrorMessage();
            return PartialView(historicos);
        }

        // GET: Historicos/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historicos historicos = db.Historicos.Find(id);
            if (historicos == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", historicos.EstadoId);
            ViewBag.TipoId = new SelectList(db.TiposPropiedades, "Id", "Nombre", historicos.TipoId);
            return PartialView(historicos);
        }

        // POST: Historicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,UsuarioId,TipoId,Descripcion,Archivo,EstadoId,Estados,TiposPropiedades,Bienes,FacturasClientes,FacturasProveedores,Familias,PedidosClientes,PedidosProveedores,Personas,PresupuestosClientes")] Historicos historicos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historicos).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a Historicos record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", historicos.EstadoId);
            ViewBag.TipoId = new SelectList(db.TiposPropiedades, "Id", "Nombre", historicos.TipoId);
            DisplayErrorMessage();
            return PartialView(historicos);
        }

        // GET: Historicos/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historicos historicos = db.Historicos.Find(id);
            if (historicos == null)
            {
                return HttpNotFound();
            }
            return PartialView(historicos);
        }

        // POST: Historicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Historicos historicos = db.Historicos.Find(id);
            db.Historicos.Remove(historicos);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a Historicos record");
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
