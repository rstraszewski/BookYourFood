using System;
using System.Collections.Generic;
using Common.Model;

namespace ReservationDomain.Model
{
    public class Table : Entity
    {
        public int TableNumber { get; set; }
        public int SeatsNumber { get; set; }
        public virtual List<Reservation> Reservations { get; set; }

        public bool IsFree(DateTime from, DateTime to)
        {
            if (Reservations == null || Reservations.Count == 0)
                return true;

            foreach (var reservation in Reservations)
            {
                var reservationStart = reservation.ReservationTime;
                var reservationEnd = reservation.ReservationTime.AddHours(reservation.Duration);


                //TODO: Think about this logic - it's wrong probably
                if (!(from >= reservationEnd || to <= reservationStart))
                    return false;
            }

            return true;
        }
    }
}