using MindMission.Application.Factories;
using MindMission.Application.Exceptions;

namespace MindMission.API.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = ResponseObjectFactory.CreateResponseObject(
                success: false,
                message: ex.Message,
                items: new List<string> { },
                pageNumber: 1,
                itemsPerPage: 1
            );

            switch (ex)
            {
                case ApiException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;

                case ValidationException e:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response = ResponseObjectFactory.CreateResponseObject(
                        success: false,
                        message: e.Message,
                        items: e.Errors,
                        pageNumber: 1,
                        itemsPerPage: e.Errors.Count
                    );
                    break;

                case KeyNotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            _logger.LogError(ex.Message);

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}