using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BookYourFood.Models;
using ReservationDomain.Model;
using Reservaton.Service;
using Utility;

namespace BookYourFood.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMealService mealService;
        private readonly IReservationService reservationService;
        private readonly IDrinkService drinkService;


        public MenuController(IMealService mealService, IReservationService reservationService, IDrinkService drinkService)
        {
            this.mealService = mealService;
            this.reservationService = reservationService;
            this.drinkService = drinkService;
        }

        // GET: Questionaire
        public ActionResult Index()
        {
            var meals = mealService.GetMeals();
            var drinks = drinkService.GetDrinks();

            var result = new ReserveFoodViewModel {Drinks = drinks, Meals = meals};
            return View(result);
        }

        [HttpPost]
        public ActionResult Index(List<MealForReservationViewModel> meals, List<DrinkForReservationViewModel> drinks)
        {
            var mealsEntities = mealService.GetMeals(meals.Where(m => m.Number > 0).Select(m => m.Id).ToList());
            var drinkEntities = drinkService.GetDrinks(drinks.Where(m => m.Number > 0).Select(m => m.Id).ToList());


            if (mealsEntities == null || mealsEntities.Count == 0)
            {
                this.FlashMessage(MessageResult.Create("You didn't choose any meal!", MessageType.Info));
                var result = new ReserveFoodViewModel { Drinks = drinkEntities, Meals = mealsEntities };
                return View(result);
            }

            var mealsToReserve = mealsEntities
                .Select(m => new MealForReservation {Meal = m, NumberOfMeals = meals.First(me => m.Id == me.Id).Number})
                .ToList();

            var drinksToReserve = drinkEntities
                .Select(m => new DrinkForReservation { Drink = m, NumberOfDrinks = drinks.First(me => m.Id == me.Id).Number })
                .ToList();

            var reservationMeal = reservationService.ReserveMeal(1L, mealsToReserve);
            var reservationDrink = reservationService.ReserveDrink(1L, drinksToReserve);

            if (reservationMeal.IsSuccessful && reservationDrink.IsSuccessful)
            {
                this.FlashMessage(MessageResult.Create("Reservation completes successfuly!"));
                return RedirectToAction("Index", "Home");
            }

            this.FlashMessage(MessageResult.Create(reservationMeal.Errors.Last(), MessageType.Error));
            this.FlashMessage(MessageResult.Create(reservationDrink.Errors.Last(), MessageType.Error));

            return View(meals);
        }

        
    }
}