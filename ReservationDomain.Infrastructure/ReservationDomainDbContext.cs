using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationDomain.Model;

namespace ReservationDomain.Infrastructure
{
    public class ReservationDomainDbContext : DbContext
    {
        public ReservationDomainDbContext()
            : base(Environment.MachineName)
        {
            System.Data.Entity.Database.SetInitializer(new ReservationDomainDbInitializer());
        }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<HashTag> HashTags { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ReservationDomain");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<HashTag>();

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

            modelBuilder.Entity<Answer>()
                .HasMany(entity => entity.HashTags)
                .WithMany();
        }
    }
}
