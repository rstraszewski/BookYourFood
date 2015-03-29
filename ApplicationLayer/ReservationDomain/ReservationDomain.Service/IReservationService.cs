using System;
using ReservationDomain.Model;
using Utility;

namespace Reservaton.Service
{
    public interface IReservationService
    {
        OperationResult<Reservation> ReserveTable(DateTime reservationTime, int duration, long tableId);
        OperationResult ReserveMeal(long reservationId, Meal meal);
        OperationResult Finalize(long reservationId, long userId);
        OperationResult Finalize(long reservationId, string surname);



    }
}