using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Common.Service;
using ReservationDomain.Infrastructure;
using ReservationDomain.Model;

namespace Reservaton.Service
{
    public class MealService : ApplicationService<ReservationDomainDbContext>, IMealService
    {
        public MealService(ReservationDomainDbContext context)
            : base(context)
        {
        }

        public int GetNumberOfMeals(bool withoutUserMeals = true)
        {
            return withoutUserMeals 
                ? _context.Meals.Count(m => m.CreatedByUser == false) 
                : _context.Meals.Count();
        }

        public List<Meal> GetMeals(bool withoutUserMeals = true)
        {
            return withoutUserMeals
                ? _context.Meals.Where(m => m.CreatedByUser == false).ToList()
                : _context.Meals.ToList();
        }

        public List<Meal> GetMeals(List<long> mealIds)
        {
            var result = _context.Meals.Where(meal => mealIds.Contains(meal.Id)).ToList();
            return result;
        }

        public List<Ingredient> GetIngredients()
        {
            return _context.Ingredients.ToList();
        }

        public List<Ingredient> GetIngredients(long mealId)
        {
            return _context.Meals.Find(mealId).Ingredients.ToList();
        }

        public decimal GetPriceForIngredients(List<long> ingredientIds)
        {
            return _context.Ingredients.Where(i => ingredientIds.Contains(i.Id)).Sum(i => i.Price)*1.5m;
        }

        public void CreateMeal(Meal meal)
        {
            _context.Meals.Add(meal);
            _context.SaveChanges();
        }

        public void UpdateMeal(Meal meal)
        {
            var mealEntity = _context.Meals.Find(meal.Id);
            mealEntity.Description = meal.Description;
            mealEntity.Name = meal.Name;
            mealEntity.Price = meal.Price;
            _context.SaveChanges();
        }

        public void RemoveMeal(Meal meal)
        {
            var mealEntity = _context.Meals.Find(meal.Id);
            _context.Meals.Remove(mealEntity);
            _context.SaveChanges();
        }

        public void SetIngredientsForMeal(long mealId, List<long> ingIds)
        {
            //TODO: Why exception? Twice the same foreign key?
            var mealEntity = _context.Meals.Find(mealId);
            var ingredients = _context.Ingredients.Where(i => ingIds.Contains(i.Id)).ToList();
            mealEntity.Ingredients.Clear();
            mealEntity.Ingredients = ingredients;
            _context.SaveChanges();
        }

        public void SetHashTagsForMeal(long mealId, List<long> ingIds)
        {
            var mealEntity = _context.Meals.Find(mealId);
            var hashTags = _context.HashTags.Where(i => ingIds.Contains(i.Id)).ToList();
            mealEntity.HashTags.Clear();
            mealEntity.HashTags = hashTags;
            _context.SaveChanges();
        }

        public List<string> GetIngredientsNames(List<long> ingredients)
        {
            return _context.Ingredients.Where(i => ingredients.Contains(i.Id)).Select(i => i.Name).ToList();
        }

        public void AddPhotoToMeal(long mealId, byte[] image)
        {
            var meal = _context.Meals.Find(mealId);
            meal.Image = image;
            _context.SaveChanges();
        }

        public void CreateIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            _context.SaveChanges();
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            var ingredientEntity = _context.Ingredients.Find(ingredient.Id);
            ingredientEntity.Description = ingredient.Description;
            ingredientEntity.Name = ingredient.Name;
            ingredientEntity.Price = ingredient.Price;
            ingredientEntity.IngredientType = ingredient.IngredientType;
            _context.SaveChanges();
        }

        public void RemoveIngredient(Ingredient ingredient)
        {
            var ingredientEntity = _context.Ingredients.Find(ingredient.Id);
            _context.Ingredients.Remove(ingredientEntity);
            _context.SaveChanges();
        }
    }
}
