using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Identity.Model;
namespace Database
{
    public class ByfDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ReservationDomain.Model.Reservation> Reservations { get; set; }

        public ByfDbContext()
            : base(System.Environment.MachineName)
        {
        }
    }
}
