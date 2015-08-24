using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Infrastructure;
using Identity.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ApplicationUserDomain.Infrastructure
{
    class DbContextConfiguration : DbConfiguration
    {
        public DbContextConfiguration()
        {
           this.SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);
        }
    }

    [DbConfigurationType(typeof(DbContextConfiguration))]
    public class ApplicationUserDomainDbContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        public ApplicationUserDomainDbContext()
            : base(Environment.MachineName)
        {
            System.Data.Entity.Database.SetInitializer(new ApplicationUserDomainDbInitializer());
        }

        public DbSet<UserMeal> UserMeals { get; set; } 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("ApplicationUserDomain");

            base.OnModelCreating(modelBuilder);

        }
    }
}
