using System;
using System.Collections.Generic;
using BookYourFood.Controllers;

namespace BookYourFood.Models
{
    public class ReservationSummaryViewModel
    {
        public long Id { get; set; }
        public DateTime ReservationTime { get; set; }
        public int Duration { get; set; }
        public TableViewModel Table { get; set; }
        public List<MealForSummary> Meals { get; set; }
        public List<DrinkForSummary> Drinks { get; set; }
        public decimal Price { get; set; }
        public string PhoneNumber { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
    }
}