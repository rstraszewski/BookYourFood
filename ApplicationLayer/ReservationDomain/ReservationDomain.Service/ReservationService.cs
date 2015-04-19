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
            if(result.IsSuccessful)
            {
                reservation.Table.Reserve();
                ByfDbContext.Reservations.Add(reservation);
                ByfDbContext.SaveChanges();
            }

            return result;
        }

        private OperationResult<Reservation> CanReservationBeCreated(Reservation reservation)
        {
            var result = OperationResult<Reservation>.CreateResult(reservation);

            if(reservation.ReservationTime < DateTime.Now)
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

        public OperationResult<Reservation> ReserveMeal(long reservationId, List<long> mealId)
        {
            var meals = (from meal in ByfDbContext.Meals where mealId.Contains(meal.Id) select meal).ToList();
//            ByfDbContext.Meals.Where(meal => mealId.Contains(meal.Id)).ToList();
            var reservation = ByfDbContext.Reservations.Find(reservationId);
            foreach (var meal in meals)
            {
                reservation.AssignMeal(meal);
                ByfDbContext.SaveChanges();
            }
            return OperationResult<Reservation>.CreateResult(reservation);
        }

        public OperationResult Finalize(long reservationId)
        {
            throw new NotImplementedException();
        }

        public OperationResult Finalize(long reservationId, string surname)
        {
            throw new NotImplementedException();
        }
    }
}
