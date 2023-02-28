using SMS.SeedData;

namespace SMS.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication UseArea(this WebApplication app)
        {
            app.MapControllerRoute
                (
                name: "MyArea",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

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
