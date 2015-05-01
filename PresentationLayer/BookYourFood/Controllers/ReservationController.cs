using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using ReservationDomain.Model;
using Reservaton.Service;
using Utility;

namespace BookYourFood.Controllers
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class PersonViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Full
        {
            get { return Name + " " + Surname; }
        }
    }

    public class ReservationController : Controller
    {
        private IReservationService reservationService;

        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        // GET: Reservation
        public ActionResult Index()
        {
            
            var p = new Person() {Name = "Tomasz", Surname = "Grochmal"};

            var model = Mapper.Map<PersonViewModel>(p);
            return View(p);
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
            return View(model);
        }

        [HttpGet]
        public ActionResult CheckAvailability(DateTime? dateTimeFrom, int? howLong)
        {
            var tables = reservationService.GetAvailableTables(dateTimeFrom, DateTime.Now.AddHours(howLong.Value));
            var result = tables.Select(table => table.Id);
            return Json(new {tables= result}, JsonRequestBehavior.AllowGet);
        }

    }

    public class DrinkForSummary
    {
        public int NumberOfDrinks { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }

        
    }

    public class MealForSummary
    {
        public int NumberOfMeals { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }

    }

    public class ReservationSummaryViewModel
    {
        public DateTime ReservationTime { get; set; }
        public int Duration { get; set; }
        public TableViewModel Table { get; set; }
        public List<MealForSummary> Meals { get; set; }
        public List<DrinkForSummary> Drinks { get; set; }
        public decimal Price { get; set; }
    }

    public class TableViewModel
    {
        public long TableNumber { get; set; }
        public int SeatsNumber { get; set; }
        public long Id { get; set; }
    }
}