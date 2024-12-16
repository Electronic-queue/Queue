using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Queue.Application.Common.Behaviors;
using Queue.Application.Common.Exceptions;

namespace Queue.WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionHandlerMiddleware(RequestDelegate next) =>
            _next = next;
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code=HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch(ex)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.ValidationResult);
                    break;

                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode=(int)code;

            if(result==string.Empty)
            {
                result=JsonSerializer.Serialize(new {error=ex.Message});
            }
            return context.Response.WriteAsync(result);
        }
    }
}
