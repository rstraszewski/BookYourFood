using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Common.Service;
using Database;
using ReservationDomain.Model;

namespace Reservaton.Service
{
    public class MealService : ApplicationService, IMealService
    {
        public MealService(ByfDbContext byfDbContext)
            : base(byfDbContext)
        {
        }

        public int GetNumberOfMeals()
        {
            return ByfDbContext.Meals.Count();
        }

        public List<Meal> GetMeals()
        {
            return ByfDbContext.Meals.ToList();
        }

        public List<Meal> GetMeals(List<long> mealIds)
        {
            var result = ByfDbContext.Meals.Where(meal => mealIds.Contains(meal.Id)).ToList();
            return result;
        }

        public List<Ingredient> GetIngredients()
        {
            return ByfDbContext.Ingredients.ToList();
        }

        public List<Ingredient> GetIngredients(long mealId)
        {
            return ByfDbContext.Meals.Find(mealId).Ingredients.ToList();
        }
    }

    public interface IMealService
    {
        int GetNumberOfMeals();
        List<Meal> GetMeals();

        List<Meal> GetMeals(List<long> mealIds);
        List<Ingredient> GetIngredients();
        List<Ingredient> GetIngredients(long mealId);
    }

    
}
