using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApplicationUserDomain.Service;
using Microsoft.AspNet.Identity;
using ReservationDomain.Service;

namespace BookYourFood.Controllers
{
    public class SelectCreatorController : Controller
    {
        private IApplicationUserService userService;
        private ApplicationUserManager userManager;
        private IAutoCreatorService autoCreatorService;

        public SelectCreatorController(IApplicationUserService userService, ApplicationUserManager userManager,
            IAutoCreatorService autoCreatorService)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.autoCreatorService = autoCreatorService;
        }

        // GET: SelectCreatorIU
        public ActionResult Index(long id)
        {
            //TODO: Change to model and strongly typed view
            ViewBag.ReservationId = id;

            var hasAnswers = false;
            if (User.Identity.IsAuthenticated)
            {
                var user = userManager.FindByName(User.Identity.Name);
                var userAnswers = userService.GetUserAnswers(user.Id);
                if (userAnswers != null && userAnswers.Count > 0)
                {
                    hasAnswers = true;
                }
            }

            ViewBag.HasAnswers = hasAnswers;

            // For testing purposes ONLY

            //var userPreferences = userService.GetUserPreferences(userManager.FindByName(User.Identity.Name).Id);
            
            //var preferredMeals = autoCreatorService.GetPreferredMealsFor(userPreferences);

            return View();
        }

        [HttpPost]
        public ActionResult Index(long id, string name)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}