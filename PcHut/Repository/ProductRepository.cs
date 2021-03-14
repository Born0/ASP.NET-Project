using PcHut.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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

        public DbSqlQuery<product> TopLaptop()
        {
            var product = context.products.SqlQuery(@"select * from product where product_id in (select top 1 product_id from sales_record where product_id in (select product_id from product where category_id = (select category_id from category where category_id = 2)) group by product_id order by sum(quantity) desc)");

            return product;
        }
    }
}