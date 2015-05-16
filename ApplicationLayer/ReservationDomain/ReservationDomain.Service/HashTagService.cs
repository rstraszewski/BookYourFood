using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using ReservationDomain.Model;

namespace Reservaton.Service
{
    public class HashTagService : IHashTagService
    {
        private readonly ByfDbContext byfDbContext;

        public HashTagService(ByfDbContext byfDbContext)
        {
            this.byfDbContext = byfDbContext;
        }

        public List<HashTag> GetHashTags()
        {
            return byfDbContext.HashTags.ToList();
        }

        public List<HashTag> GetHashTags(long mealId)
        {
            return byfDbContext.Meals.Find(mealId).HashTags.ToList();
        }
    }
}
