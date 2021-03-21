using PcHut.Models;
using PcHut.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PcHut.Controllers
{
    public class LoginController : Controller
    {
        private CredentialRepository credRepo = new CredentialRepository();
        // GET: Login
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            if(collection["user_id"]==null || collection["password"]==null || collection["user_id"] == "" || collection["password"] == "")
            {
                ViewBag.error = "Give your login info!";
                return View("Index");
            }
           else
            {
                credential temp = new credential();
                temp.user_id = int.Parse(collection["user_id"]);
                temp.password = collection["password"];
                credential cred = credRepo.LoginCheck(temp);
                if (cred == null || cred.user_type == null)
                {
                    
                    ViewBag.error = "Id or password Error!";
                    return View("Index");
                }
                else
                {
                    Session["user_id"] = cred.user_id;
                    Session["user_type"] = cred.user_type;                   
                    return RedirectToAction("Index", "Home");
                }
            }
           
        }

        public ActionResult Logout()
        {
            Session["user_id"] = null;
            Session["user_type"] = null;
            return RedirectToAction("Index", "Home");
        }

    }
}
