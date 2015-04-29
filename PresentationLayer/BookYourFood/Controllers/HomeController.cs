using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Database;
using ReservationDomain.Model;
using Reservaton.Service;
using Utility;

namespace BookYourFood.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReservationService reservationService;

        public HomeController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
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
    }
}