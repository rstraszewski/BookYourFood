using System;
using System.Collections.Generic;
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
            return View(tables);
        }

        [HttpPost]
        public ActionResult Reserve(long tableId, DateTime? dateTimeFrom, DateTime? dateTimeTo)
        {
            this.AddFlashMessage("Success!");
            return RedirectToAction("Index","Home");
        }

    }
}