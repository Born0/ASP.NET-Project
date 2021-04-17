using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PcHut.Models;
using System.Data.Entity.Infrastructure;

namespace PcHut.Repository
{
    public class UserRepository : Repository<user>
    {
        private pchutEntities2 context = new pchutEntities2();
        public DbSqlQuery<user> GetGreaterThanTwo()
        {
            var list = context.users.SqlQuery(@"Select * from [user]");
            return list;
        }

        public DbSqlQuery<user> BoughtByBuyers()
        {
            var list1 = context.users.SqlQuery(@"  select distinct [user].*
					from [user], invoice
					where [user].user_id = invoice.user_id");

            return list1;
        }

        public user GetByPhone( string phone)
        {
            user newUser = this.context.users.Where(x => x.phone == phone).FirstOrDefault();

            return newUser;

        }
        public DbSqlQuery<user> UserDetails(int id)
        {
            var userDetails = context.users.SqlQuery(@"Select * from [user] where user_id=" + id);
            return userDetails;
        }
    }
}