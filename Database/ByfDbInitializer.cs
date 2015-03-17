using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Database
{
    public class ByfDbInitializer : DropCreateDatabaseIfModelChanges<ByfDbContext>
    {
        protected override void Seed(ByfDbContext context)
        {
            context.Roles.Add(new IdentityRole("User"));
            context.Roles.Add(new IdentityRole("Administrator"));

            context.SaveChanges();
            base.Seed(context);
        }
    }
}