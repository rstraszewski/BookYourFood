using Common.Model;

namespace Identity.Model
{
    public class UserMeal : Entity
    {
        public long MealId { get; set; }

        private UserMeal()
        {
            
        }

        public UserMeal(long mealId)
        {
            MealId = mealId;
        }
    }
}
