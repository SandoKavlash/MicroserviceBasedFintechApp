using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Exceptions.Base;

namespace MicroserviceBasedFintechApp.OrderService.Api.Middlewares
{
    public class DomainErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public DomainErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch(BaseException domainException)
            {
                context.Response.StatusCode = domainException.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { Message = domainException.Message });

            }
            catch (Exception exception)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { Message = exception.Message });
            }
        }
    }
}
