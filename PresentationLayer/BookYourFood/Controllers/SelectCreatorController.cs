using System.Web.Mvc;
using ApplicationUserBC.Interfaces;
using Microsoft.AspNet.Identity;

namespace BookYourFood.Controllers
{
    public class SelectCreatorController : Controller
    {
        private readonly IApplicationUserService userService;

        public SelectCreatorController(IApplicationUserService userService)
        {
            this.userService = userService;
        }

        // GET: SelectCreatorIU
        public ActionResult Index(long id = 0)
        {
            //TODO: Change to model and strongly typed view
            ViewBag.ReservationId = id;

            var hasAnswers = false;
            if (User.Identity.IsAuthenticated)
            {
                var userAnswers = userService.GetUserAnswers(User.Identity.GetUserId());
                if (userAnswers != null && userAnswers.Count > 0)
                {
                    hasAnswers = true;
                }
            }

            ViewBag.HasAnswers = hasAnswers;

            return View();
        }

        [HttpPost]
        public ActionResult Index(long id, string name)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}