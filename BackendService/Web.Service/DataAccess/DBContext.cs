using Microsoft.EntityFrameworkCore;
using Web.Service.DataAccess.Entity;

namespace Web.Service.DataAccess
{
    public class DBContext : DbContext
    {
        public DbSet<Video>? Videos { get; set; }
        public DBContext(DbContextOptions<DBContext> options)
          : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
