using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationDomain.Model
{
    public class RatedMeal
    {
        public Meal Meal { get; set; }
        public long Score { get; set; }
    }
}
