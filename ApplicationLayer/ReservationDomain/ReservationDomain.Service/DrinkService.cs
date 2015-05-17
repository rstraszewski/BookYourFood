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

        public void CreateDrink(Drink drink)
        {
            ByfDbContext.Drinks.Add(drink);
            ByfDbContext.SaveChanges();
        }

        public void UpdateMeal(Drink drink)
        {
            var drinkEntity = ByfDbContext.Drinks.Find(drink.Id);
            drinkEntity.Name = drink.Name;
            drinkEntity.Price = drink.Price;
            drinkEntity.DrinkType = drink.DrinkType;
            ByfDbContext.SaveChanges();
        }

        public void RemoveMeal(Drink drink)
        {
            var drinkEntity = ByfDbContext.Drinks.Find(drink.Id);
            ByfDbContext.Drinks.Remove(drinkEntity);
            ByfDbContext.SaveChanges();
        }
    }
}