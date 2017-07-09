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
    public class IVAsController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: IVAs/Index
        public ActionResult Index()
        {
            var iVAs = db.IVAs.Include(i => i.Estados);
            return PartialView(iVAs.ToList());
        }

        /*
        // GET: IVAs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IVAs iVAs = db.IVAs.Find(id);
            if (iVAs == null)
            {
                return HttpNotFound();
            }
            return PartialView(iVAs);
        }
        */

        // GET: IVAs/Create
        public ActionResult Create()
        {
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre");
            return PartialView();
        }

        // POST: IVAs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,IVA,EstadoId,FechaAlta,FechaBaja,Bienes,Estados,PedidosProveedoresDetalle,PresupuestosClientesDetalle")] IVAs iVAs)
        {
            if (ModelState.IsValid)
            {
                db.IVAs.Add(iVAs);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a IVAs record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", iVAs.EstadoId);
            DisplayErrorMessage();
            return PartialView(iVAs);
        }

        // GET: IVAs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IVAs iVAs = db.IVAs.Find(id);
            if (iVAs == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", iVAs.EstadoId);
            return PartialView(iVAs);
        }

        // POST: IVAs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion,IVA,EstadoId,FechaAlta,FechaBaja,Bienes,Estados,PedidosProveedoresDetalle,PresupuestosClientesDetalle")] IVAs iVAs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iVAs).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a IVAs record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", iVAs.EstadoId);
            DisplayErrorMessage();
            return PartialView(iVAs);
        }

        // GET: IVAs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IVAs iVAs = db.IVAs.Find(id);
            if (iVAs == null)
            {
                return HttpNotFound();
            }
            return PartialView(iVAs);
        }

        // POST: IVAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IVAs iVAs = db.IVAs.Find(id);
            db.IVAs.Remove(iVAs);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a IVAs record");
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
