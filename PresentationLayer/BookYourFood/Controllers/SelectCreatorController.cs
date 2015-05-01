using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApplicationUserDomain.Service;

namespace BookYourFood.Controllers
{
    public class SelectCreatorController : Controller
    {
        private readonly IAutoCreatorService autoCreatorService;
        private readonly ApplicationUserManager userManager;

        public SelectCreatorController(IAutoCreatorService autoCreatorService,
            ApplicationUserManager userManager)
        {
            this.autoCreatorService = autoCreatorService;
            this.userManager = userManager;
        }

        // GET: SelectCreator
        public ActionResult Index(long id = 0)
        {
            var list = autoCreatorService.GetPreferredMealsFor(User.Identity.Name);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string name, long id = 0)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}