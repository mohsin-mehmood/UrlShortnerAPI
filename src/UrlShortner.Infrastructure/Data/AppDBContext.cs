using Microsoft.EntityFrameworkCore;
using UrlShortner.Core.Entities;

namespace UrlShortner.Infrastructure.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Switch off auto increment
            //modelBuilder.Entity<ShortenedUrl>().Property(s => s.Id).ValueGeneratedNever();

        }
    }
}
