using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
//using Database;
using Microsoft.AspNet.Identity;
using ReservationDomain.Model;
using Utility;

namespace BookYourFood.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ApplicationUserManager userManager;
        public HomeController(ApplicationUserManager userManager)
        {
            this.userManager = userManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Hello()
        {
            var user = userManager.FindById(User.Identity.GetUserId());
            return PartialView(user);
        }

    }
}