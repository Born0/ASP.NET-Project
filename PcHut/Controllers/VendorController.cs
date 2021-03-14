<<<<<<< HEAD
﻿using System;
=======
﻿using PcHut.Models;
using PcHut.Repository;
using System;
>>>>>>> 29be6a0f0907bd2ad9d9e101ccb8e8bac18e9f12
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
<<<<<<< HEAD
using PcHut.Models;
using PcHut.Repository;
=======
>>>>>>> 29be6a0f0907bd2ad9d9e101ccb8e8bac18e9f12

namespace PcHut.Controllers
{
    public class VendorController : Controller
    {
<<<<<<< HEAD
        // GET: Vendor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VendorWithMaxBrand()
        {
            pchutEntities1 context1 = new pchutEntities1();
            var list1 = context1.Database.SqlQuery<VendorMaxBrandViewModel>("select top 1 count(vendor_id) as NumberOfBrand, vendor_id from brand group by vendor_id order by count(vendor_id) desc").ToList();

            int? id = null;
            string amount = null;
            foreach (VendorMaxBrandViewModel i in list1)
            {
                id = i.Vendor_id;
                amount = i.NumberOfBrand.ToString();
            }
            //int id = i
            ViewData["totalAmount"] = amount;

            VendorRepository vendor = new VendorRepository();
            var vendorInfo = vendor.Get((int)id);

            return View(vendorInfo);
=======
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
>>>>>>> 29be6a0f0907bd2ad9d9e101ccb8e8bac18e9f12
        }
    }
}