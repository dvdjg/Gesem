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
    public class ProvinciasController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: Provincias/Index
        public ActionResult Index()
        {
            //var provincias = db.Provincias.Include(l => l.Idiomas).Include(l => l.Paises);
            //return PartialView(provincias.ToList());
            return PartialView();
        }

        /*
        // GET: Provincias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provincias provincias = db.Provincias.Find(id);
            if (provincias == null)
            {
                return HttpNotFound();
            }
            return PartialView(provincias);
        }
        */

        // GET: Provincias/Create
        public ActionResult Create()
        {
            ViewBag.IdiomaId = new SelectList(db.Idiomas, "Id", "Codigo");
            ViewBag.PaisId = new SelectList(db.Paises, "Id", "Nombre");
            return PartialView();
        }

        // POST: Provincias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodProvincia,PaisId,IdiomaId,Provincia,x,y,Exacto,Idiomas,Localidades,Paise")] Provincias provincias)
        {
            if (ModelState.IsValid)
            {
                db.Provincias.Add(provincias);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a Provincias record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.IdiomaId = new SelectList(db.Idiomas, "Id", "Codigo", provincias.IdiomaId);
            ViewBag.PaisId = new SelectList(db.Paises, "Id", "Nombre", provincias.PaisId);
            DisplayErrorMessage();
            return PartialView(provincias);
        }

        // GET: Provincias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provincias provincias = db.Provincias.Find(id);
            if (provincias == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdiomaId = new SelectList(db.Idiomas, "Id", "Codigo", provincias.IdiomaId);
            ViewBag.PaisId = new SelectList(db.Paises, "Id", "Nombre", provincias.PaisId);
            return PartialView(provincias);
        }

        // POST: Provincias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodProvincia,PaisId,IdiomaId,Provincia,x,y,Exacto,Idiomas,Localidades,Paise")] Provincias provincias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(provincias).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a Provincias record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.IdiomaId = new SelectList(db.Idiomas, "Id", "Codigo", provincias.IdiomaId);
            ViewBag.PaisId = new SelectList(db.Paises, "Id", "Nombre", provincias.PaisId);
            DisplayErrorMessage();
            return PartialView(provincias);
        }

        // GET: Provincias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provincias provincias = db.Provincias.Find(id);
            if (provincias == null)
            {
                return HttpNotFound();
            }
            return PartialView(provincias);
        }

        ////////////////////////////////////////////////////////////////////////////////

        // POST: Provincias/Clone/5
        [HttpPost, ActionName("CloneList")]
        [ValidateAntiForgeryToken]
        public ActionResult CloneConfirmed(List<int> ids)
        {
            try
            {
                foreach (int id in ids)
                {
                    Provincias localidad = db.Provincias.AsNoTracking().FirstOrDefault(e => e.Id == id);
                    if (localidad == null)
                    {
                        DisplayErrorMessage("No se ha podido clonar.");
                        return HttpNotFound();
                    }
                    for (var num = 1; num < 1000; ++num)
                    {
                        var strNombre = Regex.Replace(localidad.Nombre, @" \(\d+\)$", "") + " (" + num + ")";
                        var nFound = db.Provincias.Where(p => p.Nombre == strNombre);
                        if (nFound.Count() == 0)
                        {
                            localidad.Nombre = strNombre;
                            break;
                        }
                    }
                    db.Provincias.Add(localidad);
                }
                db.SaveChanges();
                DisplaySuccessMessage("Se han borrado registros de Provincias");
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
                Provincias localidad = db.Provincias.Find(id);
                if (localidad == null)
                {
                    DisplayErrorMessage("No se ha podido borrar.");
                    return HttpNotFound();
                }
                db.Provincias.Remove(localidad);
            }
            db.SaveChanges();
            DisplaySuccessMessage("Se han borrado registros de Provincias");
            return JsonRedirectToAction("Index");
        }

        // POST: Provincias/Delete/5
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
            var dict = res.Select(s => new { // Include(i => i.Paises).
                Id = s.Id,
                Pais = s.Paises.Nombre,
                Provincia = s.Nombre,
                CodProvincia = s.CodProvincia,
                Idioma = s.Idiomas.Codigo,
                X = s.X,
                Y = s.Y,
                Exacto = s.Exacto
            });
            return dict.SortBy(sortOrder).Skip(start).Take(length);
        }

        private IQueryable<Provincias> FilterResult(string search, List<string> columnFilters)
        {
            var columnFilters_0 = columnFilters[0];
            var columnFilters_1 = columnFilters[1];
            var columnFilters_2 = columnFilters[2];
            var columnFilters_3 = columnFilters[3];
            var columnFilters_4 = columnFilters[4];
            IQueryable<Provincias> results = db.Provincias.
                //Include(p => p.Paises).
                //Include(p => p.Paises.Paises).
                Where(p => (search == null || (
               p.Nombre.StartsWith(search)
            || p.Paises.Nombre.StartsWith(search)))
                && (columnFilters_1 == null || (p.Paises.Nombre != null && p.Paises.Nombre.Contains(columnFilters_1)))
                && (columnFilters_2 == null || (p.Nombre != null && p.Nombre.Contains(columnFilters_2)))
                && (columnFilters_3 == null || (p.Idiomas.Codigo != null && p.Idiomas.Codigo.Contains(columnFilters_3)))
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
        //// POST: Provincias/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Provincias provincias = db.Provincias.Find(id);
        //    db.Provincias.Remove(provincias);
        //    db.SaveChanges();
        //    DisplaySuccessMessage("Has delete a Provincias record");
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
