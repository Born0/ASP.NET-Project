using PcHut.Models;
using PcHut.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PcHut.Controllers
{
    public class BrandController : Controller
    {
        private BrandRepository brandRepository = new BrandRepository();
        private VendorRepository vendorRepository = new VendorRepository();
        // GET: Brand
        public ActionResult Index()
        {
            return View(brandRepository.GetAll());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewData["vendors"] = vendorRepository.GetAll();
            return View();
        }
        [HttpPost]
        public ActionResult Create(brand brand)
        {
            
            brandRepository.Insert(brand);
            return RedirectToAction("Index");
        }

    }
}