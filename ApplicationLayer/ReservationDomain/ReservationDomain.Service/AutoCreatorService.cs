﻿using System.Collections.Generic;
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

        public List<CompleteMeal> GetPreferredMealsFor2(List<long> userPreferences)
        {
            var preferredMeals = new List<CompleteMeal>();
            var ratedMeals = new List<RatedMeal>();

            // Get list of meals
            var meals = ByfDbContext.Meals.ToList();
            foreach (var m in meals)
            {
                var mealHashTags = m.HashTags.ToList();
                var score = 0L;
                foreach (var u in userPreferences)
                {
                    if (mealHashTags.FirstOrDefault(x => x.Id == u) != null)
                    {
                        score++;
                    }
                }
                ratedMeals.Add(new RatedMeal {Meal = m, Score = score});
            }

            ratedMeals.Sort((y, x) => x.Score.CompareTo(y.Score));

            foreach (var rm in ratedMeals)
            {
                var meal = ByfDbContext.Meals.SingleOrDefault(m => m.Id == rm.Meal.Id);
                if (meal == null)
                {
                    break;
                }
                preferredMeals.Add(new CompleteMeal {Meal = meal});
            }

            return preferredMeals;
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

    public interface IAutoCreatorService
    {
        List<RatedMeal> GetPreferredMealsFor(List<long> hashTags);
        List<RatedDrink> GetPreferredDrinksFor(List<long> hashTags);
        List<CompleteMeal> GetPreferredMealsFor2(List<long> userPreferences);
    }
}