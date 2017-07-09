using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGestion.Models;
using MvcTables;
using System.Data.Entity;
using WebGestion.App_Start;
using System.Web.UI.WebControls;

namespace WebGestion.Controllers
{
    public class GesemController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        // GET: Gesem
        public ActionResult Index()
        {
            return PartialView();
        }

        //public class ItemData
        //{
        //    public string Bien { get; set; }
        //    public decimal Precio { get; set; }
        //}
        
        //public class DataTableData
        //{
        //    public int draw { get; set; }
        //    public int recordsTotal { get; set; }
        //    public int recordsFiltered { get; set; }
        //    public List<Bienes> data { get; set; }
        //}

        //public ActionResult Provincias()
        //{
        //    return PartialView();
        //}

        //public ActionResult ListProvincias(TableRequestModel request)
        //{
        //    var provincias = db.Provincias.Include(p => p.Idiomas);
        //    var table = TableResult.From(provincias).Build<ProvinciasTable>(request);
        //    //table.Overrides.HideColumn(order => order.ShipRegion);

        //    return table;
        //}

        //private List<Bienes> FilterData(ref int recordFiltered, int start, int length, string search, int sortColumn, string sortDirection)
        //{
        //    var articulos = db.Bienes; // .Include(a => a.IVAs).Include(a => a.Familias).Include(a => a.Estados);
        //    return articulos.ToList();
        //}

        // this ajax function is called by the client for each draw of the information on the page (i.e. when paging, ordering, searching, etc.). 
        //public ActionResult AjaxGetJsonData(int draw, int start, int length)
        //{
        //    string search = Request.QueryString["search[value]"];
        //    int sortColumn = -1;
        //    string sortDirection = "asc";
        //    if (length == -1)
        //    {
        //        length = 9999;
        //    }

        //    // note: we only sort one column at a time
        //    if (Request.QueryString["order[0][column]"] != null)
        //    {
        //        sortColumn = int.Parse(Request.QueryString["order[0][column]"]);
        //    }
        //    if (Request.QueryString["order[0][dir]"] != null)
        //    {
        //        sortDirection = Request.QueryString["order[0][dir]"];
        //    }

        //    var data = db.Bienes; //from a in db.Bienes select new ItemData { Bien = a.Bien, Precio= a.Precio };
        //    DataTableData dataTableData = new DataTableData();
        //    dataTableData.draw = draw;
        //    dataTableData.recordsTotal = data.Count();
        //    int recordsFiltered = 0;
        //    bool lazy = db.Configuration.LazyLoadingEnabled = false;
        //    bool proxy = db.Configuration.ProxyCreationEnabled = false;
        //    dataTableData.data = data.ToList();
        //    //dataTableData.data = FilterData(ref recordsFiltered, start, length, search, sortColumn, sortDirection);
        //    dataTableData.recordsFiltered = recordsFiltered;

        //    var json = Json(dataTableData, JsonRequestBehavior.AllowGet);
        //    db.Configuration.LazyLoadingEnabled = lazy;
        //    db.Configuration.ProxyCreationEnabled = proxy;
        //    return json;
        //}

        //public JsonResult GetJsonResult(IQueryable<Permisos> q)
        //{
        //    var res = q.Select(e => new PermisosMetadata() { Id = e.Id, Permiso = e.Permiso, Descripcion = e.Descripcion, FechaAlta = e.FechaAlta, FechaBaja = e.FechaBaja });
        //    var json = Json(res, JsonRequestBehavior.AllowGet);
        //    return json;
        //}

        //public static DbSet<T> querySet<T>(DbContext db = null) where T : class
        //{
        //    GesemEntities dbg = (GesemEntities)db ?? new GesemEntities();
        //    if (dbg == null)
        //    {
        //        throw new Exception("Invalid DbContext for GesemEntities");
        //    }

        //    return dbg.Set<T>();
        //}

        //////////////////////////////////////////////////////////////////////////////////////////////////////

        

        //private IQueryable<object> GetResultObject<T>(DbSet<T> dbSet, string value, string sortOrder, int start, int length, List<string> columnSearch, out int countTotal) where T : class
        //{
        //    throw new NotImplementedException();
        //}
    }
}