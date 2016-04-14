using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sw.Data;
using Sw.Business;

namespace Sw.Controllers
{
    public class ProductsController : Controller
    {
        private SwContext db = new SwContext();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.Promotion = new SelectList(PromotionBase.GetPromotions(), "PromotionName", "PromotionName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if ( ModelState.IsValid )
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Promotion = new SelectList(PromotionBase.GetPromotions(), "PromotionName", "PromotionName");
            return View(product);
        }

        public ActionResult Details(int? id)
        {
            if ( id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            ViewBag.Promotion = new SelectList(PromotionBase.GetPromotions(), "PromotionName", "PromotionName", product.Promotion);
            if ( product == null )
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpPost]
        public ActionResult Details(FormCollection products)
        {
            var id       = int.Parse(products["Id"]);
            var quantity = int.Parse(products["Quantity"]);

            var product = db.Products.FirstOrDefault(p => p.Id == id);

            var productList = (List<Product>)Session["CartList"]?? new List<Product>();

            for ( int i = 0; i < quantity; i++ )
            {
                productList.Add(product);
            }

            Session["CartList"] = productList;
            Session["Count"] = productList.Count();

            return RedirectToAction("Products", "Home");
        }

        public ActionResult Edit(int? id)
        {
            if ( id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            ViewBag.Promotion = new SelectList(PromotionBase.GetPromotions(), "PromotionName", "PromotionName", product.Promotion);
            if ( product == null )
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if ( ModelState.IsValid )
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Promotion = new SelectList(PromotionBase.GetPromotions(), "PromotionName", "PromotionName", product.Promotion);
            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            if ( id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if ( product == null )
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Checkout()
        {
            var cartList = (List<Product>)Session["CartList"];
            
            var listProduct  = cartList.GroupBy(p => new {p.Id, p.Promotion },(key,group) => new ProductCheckout{Id= key.Id,Promotion = key.Promotion, Count =group.ToList().Count() }).ToList<dynamic>();
            var listCart      = PromotionBase.SetRulesPromotion(listProduct);
            var listCheckout = (from c in listCart
                                group c by new
                              {
                                  c.Id,
                                  c.Promotion,
                                  c.Name
                              } into gb
                              select new ProductCheckout()
                              {
                                  Id        = gb.Key.Id,
                                  Promotion = gb.Key.Promotion,
                                  Name      = gb.Key.Name,
                                  Price     = gb.Sum(p => p.Price),
                                  Count     = gb.Sum(p => p.Count)
                              }).ToList();
            ViewBag.Total = listCheckout.Sum(p => p.Price);
            return View(listCheckout);
        }

        public ActionResult Cart()
        {
            var list = (List<Product>)Session["CartList"];
            List<Product> cartList = new List<Product>();
            if ( list != null )
            {
                cartList = ((List<Product>)Session["CartList"]).OrderBy(p => p.Name).ToList();
                Session["CartList"] = cartList;
            }
            return View(cartList);
        }

        [HttpPost]
        public ActionResult RemoveProduct(int id)
        {
            var cartList = ((List<Product>)Session["CartList"]);
            cartList.RemoveAt(id -1);
            Session["CartList"] = cartList;
            Session["Count"] = cartList.Count();
            return View("Cart",cartList);
        }

        protected override void Dispose(bool disposing)
        {
            if ( disposing )
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
