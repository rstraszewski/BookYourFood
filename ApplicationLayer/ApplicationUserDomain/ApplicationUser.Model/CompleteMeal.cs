using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationDomain.Model;

namespace ApplicationUserDomain.Model
{
    public class CompleteMeal
    {
        public Meal Meal { get; set; }
        public Drink Drink { get; set; }
    }
}
