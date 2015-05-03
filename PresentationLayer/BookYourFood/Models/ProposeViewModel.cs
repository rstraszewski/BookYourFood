using System.Collections.Generic;
using ReservationDomain.Model;

namespace BookYourFood.Models
{
    public class ProposeViewModel
    {
        public List<RatedMeal> RatedMeals { get; set; }
        public List<RatedDrink> RatedDrinks { get; set; }
    }
}