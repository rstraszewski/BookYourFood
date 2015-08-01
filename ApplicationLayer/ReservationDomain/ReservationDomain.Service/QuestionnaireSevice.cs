using System.Collections.Generic;
using System.Linq;
using Common.Service;
using ReservationDomain.Infrastructure;
using ReservationDomain.Model;

namespace Reservaton.Service
{
    public class QuestionnaireSevice : ApplicationService<ReservationDomainDbContext>, IQuestionnaireSevice
    {
        public QuestionnaireSevice(ReservationDomainDbContext context)
            : base(context)
        {
        }

        public int GetNumberOfQuestions()
        {
            return _context.Questions.Count();
        }

        public List<Question> GetQuestions()
        {
            return _context.Questions.ToList();
        }

        public List<long> GetHashTagsFromAnswers(List<long> answerIds)
        {
            var answers = _context.Answers.Where(answer => answerIds.Contains(answer.Id));//answerIds.Select(answerId => _context.Answers.Find(answerId));
            var hashTags = answers.SelectMany(answer => answer.HashTags).Select(hashtag => hashtag.Id).ToList();
            return hashTags;
        }
    }
}