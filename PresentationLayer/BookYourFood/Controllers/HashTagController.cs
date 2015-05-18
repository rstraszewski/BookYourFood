using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reservaton.Service;

namespace BookYourFood.Controllers
{
    public class HashTagController : Controller
    {
        private readonly IHashTagService hashTagService;

        public HashTagController(IHashTagService hashTagService)
        {
            this.hashTagService = hashTagService;
        }

        [HttpGet]
        public ActionResult GetHashTags()
        {
            var hashTags = hashTagService.GetHashTags();

            return Json(hashTags, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetHashTagsForMeal(long mealId)
        {
            var hashTags = hashTagService.GetHashTags(mealId).Select(tag => tag.Id);

            return Json(hashTags, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetHashTagsForDrink(long drinkId)
        {
            var hashTags = hashTagService.GetHashTagsForDrink(drinkId).Select(tag => tag.Id);

            return Json(hashTags, JsonRequestBehavior.AllowGet);
        }
    }
}