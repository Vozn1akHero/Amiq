using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Amiq.ApiGateways.WebApp.Core
{
    public class RequestCancellationMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestCancellationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(context.RequestAborted))
            {
                cancellationTokenSource.Cancel();
                context.RequestAborted = cancellationTokenSource.Token;
                await _next(context);
            }
        }
    }
}
