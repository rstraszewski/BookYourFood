using Common.Model;

namespace ReservationDomain.Model
{
    public class DrinkForReservation : Entity
    {
        public virtual Drink Drink { get; set; }
        public int NumberOfDrinks { get; set; }
    }
}