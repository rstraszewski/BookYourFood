using Common.Model;

namespace ReservationDomain.Model
{
    public class Table : Entity
    {
        public long TableNumber { get; set; }
        public int SeatsNumber { get; set; }
        public bool IsReserved { get; set; }

        public void Reserve()
        {
            IsReserved = true;
        }

        public void Release()
        {
            IsReserved = false;
        }

    }
}