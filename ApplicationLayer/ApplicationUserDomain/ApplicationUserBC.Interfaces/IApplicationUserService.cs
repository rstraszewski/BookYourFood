using System.Collections.Generic;
using ApplicationUserBC.Interfaces.DTOs;
using Utility;

namespace ApplicationUserBC.Service
{
    public interface IApplicationUserService
    {
        OperationResult AddUserAnswers(List<long> answersIds, string userId);
        OperationResult AddFavouriteMeal(long mealId, string userId);
        OperationResult RemoveFavouriteMeal(long mealId, string userId);
        List<long> GetUserAnswers(string userId);
        List<long> GetUserFavouriteMeals(string userId);
        void ChangeNameAndSurname(string userId, string name, string surname);
    }
}