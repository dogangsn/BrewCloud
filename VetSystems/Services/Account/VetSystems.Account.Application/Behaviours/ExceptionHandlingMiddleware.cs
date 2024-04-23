using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;

namespace VetSystems.Account.Application.Behaviours
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = 500; 
 
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            var response = new Response<string>
            {
                ResponseType = ResponseType.Error,
                IsSuccessful = false,
                Data = exception.Message,
                Errors = GetErrors(exception)
            };
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }


        private static List<string> GetErrors(Exception ex)
        {
            List<string> errors = null;
            if (ex is ValidationException validationException)
            {
                //errors = validationException.Errors.Select(r => r.ErrorMessage).ToList();
            }
            else
            {
                errors = new List<string> { ex.InnerException == null ? ex.Message : ex.InnerException.Message };
            }
            return errors;
        }
    }
}
