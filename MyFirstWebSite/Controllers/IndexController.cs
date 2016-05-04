//using MyFirstWebSite.Authorise;
using MyFirstWebSite.Controllers.DBReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstWebSite.Controllers
{
    public class IndexController : Controller
    {

        public ActionResult PartCv()
        {
            return View();
        }

        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }

        [Authorize]
        public ActionResult CV()
        {
            return View();
        }

        [Authorize]
        public ActionResult Welcome()
        {
            return View();
        }

    }
}