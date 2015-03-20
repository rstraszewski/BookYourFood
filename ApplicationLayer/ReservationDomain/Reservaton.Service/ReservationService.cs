using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Service;
using Database;
using ReservationDomain.Model;
using Utility;

namespace Reservaton.Service
{
    public class ReservationService : ApplicationService, IReservationService
    {
        public ReservationService(ByfDbContext byfDbContext)
            : base(byfDbContext)
        {
        }

        public OperationResult ReserveTableForNow()
        {
              
            var reservation = new Reservation(DateTime.Now);
            ByfDbContext.Reservations.Add(reservation);
            ByfDbContext.SaveChanges();
            return OperationResult.SuccessResult();
        }
    }
}
