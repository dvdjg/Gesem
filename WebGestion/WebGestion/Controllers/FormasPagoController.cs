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
    public class FormasPagoController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: FormasPago/Index
        public ActionResult Index()
        {
            var formasPago = db.FormasPago.Include(f => f.Estados);
            return PartialView(formasPago.ToList());
        }

        /*
        // GET: FormasPago/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormasPago formasPago = db.FormasPago.Find(id);
            if (formasPago == null)
            {
                return HttpNotFound();
            }
            return PartialView(formasPago);
        }
        */

        // GET: FormasPago/Create
        public ActionResult Create()
        {
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre");
            return PartialView();
        }

        // POST: FormasPago/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FormaPago,EstadoId,FechaAlta,FechaBaja,Estados,FacturasProveedores,PedidosClientes")] FormasPago formasPago)
        {
            if (ModelState.IsValid)
            {
                db.FormasPago.Add(formasPago);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a FormasPago record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", formasPago.EstadoId);
            DisplayErrorMessage();
            return PartialView(formasPago);
        }

        // GET: FormasPago/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormasPago formasPago = db.FormasPago.Find(id);
            if (formasPago == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", formasPago.EstadoId);
            return PartialView(formasPago);
        }

        // POST: FormasPago/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FormaPago,EstadoId,FechaAlta,FechaBaja,Estados,FacturasProveedores,PedidosClientes")] FormasPago formasPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formasPago).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a FormasPago record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", formasPago.EstadoId);
            DisplayErrorMessage();
            return PartialView(formasPago);
        }

        // GET: FormasPago/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormasPago formasPago = db.FormasPago.Find(id);
            if (formasPago == null)
            {
                return HttpNotFound();
            }
            return PartialView(formasPago);
        }

        // POST: FormasPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FormasPago formasPago = db.FormasPago.Find(id);
            db.FormasPago.Remove(formasPago);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a FormasPago record");
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
