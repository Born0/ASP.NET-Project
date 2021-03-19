using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PcHut.Models;

namespace PcHut.Repository
{
    public class CartRepository: Repository<Cart>
    {
        
        private List<Cart> collection = new List<Cart>();

       
        public void AddItem(product product, int quantity)
        {
            var line = collection.Where(p => p.Products.product_id == product.product_id).FirstOrDefault();
            if (line == null)
            {
                collection.Add(
                    new Cart
                    {
                        Products = product,
                        Quantity = quantity
                    }
            );
            }
            else
            {
                line.Quantity += quantity;

            }
        }
        public void RemoveLine(product product)
        {
            collection.RemoveAll(p => p.Products.product_id == product.product_id);
        }
        public decimal ComputeTotalValue()
        { 
            decimal value=(decimal) collection.Sum(p => p.Products.price * p.Quantity);
            return value;
        }
        public IEnumerable Lines
        {
            get { return collection; }
        }
        public void Clear()
        {
            collection.Clear();

        }




    }
}