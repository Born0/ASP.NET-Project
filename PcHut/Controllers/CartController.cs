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
        private pchutEntities2 context=new pchutEntities2();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Remove(int id)
        {
            int index = AlreadyAdded(id);
            List<Item> cart = (List<Item>)Session["Cart"];
            cart.RemoveAt(index);

            Session["Cart"] = cart;
            return View("Cart");

        }
        public ActionResult AddToCart(int id)
        {
            if (Session["Cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(context.products.Find(id), 1));
                Session["Cart"] = cart;
            }
            else 
            {
                List<Item> cart = (List<Item>)Session["Cart"];
                int index = AlreadyAdded(id);
                if (index == -1)
                    cart.Add(new Item(context.products.Find(id), 1));
                else
                    cart[index].Quantity++;
                Session["Cart"] = cart;

            }
            return View("Cart");
        }
        private int AlreadyAdded(int id)
        {
            List<Item> cart = (List<Item>)Session["Cart"];
            for (int i = 0; i < cart.Count; i++)
           
                if (cart[i].Products.product_id == id)
                
                       return i;

            return -1;   
              

            }

        public ActionResult Checkout()
        {
            return View();
        }


        }
        


    }
