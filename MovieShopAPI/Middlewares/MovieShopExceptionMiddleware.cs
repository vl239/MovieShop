using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MovieShopAPI.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MovieShopExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MovieShopExceptionMiddleware> _logger;

        public MovieShopExceptionMiddleware(RequestDelegate next, ILogger<MovieShopExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                // read info from the HttpContext object and log it somewhere
                _logger.LogInformation("Inside the middleware");
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // First catch the exception
                // Then check the exception type, message
                // Then check stacktrace, where/when the exception happened, for which URL and Http method (controller, action method), for which user is user logged in
                // Save all this info somewhere -> text/json files or database
                // Can use System.IO to create text files
                // Asp.net core has builtin logging mechanism (ILogger), which can be used by any 3rd party log provider
                // Serilog (most popular library), NLog
                // Send email to Dev Team with exception at occurence

                var exceptionDetails = new ErrorModel
                {
                    Message = ex.Message,
                    //StackTrace = ex.StackTrace,
                    ExceptionDateTime = DateTime.UtcNow,
                    //ExceptionType = ex.GetType().ToString(),
                    Path = httpContext.Request.Path,
                    HttpRequestType = httpContext.Request.Method,
                    User = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : null
                };


                _logger.LogError("Exception happened, log this to text or Json file using SeriLog");

                // return http status code 500
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var result = JsonSerializer.Serialize<ErrorModel>(exceptionDetails);
                await httpContext.Response.WriteAsync(result);

                // MVC pseudocode
                // httpContext.Response.Redirect("/home/error")
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MovieShopExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseMovieShopExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MovieShopExceptionMiddleware>();
        }
    }
}

