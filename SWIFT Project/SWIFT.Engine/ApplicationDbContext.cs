using Microsoft.EntityFrameworkCore;
using SWIFT.Models;

namespace SWIFT.Engine
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<FileInfo> FileInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=SWIFT;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
    }
}
