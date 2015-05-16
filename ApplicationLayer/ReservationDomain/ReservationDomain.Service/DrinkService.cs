using System.Collections.Generic;
using System.Linq;
using Common.Service;
using Database;
using ReservationDomain.Model;

namespace Reservaton.Service
{
    public class DrinkService : ApplicationService, IDrinkService
    {
        public DrinkService(ByfDbContext byfDbContext)
            : base(byfDbContext)
        {
        }

        public int GetNumberOfDrinks()
        {
            return ByfDbContext.Drinks.Count();
        }

        public List<Drink> GetDrinks()
        {
            return ByfDbContext.Drinks.ToList();
        }

        public List<Drink> GetDrinks(List<long> drinkIds)
        {
            var result = ByfDbContext.Drinks.Where(drink => drinkIds.Contains(drink.Id)).ToList();
            return result;
        }

    }
}