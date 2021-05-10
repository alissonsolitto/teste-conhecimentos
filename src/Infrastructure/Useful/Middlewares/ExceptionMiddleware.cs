using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Net;
using Useful.ActionResult;

namespace Useful.Interfaces.Middlewares
{
    public static class ExceptionMiddleware
    {
        public static Action<IApplicationBuilder> ExceptionMiddlewareHaldler(ILogger logger)
        {
            return error =>
            {
                error.Run(async context =>
                {
                    try
                    {
                        var exception = context.Features.Get<IExceptionHandlerFeature>();
                        var idError = Guid.NewGuid();

                        if(exception != null)
                        {
                            var messageLog = $"ID: {idError} | StatusCode: {(int)HttpStatusCode.InternalServerError} | Path: {context.Request.Path} | Message: {exception.Error.Message} | StackTrace: {exception.Error.StackTrace}";

                            switch (exception.Error)
                            {
                                case WarningException:
                                    logger.LogWarning(messageLog);
                                    break;
                                default:
                                    logger.LogError(messageLog);
                                    break;
                            }
                        }
                        
                        var result = new WebApiResultModel<object>($"Ocorreu um erro no serviço. Error ID: {idError}", exception.Error);
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex.Message);
                    }
                });
            };
        }
    }
}
