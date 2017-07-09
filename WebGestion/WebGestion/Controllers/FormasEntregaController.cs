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
    public class FormasEntregaController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: FormasEntrega/Index
        public ActionResult Index()
        {
            var formasEntrega = db.FormasEntrega.Include(f => f.Estados);
            return PartialView(formasEntrega.ToList());
        }

        /*
        // GET: FormasEntrega/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormasEntrega formasEntrega = db.FormasEntrega.Find(id);
            if (formasEntrega == null)
            {
                return HttpNotFound();
            }
            return PartialView(formasEntrega);
        }
        */

        // GET: FormasEntrega/Create
        public ActionResult Create()
        {
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre");
            return PartialView();
        }

        // POST: FormasEntrega/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FormaEntrega,EstadoId,FechaAlta,FechaBaja,Estados,FacturasProveedores,PedidosClientes")] FormasEntrega formasEntrega)
        {
            if (ModelState.IsValid)
            {
                db.FormasEntrega.Add(formasEntrega);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a FormasEntrega record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", formasEntrega.EstadoId);
            DisplayErrorMessage();
            return PartialView(formasEntrega);
        }

        // GET: FormasEntrega/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormasEntrega formasEntrega = db.FormasEntrega.Find(id);
            if (formasEntrega == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", formasEntrega.EstadoId);
            return PartialView(formasEntrega);
        }

        // POST: FormasEntrega/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FormaEntrega,EstadoId,FechaAlta,FechaBaja,Estados,FacturasProveedores,PedidosClientes")] FormasEntrega formasEntrega)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formasEntrega).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a FormasEntrega record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", formasEntrega.EstadoId);
            DisplayErrorMessage();
            return PartialView(formasEntrega);
        }

        // GET: FormasEntrega/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormasEntrega formasEntrega = db.FormasEntrega.Find(id);
            if (formasEntrega == null)
            {
                return HttpNotFound();
            }
            return PartialView(formasEntrega);
        }

        // POST: FormasEntrega/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FormasEntrega formasEntrega = db.FormasEntrega.Find(id);
            db.FormasEntrega.Remove(formasEntrega);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a FormasEntrega record");
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
