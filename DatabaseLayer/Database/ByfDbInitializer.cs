using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using ReservationDomain.Model;

namespace Database
{
    public class ByfDbInitializer : DropCreateDatabaseIfModelChanges<ByfDbContext>
    {
        protected override void Seed(ByfDbContext context)
        {
            context.Roles.Add(new IdentityRole("User"));
            context.Roles.Add(new IdentityRole("Administrator"));

            context.HashTags.Add(new HashTag {Name = "salty"});
            context.HashTags.Add(new HashTag {Name = "spicy"});
            context.HashTags.Add(new HashTag {Name = "hot"});


            context.SaveChanges();
            base.Seed(context);
        }
    }
}