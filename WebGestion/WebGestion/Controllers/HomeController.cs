using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebGestion.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tabs()
        {
            return PartialView();
        }

        public ActionResult Principal()
        {
            return PartialView();
        }

        public ActionResult FlotCharts()
        {
            return PartialView("FlotCharts");
        }

        public ActionResult MorrisCharts()
        {
            return PartialView("MorrisCharts");
        }

        public ActionResult Tables()
        {
            return PartialView("Tables");
        }

        public ActionResult Forms()
        {
            return PartialView("Forms");
        }

        public ActionResult Panels()
        {
            return PartialView("Panels");
        }

        public ActionResult Buttons()
        {
            return PartialView("Buttons");
        }

        public ActionResult Notifications()
        {
            return PartialView("Notifications");
        }

        public ActionResult Typography()
        {
            return PartialView("Typography");
        }

        public ActionResult Icons()
        {
            return PartialView("Icons");
        }

        public ActionResult Grid()
        {
            return PartialView("Grid");
        }

        public ActionResult Blank()
        {
            return PartialView("Blank");
        }

        public ActionResult Login()
        {
            return PartialView("Login");
        }

    }
}