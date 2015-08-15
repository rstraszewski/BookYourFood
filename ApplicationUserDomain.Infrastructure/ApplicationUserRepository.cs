using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationUserDomain.Model.Repository;
using Common.Infrastructure;
using Common.Model;
using Identity.Model;

namespace ApplicationUserDomain.Infrastructure
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(ApplicationUserDomainDbContext context) : base(context)
        {
        }

        public void UpdateUser(ApplicationUser user)
        {
            _dbSet.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public ApplicationUser GetUser(string userId)
        {
            return _dbSet.Find(userId);
        }

        public void RemoveUnassignedAnswers()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM UserAnswer WHERE ApplicationUser_Id IS NULL");
        }

        public IList<long> GetUserAnswers(string userId)
        {
            return _dbSet.Find(userId).UserAnswers.Select(c => c.AnswerId).ToList();
        }

        public void UpdateUserGraph(ApplicationUser user)
        {
            _dbSet.AddOrUpdate();
            _context.SaveChanges();
        }

        public List<long> GetUserFavouriteMeals(string userId)
        {
            return _dbSet.Find(userId).FavouriteMeals.Select(meal => meal.MealId).ToList();
        }
    }
}
