﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BookYourFood.Models;
using ReservationDomain.Model;
using ReservationDomain.Service;
using Reservaton.Service;
using Utility;

namespace BookYourFood.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMealService mealService;
        private readonly IReservationService reservationService;
        private readonly IDrinkService drinkService;
        private readonly IAutoCreatorService autoCreatorService;


        public MenuController(IMealService mealService, IReservationService reservationService, IDrinkService drinkService, IAutoCreatorService autoCreatorService)
        {
            this.mealService = mealService;
            this.reservationService = reservationService;
            this.drinkService = drinkService;
            this.autoCreatorService = autoCreatorService;
        }

        public ActionResult ShowMenu()
        {
            var meals = mealService.GetMeals();
            var drinks = drinkService.GetDrinks();

            var result = new MenuViewModel { Drinks = drinks, Meals = meals };
            return View(result);
        }
        public ActionResult Propose()
        {
            var userPreference = new List<long> {1, 1, 2, 2, 2, 2, 4, 3, 6, 6, 4};
            var prop1 = autoCreatorService.GetPreferredMealsFor(userPreference);
            var prop2 = autoCreatorService.GetPreferredMealsFor2(userPreference);
            return View();
        }

        // GET: Questionaire
        public ActionResult Index(long id)
        {
            var meals = mealService.GetMeals();
            var drinks = drinkService.GetDrinks();

            var result = new MenuViewModel {Drinks = drinks, Meals = meals};
            return View(result);
        }

        [HttpPost]
        public ActionResult Index(List<MealForReservationViewModel> meals, List<DrinkForReservationViewModel> drinks, long id)
        {
            var mealsEntities = mealService.GetMeals(meals.Where(m => m.Number > 0).Select(m => m.Id).ToList());
            var drinkEntities = drinkService.GetDrinks(drinks.Where(m => m.Number > 0).Select(m => m.Id).ToList());


            if (mealsEntities == null || mealsEntities.Count == 0)
            {
                this.FlashMessage(MessageResult.Create("You didn't choose any meal!", MessageType.Info));
                var result = new MenuViewModel { Drinks = drinkEntities, Meals = mealsEntities };
                return View(result);
            }

            var mealsToReserve = mealsEntities
                .Select(m => new MealForReservation {Meal = m, NumberOfMeals = meals.First(me => m.Id == me.Id).Number})
                .ToList();

            var drinksToReserve = drinkEntities
                .Select(m => new DrinkForReservation { Drink = m, NumberOfDrinks = drinks.First(me => m.Id == me.Id).Number })
                .ToList();

            var reservationMeal = reservationService.ReserveMeal(id, mealsToReserve);
            var reservationDrink = reservationService.ReserveDrink(id, drinksToReserve);

            if (reservationMeal.IsSuccessful && reservationDrink.IsSuccessful)
            {
                this.FlashMessage(MessageResult.Create("One more step!"));
                return RedirectToAction("Summary", "Reservation", new {id=id});
            }

            this.FlashMessage(MessageResult.Create(reservationMeal.Errors.Last(), MessageType.Error));
            this.FlashMessage(MessageResult.Create(reservationDrink.Errors.Last(), MessageType.Error));

            return View(meals);
        }

        
    }
}