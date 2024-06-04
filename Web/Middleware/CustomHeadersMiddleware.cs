namespace Web.Middleware
{
    public class CustomHeadersMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public CustomHeadersMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Response.OnStarting(() =>
            {
                httpContext.Response.Headers["X-Custom-Header"] = "Custom header";
                return Task.CompletedTask;
            });

            await _requestDelegate(httpContext);
        }
    }
}
