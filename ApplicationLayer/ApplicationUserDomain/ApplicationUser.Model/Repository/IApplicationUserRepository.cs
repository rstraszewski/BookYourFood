using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Identity.Model;

namespace ApplicationUserDomain.Model.Repository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        void UpdateUser(ApplicationUser user);
        ApplicationUser GetUser(string userId);
        void RemoveUnassignedAnswers();
        IList<long> GetUserAnswers(string userId);
        void UpdateUserGraph(ApplicationUser user);
        List<long> GetUserFavouriteMeals(string userId);
    }
}
