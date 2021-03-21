using PcHut.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace PcHut.Repository
{
    public class CredentialRepository : Repository<credential>
    {

        public credential LoginCheck(credential cred)
        {

            credential temp = this.context.credentials.Where(x => x.user_id == cred.user_id
                                                             && x.password == cred.password).FirstOrDefault();

            if (temp != null)
            {
                return temp;
            }
            else
                return null;
        }


    }
}