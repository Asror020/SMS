using SMSCore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace SMS.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<University> Universities => Set<University>();
        public DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();
        public DbSet<Email> Emails => Set<Email>();
        public DbSet<Verification> verifications => Set<Verification>();
    }
}

