using System.Web.Mvc;

namespace BookYourFood
{
    public class NotificationActionFilterAttribute : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["Message"] = filterContext.Controller.TempData["Message"];
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }
    }
}