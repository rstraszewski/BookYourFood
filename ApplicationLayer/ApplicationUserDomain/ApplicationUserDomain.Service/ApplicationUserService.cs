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

        public List<long> GetUserPreferences(string userId)
        {
            var userAnswersList = this.GetUserAnswers(userId);
            List<long> userAnswersIdList = new List<long>();

            if(userAnswersList == null)
            {
                return userAnswersIdList;
            }

            foreach(var a in userAnswersList)
            {
                userAnswersIdList.Add(a.AnswerId);
            }

            return userAnswersIdList;
        }
    }

    public interface IApplicationUserService
    {
        OperationResult AddUserAnswers(List<long> answersIds, string userId);
        List<UserAnswer> GetUserAnswers(string userId);
        List<long> GetUserPreferences(string userId);
    }
}