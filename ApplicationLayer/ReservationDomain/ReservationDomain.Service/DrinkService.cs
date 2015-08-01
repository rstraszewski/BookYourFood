using System.Collections.Generic;
using System.Linq;
using Common.Service;
using ReservationDomain.Infrastructure;
using ReservationDomain.Model;

namespace Reservaton.Service
{
    public class DrinkService : ApplicationService<ReservationDomainDbContext>, IDrinkService
    {
        public DrinkService(ReservationDomainDbContext context)
            : base(context)
        {
        }

        public int GetNumberOfDrinks()
        {
            return _context.Drinks.Count();
        }

        public List<Drink> GetDrinks()
        {
            return _context.Drinks.ToList();
        }

        public List<Drink> GetDrinks(List<long> drinkIds)
        {
            var result = _context.Drinks.Where(drink => drinkIds.Contains(drink.Id)).ToList();
            return result;
        }

        public void CreateDrink(Drink drink)
        {
            _context.Drinks.Add(drink);
            _context.SaveChanges();
        }

        public void UpdateDrink(Drink drink)
        {
            var drinkEntity = _context.Drinks.Find(drink.Id);
            drinkEntity.Name = drink.Name;
            drinkEntity.Price = drink.Price;
            drinkEntity.DrinkType = drink.DrinkType;
            _context.SaveChanges();
        }

        public void RemoveDrink(Drink drink)
        {
            var drinkEntity = _context.Drinks.Find(drink.Id);
            _context.Drinks.Remove(drinkEntity);
            _context.SaveChanges();
        }

        public void SetHashTagsForDrink(long drinkId, List<long> tagsIds)
        {
            var drinkEntity = _context.Drinks.Find(drinkId);
            var hashTags = _context.HashTags.Where(i => tagsIds.Contains(i.Id)).ToList();
            drinkEntity.HashTags.Clear();
            drinkEntity.HashTags = hashTags;
            _context.SaveChanges();
        }
    }
}