using Microsoft.EntityFrameworkCore;
using PT.DAL.Configurations;
using PT.DAL.Entities;

namespace PT.DAL
{
    // dotnet tool restore
    // dotnet ef migrations add Initial_Create -p PT.DAL
    // dotnet ef database update -p PT.DAL -s PT.Setup
    public class PtDbContext : DbContext
    {
        public PtDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WorkoutConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutCalenderItem> WorkoutCalenderItem { get; set; }
    }
}