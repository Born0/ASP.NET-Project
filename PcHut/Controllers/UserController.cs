using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PcHut.Repository;
using PcHut.Models;


namespace PcHut.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            UserRepository users = new UserRepository();
            var allUsers = users.GetGreaterThanTwo();
            return View(allUsers);
        }

        [HttpGet]
        public ActionResult ProductBoughtByBuyers()
        {
            UserRepository buyers = new UserRepository();
            var allBuyers = buyers.BoughtByBuyers();
            return View(allBuyers);
        }
    }
}