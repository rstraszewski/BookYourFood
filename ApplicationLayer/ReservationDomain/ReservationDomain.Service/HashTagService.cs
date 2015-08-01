using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationDomain.Infrastructure;
using ReservationDomain.Model;

namespace Reservaton.Service
{
    public class HashTagService : IHashTagService
    {
        private readonly ReservationDomainDbContext _context;

        public HashTagService(ReservationDomainDbContext context)
        {
            this._context = context;
        }

        public List<HashTag> GetHashTags()
        {
            return _context.HashTags.ToList();
        }

        public List<HashTag> GetHashTags(long mealId)
        {
            return _context.Meals.Find(mealId).HashTags.ToList();
        }

        public List<HashTag> GetHashTagsForDrink(long drinkId)
        {
            return _context.Drinks.Find(drinkId).HashTags.ToList();
        }
    }
}
