using System.Collections.Generic;
using ReservationDomain.Model;

namespace BookYourFood.Models
{
    public class MenuViewModel
    {
        public List<Meal> Meals { get; set; }
        public List<Drink> Drinks { get; set; }
    }
}