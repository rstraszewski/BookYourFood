﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ReservationDomain.Model;
using Reservaton.Service;

namespace BookYourFood.Controllers
{
    public class DrinkController : Controller
    {
        private readonly IDrinkService drinkService;

        public DrinkController(IDrinkService drinkService)
        {
            this.drinkService = drinkService;
        }
        public ActionResult Show()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetDrink([DataSourceRequest] DataSourceRequest request)
        {
            var drinks = drinkService.GetDrinks();
            return Json(drinks.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreatDrink([DataSourceRequest] DataSourceRequest request, Drink drink)
        {
            if (drink != null && ModelState.IsValid)
            {
                drinkService.CreateDrink(drink);
            }
            return Json(new[] { drink }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult UpdateDrink([DataSourceRequest] DataSourceRequest request, Drink drink)
        {
            if (drink != null && ModelState.IsValid)
            {
                drinkService.UpdateMeal(drink);
            }

            return Json(new[] { drink }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult DestroyDrink([DataSourceRequest] DataSourceRequest request, Drink drink)
        {
            if (drink != null)
            {
                drinkService.RemoveMeal(drink);
            }

            return Json(new[] { drink }.ToDataSourceResult(request, ModelState));
        }

    }
}