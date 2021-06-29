using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestCurrencyAnalyzer.Application.Dtos;
using TestCurrencyAnalyzer.Application.Exceptions;

namespace TestCurrencyAnalyzer.Application.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException exceptionObj)
            {
                await HandleValidationExceptionAsync(context, exceptionObj);
            }
            catch (Exception exceptionObj)
            {
                await HandleExceptionAsync(context, exceptionObj);
            }
        }

        private Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            string result = new ValidationErrorDetailsDto((int)HttpStatusCode.BadRequest, exception.Message, exception.Errors).ToString();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(result);
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string result = new ErrorDetailsDto((int)HttpStatusCode.BadRequest, exception.Message).ToString();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(result);
        }
    }
}
