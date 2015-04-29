﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            var result = CanReservationBeCreated(reservation);
            if (result.IsSuccessful)
            {
                reservation.Table.Reserve();
                ByfDbContext.Reservations.Add(reservation);
                ByfDbContext.SaveChanges();
            }

            return result;
        }

        public OperationResult<Reservation> ReserveMeal(long reservationId, List<MealForReservation> reservedMeals)
        {
            var reservation = ByfDbContext.Reservations.Find(reservationId);
            reservation.OrderMeals(reservedMeals);

            ByfDbContext.SaveChanges();
            return OperationResult<Reservation>.CreateResult(reservation);
        }

        public OperationResult<Reservation> ReserveDrink(long reservationId, List<DrinkForReservation> reservedDrinks)
        {
            var reservation = ByfDbContext.Reservations.Find(reservationId);
            reservation.OrderDrinks(reservedDrinks);

            ByfDbContext.SaveChanges();
            return OperationResult<Reservation>.CreateResult(reservation);
        }

        public OperationResult Finalize(long reservationId)
        {
            var reservation = ByfDbContext.Reservations.Find(reservationId);
            reservation.FinalizeReservation();

            ByfDbContext.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult Finalize(long reservationId, string surname)
        {
            var reservation = ByfDbContext.Reservations.Find(reservationId);
            reservation.AssignPerson(surname);
            reservation.FinalizeReservation();

            ByfDbContext.SaveChanges();
            return OperationResult.Success();
        }

        private OperationResult<Reservation> CanReservationBeCreated(Reservation reservation)
        {
            var result = OperationResult<Reservation>.CreateResult(reservation);

            if (reservation.ReservationTime < DateTime.Now)
            {
                result.AddError("Reservation time must be later than now");
            }
            if (reservation.Table == null)
            {
                result.AddError("Table was not found");
            }
            else if (reservation.Table.IsReserved)
            {
                result.AddError("Table is already reserved");
            }

            return result;
        }
    }
}
