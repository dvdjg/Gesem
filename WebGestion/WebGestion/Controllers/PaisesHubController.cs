using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebGestion.Controllers
{
    public class PaisesHubController : BaseController
    {
        // GET: PaisesHub
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}