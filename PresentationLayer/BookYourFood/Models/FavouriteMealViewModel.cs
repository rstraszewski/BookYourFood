using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReservationDomain.Model;

namespace BookYourFood.Models
{
    public class FavouriteMealViewModel
    {
        public List<MealViewModel> FavouriteMeals { get; set; }
    }
}