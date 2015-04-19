using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Utility;

namespace BookYourFood
{
    public class NotificationActionFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var msg = filterContext.Controller.TempData["Message"] as MessageResult;
            if (msg != null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    var result = filterContext.Result as JsonResult;
                    if (result != null)
                    {
                        result.Data = new
                        {
                            result = result.Data,
                            message = new {content = msg.Message, type = msg.MessageType.GetDescription()}
                        };
                    }
                }
                else
                {
                    filterContext.Controller.ViewData["Message"] = msg;
                }
            }

            var msgs = filterContext.Controller.TempData["Messages"] as List<MessageResult>;
            if (msgs != null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    var result = filterContext.Result as JsonResult;
                    if (result != null)
                    {
                        result.Data = new
                        {
                            result = result.Data,
                            messages = msgs.Select( m => new { content = m.Message, type = m.MessageType.GetDescription() })
                        };
                    }
                }
                else
                {
                    filterContext.Controller.ViewData["Messages"] = msgs;
                }
            }
        }
    }
}