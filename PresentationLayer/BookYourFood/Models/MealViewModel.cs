using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookYourFood.Models
{
    public class MealViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? Rating { get; set; }
        public string ImageData { get; set; }
    }
}