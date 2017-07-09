using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebGestion.Models;

namespace WebGestion.Controllers
{
    public class LocalidadesController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: Localidades/Index
        public ActionResult Index()
        {
            //var localidades = db.Localidades.Include(l => l.Idiomas).Include(l => l.Provincias);
            //return PartialView(localidades.ToList());
            return PartialView();
        }

        /*
        // GET: Localidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidades localidades = db.Localidades.Find(id);
            if (localidades == null)
            {
                return HttpNotFound();
            }
            return PartialView(localidades);
        }
        */

        // GET: Localidades/Create
        public ActionResult Create()
        {
            ViewBag.IdiomaId = new SelectList(db.Idiomas, "Id", "Codigo");
            ViewBag.ProvinciaId = new SelectList(db.Provincias, "Id", "Provincia");
            return PartialView();
        }

        // POST: Localidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodLocalidad,ProvinciaId,IdiomaId,Localidad,x,y,Exacto,DireccionesFacturacion,Idiomas,Locales,Provincias,Personas")] Localidades localidades)
        {
            if (ModelState.IsValid)
            {
                db.Localidades.Add(localidades);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a Localidades record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.IdiomaId = new SelectList(db.Idiomas, "Id", "Codigo", localidades.IdiomaId);
            ViewBag.ProvinciaId = new SelectList(db.Provincias, "Id", "Provincia", localidades.ProvinciaId);
            DisplayErrorMessage();
            return PartialView(localidades);
        }

        // GET: Localidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidades localidades = db.Localidades.Find(id);
            if (localidades == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdiomaId = new SelectList(db.Idiomas, "Id", "Codigo", localidades.IdiomaId);
            ViewBag.ProvinciaId = new SelectList(db.Provincias, "Id", "Provincia", localidades.ProvinciaId);
            return PartialView(localidades);
        }

        // POST: Localidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodLocalidad,ProvinciaId,IdiomaId,Localidad,x,y,Exacto,DireccionesFacturacion,Idiomas,Locales,Provincias,Personas")] Localidades localidades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(localidades).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a Localidades record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.IdiomaId = new SelectList(db.Idiomas, "Id", "Codigo", localidades.IdiomaId);
            ViewBag.ProvinciaId = new SelectList(db.Provincias, "Id", "Provincia", localidades.ProvinciaId);
            DisplayErrorMessage();
            return PartialView(localidades);
        }

        // GET: Localidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidades localidades = db.Localidades.Find(id);
            if (localidades == null)
            {
                return HttpNotFound();
            }
            return PartialView(localidades);
        }

        ////////////////////////////////////////////////////////////////////////////////

        // POST: Localidades/Clone/5
        [HttpPost, ActionName("CloneList")]
        [ValidateAntiForgeryToken]
        public ActionResult CloneConfirmed(List<int> ids)
        {
            try
            {
                foreach (int id in ids)
                {
                    Localidades localidad = db.Localidades.AsNoTracking().FirstOrDefault(e => e.Id == id);
                    if (localidad == null)
                    {
                        DisplayErrorMessage("No se ha podido clonar.");
                        return HttpNotFound();
                    }
                    for (var num = 1; num < 1000; ++num)
                    {
                        var strNombre = Regex.Replace(localidad.Nombre, @" \(\d+\)$", "") + " (" + num + ")";
                        var nFound = db.Localidades.Where(p => p.Nombre == strNombre);
                        if (nFound.Count() == 0)
                        {
                            localidad.Nombre = strNombre;
                            break;
                        }
                    }
                    db.Localidades.Add(localidad);
                }
                db.SaveChanges();
                DisplaySuccessMessage("Se han borrado registros de Localidades");
            }
            catch (Exception e)
            {
                String msg = "";
                Exception ex = e;
                while (null != (ex = ex.InnerException))
                {
                    msg += "<li>" + ex.Message + "</li>\n";
                }
                DisplayErrorMessage(msg);
            }
            return JsonRedirectToAction("Index");
        }

        [HttpPost, ActionName("DeleteList")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(List<int> ids)
        {
            foreach (int id in ids)
            {
                Localidades localidad = db.Localidades.Find(id);
                if (localidad == null)
                {
                    DisplayErrorMessage("No se ha podido borrar.");
                    return HttpNotFound();
                }
                db.Localidades.Remove(localidad);
            }
            db.SaveChanges();
            DisplaySuccessMessage("Se han borrado registros de Localidades");
            return JsonRedirectToAction("Index");
        }

        // POST: Localidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return DeleteConfirmed(new List<int> { id });
        }

        private void DisplaySuccessMessage(string msgText)
        {
            TempData["SuccessMessage"] = msgText;
        }

        private void DisplayErrorMessage(string msgText = "<li>Error indefinido</li>")
        {
            TempData["ErrorMessage"] = msgText;
        }

        public IQueryable<object> GetResultObject(string search, string sortOrder, int start, int length, List<string> columnFilters, out int countTotal)
        {
            var res = FilterResult(search, columnFilters);
            countTotal = res.Count();
            var dict = res.Select(s => new { // Include(i => i.Provincias).
                Id = s.Id,
                Pais = s.Provincias.Paises.Nombre,
                Provincia = s.Provincias.Nombre,
                Localidad = s.Nombre,
                CodLocalidad = s.CodLocalidad,
                Idioma = s.Idiomas.Codigo,
                X = s.X,
                Y = s.Y,
                Exacto = s.Exacto
            });
            return dict.SortBy(sortOrder).Skip(start).Take(length);
        }

        private IQueryable<Localidades> FilterResult(string search, List<string> columnFilters)
        {
            var columnFilters_0 = columnFilters[0];
            var columnFilters_1 = columnFilters[1];
            var columnFilters_2 = columnFilters[2];
            var columnFilters_3 = columnFilters[3];
            var columnFilters_4 = columnFilters[4];
            IQueryable<Localidades> results = db.Localidades.
                //Include(p => p.Provincias).
                //Include(p => p.Provincias.Paises).
                Where(p => (search == null || (
               p.Nombre.StartsWith(search)
            || p.Provincias.Nombre.StartsWith(search)
            || p.Provincias.Paises.Nombre.StartsWith(search)))
                && (columnFilters_1 == null || (p.Provincias.Paises.Nombre != null && p.Provincias.Paises.Nombre.Contains(columnFilters_1)))
                && (columnFilters_2 == null || (p.Provincias.Nombre != null && p.Provincias.Nombre.Contains(columnFilters_2)))
                && (columnFilters_3 == null || (p.Nombre != null && p.Nombre.Contains(columnFilters_3)))
                && (columnFilters_4 == null || (p.Idiomas.Codigo != null && p.Idiomas.Codigo.Contains(columnFilters_4)))
                );
            return results;
        }

        public JsonResult DataHandler(DTParameters param)
        {
            try
            {
                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                int countTotal = 0;
                var data = GetResultObject(
                    param.Search.Value,
                    param.SortOrder,
                    param.Start,
                    param.Length,
                    columnSearch,
                    out countTotal);
                var dataList = data.ToList();
                //int count = Permisos.Count(dtsource, param.Search.Value, columnSearch);
                //var json = GetJsonResult(data);
                var result = new DTResult<object>
                {
                    draw = param.Draw,
                    data = dataList,
                    recordsFiltered = countTotal, // data.Count(),
                    recordsTotal = countTotal
                };
                var json = Json(result, JsonRequestBehavior.AllowGet);

                return json;
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        //// POST: Localidades/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Localidades localidades = db.Localidades.Find(id);
        //    db.Localidades.Remove(localidades);
        //    db.SaveChanges();
        //    DisplaySuccessMessage("Has delete a Localidades record");
        //    return JsonRedirectToAction("Index");
        //}

        //private void DisplaySuccessMessage(string msgText)
        //{
        //    TempData["SuccessMessage"] = msgText;
        //}

        //private void DisplayErrorMessage()
        //{
        //    TempData["ErrorMessage"] = "Save changes was unsuccessful.";
        //}

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
