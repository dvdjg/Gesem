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
    public class PaisesController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: Paises/Index
        public ActionResult Index()
        {
            //var paises = db.Paises.Include(p => p.Idiomas);
            return PartialView();
        }

        /*
        // GET: Paises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paises paises = db.Paises.Find(id);
            if (paises == null)
            {
                return HttpNotFound();
            }
            return PartialView(paises);
        }
        */

        // GET: Paises/Create
        public ActionResult Create()
        {
            ViewBag.IdiomaId = new SelectList(db.Idiomas, "Id", "Codigo");
            return PartialView();
        }

        // POST: Paises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodPais,IdiomaId,Nombre,X,Y,Idiomas,Provincias")] Paises paises)
        {
            if (ModelState.IsValid)
            {
                db.Paises.Add(paises);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a Paises record");
                return JsonRedirectToAction("Index");
            }

            ViewBag.IdiomaId = new SelectList(db.Idiomas, "Id", "Codigo", paises.IdiomaId);
            DisplayErrorMessage();
            return PartialView(paises);
        }

        // GET: Paises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paises paises = db.Paises.Find(id);
            if (paises == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdiomaId = new SelectList(db.Idiomas, "Id", "Codigo", paises.IdiomaId);
            return PartialView(paises);
        }

        // POST: Paises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodPais,IdiomaId,Nombre,X,Y,Idiomas,Provincias")] Paises paises)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paises).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a Paises record");
                return JsonRedirectToAction("Index");
            }
            ViewBag.IdiomaId = new SelectList(db.Idiomas, "Id", "Codigo", paises.IdiomaId);
            DisplayErrorMessage();
            return PartialView(paises);
        }

        // GET: Paises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paises paises = db.Paises.Find(id);
            if (paises == null)
            {
                return HttpNotFound();
            }
            return PartialView(paises);
        }
        [HttpPost, ActionName("CloneList")]
        [ValidateAntiForgeryToken]
        public ActionResult CloneConfirmed(List<int> ids)
        {
            try
            {
                foreach (int id in ids)
                {
                    Paises paises = db.Paises.AsNoTracking().FirstOrDefault(e => e.Id == id);
                    if (paises == null)
                    {
                        DisplayErrorMessage("No se ha podido clonar.");
                        return HttpNotFound();
                    }
                    for (var num = 1; num < 1000; ++num)
                    {
                        var strNombre = Regex.Replace(paises.Nombre, @" \(\d+\)$", "") + " (" + num + ")";
                        var nFound = db.Paises.Where(p => p.Nombre == strNombre);
                        if (nFound.Count() == 0)
                        {
                            paises.Nombre = strNombre;
                            break;
                        }
                    }

                    db.Paises.Add(paises);
                }
                db.SaveChanges();
                DisplaySuccessMessage("Se han borrado registros de Paises");
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
                Paises record = db.Paises.Find(id);
                if (record == null)
                {
                    DisplayErrorMessage("No se ha podido borrar.");
                    return HttpNotFound();
                }
                db.Paises.Remove(record);
            }
            db.SaveChanges();
            DisplaySuccessMessage("Se han borrado registros");
            return JsonRedirectToAction("Index");
        }
        // POST: Paises/Delete/5
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

        //////////////////////////////////////////////////////////////////////////////////////////////////

        public IQueryable<object> GetResultObject(string search, string sortOrder, int start, int length, List<string> columnFilters, out int countTotal)
        {
            //var res = GetResult(search, sortOrder, start, length, columnFilters, out countTotal);
            var res = db.Paises.Where(p => search == null || (
               p.Nombre != null && p.Nombre.ToLower().Contains(search.ToLower()))
               );
            countTotal = res.Count();
            var dict = res.Select(s => new {
                Id = s.Id,
                Pais = s.Nombre,
                IdiomaId = s.IdiomaId,
                Idioma = s.Idiomas.Codigo,
                X = s.X,
                Y = s.Y,
                CodPais = s.CodPais });
            return dict.SortBy(sortOrder).Skip(start).Take(length);
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
                //int count = Paises.Count(dtsource, param.Search.Value, columnSearch);
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
    }
}
