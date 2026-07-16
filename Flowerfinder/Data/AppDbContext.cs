using Microsoft.EntityFrameworkCore;
using Flowerfinder.Models;

namespace Flowerfinder.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Flower> Flowers { get; set; }  // this becomes the "Flowers" table
        public DbSet<IdentifyRecord> IdentifyRecords { get; set; }  // saved photo identifications
    }
}
