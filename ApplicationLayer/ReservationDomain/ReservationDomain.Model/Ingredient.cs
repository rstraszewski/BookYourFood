using System.Collections.Generic;
using Common.Model;

namespace ReservationDomain.Model
{
    public class Ingredient: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IngredientType IngredientType { get; set; }

        public string IngredientTypeAsString
        {
            get { return IngredientType.ToString(); }
        }

        public IngredientImportance IngredientImportance { get; set; }

        public string IngredientImportanceAsString
        {
            get { return IngredientImportance.ToString(); }
        }

        public virtual List<HashTag> HashTags { get; set; }

    }

    public enum IngredientImportance
    {
        Main,
        Secondary
    }

    public enum IngredientType
    {
        Meat,
        Spice,
        Other
    }
}