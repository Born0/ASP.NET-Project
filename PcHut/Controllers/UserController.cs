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

        public ActionResult Account()
        {
            UserRepository users = new UserRepository();
            if (Session["user_id"] != null)
            {
                int id = int.Parse(Session["user_id"].ToString());
                return View(users.Get(id));
            }
            else
                return Content("Please Login");
            
        }
        [HttpGet]
        public ActionResult Edit()
        {
            UserRepository users = new UserRepository();
            if (Session["user_id"] != null)
            {
                int id = int.Parse(Session["user_id"].ToString());
                return View(users.Get(id));
            }
            else
                return View();
           
        }
        [HttpPost]
        public ActionResult Edit(user user)
        {
            if(ModelState.IsValid)
            {
                UserRepository users1 = new UserRepository();
                users1.Update(user);
                return View("Account", user);
            }
            else
            {
                return View("Edit",user);
            }

           
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



            ViewData["viewUserName"] = newUser.user_name;
            ViewData["viewUserEmail"] = newUser.email;
            ViewData["viewUserPhone"] = newUser.phone;
            ViewData["viewUserPassword"] = newUser.password;



            //Validation for the registration form
            if (newUser.user_name == null || newUser.user_name == "")
            {
                ViewData["errUserName"] = "Please Give a User Name";
                return View();
            }



            if (collection["userName"].Any(c => char.IsDigit(c)))
            {
                ViewData["errUserName"] = "Name Cannot Contain any Digit";
                return View();
            }



            if (newUser.email == null || newUser.email == "")
            {
                ViewData["errUserEmail"] = "Please Give a User Email";
                return View();
            }



            if (newUser.password == null || newUser.password == "")
            {
                ViewData["errUserPassword"] = "Please Give a User Password";
                return View();
            }

            if (collection["userPassword"].Length < 5)
            {
                ViewData["errUserPassword"] = "Password Must be at least 5 characters long";
                return View();
            }



            if (newUser.phone == null || newUser.phone == "")
            {
                ViewData["errUserPhone"] = "Please Give a Phone Number";
                return View();
            }



            if (collection["userPhone"].Any(c => char.IsLetter(c)))
            {
                ViewData["errUserPhone"] = "Phone Cannot Contain any Character";
                return View();
            }



            newUser.registration_date = DateTime.Now;



            UserRepository uRepo = new UserRepository();
            uRepo.Insert(newUser);
            int id = uRepo.GetByPhone(newUser.phone).user_id;




            credential nCred = new credential();
            nCred.user_id = id;
            nCred.password = newUser.password;
            nCred.user_type = "3";



            CredentialRepository credentialRepository = new CredentialRepository();
            credentialRepository.Insert(nCred);

            return View("UserId", nCred);
            //return Content("your Id is: " + id);

            //return RedirectToAction("Index", "Product");
        }
        [HttpGet]
        public ActionResult TopThreeCustomerGraph()
        {
            pchutEntities2 context1 = new pchutEntities2();
            var list1 = context1.Database.SqlQuery<TopCustomerViewModel>("select top 3 user_id, sum(total_ammount) as Column1 from invoice group by user_id order by sum(total_ammount) desc").ToList();

            List<BarChartModel> topThreeCustomers = new List<BarChartModel>();

            foreach (TopCustomerViewModel topThree in list1)
            {
                TopCustomerViewModel tcvm = new TopCustomerViewModel();
                UserRepository user = new UserRepository();
                var userDetails = user.Get(topThree.User_Id);

                user userInfo = new user();
                userInfo.user_name = userDetails.user_name;

                //tcvm.User_Id = topThree.User_Id;
                tcvm.Column1 = topThree.Column1;

                BarChartModel topChartModel = new BarChartModel(userInfo.user_name, (double)tcvm.Column1);
                topThreeCustomers.Add(topChartModel);
            }

            ViewBag.DataPoints = Newtonsoft.Json.JsonConvert.SerializeObject(topThreeCustomers);
            return View();
        }
    }
}