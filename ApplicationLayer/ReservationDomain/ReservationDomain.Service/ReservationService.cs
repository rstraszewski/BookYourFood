using System;
using System.Collections.Generic;
using System.Linq;
using Common.Service;
using ReservationDomain.Infrastructure;
using ReservationDomain.Model;
using Utility;

namespace Reservaton.Service
{
    public class ReservationService : ApplicationService<ReservationDomainDbContext>, IReservationService
    {
        public ReservationService(ReservationDomainDbContext context)
            : base(context)
        {
        }

        public OperationResult<Reservation> ReserveTable(DateTime reservationTime, int duration, long tableId)
        {
            var table = _context.Tables.Find(tableId);
            var reservation = new Reservation(reservationTime, duration, table);
            var result = CanReservationBeCreated(reservation);
            if (result.IsSuccessful)
            {
                _context.Reservations.Add(reservation);
                _context.SaveChanges();
            }

            return result;
        }

        public OperationResult<Reservation> ReserveMeal(long reservationId, List<MealForReservation> reservedMeals)
        {
            var reservation = _context.Reservations.Find(reservationId);
            reservation.OrderMeals(reservedMeals);

            _context.SaveChanges();
            return OperationResult<Reservation>.CreateResult(reservation);
        }

        public OperationResult<Reservation> ReserveDrink(long reservationId, List<DrinkForReservation> reservedDrinks)
        {
            var reservation = _context.Reservations.Find(reservationId);
            reservation.OrderDrinks(reservedDrinks);

            _context.SaveChanges();
            return OperationResult<Reservation>.CreateResult(reservation);
        }

        public OperationResult Finalize(long reservationId)
        {
            var reservation = _context.Reservations.Find(reservationId);
            reservation.FinalizeReservation();

            _context.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult Finalize(long reservationId, string surname, string phoneNumber)
        {
            var reservation = _context.Reservations.Find(reservationId);
            reservation.AssignOwner(surname, phoneNumber);
            reservation.FinalizeReservation();

            _context.SaveChanges();
            return OperationResult.Success();
        }

        public List<Table> GetTables()
        {
            var result = _context.Tables.ToList();
            return result;
        }

        public List<Table> GetAvailableTables(DateTime? dateTimeFrom, DateTime? dateTimeTo)
        {
            if(dateTimeFrom.HasValue && dateTimeTo.HasValue)
            {
                var tables = _context.Tables.ToList();
                var result = tables.Where(table => table.IsFree(dateTimeFrom.Value, dateTimeTo.Value)).ToList();
                return result;
            }

            throw new Exception("You need to provide dates!");
        }

        public Reservation GetReservation(long id)
        {

            var reservation = _context.Reservations.FirstOrDefault(r => r.Id == id);

            return reservation;
        }

        public OperationResult Finalize(long reservationId, string phoneNumber, string surname, string userId)
        {
            var reservation = _context.Reservations.Find(reservationId);
            reservation.AssignUser(userId, surname, phoneNumber);
            reservation.FinalizeReservation();

            _context.SaveChanges();
            return OperationResult.Success();
        }

        public List<Reservation> GetReservationsForToday()
        {
            var startDate = DateTime.Today;
            var endDate = startDate.AddDays(1);
            return _context.Reservations.Where(r => r.ReservationTime >= startDate && r.ReservationTime < endDate && r.IsFinalized).OrderBy(r => r.ReservationTime).ToList();
        }

        public IQueryable<ReservationDto> GetReservationsQueryable()
        {
            return _context.Reservations.Where(r => r.IsFinalized).Select(r => new ReservationDto()
            {
                Duration = r.Duration,
                ReservationTime = r.ReservationTime,
                TableNumber = r.Table.TableNumber,
                UserSurname = r.Owner.FullName,
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
}
