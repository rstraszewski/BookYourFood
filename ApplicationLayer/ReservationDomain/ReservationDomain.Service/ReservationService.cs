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

        public OperationResult<Reservation> ReserveTable(DateTime reservationTime, int duration, long tableId)
        {
            var table = ByfDbContext.Tables.Find(tableId);
            var reservation = new Reservation(reservationTime, duration, table);
            var result = CanTableBeReserved(reservation);
            if(result.Success)
            {
                ByfDbContext.Reservations.Add(reservation);
                ByfDbContext.SaveChanges();
            }

            return result;
        }

        private OperationResult<Reservation> CanTableBeReserved(Reservation reservation)
        {
            var result = OperationResult<Reservation>.CreateResult(reservation);

            if(reservation.ReservationTime < DateTime.Now)
            {
                result.AddError("Reservation time must be later than now");
            }

            return result;
        }

        public OperationResult ReserveMeal(long reservationId, Meal meal)
        {
            throw new NotImplementedException();
        }

        public OperationResult Finalize(long reservationId, long userId)
        {
            throw new NotImplementedException();
        }

        public OperationResult Finalize(long reservationId, string surname)
        {
            throw new NotImplementedException();
        }
    }
}
