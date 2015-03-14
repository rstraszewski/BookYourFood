using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace ReservationDomain.Model
{
    public class Reservation: Entity
    {
        public DateTime ReservationTime { get; set; }

        public Reservation(DateTime time)
        {
            ReservationTime = time;
        }

        //Needed by EntityFramework
        protected Reservation() { }
    }
}
