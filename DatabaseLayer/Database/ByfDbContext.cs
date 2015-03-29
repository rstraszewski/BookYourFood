using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Identity.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using ReservationDomain.Model;

namespace Database
{
    public class ByfDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<HashTag> HashTags { get; set; }
        public DbSet<Table> Tables { get; set; }

        public ByfDbContext()
            : base(Environment.MachineName)
        {
            System.Data.Entity.Database.SetInitializer(new ByfDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Meal>()
                .HasMany(entity => entity.HashTags)
                .WithMany();

            modelBuilder.Entity<Meal>()
                .HasMany(entity => entity.Ingredients)
                .WithMany();

            modelBuilder.Entity<Drink>()
                .HasMany(entity => entity.HashTags)
                .WithMany();

            modelBuilder.Entity<Ingredient>()
                .HasMany(entity => entity.HashTags)
                .WithMany();

        }
    }
}
