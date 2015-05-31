using System.Collections.Generic;
using BookYourFood.Controllers;
using ReservationDomain.Model;

namespace BookYourFood.Models
{
    public class MenuViewModel
    {
        public List<MealViewModel> Meals { get; set; }
        public List<Drink> Drinks { get; set; }
        public List<MealViewModel> FavouriteUserMeals { get; set; }
    }

    public class CreatorViewModel
    {
        public List<Meal> Meals { get; set; }
        public List<Drink> Drinks { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}