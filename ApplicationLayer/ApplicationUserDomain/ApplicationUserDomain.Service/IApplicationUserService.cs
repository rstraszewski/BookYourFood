using System.Collections.Generic;
using Identity.Model;
using Utility;

namespace ApplicationUserDomain.Service
{
    public interface IApplicationUserService
    {
        OperationResult AddUserAnswers(List<long> answersIds, string userId);
        List<UserAnswer> GetUserAnswers(string userId);
        List<long> GetUserPreferences(string userId);
        void ChangeNameAndSurname(string userId, string name, string surname);
    }
}