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
        private readonly IMealService _mealService;
        private readonly IReservationService _reservationService;

        public MenuController(IMealService mealService, IReservationService reservationService)
        {
            _mealService = mealService;
            _reservationService = reservationService;
        }

        // GET: Questionaire
        public ActionResult Index()
        {
            this.FlashMessage(MessageResult.Create(
                "You need to complete questionaire, so we can predict your desires!", MessageType.Info));
            var meals = _mealService.GetMeals();
            return View(meals);
        }

        [HttpPost]
        public ActionResult Index(List<MealForReservationViewModel> meals)
        {
            var mealsEntities = _mealService.GetMeals(meals.Where(m => m.Number > 0).Select(m => m.Id).ToList());

            if (meals == null)
            {
                this.FlashMessage(MessageResult.Create("You didn't choose any meal!", MessageType.Info));
                return View();
            }

            var mealsToReserve = mealsEntities
                .Select(m => new MealForReservation {Meal = m, NumberOfMeals = meals.First(me => m.Id == me.Id).Number})
                .ToList();

            var reservation = _reservationService.ReserveMeal(1L, mealsToReserve);

            if (reservation.IsSuccessful)
            {
                this.FlashMessage(MessageResult.Create("Reservation completes successfuly!"));
                return RedirectToAction("Index", "Home");
            }

            this.FlashMessage(MessageResult.Create(reservation.Errors.Last(), MessageType.Error));

            return View(meals);
        }

        
    }
}