﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BookYourFood.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ReservationDomain.Model;
using Reservaton.Service;
using Utility;

namespace BookYourFood.Controllers
{
    [Authorize(Roles = "Administrator,Restaurant")]
    public class MealController : Controller
    {
        private readonly IMealService mealService;

        public MealController(IMealService mealService)
        {
            this.mealService = mealService;
        }

        [Authorize(Roles = "Administrator,Restaurant")]
        public ActionResult Show()
        {
            return View();
        }

        [Authorize(Roles = "Administrator,Restaurant")]
        public ActionResult SaveImage(IEnumerable<HttpPostedFileBase> image, long mealId)
        {
            MemoryStream target = new MemoryStream();
            image.First().InputStream.CopyTo(target);
            byte[] data = target.ToArray();
            mealService.AddPhotoToMeal(mealId, data);
            this.FlashMessage("Ok");
            return Json(new { Response = "Ok" });
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetMeals([DataSourceRequest] DataSourceRequest request)
        {
            var meals = Mapper.Map<List<MealViewModel>>(mealService.GetMeals());
            return Json(meals.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrator,Restaurant")]
        public ActionResult SetIngridents(long mealId, List<long> ingIds)
        {
            mealService.SetIngredientsForMeal(mealId, ingIds);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [Authorize(Roles = "Administrator,Restaurant")]
        public ActionResult SetHashTags(long mealId, List<long> ingIds)
        {
            mealService.SetHashTagsForMeal(mealId, ingIds);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Restaurant")]
        public ActionResult CreateMeal([DataSourceRequest] DataSourceRequest request, Meal meal)
        {
            if (meal != null && ModelState.IsValid)
            {
                mealService.CreateMeal(meal);
            }

            return Json(new[] { meal }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Restaurant")]
        public ActionResult UpdateMeal([DataSourceRequest] DataSourceRequest request, Meal meal)
        {
            if (meal != null && ModelState.IsValid)
            {
                mealService.UpdateMeal(meal);
            }

            return Json(new[] { meal }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Restaurant")]
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