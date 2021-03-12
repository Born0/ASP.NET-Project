using PcHut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PcHut.Repository
{
    public class CategoryRepository : Repository<category>
    {
        public List<product> GetProducts(int id)
        {
            return null;
        }
    }
}