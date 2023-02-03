using Newtonsoft.Json;
using SMS.DAL.Data;
using SMSCore.Models.Entities;

namespace SMS.SeedData
{
    public static class SeedData
    {
        public static void InitializeSeedData(this IServiceProvider serviceProvider)
        {
            var hostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (dbContext == null)
                throw new InvalidOperationException();

            SeedEmailTemplates(dbContext, hostEnvironment);
        }

        private static void SeedEmailTemplates(ApplicationDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            if (dbContext.EmailTemplates.Any())
                return;

            var emailTemplatesData = JsonConvert
                .DeserializeObject<IEnumerable<EmailTemplate>>
                (File.ReadAllText(Path.Combine(hostEnvironment.ContentRootPath, "SeedData", "Data", "EmailTemplates.json")))!;

            dbContext.EmailTemplates.AddRange(emailTemplatesData.Select(x => new EmailTemplate
            {
                Name = x.Name,
                Subject = x.Subject,
                Body = JsonConvert.DeserializeObject<string>(x.Body)!,
            }));

            dbContext.SaveChanges();
        }
    }
}
