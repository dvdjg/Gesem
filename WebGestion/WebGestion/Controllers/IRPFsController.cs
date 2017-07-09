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
    public class IRPFsController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: IRPFs/Index
        public ActionResult Index()
        {
            var iRPFs = db.IRPFs.Include(i => i.Estados);
            return PartialView(iRPFs.ToList());
        }

        /*
        // GET: IRPFs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IRPFs iRPFs = db.IRPFs.Find(id);
            if (iRPFs == null)
            {
                return HttpNotFound();
            }
            return PartialView(iRPFs);
        }
        */

        // GET: IRPFs/Create
        public ActionResult Create()
        {
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre");
            return PartialView();
        }

        // POST: IRPFs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,IRPF,EstadoId,FechaAlta,FechaBaja,Estados,FacturasClientes")] IRPFs iRPFs)
        {
            if (ModelState.IsValid)
            {
                db.IRPFs.Add(iRPFs);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a IRPFs record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", iRPFs.EstadoId);
            DisplayErrorMessage();
            return PartialView(iRPFs);
        }

        // GET: IRPFs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IRPFs iRPFs = db.IRPFs.Find(id);
            if (iRPFs == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", iRPFs.EstadoId);
            return PartialView(iRPFs);
        }

        // POST: IRPFs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion,IRPF,EstadoId,FechaAlta,FechaBaja,Estados,FacturasClientes")] IRPFs iRPFs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iRPFs).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a IRPFs record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Nombre", iRPFs.EstadoId);
            DisplayErrorMessage();
            return PartialView(iRPFs);
        }

        // GET: IRPFs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IRPFs iRPFs = db.IRPFs.Find(id);
            if (iRPFs == null)
            {
                return HttpNotFound();
            }
            return PartialView(iRPFs);
        }

        // POST: IRPFs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IRPFs iRPFs = db.IRPFs.Find(id);
            db.IRPFs.Remove(iRPFs);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a IRPFs record");
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
