﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace ReservationDomain.Model
{
    public class Reservation: Entity
    {
        public DateTime ReservationTime { get; set; }
        public int Duration { get; set; }
        public Table Table { get; set; }
        public long? UserId { get; set; }
        public string UserSurname { get; set; }
        public bool IsFinalized { get; set; }
        public List<Meal> Meals { get; set; }
        public List<Drink> Drinks { get; set; }

        //Needed by EntityFramework
        protected Reservation() { }
        public Reservation(DateTime reservationTime, int duration, Table table)
        {
            ReservationTime = reservationTime;
            Duration = duration;
            Table = table;
        }

        public void AssignMeal(Meal meal)
        {
            Meals.Add(meal);
        }

        public void FinalizeReservation()
        {
            IsFinalized = true;
        }
    }
}
