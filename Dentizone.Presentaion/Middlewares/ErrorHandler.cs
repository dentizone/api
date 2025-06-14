using Dentizone.Application.DTOs;
using Dentizone.Domain.Exceptions;
using System.Net;
using System.Text.Json;
using Microsoft.Data.SqlClient;

namespace Dentizone.Presentaion.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                Message = "An error occurred while processing your request.",
                Details = exception.Message,
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            // Map specific exceptions to custom status codes and messages
            switch (exception)
            {
                case NotFoundException notFoundException:
                    errorResponse.Message = notFoundException.Message;
                    errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case BadActionException badRequestException:
                    errorResponse.Message = badRequestException.Message;
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case UserLockedOutException userLockedOutException:
                    errorResponse.Message = userLockedOutException.Message;
                    errorResponse.StatusCode = (int)HttpStatusCode.Forbidden;
                    break;

                case UserAlreadyExistsException userAlreadyExistsException:
                    errorResponse.Message = userAlreadyExistsException.Message;
                    errorResponse.StatusCode = (int)HttpStatusCode.Conflict;
                    break;

                case SqlException expSqlException:
                    errorResponse.Message = "A database error occurred. Please try again later.";
                    errorResponse.Details = expSqlException.Message;
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;


                default:
                    errorResponse.Message = "An unexpected error occurred.";
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            response.StatusCode = errorResponse.StatusCode;

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            return response.WriteAsync(jsonResponse);
        }
    }
}