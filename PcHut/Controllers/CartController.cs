using PcHut.Models;
using PcHut.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PcHut.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private ProductRepository repository;



        public ViewResult Index(CartRepository cart, string returnUrl)
        {
            cart = null;
            if (HttpContext.Session != null)
            {
               /// cart = (CartRepository)HttpContext.Session["Cart"]; change

            }
            if (cart == null)
            {
                cart = new CartRepository();
              //  HttpContext.Session["Cart"] = cart; change
            }

            return View();



        }


        [HttpPost]
        public ActionResult AddToCart(CartRepository cart, int productId)
        {
            cart = null;
            if (HttpContext.Session != null)
            {
                cart = (CartRepository)HttpContext.Session["Cart"];

            }
            if (cart == null)
            {
                cart = new CartRepository();
                HttpContext.Session["Cart"] = cart;
            }
            var product = repository.Get(productId);
            if (product != null)
            {
                cart.AddItem(product, 1);

            }
            return View("List");

        }
        public RedirectToRouteResult RemoveFromCart(CartRepository cart, int productId, string returnUrl)
        {
            cart = null;
            if (HttpContext.Session != null)
            {
                cart = (CartRepository)HttpContext.Session["Cart"];

            }
            if (cart == null)
            {
                cart = new CartRepository();
                HttpContext.Session["Cart"] = cart;
            }
            var product = repository.Get(productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public PartialViewResult Summary(CartRepository cart)
        {
            cart = null;
            if (HttpContext.Session != null)
            {
                cart = (CartRepository)HttpContext.Session["Cart"];

            }
            if (cart == null)
            {
                cart = new CartRepository();
                HttpContext.Session["Cart"] = cart;
            }
            return PartialView(cart);
        }
    }
}