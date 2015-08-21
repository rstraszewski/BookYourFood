using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ReservationDomain.Model;
using Reservaton.Service;
using UtilityMvc;

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

        [HttpGet]
        [Authorize(Roles = "Administrator,Restaurant")]
        public ActionResult GetIngredientsForGrid([DataSourceRequest] DataSourceRequest request)
        {
            var tables = mealService.GetIngredients();
            return Json(tables.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrator,Restaurant")]
        public ActionResult CreateIngredient([DataSourceRequest] DataSourceRequest request, Ingredient ingredient)
        {
            if (ingredient != null && ModelState.IsValid)
            {
                mealService.CreateIngredient(ingredient);
            }
            return Json(new[] { ingredient }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Restaurant")]
        public ActionResult UpdateIngredient([DataSourceRequest] DataSourceRequest request, Ingredient ingredient)
        {
            if (ingredient != null && ModelState.IsValid)
            {
                mealService.UpdateIngredient(ingredient);
            }

            return Json(new[] { ingredient }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Restaurant")]
        public ActionResult DestroyIngredient([DataSourceRequest] DataSourceRequest request, Ingredient ingredient)
        {
            if (ingredient != null)
            {
                mealService.RemoveIngredient(ingredient);
            }

            return Json(new[] { ingredient }.ToDataSourceResult(request, ModelState));
        }

        [Authorize(Roles = "Administrator,Restaurant")]
        public ActionResult Show()
        {
            ViewData["IngredientType_Data"] = SelectListHelper.EnumToSelectList(typeof(IngredientType));
            ViewData["IngredientImportance_Data"] = SelectListHelper.EnumToSelectList(typeof(IngredientImportance));

            return View();
        }
    }
}