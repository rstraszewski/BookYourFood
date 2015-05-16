using System.Collections.Generic;
using ReservationDomain.Model;

namespace ReservationDomain.Service
{
    public interface IAutoCreatorService
    {
        List<RatedMeal> GetPreferredMealsFor(List<long> hashTags);
        List<RatedDrink> GetPreferredDrinksFor(List<long> hashTags);
    }
}