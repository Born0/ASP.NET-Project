using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PcHut.Models;
using PcHut.Repository;

namespace PcHut.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        public ActionResult Index()
        {
            return View();
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
    }
}