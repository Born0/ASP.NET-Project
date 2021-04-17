using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PcHut.Repository;
using PcHut.Models;

namespace PcHut.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult CategoryProductAmountChart()
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            var list = categoryRepository.NumberOfProductsInCategory();

            List<BarChartModel> categoriesProductAmount = new List<BarChartModel>();

            foreach (var data in list)
            {
                BarChartModel chart = new BarChartModel(data.Name, (double)data.DefaultCount);
                categoriesProductAmount.Add(chart);
            }

            ViewBag.DataPoints = Newtonsoft.Json.JsonConvert.SerializeObject(categoriesProductAmount);
            return View();
        }
    }
}