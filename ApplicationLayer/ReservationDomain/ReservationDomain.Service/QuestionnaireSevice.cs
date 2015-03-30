using System.Collections.Generic;
using System.Linq;
using Common.Service;
using Database;
using ReservationDomain.Model;

namespace Reservaton.Service
{
    public class QuestionnaireSevice : ApplicationService, IQuestionnaireSevice
    {
        public QuestionnaireSevice(ByfDbContext byfDbContext)
            : base(byfDbContext)
        {
        }

        public List<Question> GetQuestions()
        {
            return ByfDbContext.Questions.ToList();
        }
    }

    public interface IQuestionnaireSevice
    {
        List<Question> GetQuestions();
    }
}