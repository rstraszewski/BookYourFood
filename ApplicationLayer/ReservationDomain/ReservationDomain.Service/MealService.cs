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

        public void CreateMeal(Meal meal)
        {
            ByfDbContext.Meals.Add(meal);
            ByfDbContext.SaveChanges();
        }

        public void UpdateMeal(Meal meal)
        {
            var mealEntity = ByfDbContext.Meals.Find(meal.Id);
            mealEntity.Description = meal.Description;
            mealEntity.Name = meal.Name;
            mealEntity.Price = meal.Price;
            ByfDbContext.SaveChanges();
        }

        public void RemoveMeal(Meal meal)
        {
            var mealEntity = ByfDbContext.Meals.Find(meal.Id);
            ByfDbContext.Meals.Remove(mealEntity);
            ByfDbContext.SaveChanges();
        }

        public void SetIngredientsForMeal(long mealId, List<long> ingIds)
        {
            //TODO: Why exception? Twice the same foreign key?
            var mealEntity = ByfDbContext.Meals.Find(mealId);
            var ingredients = ByfDbContext.Ingredients.Where(i => ingIds.Contains(i.Id)).ToList();
            mealEntity.Ingredients.Clear();
            mealEntity.Ingredients = ingredients;
            ByfDbContext.SaveChanges();
        }

        public void SetHashTagsForMeal(long mealId, List<long> ingIds)
        {
            var mealEntity = ByfDbContext.Meals.Find(mealId);
            var hashTags = ByfDbContext.HashTags.Where(i => ingIds.Contains(i.Id)).ToList();
            mealEntity.HashTags.Clear();
            mealEntity.HashTags = hashTags;
            ByfDbContext.SaveChanges();
        }
    }

    public interface IMealService
    {
        int GetNumberOfMeals();
        List<Meal> GetMeals();
        List<Meal> GetMeals(List<long> mealIds);
        List<Ingredient> GetIngredients();
        List<Ingredient> GetIngredients(long mealId);
        void CreateMeal(Meal meal);
        void UpdateMeal(Meal meal);
        void RemoveMeal(Meal meal);
        void SetIngredientsForMeal(long mealId, List<long> ingIds);
        void SetHashTagsForMeal(long mealId, List<long> ingIds);
    }

    
}
