using Microsoft.EntityFrameworkCore;

namespace CRUDelicious2.Models
{
    public class YourContext : DbContext
    {
        public YourContext (DbContextOptions<YourContext> options) : base(options) {}
        public DbSet<Dishes> dishes { get; set; }
    }
}