using Sw.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sw.Controllers
{
    public class HomeController : Controller
    {
        private SwContext db = new SwContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Products()
        {
            var productList = db.Products.ToList();
            return View(productList);
        }
    }
}