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
            
                ProductRepository product1 = new ProductRepository();
                var allProducts = product1.GetAll();
                return View(allProducts);
            
            
        }

        public ActionResult Filter(FormCollection collection)
        {
            List<product> products = new List<product>();
            int min, max;
            if (collection["minimum"] == ""  && collection["maximum"] == "")
            {
                min = 0;
                max = int.MaxValue;
                products = product1.PriceFilter(min, max);
                return View(products);
            }
            else if (collection["minimum"] == "" || collection["minimum"] == null  || collection["maximum"] == "" || collection["maximum"] == null)
            {
                if (collection["minimum"] == "" || collection["minimum"] == null)
                {
                    min = 0;
                    max = int.Parse(collection["maximum"]);
                    products = product1.PriceFilter(min, max);
                    return View(products);
                }
                else /*if(collection["maximum"] == "" || collection["maximum"] == null)*/
                {
                    min = int.Parse(collection["minimum"]);
                    max = int.MaxValue;
                    products = product1.PriceFilter(min, max);
                    return View(products);
                }
               /* else
                {
                    min = 0;
                    max = int.MaxValue;
                    products = product1.PriceFilter(min, max);
                    return View(products);
                }*/

            }
            else
            {
                min =int.Parse( collection["minimum"]);
                max = int.Parse(collection["maximum"]);
                products = product1.PriceFilter(min, max);
                return View(products);
            }
            
            
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
        public ActionResult Create(ImageViewModel product)
        {
            if (ModelState.IsValid) //If the form validation is done properly, then it will be true and will create a product
            {
                try
                {
                    string filePath = Server.MapPath("~/Image/");
                    if(product.ProductPic!=null)
                    {
                        string fileName = Path.GetFileName(product.ProductPic.FileName);
                        string fullFilePath = Path.Combine(filePath, fileName);
                        product.ProductPic.SaveAs(fullFilePath);
                        product.image = "~/Image/" + product.ProductPic.FileName;
                    }
                    else
                    {
                        ViewBag.imgError = "Upload image";
                    }

     
                }
                catch (Exception ex) { }
                
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
            //if the form validation is not done properly then it will show the validation message and will not create product
            CategoryRepository categoryList = new CategoryRepository();
            ViewData["categories"] = categoryList.GetAll();



            BrandRepository brandList = new BrandRepository();
            ViewData["brands"] = brandList.GetAll();
            return View();
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
                pr.brand = p.brand;
                pr.category = p.category;
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
            if (ModelState.IsValid) //If the form validation is done properly, then it will be true and will create a product
            {
                //Exception is handled. Because at the time of edit if the user
                //does not select any new image then the prevoius image path will be sent
                //therefoere only the old image will be staying
                //If user modify the image by giving a new one the no exception will be thrown.
                try
                {
                    string filePath = Server.MapPath("~/Image/");
                    if (product.ProductPic != null)
                    {
                        string fileName = Path.GetFileName(product.ProductPic.FileName);
                        string fullFilePath = Path.Combine(filePath, fileName);
                        product.ProductPic.SaveAs(fullFilePath);
                        product.image = "~/Image/" + product.ProductPic.FileName;
                    }
                    else
                    {
                        ViewBag.imgError = "Upload image";
                    }
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
            //if the form validation is not done properly then it will show the validation message and will not create product
            /*CategoryRepository categoryList = new CategoryRepository();
            ViewData["categories"] = categoryList.GetAll();

 

            BrandRepository brandList = new BrandRepository();
            ViewData["brands"] = brandList.GetAll();*/
            try
            {
                string filePath = Server.MapPath("~/Image/");
                if (product.ProductPic != null)
                {
                    string fileName = Path.GetFileName(product.ProductPic.FileName);
                    string fullFilePath = Path.Combine(filePath, fileName);
                    product.ProductPic.SaveAs(fullFilePath);
                    product.image = "~/Image/" + product.ProductPic.FileName;
                }
                else
                {
                    ViewBag.imgError = "Upload image";
                }
            }
            catch (Exception ex) { }



            ImageViewModel singleProduct1 = new ImageViewModel();
            singleProduct1.product_id = product.product_id;
            singleProduct1.product_name = product.product_name;
            singleProduct1.brand_id = product.brand_id;
            singleProduct1.category_id = product.category_id;
            singleProduct1.price = product.price;
            singleProduct1.image = product.image;
            singleProduct1.specification = product.specification;
            singleProduct1.Special = product.Special;
            singleProduct1.brand = product.brand;
            singleProduct1.category = product.category;
            singleProduct1.warranty = product.warranty;



            //ProductRepository product1 = new ProductRepository();
            singleProduct1.status = product.status;
            //product1.Update(singleProduct);
            CategoryRepository categoryList = new CategoryRepository();
            ViewData["categories"] = categoryList.GetAll();



            BrandRepository brandList = new BrandRepository();
            ViewData["brands"] = brandList.GetAll();
            return View(singleProduct1);
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