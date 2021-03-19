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
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(brandRepository.Get(id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            brandRepository.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewData["vendors"] = vendorRepository.GetAll();
            return View(brandRepository.Get(id));
        }
        [HttpPost]
        public ActionResult Edit(brand brand)
        {

          
            brandRepository.Update(brand);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {

            ViewData["vendors"] = vendorRepository.GetAll();
            return View(brandRepository.Get(id));
        }

    }
}
