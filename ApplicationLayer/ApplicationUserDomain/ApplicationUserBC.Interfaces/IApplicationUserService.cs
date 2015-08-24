using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationUserBC.Interfaces.DTOs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Utility;

namespace ApplicationUserBC.Interfaces
{
    public interface IApplicationUserService
    {
        OperationResult AddUserAnswers(List<long> answersIds, string userId);
        OperationResult AddFavouriteMeal(long mealId, string userId);
        OperationResult RemoveFavouriteMeal(long mealId, string userId);
        List<long> GetUserAnswers(string userId);
        List<long> GetUserFavouriteMeals(string userId);
        void ChangeNameAndSurname(string userId, string name, string surname);
        Task<IdentityResult> CreateUserAccountAndSignIn(UserDto user, string password);
        void LogOff();
        Task<SignInStatus> PasswordSignInAsync(string email, string password, bool rememberMe, bool shouldLockout);
        UserDto GetUserById(string getUserId);
    }
}