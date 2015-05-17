using System.Collections.Generic;
using ReservationDomain.Model;

namespace Reservaton.Service
{
    public interface IDrinkService
    {
        int GetNumberOfDrinks();
        List<Drink> GetDrinks();
        List<Drink> GetDrinks(List<long> drinkIds);

        void CreateDrink(Drink drink);
        void UpdateMeal(Drink drink);
        void RemoveMeal(Drink drink);
    }
}