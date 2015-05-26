using System.Collections.Generic;
using Identity.Model;
using Utility;

namespace ApplicationUserDomain.Service
{
    public interface IApplicationUserService
    {
        OperationResult AddUserAnswers(List<long> answersIds, string userId);
        OperationResult AddFavouriteMeal(long mealId, string userId);
        OperationResult RemoveFavouriteMeal(long mealId, string userId);
        List<UserAnswer> GetUserAnswers(string userId);
        List<long> GetUserPreferences(string userId);
        List<long> GetUserFavouriteMeals(string userId);
        void ChangeNameAndSurname(string userId, string name, string surname);
    }
}