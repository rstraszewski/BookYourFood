﻿using System;
using System.Collections.Generic;
using ReservationDomain.Model;
using Utility;

namespace Reservaton.Service
{
    public interface IReservationService
    {
        OperationResult<Reservation> ReserveTable(DateTime reservationTime, int duration, long tableId);
        OperationResult<Reservation> ReserveMeal(long reservationId, List<MealForReservation> reservedMeals);
        OperationResult<Reservation> ReserveDrink(long reservationId, List<DrinkForReservation> reservedDrinks);
        OperationResult Finalize(long reservationId);
        OperationResult Finalize(long reservationId, string surname);
        List<Table> GetTables();
        List<Table> GetAvailableTables(DateTime? dateTimeFrom, DateTime? dateTimeTo);
        Reservation GetReservation(long id);
    }
}