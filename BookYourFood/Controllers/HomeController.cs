using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookYourFood.Models;
using Kendo.Mvc;
using Reservaton.Service;

namespace BookYourFood.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReservationService _reservationService;

        public HomeController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public ActionResult Index()
        {
            var mail = new ExternalLoginConfirmationViewModel()
            {
                Email = "rstraszewski2gmail.com"
            };
            return View(mail);
        }

        public ActionResult Create()
        {
            var status = _reservationService.ReserveTableForNow();
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