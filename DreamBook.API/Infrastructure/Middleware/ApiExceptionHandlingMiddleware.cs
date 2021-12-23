using DreamBook.API.Responses;
using DreamBook.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace DreamBook.API.Infrastructure.Middleware
{
    public class ApiExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionHandlingMiddleware> _logger;

        public ApiExceptionHandlingMiddleware(RequestDelegate next, ILogger<ApiExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

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

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var errorResponse = new ErrorResponse()
            {
                Error = ex.Message,
                Status = ex switch
                {
                    EntityNotFoundException => (int)HttpStatusCode.NotFound,
                    BusinessLogicException or EntityCanNotBeDeletedExxeption or BadHttpRequestException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError
                },
                Title = ex switch
                {
                    IValidaionException => "One or more validation errors occurred.",
                    BadHttpRequestException => "Bad request",
                    _ => "Internal Server Error."
                }
            };

            context.Response.StatusCode = errorResponse.Status;
            var result = JsonSerializer.Serialize(errorResponse);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }
    }
}
