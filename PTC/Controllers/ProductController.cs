using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PTC.ViewModelLayer;
namespace PTC.Controllers
{
    public class ProductController : Controller
    {
        ProductViewModel pvm = new ProductViewModel();
        // GET: Product
        public ActionResult Index()
        {
           
            pvm.HandleRequest("Get");
            return View(pvm);
        }
        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(ProductViewModel product)
        {
            product.HandleRequest("Add");
            return View();
        }
        public ActionResult EditProduct(int id)
        {
            pvm.productId = id;
            pvm.HandleRequest("Edit");
            return View(pvm);
        }
        [HttpPost]
        public ActionResult EditProduct(ProductViewModel obj)
        {
           obj.HandleRequest("Update");
            return RedirectToAction("Index");
        }

    }
}