using bilog.Models;
using Microsoft.EntityFrameworkCore;

namespace bilog.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        
        public DbSet<BlogPost> BlogPosts { get; set; }
    }
}