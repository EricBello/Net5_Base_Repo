using Core.Exceptions;
using Core.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;


namespace Api.Middlewares
{
    public class LogsAndErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogsAndErrorHandlerMiddleware> _logger;

        public LogsAndErrorHandlerMiddleware(RequestDelegate next, ILogger<LogsAndErrorHandlerMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                await _next(context);
                sw.Stop();
                LogginDefault(context, sw);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                string message = (ex != null && ex.Message != null) ? ex.Message : "";
                message = (ex != null && ex.InnerException != null && ex.InnerException != null) ? " " + ex.InnerException.GetBaseException().Message : "";
                var responseModel = new Response<string>() { Succeeded = false, Message = message };

                switch (ex)
                {
                    case ApiException e:
                        //custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case ApiValidationException e:
                        //custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors;
                        break;
                    case KeyNotFoundException e:
                        //not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                sw.Stop();
                LogginError(context, sw, ex);

                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }

        private void LogginDefault(HttpContext context, Stopwatch sw)
        {
            var statusCode = context.Response?.StatusCode;
            string LogInfo = string.Format("HTTP {0} {1} responded {2} in {3} ms", context.Request.Method, context.Request.Path, statusCode, sw.Elapsed.TotalMilliseconds);
            _logger.LogInformation(LogInfo);
        }

        private void LogginError(HttpContext context, Stopwatch sw, Exception ex)
        {
            var statusCode = context.Response?.StatusCode;
            string errorMessage = (ex != null && ex.Message != null) ? ex.Message : "";
            string innerMessage = (ex != null && ex.InnerException != null && ex.InnerException != null) ? ex.InnerException.GetBaseException().Message : "";
            string LogInfo = string.Format("HTTP {0} {1} responded {2} in {3} ms, ex.Message=({4}, {5})", context.Request.Method, context.Request.Path, statusCode, sw.Elapsed.TotalMilliseconds, errorMessage, innerMessage);

            if (statusCode == (int)HttpStatusCode.InternalServerError) _logger.LogError(LogInfo, ex); else _logger.LogWarning(LogInfo, ex);

        }

    }
}
