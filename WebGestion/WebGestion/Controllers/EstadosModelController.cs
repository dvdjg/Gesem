using PerpetuumSoft.Knockout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGestion.Models;

namespace WebGestion.Controllers
{
    public class EstadosModelController : BaseController
    {
        private GesemEntities db = new GesemEntities();

        protected EstadosModel IndexModel()
        {
            var estados = db.Estados.Select(s => new Item() { Id = s.Id, Nombre = s.Nombre, Desc = s.Descripcion, PadreId = s.PadreId } );
            var model = new EstadosModel {
                ItemToAdd = new Item { Id = 0, Nombre = "Nuevo estado", Desc = "Descripción del estado", PadreId = 0 },
                Items = estados.ToList()
            };
            return model;
        }

        public ActionResult IndexJSON()
        {
            var json = Json(IndexModel(), JsonRequestBehavior.AllowGet);
            return json;
        }

        public ActionResult Index()
        {
            InitializeViewBag("Edición de estados");
            return PartialView(); // IndexModel()
        }

        public ActionResult AddItem(EstadosModel model)
        {
            model.AddItem();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}

