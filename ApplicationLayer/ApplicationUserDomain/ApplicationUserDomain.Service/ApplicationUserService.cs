using System.Collections.Generic;
using System.Linq;
using Common.Service;
using Database;
using Identity.Model;
using Utility;

namespace ApplicationUserDomain.Service
{
    public class ApplicationUserService : ApplicationService, IApplicationUserService
    {
        public ApplicationUserService(ByfDbContext byfDbContext)
            : base(byfDbContext)
        {
        }

        public OperationResult AddUserAnswers(List<long> answersIds, string userId)
        {
            var entity = ByfDbContext.Users.Find(userId);

            if (entity == null)
            {
                return OperationResult.ErrorResult("User not found");
            }

            var answers = answersIds.Select(answer => new UserAnswer {AnswerId = answer}).ToList();
            entity.UserAnswers.RemoveAll(a => true);
            entity.UserAnswers = answers;
            ByfDbContext.SaveChanges();
            ByfDbContext.Database.ExecuteSqlCommand("DELETE FROM UserAnswer WHERE ApplicationUser_Id IS NULL");
            return OperationResult.Success();
        }

        public List<UserAnswer> GetUserAnswers(string userId)
        {
            return ByfDbContext.Users.Find(userId).UserAnswers.ToList();
        }

        public void ChangeNameAndSurname(string userId, string name, string surname)
        {
            var user = ByfDbContext.Users.Find(userId);
            user.Name = name;
            user.Surname = surname;
            ByfDbContext.SaveChanges();
        }


        public List<long> GetUserPreferences(string userId)
        {
            var userAnswerList = this.GetUserAnswers(userId);

            var userAnswerIdList = userAnswerList.Select(answer => answer.AnswerId).ToList();

            return userAnswerIdList;
        }

        public OperationResult AddFavouriteMeal(long mealId, string userId)
        {
            var user = ByfDbContext.Users.Find(userId);
            if(user == null)
            {
                return OperationResult.ErrorResult("User not found.");
            }

            if (user.FavouriteMeals.FirstOrDefault(m => m.MealId == mealId) == null)
            {
                user.FavouriteMeals.Add(new UserMeal { MealId = mealId });
                ByfDbContext.SaveChanges();
            }            
            
            return OperationResult.Success();
        }

        public List<long> GetUserFavouriteMeals(string userId)
        {
            var user = ByfDbContext.Users.Find(userId);

            if(user != null)
            {
                var userFavouriteMeals = user.FavouriteMeals.Select(m => m.MealId).ToList();
                if (userFavouriteMeals != null)
                {
                    return userFavouriteMeals;
                }
            }

            return new List<long>();
        }

        public OperationResult RemoveFavouriteMeal(long mealId, string userId)
        {
            var favouriteMeal = ByfDbContext
                .Users
                .Find(userId)
                .FavouriteMeals.Find(m => m.MealId == mealId);

            ByfDbContext.UserMeals.Remove(favouriteMeal);
            ByfDbContext.SaveChanges();
            return OperationResult.Success();
        }
    }
}