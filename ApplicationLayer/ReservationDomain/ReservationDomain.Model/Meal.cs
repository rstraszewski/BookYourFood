﻿using System.Collections.Generic;
using Common.Model;

namespace ReservationDomain.Model
{
    public class Meal: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? Rating { get; set; }
        public virtual List<Ingredient> Ingredients { get; set; }
        public virtual List<HashTag> HashTags { get; set; }
        public bool CreatedByUser { get; set; }
        public byte[] Image { get; set; }
    }
}