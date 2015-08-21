using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ApplicationUserBC.Interfaces;
using Microsoft.AspNet.Identity;
using ReservationDomain.Model;
using Reservaton.Service;
using Utility;

namespace BookYourFood.Controllers
{
    public class QuestionaireViewModel
    {
        public List<Question> Questions { get; set; }
        public List<long> Answers { get; set; }
    }

    [Authorize]
    public class QuestionaireController : Controller
    {
        private readonly IQuestionnaireSevice questionnaireSevice;
        private readonly IApplicationUserService applicationUserService;

        public QuestionaireController(IQuestionnaireSevice questionnaireSevice, 
            IApplicationUserService applicationUserService)
        {
            this.questionnaireSevice = questionnaireSevice;
            this.applicationUserService = applicationUserService;
        }

        // GET: Questionaire
        public ActionResult Index()
        {
            this.FlashMessage(MessageResult.Create(
                "You need to complete questionaire, so we can predict your desires!", MessageType.Info));
            var questions = questionnaireSevice.GetQuestions();
            var userAnswers = applicationUserService.GetUserAnswers(User.Identity.GetUserId());
            var model = new QuestionaireViewModel() {Answers = userAnswers, Questions = questions};
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(List<long> answerIds)
        {
            var questions = questionnaireSevice.GetQuestions();
            var model = new QuestionaireViewModel() { Answers = answerIds, Questions = questions };
            var numberOfQuestions = questionnaireSevice.GetNumberOfQuestions();

            if (answerIds == null || answerIds.Count < numberOfQuestions)
            {
                this.FlashMessage(MessageResult.Create("You didn't answer all questions!", MessageType.Error));
                return View(model);
            }

            var result = applicationUserService.AddUserAnswers(answerIds, User.Identity.GetUserId());
            
            if(result.IsSuccessful)
            {
                this.FlashMessage(MessageResult.Create("Questionaire was completes successfuly!"));
                return RedirectToAction("Index", "Home");
            }

            this.FlashMessage(MessageResult.Create(result.Errors.Last(), MessageType.Error));
            return View(model);
        }
    }
}