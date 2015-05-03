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
            entity.UserAnswers.RemoveAll(a => true);
            entity.UserAnswers = answers;
            ByfDbContext.SaveChanges();
            ByfDbContext.Database.ExecuteSqlCommand("DELETE FROM UserAnswer WHERE ApplicationUser_Id IS NULL");
            return OperationResult.Success();
        }

        public List<UserAnswer> GetUserAnswers(string userId)
        {
            return ByfDbContext.Users.Find(userId).UserAnswers.ToList();
        }

        public void ChangeNameAndSurname(string userId, string name, string surname)
        {
            var user = ByfDbContext.Users.Find(userId);
            user.Name = name;
            user.Surname = surname;
            ByfDbContext.SaveChanges();
        }


        public List<long> GetUserPreferences(string userId)
        {
            var userAnswerList = this.GetUserAnswers(userId);
            //List<long> userAnswersIdList = new List<long>();

            /*if(userAnswersList == null)
            {
                return userAnswersIdList;
            }

            foreach(var a in userAnswersList)
                {
                userAnswersIdList.Add(a.AnswerId);
            }*/

            var userAnswerIdList = userAnswerList.Select(answer => answer.AnswerId).ToList();

            return userAnswerIdList;
        }
    }

    public interface IApplicationUserService
    {
        OperationResult AddUserAnswers(List<long> answersIds, string userId);
        List<UserAnswer> GetUserAnswers(string userId);
        List<long> GetUserPreferences(string userId);
        void ChangeNameAndSurname(string userId, string name, string surname);
        //List<string> GetUserPreferences(string userId);
    }
}