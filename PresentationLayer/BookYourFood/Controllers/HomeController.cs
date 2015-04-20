using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using BookYourFood.Models;
using Kendo.Mvc;
using Reservaton.Service;
using Utility;

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
            this.AddFlashMessage(MessageResult.Create("Some test message"));
            this.AddFlashMessage(MessageResult.Create("Some test message asdasda"));
            return View(mail);
        }

       /* public ActionResult Create()
        {
            var result = _reservationService.ReserveTable(DateTime.Now.AddDays(2), 2, 1);
            var sth = new
            {
                sadasd = "asd"
            };
            return this.JsonOperationResult(sth, MessageResult.Create("Some test message"));
        }*/

        public ActionResult Create()
        {
            var result = _reservationService.ReserveTable(DateTime.Now.AddDays(2), 2, 1);
            var sth = new
            {
                sadasd = "asd"
            };

            this.AddFlashMessage(MessageResult.Create("Some test message22222"));
            this.AddFlashMessage(MessageResult.Create("Some test message7777"));
            return Json(sth);
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