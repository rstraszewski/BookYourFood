using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Reservaton.Service;

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
    }
}