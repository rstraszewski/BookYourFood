using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ReservationDomain.Model;
using Reservaton.Service;

namespace BookYourFood.Controllers
{
    public class MealController : Controller
    {
        private readonly IMealService mealService;

        public MealController(IMealService mealService)
        {
            this.mealService = mealService;
        }

        public ActionResult Show()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetMeals([DataSourceRequest] DataSourceRequest request)
        {
            var meals = mealService.GetMeals();
            return Json(meals.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetIngridents(long mealId, List<long> ingIds)
        {
            mealService.SetIngredientsForMeal(mealId, ingIds);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult CreateMeal([DataSourceRequest] DataSourceRequest request, Meal meal)
        {
            if (meal != null && ModelState.IsValid)
            {
                mealService.CreateMeal(meal);
            }

            return Json(new[] { meal }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult UpdateMeal([DataSourceRequest] DataSourceRequest request, Meal meal)
        {
            if (meal != null && ModelState.IsValid)
            {
                mealService.UpdateMeal(meal);
            }

            return Json(new[] { meal }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult DestroyMeal([DataSourceRequest] DataSourceRequest request, Meal meal)
        {
            if (meal != null)
            {
                mealService.RemoveMeal(meal);
            }

            return Json(new[] { meal }.ToDataSourceResult(request, ModelState));
        }
    }
}