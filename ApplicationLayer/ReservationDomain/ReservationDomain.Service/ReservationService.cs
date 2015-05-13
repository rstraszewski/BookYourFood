using System;
using System.Collections.Generic;
using System.Linq;
using Common.Service;
using Database;
using ReservationDomain.Model;
using Utility;

namespace Reservaton.Service
{
    public class ReservationService : ApplicationService, IReservationService
    {
        public ReservationService(ByfDbContext byfDbContext)
            : base(byfDbContext)
        {
        }

        public OperationResult<Reservation> ReserveTable(DateTime reservationTime, int duration, long tableId)
        {
            var table = ByfDbContext.Tables.Find(tableId);
            var reservation = new Reservation(reservationTime, duration, table);
            var result = CanReservationBeCreated(reservation);
            if (result.IsSuccessful)
            {
                ByfDbContext.Reservations.Add(reservation);
                ByfDbContext.SaveChanges();
            }

            return result;
        }

        public OperationResult<Reservation> ReserveMeal(long reservationId, List<MealForReservation> reservedMeals)
        {
            var reservation = ByfDbContext.Reservations.Find(reservationId);
            reservation.OrderMeals(reservedMeals);

            ByfDbContext.SaveChanges();
            return OperationResult<Reservation>.CreateResult(reservation);
        }

        public OperationResult<Reservation> ReserveDrink(long reservationId, List<DrinkForReservation> reservedDrinks)
        {
            var reservation = ByfDbContext.Reservations.Find(reservationId);
            reservation.OrderDrinks(reservedDrinks);

            ByfDbContext.SaveChanges();
            return OperationResult<Reservation>.CreateResult(reservation);
        }

        public OperationResult Finalize(long reservationId)
        {
            var reservation = ByfDbContext.Reservations.Find(reservationId);
            reservation.FinalizeReservation();

            ByfDbContext.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult Finalize(long reservationId, string surname)
        {
            var reservation = ByfDbContext.Reservations.Find(reservationId);
            reservation.AssignPerson(surname);
            reservation.FinalizeReservation();

            ByfDbContext.SaveChanges();
            return OperationResult.Success();
        }

        public List<Table> GetTables()
        {
            var result = ByfDbContext.Tables.ToList();
            return result;
        }

        public List<Table> GetAvailableTables(DateTime? dateTimeFrom, DateTime? dateTimeTo)
        {
            if(dateTimeFrom.HasValue && dateTimeTo.HasValue)
            {
                var tables = ByfDbContext.Tables.ToList();
                var result = tables.Where(table => table.IsFree(dateTimeFrom.Value, dateTimeTo.Value)).ToList();
                return result;
            }

            throw new Exception("You need to provide dates!");
        }

        public Reservation GetReservation(long id)
        {

            var reservation = ByfDbContext.Reservations.FirstOrDefault(r => r.Id == id);

            return reservation;
        }

        public OperationResult Finalize(long reservationId, string phoneNumber, string surname, string userId)
        {
            var reservation = ByfDbContext.Reservations.Find(reservationId);
            reservation.AssignPerson(surname);
            reservation.UserId = userId;
            reservation.UserPhoneNumber = phoneNumber;
            reservation.FinalizeReservation();

            ByfDbContext.SaveChanges();
            return OperationResult.Success();
        }

        public List<Reservation> GetReservationsForToday()
        {
            var startDate = DateTime.Today;
            var endDate = startDate.AddDays(1);
            return ByfDbContext.Reservations.Where(r => r.ReservationTime >= startDate && r.ReservationTime < endDate && r.IsFinalized).OrderBy(r => r.ReservationTime).ToList();
        }

        public IQueryable<ReservationDto> GetReservationsQueryable()
        {
            return ByfDbContext.Reservations.Where(r => r.IsFinalized).Select(r => new ReservationDto()
            {
                Duration = r.Duration,
                ReservationTime = r.ReservationTime,
                TableNumber = r.Table.TableNumber,
                UserSurname = r.UserSurname,
                UserPhoneNumber = r.UserPhoneNumber
            });
        }



        private OperationResult<Reservation> CanReservationBeCreated(Reservation reservation)
        {
            var result = OperationResult<Reservation>.CreateResult(reservation);

            if (reservation.ReservationTime < DateTime.Now)
            {
                result.AddError("Reservation time must be later than now");
            }
            if (reservation.Table == null)
            {
                result.AddError("Table was not found");
            }

            return result;
        }
    }

    public class ReservationDto
    {
        public DateTime ReservationTime { get; set; }

        public string ReservationTimeAsString
        {
            get { return ReservationTime.ToShortDateString() + " " + ReservationTime.ToShortTimeString(); }
        }

        public DateTime ReservationDate
        {
            get
            {
                return ReservationTime.Date;  
            } 
        }

        public int Duration { get; set; }
        public long TableNumber { get; set; }
        public string UserSurname { get; set; }
        public string UserPhoneNumber { get; set; }
    }
}
