using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PT.DAL
{
    public class PtDbContextDesignTimeFactory : IDesignTimeDbContextFactory<PtDbContext>
    {
        public PtDbContext CreateDbContext(string[] args)
        {
            return new PtDbContext(new DbContextOptionsBuilder()
                .UseSqlServer("Server=(local);Database=PT;Integrated Security=True").Options);
        }
    }
}