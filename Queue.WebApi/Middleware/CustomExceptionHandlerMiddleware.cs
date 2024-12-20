using FluentValidation;
using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Queue.Domain.Common.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Queue.WebApi.Middleware;

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
        catch (FluentValidation.ValidationException ex)
        {
            var result = JsonSerializer.Serialize(ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(result);
        }
        catch (Exception ex)
        {
            var result = JsonSerializer.Serialize(ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(result);
        }
    }

    //private Task HandleExceptionAsync(HttpContext context, Exception ex)
    //{
    //    var code = HttpStatusCode.InternalServerError;
    //    var result = string.Empty;
    //    switch (ex)
    //    {

    //        case NotFoundException:
    //            code = HttpStatusCode.NotFound;
    //            break;
    //    }
    //    context.Response.ContentType = "application/json";
    //    context.Response.StatusCode = (int)code;

    //    if (result == string.Empty)
    //    {
    //        result = JsonSerializer.Serialize(new { error = ex.Message });
    //    }
    //    return context.Response.WriteAsync(result);
    //}
}
