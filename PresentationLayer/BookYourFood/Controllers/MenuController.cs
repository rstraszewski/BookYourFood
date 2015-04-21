using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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
        public ActionResult Index(List<long> mealsNum, List<long> mealsIds)
        {
            var meals = _mealService.GetMeals();

            if (mealsIds == null)
            {
                this.FlashMessage(MessageResult.Create("You didn't choose any meal!", MessageType.Info));
                return View(meals);
            }

            var reservation = _reservationService.ReserveMeal(1L,mealsIds);

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