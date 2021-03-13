using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PcHut.Repository;
using PcHut.Models;
using System.IO;

namespace PcHut.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            ProductRepository products = new ProductRepository();
            var allProducts = products.GetAll();
            return View(allProducts);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CategoryRepository categoryList = new CategoryRepository();
            ViewData["categories"] = categoryList.GetAll();

            BrandRepository brandList = new BrandRepository();
            ViewData["brands"] = brandList.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult Create(product product)
        {
            /*string filePath = Server.MapPath("~/Image/");
            string fileName = Path.GetFileName(product.image);

            string fullFilePath = Path.Combine(filePath, fileName);
            product.image.SaveAs(fullFilePath);
            product.image = "~/Image/" + product.image.FileName;*/

            ProductRepository addProduct = new ProductRepository();
            product.status = 1;
            addProduct.Insert(product);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetTopSold()
        {
            ProductRepository products = new ProductRepository();
            var allUsers = products.TopProductSold();
            return View(allUsers);
        }

        /*[HttpGet]
        public ActionResult ProductBoughtByBuyers()
        {
            ProductRepository buyers = new ProductRepository();
            var allBuyers = buyers.BoughtByBuyers();
            return View(allBuyers);
        }*/
        [HttpPost]
        public ActionResult SearchProduct(FormCollection collection )
        {
            string name = collection["search"];
            ProductRepository productRepository = new ProductRepository();
           List<product> products= productRepository.Search(name);
            return View(products);
        }
    }
}