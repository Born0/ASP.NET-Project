using PcHut.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
namespace PcHut.Repository
{
    public class ProductRepository : Repository<product>
    {
        public List<product> GetTopProducts(int top)
        {
            return this.context.products.OrderByDescending(x => x.price).Take(top).ToList();
        }

        public DbSqlQuery<product> TopProductSold()
        {
            var list1 = context.products.SqlQuery(@"select product_id, product_name, price
                    from [product]
                    where product_id = (
                    select product_id from [sales_record]
					
                    having count(*) in (
                    select max(count(product_id)) 
					from [sales_record]
                    group by product_id)
                    group by product_id
                    )");

            return list1;
        }
<<<<<<< HEAD

        /*public List<product> SearchProduct(string productName)
        {
            *//*return this.context.products.OrderByDescending(x => x.price).Take(top).ToList();*//*
            var list = context.products(x => )
        }*/
=======
        public List<product> Search(string name)
        {
            // return this.context.products.Where(x => x.product_name == name).ToList();
            return this.context.products.Where(x => x.product_name.Contains(name)).ToList();
        }
        public List<product> SearchByType(string type)
        {
            List<product> products = new List<product>();
            List<description> descriptions= this.context.descriptions.Where(x => x.extra == type).ToList();
            foreach(var item in descriptions)
            {
                int id = item.product_id;
                products.Add(this.context.products.Where(x => x.product_id == id).FirstOrDefault());
            }
            return products;
        }
>>>>>>> eb9aeab208d2872259718bef966490f81c79917f
    }
}