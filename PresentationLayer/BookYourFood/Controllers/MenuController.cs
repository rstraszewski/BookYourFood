using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ApplicationUserBC.Service;
using ApplicationUserDomain.Service;
using AutoMapper;
using BookYourFood.Models;
using Microsoft.AspNet.Identity;
using ReservationDomain.Model;
using ReservationDomain.Service;
using Reservaton.Service;
using Utility;

namespace BookYourFood.Controllers
{
    public class MenuController : Controller
    {
        private readonly IApplicationUserService applicationUserService;
        private readonly IAutoCreatorService autoCreatorService;
        private readonly IDrinkService drinkService;
        private readonly IMealService mealService;
        private readonly IQuestionnaireSevice questionnaireSevice;
        private readonly IReservationService reservationService;

        public MenuController(IMealService mealService, IReservationService reservationService,
            IDrinkService drinkService, IAutoCreatorService autoCreatorService,
            IApplicationUserService applicationUserService, IQuestionnaireSevice questionnaireSevice)
        {
            this.mealService = mealService;
            this.reservationService = reservationService;
            this.drinkService = drinkService;
            this.autoCreatorService = autoCreatorService;
            this.applicationUserService = applicationUserService;
            this.questionnaireSevice = questionnaireSevice;
        }

        public ActionResult ShowMenu()
        {
            var meals = Mapper.Map<List<MealViewModel>>(mealService.GetMeals());

            var drinks = drinkService.GetDrinks();
            var tmpFavouriteUserMeals = applicationUserService
                .GetUserFavouriteMeals(User.Identity.GetUserId());

            var favouriteUserMeals = meals
                .Where(m => tmpFavouriteUserMeals.Contains(m.Id))
                .ToList();

            var result = new MenuViewModel
            {
                Drinks = drinks,
                Meals = meals,
                FavouriteUserMeals = (favouriteUserMeals != null ? favouriteUserMeals : new List<MealViewModel>())
            };
            return View(result);
        }

        [HttpGet]
        public JsonResult AddMealToUserFavourites(long id)
        {
            applicationUserService.AddFavouriteMeal(id, User.Identity.GetUserId());

            // zwróc informację o tym, że dodałeś danie do listy użytkownika
            return Json(id, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowCreator(long id)
        {
            ViewBag.Id = id;
            var meals = mealService.GetMeals();
            var drinks = drinkService.GetDrinks();
            var ingredients = mealService.GetIngredients();
            var result = new CreatorViewModel { Drinks = drinks, Meals = meals, Ingredients = ingredients };
            return View(result);
        }

        [Authorize]
        public ActionResult Propose(long id)
        {
            //var userPreference = new List<long> {1, 1, 2, 2, 2, 2, 4, 3, 6, 6, 4}; Test data
            ViewBag.Id = id;
            var userAnswers = applicationUserService.GetUserAnswers(User.Identity.GetUserId());

            if (userAnswers == null || userAnswers.Count == 0)
            {
                this.FlashMessage("You need to fill in questionaire first!", MessageType.Error);
                return RedirectToAction("Index", "SelectCreator", new { id });
            }

            var userPreference = questionnaireSevice.GetHashTagsFromAnswers(userAnswers);

            var proposedMeals = autoCreatorService.GetPreferredMealsFor(userPreference);
            var proposedDrinks = autoCreatorService.GetPreferredDrinksFor(userPreference);

            var model = new ProposeViewModel { RatedMeals = proposedMeals, RatedDrinks = proposedDrinks };

            return View(model);
        }

        // GET: Questionaire
        public ActionResult Index(long id)
        {
            var tmpFavouriteMeals = applicationUserService
                .GetUserFavouriteMeals(User.Identity.GetUserId());

            var userFavouriteMeals = mealService.GetMeals()
                .Where(m => tmpFavouriteMeals.Contains(m.Id))
                .ToList();

            var meals = mealService.GetMeals()
                .Where(m => !tmpFavouriteMeals.Contains(m.Id))
                .ToList();

            var drinks = drinkService.GetDrinks();

            // In case registered user don't have any favourite meals
            if (userFavouriteMeals == null)
            {
                userFavouriteMeals = new List<Meal>();
            }


            var result = new MenuViewModel { Drinks = drinks, 
                Meals = Mapper.Map<List<MealViewModel>>(meals), 
                FavouriteUserMeals = Mapper.Map<List<MealViewModel>>(userFavouriteMeals) };
            return View(result);
        }

        [HttpPost]
        public ActionResult Index(List<MealForReservationViewModel> meals, List<DrinkForReservationViewModel> drinks,
            long id, List<CustomMealViewModel> customMeals)
        {
            var mealsEntities = mealService.GetMeals(meals.Where(m => m.Number > 0).Select(m => m.Id).ToList());
            var drinkEntities = drinkService.GetDrinks(drinks.Where(m => m.Number > 0).Select(m => m.Id).ToList());


            if (mealsEntities.Count == 0 && customMeals.Count == 0)
            {
                this.FlashMessage(MessageResult.Create("You didn't choose any meal!", MessageType.Info));
                var result = new MenuViewModel { Drinks = drinkEntities, Meals = Mapper.Map<List<MealViewModel>>(mealsEntities) };
                return View(result);
            }

            var createdMealsToReserve = new List<MealForReservation>();

            if (customMeals != null)
            {
                var mealsToCreate = customMeals.Where(m => m.Count != 0).ToList();
                if (mealsToCreate != null)
                {
                    foreach (var mealToCreate in mealsToCreate)
                    {
                        var mealMap = Mapper.Map<Meal>(mealToCreate);
                        mealMap.CreatedByUser = true;
                        mealService.CreateMeal(mealMap);
                        createdMealsToReserve.Add(new MealForReservation
                        {
                            Meal = mealMap,
                            NumberOfMeals = mealToCreate.Count
                        });
                    }
                }
            }

            var mealsToReserve = mealsEntities
                .Select(m => new MealForReservation { Meal = m, NumberOfMeals = meals.First(me => m.Id == me.Id).Number })
                .ToList();

            mealsToReserve.AddRange(createdMealsToReserve);

            var drinksToReserve = drinkEntities
                .Select(
                    m => new DrinkForReservation { Drink = m, NumberOfDrinks = drinks.First(me => m.Id == me.Id).Number })
                .ToList();

            var reservationMeal = reservationService.ReserveMeal(id, mealsToReserve);
            var reservationDrink = reservationService.ReserveDrink(id, drinksToReserve);

            if (reservationMeal.IsSuccessful && reservationDrink.IsSuccessful)
            {
                this.FlashMessage(MessageResult.Create("One more step!"));
                return RedirectToAction("Summary", "Reservation", new { id });
            }

            this.FlashMessage(MessageResult.Create(reservationMeal.Errors.Last(), MessageType.Error));
            this.FlashMessage(MessageResult.Create(reservationDrink.Errors.Last(), MessageType.Error));

            return View(meals);
        }
    }
}