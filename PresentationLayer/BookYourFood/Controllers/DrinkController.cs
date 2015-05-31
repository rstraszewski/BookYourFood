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
using UtilityMvc;

namespace BookYourFood.Controllers
{
    [Authorize(Roles = "Administrator,Restaurant")]
    public class DrinkController : Controller
    {
        private readonly IDrinkService drinkService;

        public DrinkController(IDrinkService drinkService)
        {
            this.drinkService = drinkService;
        }

        public ActionResult Show()
        {

            ViewData["DrinkType_Data"] = SelectListHelper.EnumToSelectList(typeof(DrinkType));
            return View();
        }

        [HttpGet]
        public ActionResult GetDrink([DataSourceRequest] DataSourceRequest request)
        {
            var drinks = drinkService.GetDrinks();
            return Json(drinks.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateDrink([DataSourceRequest] DataSourceRequest request, Drink drink)
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
                drinkService.UpdateDrink(drink);
            }

            return Json(new[] { drink }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult DestroyDrink([DataSourceRequest] DataSourceRequest request, Drink drink)
        {
            if (drink != null)
            {
                drinkService.RemoveDrink(drink);
            }

            return Json(new[] { drink }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult SetHashTags(long drinkId, List<long> tagsIds)
        {
            drinkService.SetHashTagsForDrink(drinkId, tagsIds);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}