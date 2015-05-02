using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationDomain.Model;
using Common.Service;
using Database;
using Utility;

namespace ReservationDomain.Service
{
    public class AutoCreatorService : ApplicationService, IAutoCreatorService
    {
        public AutoCreatorService(ByfDbContext byfDbContext)
            : base(byfDbContext)
        {
        }

        public List<CompleteMeal> GetPreferredMealsFor(List<string> userPreferences)
        {
            var List = new List<CompleteMeal>();
            var ratedMeals = new List<RatedMeals>();

            // Get list of meals
            var meals =  ByfDbContext.Meals.ToList();
            foreach(var m in meals)
            {
                var mealHashTags = m.HashTags.ToList();
                long score = 0;
                foreach(var u in userPreferences)
                {
                    if (mealHashTags.FirstOrDefault(x => x.Name == u) != null)
                    {
                        score++;
                    }
                }
                ratedMeals.Add(new RatedMeals() { MealId = m.Id, Score = score });
            }

            ratedMeals.Sort( (y,x) => x.Score.CompareTo(y.Score) );


            return List;
        }
    }

    public interface IAutoCreatorService
    {
        List<CompleteMeal> GetPreferredMealsFor(List<string> userPreferences);
    }
}
