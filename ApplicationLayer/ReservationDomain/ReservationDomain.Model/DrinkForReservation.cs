using Common.Model;

namespace ReservationDomain.Model
{
    public class DrinkForReservation : Entity
    {
        public Drink Drink { get; set; }
        public int NumberOfDrinks { get; set; }
    }
}