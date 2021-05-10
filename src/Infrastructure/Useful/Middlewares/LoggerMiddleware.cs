using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using Useful.ActionResult;
using Useful.Exceptions;

namespace Useful.Interfaces.Middlewares
{
    public class LoggerMiddleware : IMiddleware
    {
        private readonly ILogger<LoggerMiddleware> _logger;

        public LoggerMiddleware(ILogger<LoggerMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                _logger.LogInformation(context.Request.ToString());
                await next(context);
            }
            catch (Exception ex)
            {
                var idError = Guid.NewGuid();
                var messageLog = $"Error ID: {idError}. {ex.Message}";

                switch (ex)
                {
                    case WarningException:
                        _logger.LogWarning(messageLog);
                        break;
                    default:
                        _logger.LogError(messageLog);
                        break;
                }

                var result = new WebApiResultModel<object>($"Ocorreu um erro no serviço, contate o administrador do sistema para mais detalhes. Error ID: {idError}");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }
    }
}
