using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace ReservationDomain.Model
{
    public class Reservation : Entity
    {
        public DateTime ReservationDate
        {
            get { return ReservationTime.Date; }
        }

        public DateTime ReservationTime { get; set; }
        public int Duration { get; set; }
        public virtual Table Table { get; set; }
        public string UserId { get; set; }
        public string UserSurname { get; set; }
        public bool IsFinalized { get; set; }
        public virtual List<MealForReservation> Meals { get; set; }
        public virtual List<DrinkForReservation> Drinks { get; set; }
        public string UserPhoneNumber { get; set; }

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
            var mealCost = 0m;
            var drinkCost = 0m;
            if (Meals != null)
            {
                mealCost = Meals.Select(elem => elem.Meal.Price * (decimal)elem.NumberOfMeals).Sum();
            }
            if (Meals != null)
            {
                drinkCost = Drinks.Select(elem => elem.Drink.Price * (decimal)elem.NumberOfDrinks).Sum();
            }

            if (withTip)
            {
                return (mealCost + drinkCost) * 1.12m;
            }

            return mealCost + drinkCost;
        }

        public void OrderDrinks(List<DrinkForReservation> drinks)
        {
            Drinks.AddRange(drinks);
        }
    }
}
