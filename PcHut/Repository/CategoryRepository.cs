using PcHut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PcHut.Repository
{
    public class CategoryRepository : Repository<category>
    {
        pchutEntities2 context = new pchutEntities2();

        public List<product> GetProducts(int id)
        {
            return null;
        }

        public List<DefaultViewModel> NumberOfProductsInCategory()
        {
            var list = context.Database.SqlQuery<DefaultViewModel>("select category_name as Name, count(product.category_id) as DefaultCount from category, product where category.category_id = product.category_id and category.category_id in ( select category_id from product group by category_id) group by category.category_name");

            List<DefaultViewModel> info = new List<DefaultViewModel>();

            foreach (DefaultViewModel data in list)
            {
                /*info.Name = data.Name;
                info.DefaultCount = data.DefaultCount;*/
                info.Add(data);
            }

            return info;
        }
    }
}