using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace ReservationDomain.Model
{
    public class MealForReservation : Entity
    {
        public virtual Meal Meal { get; set; }
        public int NumberOfMeals { get; set; }
    }
}
