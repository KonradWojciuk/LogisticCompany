namespace Web.Middleware
{
    public static class CustomHeadersMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomHeadersMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomHeadersMiddleware>();
        }
    }
}
