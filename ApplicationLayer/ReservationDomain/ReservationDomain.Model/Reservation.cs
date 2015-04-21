using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace ReservationDomain.Model
{
    public class Reservation: Entity
    {
        public DateTime ReservationTime { get; set; }
        public int Duration { get; set; }
        public Table Table { get; set; }
        public long? UserId { get; set; }
        public string UserSurname { get; set; }
        public bool IsFinalized { get; set; }
        public virtual List<MealForReservation> Meals { get; set; }
        public virtual List<DrinkForReservation> Drinks { get; set; }

        //Needed by EntityFramework
        protected Reservation() { }
        public Reservation(DateTime reservationTime, int duration, Table table)
        {
            ReservationTime = reservationTime;
            Duration = duration;
            Table = table;
        }

        public void AssignMeal(MealForReservation meal)
        {
            Meals.Add(meal);
        }

        public void OrderMeals(IEnumerable<MealForReservation> meals)
        {
            Meals.AddRange(meals);
        }

        public void AssignPerson(string surname)
        {
            UserSurname = surname;
        }

        public void FinalizeReservation()
        {
            IsFinalized = true;
        }

        public decimal CalculateCost(bool withTip)
        {
            var mealCost = Meals.Select(elem => elem.Meal.Price*(decimal)elem.NumberOfMeals).Sum();
            var drinkCost = Drinks.Select(elem => elem.Drink.Price*(decimal)elem.NumberOfDrinks).Sum();
            
            if (withTip)
            {
                return (mealCost + drinkCost)*1.12m;
            }

            return mealCost + drinkCost;
        }
    }
}
