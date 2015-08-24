using System.Net;
using System.Web.Mvc;
using ApplicationUserBC.Interfaces;
using Microsoft.AspNet.Identity;
//using Database;

namespace BookYourFood.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IApplicationUserService applicationUserService;
        public HomeController(IApplicationUserService applicationUserService)
        {
            this.applicationUserService = applicationUserService;
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
            var user = applicationUserService.GetUserById(User.Identity.GetUserId());
            return PartialView(user);
        }

    }
}