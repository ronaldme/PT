using Microsoft.EntityFrameworkCore;
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

        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutCalenderItem> WorkoutCalenderItem { get; set; }
    }
}