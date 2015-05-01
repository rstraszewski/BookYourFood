using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationUserDomain.Model;
using ReservationDomain.Model;
using Common.Service;
using Database;
using Utility;
using Identity.Model;

namespace ApplicationUserDomain.Service
{
    public class AutoCreatorService : ApplicationService, IAutoCreatorService
    {
        public AutoCreatorService(ByfDbContext byfDbContext)
            : base(byfDbContext)
        {
        }

        public List<CompleteMeal> GetPreferredMealsFor(string userId)
        {
            var List = new List<CompleteMeal>();

            // Get user preferences
            ApplicationUser entity = ByfDbContext.Users.Where(u => u.UserName == userId).SingleOrDefault();

            if (entity == null)
            {
                throw new NullReferenceException();
            }

            var answers = entity.UserAnswers;
            var userHashTagsIdsList = GetHashTagListFromUserAnswers(answers);
            return List;
        }

        private List<long> GetHashTagListFromUserAnswers(List<UserAnswer> answers)
        {
            var List = new List<long>() ;

            foreach(var a in answers)
            {
                var answer = ByfDbContext.Answers.Find(a.AnswerId);
                foreach (var h in answer.HashTags)
                {
                    List.Add(h.Id);
                }
            }

            return List;
        }
    }

    public interface IAutoCreatorService
    {
        List<CompleteMeal> GetPreferredMealsFor(string userId);
    }
}
