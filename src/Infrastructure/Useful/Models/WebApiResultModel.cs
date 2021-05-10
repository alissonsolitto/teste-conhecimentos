using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;

namespace Useful.ActionResult
{
    public class WebApiResultModel<TData>
    {
        public ReturnCode Code { get; set; }
        public string Message { get; set; }
        public TData Data { get; set; }
        public List<string> Validators { get; set; }

        public WebApiResultModel()
        {
            Code = ReturnCode.Success;
            Message = string.Empty;
            Data = default;
            Validators = new List<string>();
        }

        public WebApiResultModel(TData data, string message = "") : this()
        {
            Data = data;
            Message = message;
        }

        public WebApiResultModel(string message = "", Exception exception = null) : this()
        {
            Code = exception == null ? ReturnCode.Success : (exception is WarningException ? ReturnCode.Warning : ReturnCode.Error);
            Message = message;
        }

        public WebApiResultModel(Exception exception) : this()
        {
            Code = exception == null ? ReturnCode.Success : (exception is WarningException ? ReturnCode.Warning : ReturnCode.Error);
            Message = exception.Message;
        }

        public WebApiResultModel(List<string> validators) : this()
        {
            Data = default;
            Code = ReturnCode.Warning;
            Message = "Dados incorretos.";
            Validators = validators;
        }
    }

    public enum ReturnCode
    {
        Success = 0,
        Warning = 1,
        Error = 2
    }
}
