using System.Collections.Generic;
using System.Linq;
using Common.Service;
using Database;
using ReservationDomain.Model;

namespace ReservationDomain.Service
{
    public class AutoCreatorService : ApplicationService, IAutoCreatorService
    {
        public AutoCreatorService(ByfDbContext byfDbContext)
            : base(byfDbContext)
        {
        }

        public List<RatedDrink> GetPreferredDrinksFor(List<long> hashTags)
        {
            var ratedDrinks = ByfDbContext.Drinks
                .Select(drink => new RatedDrink
                {
                    Drink = drink,
                    Score = hashTags.Count(u => drink.HashTags.Any(hashTag => hashTag.Id == u))
                })
                .OrderByDescending(ratedDrink => ratedDrink.Score)
                .Take(3)
                .ToList();

            return ratedDrinks;
        }

        public List<RatedMeal> GetPreferredMealsFor(List<long> hashTags)
        {
            var ratedMeals = ByfDbContext.Meals
                .Select(meal => new RatedMeal
                {
                    Meal = meal,
                    Score = hashTags.Count(u => meal.HashTags.Any(hashTag => hashTag.Id == u))
                })
                .OrderByDescending(ratedMeal => ratedMeal.Score)
                .Take(3)
                .ToList();

            return ratedMeals;
        }
    }
}