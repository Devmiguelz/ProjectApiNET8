using PruebaAnnarApi.Application.Exceptions; 
using System.Net; 
using System.Text.Json; 
namespace PruebaAnnarApi.Middleware 
{ 
    public class ErrorHandlingMiddleware 
    { 
        private readonly RequestDelegate _next; 
        private readonly ILogger<ErrorHandlingMiddleware> _logger; 
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger) 
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
                _logger.LogError(ex, "An unhandled exception has occurred."); 
                await HandleExceptionAsync(context, ex); 
            } 
        } 
        private Task HandleExceptionAsync(HttpContext context, Exception exception) 
        { 
            var response = context.Response; 
            response.ContentType = "application/json"; 
            var mensajeError = new MessageError 
            { 
                Message = exception.Message 
            }; 
            switch (exception) 
            { 
                case NotFoundException _: 
                    response.StatusCode = (int)HttpStatusCode.NotFound; 
                    mensajeError.StatusCode = response.StatusCode; 
                    mensajeError.Description = "The requested resource was not found."; 
                    break; 
                case ValidationException _: 
                    response.StatusCode = (int)HttpStatusCode.BadRequest; 
                    mensajeError.StatusCode = response.StatusCode; 
                    mensajeError.Description = "There was a validation error."; 
                    break; 
                case UnauthorizedException _: 
                    response.StatusCode = (int)HttpStatusCode.Unauthorized; 
                    mensajeError.StatusCode = response.StatusCode; 
                    mensajeError.Description = "You are not authorized to perform this action."; 
                    break; 
                case ConflictException _: 
                    response.StatusCode = (int)HttpStatusCode.Conflict; 
                    mensajeError.StatusCode = response.StatusCode; 
                    mensajeError.Description = "There was a conflict with the current state of the resource."; 
                    break; 
                default: 
                    response.StatusCode = (int)HttpStatusCode.InternalServerError; 
                    mensajeError.StatusCode = response.StatusCode; 
                    mensajeError.Description = "An unexpected error occurred."; 
                    break; 
            } 
            var result = JsonSerializer.Serialize(mensajeError); 
            return response.WriteAsync(result); 
        } 
    } 
} 
