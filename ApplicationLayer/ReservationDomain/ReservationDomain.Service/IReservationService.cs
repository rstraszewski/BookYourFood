using System;
using System.Collections.Generic;
using ReservationDomain.Model;
using Utility;

namespace Reservaton.Service
{
    public interface IReservationService
    {
        OperationResult<Reservation> ReserveTable(DateTime reservationTime, int duration, long tableId);
        OperationResult<Reservation> ReserveMeal(long reservationId, List<long> mealId);
        OperationResult Finalize(long reservationId);
        OperationResult Finalize(long reservationId, string surname);



    }
}