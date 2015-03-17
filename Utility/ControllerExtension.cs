using System.Web.Mvc;
using Utility;

namespace Utility
{
    public static class ControllerExtension
    {
        public static void FlashMessage(this Controller controller, MessageResult msg)
        {
            controller.TempData["Message"] = msg;
        }

        public static JsonResult JsonOperationResult(this Controller controller, object data, MessageResult message, JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
        {
            var result =
                new
                {
                    result = data,
                    message = new { content = message.Message, type = message.MessageType.GetDescription() }
                };

            return new JsonResult()
            {
                Data = result,
                ContentType = null,
                ContentEncoding = null,
                JsonRequestBehavior = behavior
            };
        }
    }

    
}