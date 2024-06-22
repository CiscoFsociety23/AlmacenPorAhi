using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlmacenPorAhiASP.Controllers
{
    public class HomeController : Controller
    {

        public HomeController() { }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Ingress()
        {
            return View();
        }

    }
}
