using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BookYourFood.Models;
using Microsoft.AspNet.Identity;
using ReservationDomain.Model;
using Reservaton.Service;
using Utility;

namespace BookYourFood.Controllers
{
    public class ReservationController : Controller
    {
        private IReservationService reservationService;
        private ApplicationUserManager userManager;
        public ReservationController(IReservationService reservationService, ApplicationUserManager userManager)
        {
            this.reservationService = reservationService;
            this.userManager = userManager;
        }

        // GET: Reservation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reserve()
        {
            var tables = reservationService.GetTables();
            var result = Mapper.Map<List<TableViewModel>>(tables);
            return View(result);
        }

        [HttpPost]
        public ActionResult Reserve(long? tableId, DateTime? dateTimeFrom, int? howLong)
        {
            if (tableId == null)
            {
                this.AddFlashMessage("You didn't choose table!",MessageType.Error);
                return RedirectToAction("Reserve");
            }

            if (dateTimeFrom == null)
            {
                this.AddFlashMessage("You didn't choose date and time!", MessageType.Error);
                return RedirectToAction("Reserve");
            }

            if (howLong == null)
            {
                this.AddFlashMessage("You didn't choose duration!", MessageType.Error);
                return RedirectToAction("Reserve");
            }

            this.AddFlashMessage("Success!");
            var result = reservationService.ReserveTable(dateTimeFrom.Value, howLong.Value, tableId.Value);
            if (result.IsSuccessful)
            {
                return RedirectToAction("Index", "SelectCreator", new { id = result.Result.Id });
            }

            this.AddFlashMessage("Something went wrong :(", MessageType.Error);
            return RedirectToAction("Reserve");
        }

        public ActionResult Summary(long id)
        {
            var reservation = reservationService.GetReservation(id);
            var model = Mapper.Map<ReservationSummaryViewModel>(reservation);
            if (User.Identity.IsAuthenticated)
            {
                var user = userManager.FindByName(User.Identity.Name);
                model.Name = user.Name;
                model.PhoneNumber = user.PhoneNumber;
                model.Surname = user.Surname;
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult CheckAvailability(DateTime? dateTimeFrom, int? howLong)
        {
            var tables = reservationService.GetAvailableTables(dateTimeFrom, dateTimeFrom.Value.AddHours(howLong.Value));
            var result = tables.Select(table => table.Id);
            return Json(new {tables= result}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Finalize(long id, string phone, string surname)
        {
            var reservation = reservationService.GetReservation(id);
            string userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = userManager.FindByName(User.Identity.Name).Id;
            }
            var result = reservationService.Finalize(id, phone, surname, userId);
            this.FlashMessage(result.ToMessageResult());
            return RedirectToAction("Index","Home");
        }

    }
}