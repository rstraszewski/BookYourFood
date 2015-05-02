using System.Collections.Generic;
using System.Linq;
using Common.Service;
using Database;
using Identity.Model;
using Utility;

namespace ApplicationUserDomain.Service
{
    public class ApplicationUserService : ApplicationService, IApplicationUserService
    {
        public ApplicationUserService(ByfDbContext byfDbContext)
            : base(byfDbContext)
        {
        }

        public OperationResult AddUserAnswers(List<long> answersIds, string userId)
        {
            var entity = ByfDbContext.Users.Find(userId);

            if (entity == null)
            {
                return OperationResult.ErrorResult("User not found");
            }

            var answers = answersIds.Select(answer => new UserAnswer {AnswerId = answer}).ToList();
            entity.UserAnswers = answers;
            ByfDbContext.SaveChanges();

            return OperationResult.Success();
        }

        public List<UserAnswer> GetUserAnswers(string userId)
        {
            return ByfDbContext.Users.Find(userId).UserAnswers.ToList();
        }


        public List<string> GetUserPreferences(string userId)
        {
            var userPrefereces = new List<string>();
            var userAnswers = this.GetUserAnswers(userId);

            if(userAnswers == null)
            {
                return userPrefereces;
            }

            foreach(var a in userAnswers)
            {
                var hashTagsList = ByfDbContext.Answers.Find(a.AnswerId).HashTags.ToList();
                foreach(var h in hashTagsList)
                {
                    userPrefereces.Add(h.Name);
                }
            }

            return userPrefereces;
        }
    }

    public interface IApplicationUserService
    {
        OperationResult AddUserAnswers(List<long> answersIds, string userId);
        List<UserAnswer> GetUserAnswers(string userId);
        List<string> GetUserPreferences(string userId);
    }
}