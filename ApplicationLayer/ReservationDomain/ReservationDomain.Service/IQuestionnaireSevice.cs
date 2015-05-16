using System.Collections.Generic;
using ReservationDomain.Model;

namespace Reservaton.Service
{
    public interface IQuestionnaireSevice
    {
        int GetNumberOfQuestions();
        List<Question> GetQuestions();
        List<long> GetHashTagsFromAnswers(List<long> answerIds);
    }
}