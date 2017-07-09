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
    public class TiposFacturasController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: TiposFacturas/Index
        public ActionResult Index()
        {
            return PartialView(db.TiposFacturas.ToList());
        }

        /*
        // GET: TiposFacturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposFacturas tiposFacturas = db.TiposFacturas.Find(id);
            if (tiposFacturas == null)
            {
                return HttpNotFound();
            }
            return PartialView(tiposFacturas);
        }
        */

        // GET: TiposFacturas/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: TiposFacturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Prefijo,Descripcion,FacturasClientes,FacturasProveedores")] TiposFacturas tiposFacturas)
        {
            if (ModelState.IsValid)
            {
                db.TiposFacturas.Add(tiposFacturas);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a TiposFacturas record");
                return JsonRedirectToAction("Index");
            }

            DisplayErrorMessage();
            return PartialView(tiposFacturas);
        }

        // GET: TiposFacturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposFacturas tiposFacturas = db.TiposFacturas.Find(id);
            if (tiposFacturas == null)
            {
                return HttpNotFound();
            }
            return PartialView(tiposFacturas);
        }

        // POST: TiposFacturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Prefijo,Descripcion,FacturasClientes,FacturasProveedores")] TiposFacturas tiposFacturas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiposFacturas).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a TiposFacturas record");
                return JsonRedirectToAction("Index");
            }
            DisplayErrorMessage();
            return PartialView(tiposFacturas);
        }

        // GET: TiposFacturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposFacturas tiposFacturas = db.TiposFacturas.Find(id);
            if (tiposFacturas == null)
            {
                return HttpNotFound();
            }
            return PartialView(tiposFacturas);
        }

        // POST: TiposFacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TiposFacturas tiposFacturas = db.TiposFacturas.Find(id);
            db.TiposFacturas.Remove(tiposFacturas);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a TiposFacturas record");
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
