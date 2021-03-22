using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PcHut.Models
{
    public class Item
    {
        public product Products { get; set; }
        public int Quantity { get; set; }

        public double Total { get; set; }

        public Item(product product, int quantity)
        {
            this.Products = product;
            this.Quantity = quantity;
        }


    }
}
