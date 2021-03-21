using PcHut.Models;
using PcHut.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PcHut.Controllers
{
    public class AdminController : Controller
    {
        private UserRepository userRepository = new UserRepository();
        // GET: Admin
        public ActionResult Stats()
        {

            return View();
        }

        public ActionResult Management()
        {

            List<user> users = userRepository.GetAll();
            return View(users);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(user nUser, FormCollection collection)
        {
            nUser.registration_date = DateTime.Now;
            userRepository.Insert(nUser);

            credential cred = new credential();
            cred.user_id = userRepository.GetByPhone(nUser.phone).user_id;
            cred.password = nUser.password;
            string type = collection["type"];
            cred.user_type = type;
            CredentialRepository credentialRepository = new CredentialRepository();
            credentialRepository.Insert(cred);

            return RedirectToAction("Management");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            user user = userRepository.Get(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(user cUser)
        {
            userRepository.Update(cUser);
            return RedirectToAction("Management");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            user cUser = userRepository.Get(id);
            return View(cUser);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            userRepository.Delete(id);
            return RedirectToAction("Management");
        }
    }
}