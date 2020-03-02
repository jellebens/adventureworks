using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Identity
{
    public class AdventureWorksUser : IdentityUser
    {
        public AdventureWorksUser()
        {
        }

        public AdventureWorksUser(string userName) : base(userName)
        {
        }
    }
}
