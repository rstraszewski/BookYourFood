using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Identity.Model
{
    public class ApplicationUser : IdentityUser, IAggregateRoot
    {
        public virtual List<UserAnswer> UserAnswers { get; set; }
        public virtual List<UserMeal> FavouriteMeals { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public void ChangeUserNameNadSurname(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
        public IEnumerable<IDomainEvent> Events { get; } = new List<IDomainEvent>();

        public void AnswerQuestions(List<UserAnswer> answers)
        {
            UserAnswers.RemoveAll(c => true);
            UserAnswers.AddRange(answers);
        }

        public void LikeMeal(long mealId)
        {
            if (CheckIfUserDislikeMeal(mealId))
            {
                FavouriteMeals.Add(new UserMeal(mealId));
            }
        }

        public void UnlikeMeal(long mealId)
        {
            if (CheckIfUserLikeMeal(mealId))
            {
                FavouriteMeals.RemoveAll(meal => meal.MealId == mealId);
            }
        }

        public bool CheckIfUserLikeMeal(long mealId)
        {
            return FavouriteMeals.FirstOrDefault(meal => meal.MealId == mealId) != null;
        }

        public bool CheckIfUserDislikeMeal(long mealId)
        {
            return !CheckIfUserLikeMeal(mealId);
        }
    }
}
