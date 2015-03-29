using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class MessageResult
    {
        public string Message { get; set; }
        public MessageType MessageType { get; set; }

        private MessageResult(string msg, MessageType type)
        {
            Message = msg;
            MessageType = type;
        }

        public static MessageResult Create(string msg, MessageType type = MessageType.Success)
        {
            return new MessageResult(msg, type);
        }
    }

    public enum MessageType
    {
        [Description("success")]
        Success,
        [Description("success")]
        Info,
        [Description("success")]
        Error,
        [Description("success")]
        Warning
    }
    public class OperationResult
    {
        private OperationResult()
        {
            Success = true;
            Errors = new List<string>();
        }

        public bool Success { get; set; }

        public List<string> Errors { get; private set; }

        public void AddError(string error)
        {
            Success = false;
            Errors.Add(error);
        }

        public static OperationResult CreateResult()
        {
            return new OperationResult();
        }

        public static OperationResult ErrorResult(string error)
        {
            var temp = new OperationResult();
            temp.AddError(error);
            return temp;
        }
    }
    public class OperationResult<T>
    {
        private OperationResult(T result)
        {
            Success = true;
            Errors = new List<string>();
            Result = result;
        }

        public bool Success { get; set; }
        public T Result { get; set; }

        public List<string> Errors { get; private set; }

        public void AddError(string error)
        {
            Success = false;
            Errors.Add(error);
        }

        public static OperationResult<T> CreateResult(T result)
        {
            return new OperationResult<T>(result);
        }

        public static OperationResult<T> ErrorResult(string error, T result)
        {
            var temp = new OperationResult<T>(result);
            temp.AddError(error);
            return temp;
        }
    }
}
