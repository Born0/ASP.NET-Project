using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PcHut.Repository;
using PcHut.Models;
using System.Data.Entity.Infrastructure;



namespace PcHut.Controllers
{
    public class UserController : Controller
    {
        protected pchutEntities2 context1 = new pchutEntities2();

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

        [HttpGet]
        public ActionResult TopCustomerDetails()
        {

            var list1 = context1.Database.SqlQuery<TopCustomerViewModel>("select top 1 user_id, sum(total_ammount) as Column1 from invoice group by user_id order by sum(total_ammount) desc").ToList();

            int? id = null;
            string amount = null;
            foreach (TopCustomerViewModel i in list1)
            {
                id = i.User_Id;
                amount = i.Column1.ToString();
            }
            //int id = i
            ViewData["totalAmount"] = amount;

            UserRepository user = new UserRepository();
            var userInfo = user.Get((int)id);

            return View(userInfo);
        }

        public ActionResult TopSellerReferenceDetails()
        {
            pchutEntities2 context1 = new pchutEntities2();
            var list1 = context1.Database.SqlQuery<TopSellerRef>("select top 1 sum(total_ammount) as ToatalSumAmount, seller_refference from invoice group by seller_refference order by sum(total_ammount) desc").ToList();

            int? id = null;
            string amount = null;
            foreach (TopSellerRef i in list1)
            {
                id = i.Seller_Refference;
                amount = i.ToatalSumAmount.ToString();
            }

            ViewData["sumAmount"] = amount;

            UserRepository sellerReference = new UserRepository();
            var seller = sellerReference.Get((int)id);

            return View(seller);
        }

        [HttpGet]
        public ActionResult Registration()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Registration(FormCollection collection)
        {
            user newUser = new user();
            newUser.user_name = collection["userName"];
            newUser.email = collection["userEmail"];
            newUser.phone = collection["userPhone"];
            newUser.password = collection["userPassword"];
            newUser.registration_date = DateTime.Now;

            UserRepository uRepo = new UserRepository();
            uRepo.Insert(newUser);
            int id= uRepo.GetByPhone(newUser.phone).user_id;


            credential nCred = new credential();
            nCred.user_id = id;
            nCred.password = newUser.password;
            nCred.user_type = "3";

            CredentialRepository credentialRepository = new CredentialRepository();
            credentialRepository.Insert(nCred);

            return RedirectToAction("Index", "Product");
        }
    }
}