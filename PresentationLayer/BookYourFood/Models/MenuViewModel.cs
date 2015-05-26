using System.Collections.Generic;
using ReservationDomain.Model;

namespace BookYourFood.Models
{
    public class MenuViewModel
    {
        public List<Meal> Meals { get; set; }
        public List<Drink> Drinks { get; set; }
        public List<Meal> FavouriteUserMeals { get; set; }
    }

    public class CreatorViewModel
    {
        public List<Meal> Meals { get; set; }
        public List<Drink> Drinks { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}