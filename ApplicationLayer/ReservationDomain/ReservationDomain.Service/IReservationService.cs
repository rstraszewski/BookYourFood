using System;
using ReservationDomain.Model;
using Utility;

namespace Reservaton.Service
{
    public interface IReservationService
    {
        OperationResult<long> ReserveTable(DateTime reservationTime, int duration, Table table);
        OperationResult ReserveMeal(long reservationId, Meal meal);
        OperationResult Finalize(long reservationId, long userId);
        OperationResult Finalize(long reservationId, string surname);



    }
}