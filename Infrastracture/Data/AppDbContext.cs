using Microsoft.EntityFrameworkCore;

namespace File_sending.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts): base(opts)
        {
            
        }
        public DbSet<Models.FileSpecs> UserFileInfo { get; set; }
    }
}
