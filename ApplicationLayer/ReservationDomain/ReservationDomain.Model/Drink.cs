using System.Collections.Generic;
using Common.Model;

namespace ReservationDomain.Model
{
    public class Drink : Entity
    {
        public string Name { get; set; }
        public DrinkType DrinkType { get; set; }
        public List<HashTag> HashTags { get; set; }
    }

    public enum DrinkType
    {
        Wine,
        Beer,
        Soda
    }
}