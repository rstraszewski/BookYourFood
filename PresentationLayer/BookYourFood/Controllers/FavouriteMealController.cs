using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;
using Microsoft.AspNet.Identity;
using BookYourFood.Models;
using ApplicationUserDomain.Service;
using ReservationDomain.Model;
using ReservationDomain.Service;
using Reservaton.Service;

namespace BookYourFood.Controllers
{
    public class FavouriteMealController : Controller
    {
        private readonly IApplicationUserService applicationUserService;
        private readonly IMealService mealService;

        public FavouriteMealController(IApplicationUserService applicationUserService, IMealService mealService)
        {
            this.applicationUserService = applicationUserService;
            this.mealService = mealService;
        }

        // GET: FavouriteMeal
        public ActionResult Index()
        {
            var tmpUserFavouriteMeals = applicationUserService
                .GetUserFavouriteMeals(User.Identity.GetUserId());

            var userFavouriteMeals = mealService.GetMeals()
                .Where(m => tmpUserFavouriteMeals.Contains(m.Id))
                .ToList();

            FavouriteMealViewModel model = new FavouriteMealViewModel
            { 
                FavouriteMeals = userFavouriteMeals
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(long id)
        {
            applicationUserService.RemoveFavouriteMeal(id, User.Identity.GetUserId());

            var tmpUserFavouriteMeals = applicationUserService
                .GetUserFavouriteMeals(User.Identity.GetUserId());

            var userFavouriteMeals = mealService.GetMeals()
                .Where(m => tmpUserFavouriteMeals.Contains(m.Id))
                .ToList();

            FavouriteMealViewModel model = new FavouriteMealViewModel
            {
                FavouriteMeals = userFavouriteMeals
            };

            return View(model);
        }
    }
}