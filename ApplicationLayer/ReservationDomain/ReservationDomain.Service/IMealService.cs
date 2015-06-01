using System.Collections.Generic;
using ReservationDomain.Model;

namespace Reservaton.Service
{
    public interface IMealService
    {
        int GetNumberOfMeals(bool withoutUserMeals = true);
        List<Meal> GetMeals(bool withoutUserMeals = true);
        List<Meal> GetMeals(List<long> mealIds);
        List<Ingredient> GetIngredients();
        List<Ingredient> GetIngredients(long mealId);
        decimal GetPriceForIngredients(List<long> ingredientIds);
        void CreateMeal(Meal meal);
        void UpdateMeal(Meal meal);
        void RemoveMeal(Meal meal);
        void SetIngredientsForMeal(long mealId, List<long> ingIds);
        void SetHashTagsForMeal(long mealId, List<long> ingIds);
        List<string> GetIngredientsNames(List<long> ingredients);
        void AddPhotoToMeal(long mealId, byte[] image);
        void CreateIngredient(Ingredient ingredient);
        void UpdateIngredient(Ingredient ingredient);
        void RemoveIngredient(Ingredient ingredient);
    }
}