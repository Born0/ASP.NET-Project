using PcHut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PcHut.Repository
{
    public class VendorRepository: Repository<vendor>
    {
        private pchutEntities2 context1 = new pchutEntities2();
        /*public VendorMaxBrandViewModel MaxBrandOfVendor()
        {
            pchutEntities1 context1 = new pchutEntities1();
            var vendor = context1.Database.SqlQuery<VendorMaxBrandViewModel>("select top 1 count(vendor_id) as NumberOfBrand, vendor_id from brand group by vendor_id order by count(vendor_id) desc").ToList();

            int id;
            string amount = null;

            foreach (VendorMaxBrandViewModel i in vendor)
            {
                id = i.Vendor_id;
                amount = i.NumberOfBrand.ToString();
            }

            VendorMaxBrandViewModel vendor1 = new VendorMaxBrandViewModel();
            vendor1.Vendor_id = id;
            vendor1.NumberOfBrand = (int)amount;

            return i;
        }*/

        public List<Top3VendorViewModel> Top3Vendors()
        {
            var list1 = context1.Database.SqlQuery<Top3VendorViewModel>("select top 3 count(vendor_id) as NumberOfBrand, vendor_id from brand group by vendor_id order by count(vendor_id) desc").ToList();
            return list1;
        }
    }
}