using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Reservaton.Service;

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
        public string TmpConflict { get; set; }

        public string Full
        {
            get { return Name + " " + TmpConflict; }
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
    }
}