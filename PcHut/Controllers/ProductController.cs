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

        public ActionResult SpecialCategory(FormCollection collection)
        {
            string type = collection["productType"];
            ProductRepository productRepository = new ProductRepository();
            return View(productRepository.SearchByType(type));
        }
        //////////////
        [HttpGet]
        public ActionResult TopLaptopDetails()
        {
            ProductRepository products = new ProductRepository();
            var laptop = products.TopLaptop();

            product pr = new product();

            foreach (product p in laptop)
            {
                pr.product_id = p.product_id;
                pr.product_name = p.product_name;
                pr.price = p.price;
                pr.warranty = p.warranty;
            }
            return View(pr);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CategoryRepository categoryList = new CategoryRepository();
            ViewData["categories"] = categoryList.GetAll();

            BrandRepository brandList = new BrandRepository();
            ViewData["brands"] = brandList.GetAll();

            ProductRepository product = new ProductRepository();
            product product1 = product.Get(id);

            return View(product1);
        }

        [HttpPost]
        public ActionResult Edit(product product)
        {
            ProductRepository product1 = new ProductRepository();
            product.status = 1;
            product1.Update(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            ProductRepository product = new ProductRepository();
            product product1 = product.Get(id);

            return RedirectToAction("ChangeProductStatus", product1);
        }

        public ActionResult ChangeProductStatus(product product)
        {
            ProductRepository product1 = new ProductRepository();
            product.status = 0;
            product1.Update(product);

            return RedirectToAction("Index");
        }
    }
}