using System.Collections.Generic;
using ReservationDomain.Model;

namespace Reservaton.Service
{
    public interface IHashTagService
    {
        List<HashTag> GetHashTags();
        List<HashTag> GetHashTags(long mealId);
        List<HashTag> GetHashTagsForDrink(long drinkId);
    }
}