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
        private ProductRepository product1 = new ProductRepository();
        // GET: Product
        public ActionResult Index()
        {
            List<product> products = TempData["products"] as List<product>;
            if (products != null)
            {
                return View(TempData["products"]);
            }
            else
            {
                ProductRepository product1 = new ProductRepository();
                var allProducts = product1.GetAll();
                return View(allProducts);
            }
            
        }

        /*public ActionResult Index(List<product> products)
        {
            return View(products);
        }*/

       /* public void PriceFilter(FormCollection collection)
        {
            float min = float.Parse(collection["minimum"]);
            ViewBag.min = min;
            float max = float.Parse(collection["maximum"]);
            List<product> products = product1.PriceFilter(min, max);  //--------send list to index
            //Index(products);
            TempData["products"] = products;
            Index();
        }*/

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
        public ActionResult Create(ImageViewModel product)
        {
            try
            {
                string filePath = Server.MapPath("~/Image/");
                string fileName = Path.GetFileName(product.ProductPic.FileName);

                string fullFilePath = Path.Combine(filePath, fileName);
                product.ProductPic.SaveAs(fullFilePath);
                product.image = "~/Image/" + product.ProductPic.FileName;
            }
            catch (Exception ex) { }
            /*try
            {*/
            /*string FileName = Path.GetFileNameWithoutExtension(product.ProductPic.FileName);

            //To Get File Extension  
            string FileExtension = Path.GetExtension(product.ProductPic.FileName);

            //Add Current Date To Attached File Name  
            FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

            //Get Upload path from Web.Config file AppSettings.  
            string UploadPath = ConfigurationManager.AppSettings["ProductImage"].ToString();

            //Its Create complete path to store in server.  
            product.image = UploadPath + FileName;

            //To copy and save file into server.  
            product.ProductPic.SaveAs(product.image);*/
            /*}
            catch (Exception e) { }*/

            product prod = new product();
            prod.product_name = product.product_name;
            prod.brand_id = product.brand_id;
            prod.category_id = product.category_id;
            prod.price = product.price;
            prod.image = product.image;
            prod.specification = product.specification;
            prod.Special = product.Special;
            /*prod.brand = product.brand;
            prod.category = product.category;*/
            prod.warranty = product.warranty;

            ProductRepository addProduct = new ProductRepository();
            prod.status = 1;
            addProduct.Insert(prod);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            ProductRepository repository = new ProductRepository();
            var toDelete = repository.Get(id);
            return View(toDelete);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            ProductRepository repository = new ProductRepository();
            repository.Delete(id);
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

            ImageViewModel singleProduct = new ImageViewModel();

            singleProduct.product_id = product1.product_id;
            singleProduct.product_name = product1.product_name;
            singleProduct.brand_id = product1.brand_id;
            singleProduct.category_id = product1.category_id;
            singleProduct.price = product1.price;
            singleProduct.image = product1.image;
            singleProduct.specification = product1.specification;
            singleProduct.Special = product1.Special;
            singleProduct.status = product1.status;
            /*prod.brand = product.brand;
            prod.category = product.category;*/
            singleProduct.warranty = product1.warranty;

            return View(singleProduct);
        }

        [HttpPost]
        public ActionResult Edit(ImageViewModel product)
        {
            //Exception is handled. Because at the time of edit if the user
            //does not select any new image then the prevoius image path will be sent
            //therefoere only the old image will be staying
            //If user modify the image by giving a new one the no exception will be thrown.
            try
            {
                string filePath = Server.MapPath("~/Image/");
                string fileName = Path.GetFileName(product.ProductPic.FileName);

                string fullFilePath = Path.Combine(filePath, fileName);
                product.ProductPic.SaveAs(fullFilePath);
                product.image = "~/Image/" + product.ProductPic.FileName;
            }
            catch (Exception ex) { }

            product singleProduct = new product();
            singleProduct.product_id = product.product_id;
            singleProduct.product_name = product.product_name;
            singleProduct.brand_id = product.brand_id;
            singleProduct.category_id = product.category_id;
            singleProduct.price = product.price;
            singleProduct.image = product.image;
            singleProduct.specification = product.specification;
            singleProduct.Special = product.Special;
            singleProduct.brand = product.brand;
            singleProduct.category = product.category;
            singleProduct.warranty = product.warranty;

            ProductRepository product1 = new ProductRepository();
            singleProduct.status = product.status;
            product1.Update(singleProduct);
            return RedirectToAction("Index");
        }



        public ActionResult ChangeStatus(int id)
        {
            ProductRepository product1 = new ProductRepository();
            product product = product1.Get(id);
            if (product.status == 1)
            {
                product.status = 0;
                product1.Update(product);
            }
            else
            {
                product.status = 1;
                product1.Update(product);
            }

            return RedirectToAction("Index");
        }

        
    }
}