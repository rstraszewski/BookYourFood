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

        public List<CompleteMeal> GetPreferredMealsFor(List<long> userPreferences)
        {
            var preferredMeals = new List<CompleteMeal>();
            var ratedMeals = new List<RatedMeals>();

            // Get list of meals
            var meals =  ByfDbContext.Meals.ToList();
            foreach(var m in meals)
            {
                var mealHashTags = m.HashTags.ToList();
                long score = 0;
                foreach(var u in userPreferences)
                {
                    if (mealHashTags.FirstOrDefault(x => x.Id == u) != null)
                    {
                        score++;
                    }
                }
                ratedMeals.Add(new RatedMeals() { MealId = m.Id, Score = score });
            }

            ratedMeals.Sort( (y,x) => x.Score.CompareTo(y.Score) );

            foreach(var rm in ratedMeals)
            {
                var meal = ByfDbContext.Meals.SingleOrDefault( m => m.Id == rm.MealId);
                if(meal == null)
                {
                    break;
                }
                preferredMeals.Add(new CompleteMeal() { Meal = meal });
            }

            return preferredMeals;
        }
    }

    public interface IAutoCreatorService
    {
        List<CompleteMeal> GetPreferredMealsFor(List<long> userPreferences);
    }
}
