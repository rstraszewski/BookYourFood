﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Reservaton.Service;
using Utility;

namespace BookYourFood.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IMealService mealService;

        public IngredientController(IMealService mealService)
        {
            this.mealService = mealService;
        }

        [HttpGet]
        public ActionResult GetIngredients()
        {
            var ingredients = mealService.GetIngredients();

            return Json(ingredients, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetIngredientsForMeal(long mealId)
        {
            var ingredients = mealService.GetIngredients(mealId).Select(ing => ing.Id);

            return Json(ingredients, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetInformationForIngredients(List<long> ingredients)
        {
            var price = mealService.GetPriceForIngredients(ingredients);
            var names = mealService.GetIngredientsNames(ingredients);
            return Json(new {Price = price, Names = string.Join(", ",names)}, JsonRequestBehavior.AllowGet);
        }
    }
}