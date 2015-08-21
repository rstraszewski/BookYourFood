using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ApplicationUserBC.Interfaces;
using AutoMapper;
using BookYourFood.Models;
using Microsoft.AspNet.Identity;
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
                FavouriteMeals = Mapper.Map<List<MealViewModel>>(userFavouriteMeals)
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
                FavouriteMeals = Mapper.Map<List<MealViewModel>>(userFavouriteMeals)
            };

            return View(model);
        }
    }
}