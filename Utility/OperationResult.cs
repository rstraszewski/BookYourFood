﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
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

        public static OperationResult SuccessResult()
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
}
