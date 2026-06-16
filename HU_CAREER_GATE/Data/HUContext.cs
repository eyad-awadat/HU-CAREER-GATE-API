using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HUCAREERGATE.Data
{
    public class HUContext:IdentityDbContext
    {
        public IConfiguration configuration;
        public HUContext(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Hr> Hrs { get; set; }
        public DbSet<HRTask> HRTasks { get; set; }
        public DbSet<TaskSubmission> taskSubmissions { get; set; }
        public DbSet<TaskQuestion> taskQuestions { get; set; }
        public DbSet<TaskViolation> TaskViolation { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("conn"));
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
            .Property(s => s.IsActive)
            .HasDefaultValue(true);

            modelBuilder.Entity<HRTask>()
            .Property(s => s.IsActive)
            .HasDefaultValue(true);

            modelBuilder.Entity<Hr>()
            .Property(h => h.IsActive)
             .HasDefaultValue(true);

            modelBuilder.Entity<Student>()
            .HasOne(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Hr>()
            .HasOne(h => h.User)
            .WithMany()
            .HasForeignKey(h => h.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
