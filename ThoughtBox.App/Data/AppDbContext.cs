using Microsoft.EntityFrameworkCore;
using ThoughtBox.App.Models;

namespace ThoughtBox.App.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public DbSet<Thought> Thoughts { get; set; }
        public DbSet<DeleteRequest> DeleteRequests { get; set; }
        public DbSet<Viewer> Viewers { get; set; }
    }
}
