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

        public int GetNumberOfQuestions()
        {
            return ByfDbContext.Questions.Count();
        }

        public List<Question> GetQuestions()
        {
            return ByfDbContext.Questions.ToList();
        }

        public List<long> GetHashTagsFromAnswers(List<long> answerIds)
        {
            var answers = ByfDbContext.Answers.Where(answer => answerIds.Contains(answer.Id));//answerIds.Select(answerId => ByfDbContext.Answers.Find(answerId));
            var hashTags = answers.SelectMany(answer => answer.HashTags).Select(hashtag => hashtag.Id).ToList();
            return hashTags;
        }
    }

    public interface IQuestionnaireSevice
    {
        int GetNumberOfQuestions();
        List<Question> GetQuestions();
        List<long> GetHashTagsFromAnswers(List<long> answerIds);
    }
}