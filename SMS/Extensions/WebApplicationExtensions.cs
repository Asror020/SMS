using SMS.SeedData;

namespace SMS.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication UseMVC(this WebApplication app)
        {
            return app;
        }
        public static WebApplication UseSeedData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            scope.ServiceProvider.InitializeSeedData();

            return app;
        }
    }
}
