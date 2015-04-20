using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ApplicationUserDomain.Service;
using Microsoft.AspNet.Identity;
using Reservaton.Service;
using Utility;

namespace BookYourFood.Controllers
{
    public class QuestionaireController : Controller
    {
        private readonly IQuestionnaireSevice questionnaireSevice;
        private readonly IApplicationUserService applicationUserService;

        private readonly ApplicationUserManager userManager;
        public QuestionaireController(IQuestionnaireSevice questionnaireSevice, IApplicationUserService applicationUserService, ApplicationUserManager userManager)
        {
            this.questionnaireSevice = questionnaireSevice;
            this.applicationUserService = applicationUserService;
            this.userManager = userManager;
        }

        // GET: Questionaire
        public ActionResult Index()
        {
            this.FlashMessage(MessageResult.Create(
                "You need to complete questionaire, so we can predict your desires!", MessageType.Info));
            var questions = questionnaireSevice.GetQuestions();
            return View(questions);
        }

        [HttpPost]
        public ActionResult Index(List<long> answerIds, List<long> numbers)
        {
            var questions = questionnaireSevice.GetQuestions();
            var numberOfQuestions = questionnaireSevice.GetNumberOfQuestions();

            if (answerIds == null || answerIds.Count < numberOfQuestions)
            {
                this.FlashMessage(MessageResult.Create("You didn't answer all questions!", MessageType.Error));
                return View(questions);
            }

            var user = userManager.FindByName(User.Identity.Name);
            var result = applicationUserService.AddUserAnswers(answerIds, user.Id);
            
            if(result.IsSuccessful)
            {
                this.FlashMessage(MessageResult.Create("Questionaire was completes successfuly!"));
                return RedirectToAction("Index", "Home");
            }

            this.FlashMessage(MessageResult.Create(result.Errors.Last(), MessageType.Error));
            return View(questions);
        }
    }
}