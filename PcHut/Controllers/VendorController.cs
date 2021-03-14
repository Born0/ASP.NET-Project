using PcHut.Models;
using PcHut.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PcHut.Controllers
{
    public class VendorController : Controller
    {
        private VendorRepository vendorRepository = new VendorRepository();
        // GET: Vendor
        public ActionResult Index()
        { 
            vendorRepository.GetAll();
            return View(vendorRepository.GetAll());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(vendor vendor)
        {
            vendorRepository.Insert(vendor);
            return RedirectToAction("Index");
        }
    }
}