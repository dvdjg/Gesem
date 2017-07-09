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
    public class BienesController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: Bienes/Index
        public ActionResult Index()
        {
            var articulos = db.Bienes.Include(a => a.Estados).Include(a => a.Familias).Include(a => a.IVAs);
            return PartialView(articulos.ToList());
        }

        /*
        // GET: Bienes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bienes articulos = db.Bienes.Find(id);
            if (articulos == null)
            {
                return HttpNotFound();
            }
            return PartialView(articulos);
        }
        */

        // GET: Bienes/Create
        public ActionResult Create()
        {
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre");
            ViewBag.FamiliaId = new SelectList(db.Familias, "Id", "CodFamilia");
            ViewBag.IVAId = new SelectList(db.IVAs, "Id", "Descripcion");
            return PartialView();
        }

        // POST: Bienes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodBien,Bien,FamiliaId,IVAId,Precio,PrecioBase,Descuento,EstadoId,Estados,Familias,IVAs,BienesCompuestos,BienesCompuestos1,Inventario,PedidosProveedoresDetalle,PresupuestosClientesDetalle,Historicos")] Bienes articulos)
        {
            if (ModelState.IsValid)
            {
                db.Bienes.Add(articulos);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a Bienes record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", articulos.EstadoId);
            ViewBag.FamiliaId = new SelectList(db.Familias, "Id", "CodFamilia", articulos.FamiliaId);
            ViewBag.IVAId = new SelectList(db.IVAs, "Id", "Descripcion", articulos.IVAId);
            DisplayErrorMessage();
            return PartialView(articulos);
        }

        // GET: Bienes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bienes articulos = db.Bienes.Find(id);
            if (articulos == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", articulos.EstadoId);
            ViewBag.FamiliaId = new SelectList(db.Familias, "Id", "CodFamilia", articulos.FamiliaId);
            ViewBag.IVAId = new SelectList(db.IVAs, "Id", "Descripcion", articulos.IVAId);
            return PartialView(articulos);
        }

        // POST: Bienes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodBien,Bien,FamiliaId,IVAId,Precio,PrecioBase,Descuento,EstadoId,Estados,Familias,IVAs,BienesCompuestos,BienesCompuestos1,Inventario,PedidosProveedoresDetalle,PresupuestosClientesDetalle,Historicos")] Bienes articulos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articulos).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a Bienes record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", articulos.EstadoId);
            ViewBag.FamiliaId = new SelectList(db.Familias, "Id", "CodFamilia", articulos.FamiliaId);
            ViewBag.IVAId = new SelectList(db.IVAs, "Id", "Descripcion", articulos.IVAId);
            DisplayErrorMessage();
            return PartialView(articulos);
        }

        // GET: Bienes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bienes articulos = db.Bienes.Find(id);
            if (articulos == null)
            {
                return HttpNotFound();
            }
            return PartialView(articulos);
        }

        // POST: Bienes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bienes articulos = db.Bienes.Find(id);
            db.Bienes.Remove(articulos);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a Bienes record");
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
