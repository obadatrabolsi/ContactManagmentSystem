using CMS.Core.Extensions;
using CMS.Core.Models;
using System.Net;

namespace CMS.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string result;

            context.Response.ContentType = "application/json";

            if (exception is BadHttpRequestException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                result = ApiResponse.Error(exception.Message).ToJson();
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                result = ApiResponse.Error(exception.Message).ToJson();

            }

            return context.Response.WriteAsync(result);
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}