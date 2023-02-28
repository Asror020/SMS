using SMSCore.Models.Entities;
using Microsoft.EntityFrameworkCore;
using SMS.DAL.Converters;

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
        public DbSet<Verification> Verifications => Set<Verification>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<SelectionItem> SelectionItems => Set<SelectionItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(builder =>
            {
                builder.Property(x => x.DateOfBirth).HasConversion<DateOnlyConverter, DateOnlyComparer>().HasColumnType("Date");
            });
        }
    }
}

