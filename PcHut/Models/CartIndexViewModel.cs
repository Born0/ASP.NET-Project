using PcHut.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PcHut.Models
{
    public class CartIndexViewModel
    {
       public CartRepository Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}