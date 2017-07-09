using Microsoft.AspNet.Identity;
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
    public class AspNetUsersController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        public IdentityResult RefreshUserGroupRoles(int userId)
        {
            // Asegura que la tabla AspNetUserRoles se corresponda con los roles asignados los grupos del usuario
            AspNetUsers aspNetUser = db.AspNetUsers.Find(userId);
            if (aspNetUser == null)
            {
                throw new ArgumentNullException("User");
            }
            //var groups = aspNetUser.ApplicationGroups;
            //var userGroups = groups.Any(g => g.AspNetUsers.Contains(aspNetUser));
            //var usr = aspNetUser.ApplicationGroups.Any(g => g.AspNetRoles.Contains());
            var roles = db.AspNetRoles.Where(r => aspNetUser.ApplicationGroups.Any(g => g.AspNetRoles.Contains(r)));

            return IdentityResult.Success;
        }

        // GET: AspNetUsers/Index
        public ActionResult Index()
        {
            //var aspNetUsers = db.AspNetUsers.Include(a => a.Estados).Include(a => a.Empleados);
            //return PartialView(aspNetUsers.ToList());
            return PartialView();
        }

        /*
        // GET: AspNetUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return PartialView(aspNetUsers);
        }
        */

        // GET: AspNetUsers/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Estados, "Id", "Nombre");
            ViewBag.Id = new SelectList(db.Empleados, "Id", "CodEmpleado");
            return PartialView();
        }

        // POST: AspNetUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,EstadoId,FechaAlta,FechaBaja,AspNetUserClaims,AspNetUserLogins,Estados,Empleados,PedidosClientes,Personas,PresupuestosClientes,CambiosLog,ApplicationGroups,AspNetRoles")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(aspNetUsers);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a AspNetUsers record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Estados, "Id", "Nombre", aspNetUsers.Id);
            ViewBag.Id = new SelectList(db.Empleados, "Id", "CodEmpleado", aspNetUsers.Id);
            DisplayErrorMessage();
            return PartialView(aspNetUsers);
        }

        // GET: AspNetUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Estados, "Id", "Nombre", aspNetUsers.Id);
            ViewBag.Id = new SelectList(db.Empleados, "Id", "CodEmpleado", aspNetUsers.Id);
            return PartialView(aspNetUsers);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,EstadoId,FechaAlta,FechaBaja,AspNetUserClaims,AspNetUserLogins,Estados,Empleados,PedidosClientes,Personas,PresupuestosClientes,CambiosLog,ApplicationGroups,AspNetRoles")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUsers).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a AspNetUsers record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Estados, "Id", "Nombre", aspNetUsers.Id);
            ViewBag.Id = new SelectList(db.Empleados, "Id", "CodEmpleado", aspNetUsers.Id);
            DisplayErrorMessage();
            return PartialView(aspNetUsers);
        }

        // GET: AspNetUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return PartialView(aspNetUsers);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUsers);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a AspNetUsers record");
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
