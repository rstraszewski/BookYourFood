using System.Collections.Generic;
using Common.Model;

namespace ReservationDomain.Model
{
    public class Ingredient: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IngredientType IngredientType { get; set; }
        public IngredientImportance IngredientImportance { get; set; }
        public List<HashTag> HashTags { get; set; }
    }

    public enum IngredientImportance
    {
        Main,
        Secondary
    }

    public enum IngredientType
    {
        Meat,
        Spice
    }
}