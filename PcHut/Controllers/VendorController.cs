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

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(vendorRepository.Get(id));
        }
        [HttpPost,ActionName("Delete") ]
        public ActionResult ConfirmDelete(int id)
        {
            vendorRepository.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(vendorRepository.Get(id));
        }
        [HttpPost]
        public ActionResult Edit(vendor vendor)
        {
            vendorRepository.Update(vendor);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {


            return View(vendorRepository.Get(id));
        }

        public ActionResult VendorWithMaxBrand()
        {
            pchutEntities2 context1 = new pchutEntities2();
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
        }

        public ActionResult Top3VendorChart()
        {
            List<Top3VendorViewModel> vendors= vendorRepository.Top3Vendors();
            List<BarChartModel> top3Vendors = new List<BarChartModel>();
            foreach (var item in vendors)
            {
                Top3VendorViewModel top3 = new Top3VendorViewModel();
                vendor vendor = new vendor();
                vendor.vendor_name = vendorRepository.Get(item.vendor_id).vendor_name;
                top3.NumberOfBrand = item.NumberOfBrand;
                BarChartModel topChartModel = new BarChartModel(vendor.vendor_name, (double)top3.NumberOfBrand);
                top3Vendors.Add(topChartModel);
            }

            ViewBag.DataPoints = Newtonsoft.Json.JsonConvert.SerializeObject(top3Vendors);
            return View();
        }



    }
}
