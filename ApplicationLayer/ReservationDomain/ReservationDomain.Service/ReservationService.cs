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

        public OperationResult<long> ReserveTable(DateTime reservationTime, int duration, Table table)
        {
            throw new NotImplementedException();
            //TODO: create reservation using data provided by controller (User, Time, 
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
