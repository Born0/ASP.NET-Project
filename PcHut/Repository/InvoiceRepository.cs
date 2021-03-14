using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using PcHut.Models;
using PcHut.Repository;

namespace PcHut.Repository
{
    public class InvoiceRepository : Repository<product>
    {
        public IQueryable<TopCustomerViewModel> TopCustomer()
        {
            var list = from invoices in context.invoices
                       group invoices by new
                       {
                           invoices.user_id
                       } into g
                       orderby
                         g.Sum(p => p.total_ammount) descending
                       select new
                       {
                           g.Key.user_id,
                           Column1 = (double?)g.Sum(p => p.total_ammount)
                       };
            return (IQueryable<TopCustomerViewModel>)list;
        }
    }
}