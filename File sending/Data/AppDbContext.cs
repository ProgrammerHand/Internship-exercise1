using File_sending.Models;
using File_sending.Repository;
using Microsoft.EntityFrameworkCore;

namespace File_sending.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts): base(opts)
        {
            
        }
        public DbSet<UserFileInfo> UserFileInfo { get; set; }
    }
}
