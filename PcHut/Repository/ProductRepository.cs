using PcHut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PcHut.Repository
{
    public class ProductRepository : Repository<product>
    {
        public List<product> GetTopProducts(int top)
        {
            return this.context.products.OrderByDescending(x => x.price).Take(top).ToList();
        }
    }
}