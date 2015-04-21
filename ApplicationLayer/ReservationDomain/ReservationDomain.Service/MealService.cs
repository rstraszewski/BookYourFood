using System.Collections.Generic;
using System.Linq;
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
    }

    public interface IMealService
    {
        int GetNumberOfMeals();
        List<Meal> GetMeals();
    }
}
