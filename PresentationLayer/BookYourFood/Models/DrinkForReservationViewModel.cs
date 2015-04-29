using System.Collections.Generic;
using ReservationDomain.Model;

namespace BookYourFood.Models
{
    public class DrinkForReservationViewModel
    {
        public long Id { get; set; }
        public int Number { get; set; }
    }

    public class ReserveFoodViewModel
    {
        public List<Meal> Meals { get; set; }
        public List<Drink> Drinks { get; set; }
    }
}