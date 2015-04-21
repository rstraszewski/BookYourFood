using System.Collections.Generic;
using System.Web.Mvc;

namespace Utility
{
    public static class ControllerExtension
    {
        public static void FlashMessage(this Controller controller, MessageResult msg)
        {
            controller.TempData["Message"] = msg;
        }

        public static void AddFlashMessage(this Controller controller, MessageResult msg)
        {
            if (controller.TempData["Messages"] != null)
            {
                ((List<MessageResult>)controller.TempData["Messages"]).Add(msg);
            }
            else
            {
                controller.TempData["Messages"] = new List<MessageResult> {msg};
            }
        }

        public static void AddFlashMessage(this Controller controller, string msg,
            MessageType type = MessageType.Success)
        {
            AddFlashMessage(controller, MessageResult.Create(msg, type));
        }

        public static void FlashMessage(this Controller controller, string msg, MessageType type = MessageType.Success)
        {
            controller.TempData["Message"] = MessageResult.Create(msg, type);
        }

        public static JsonResult JsonOperationResult(this Controller controller, object data, MessageResult message,
            JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
        {
            var result =
                new
                {
                    result = data,
                    message = new {content = message.Message, type = message.MessageType.GetDescription()}
                };

            return new JsonResult
            {
                Data = result,
                ContentType = null,
                ContentEncoding = null,
                JsonRequestBehavior = behavior
            };
        }

        public static JsonResult JsonOperationResult(this Controller controller, MessageResult message,
            JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
        {
            var result =
                new
                {
                    message = new {content = message.Message, type = message.MessageType.GetDescription()}
                };

            return new JsonResult
            {
                Data = result,
                ContentType = null,
                ContentEncoding = null,
                JsonRequestBehavior = behavior
            };
        }
    }
}