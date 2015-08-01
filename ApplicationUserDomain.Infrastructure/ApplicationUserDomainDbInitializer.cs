using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ApplicationUserDomain.Infrastructure
{
    public class ApplicationUserDomainDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationUserDomainDbContext>
    {
        protected override void Seed(ApplicationUserDomainDbContext context)
        {
            context.Roles.Add(new IdentityRole("User"));
            context.Roles.Add(new IdentityRole("Administrator"));
            context.Roles.Add(new IdentityRole("Restaurant"));
        }
    }
}
