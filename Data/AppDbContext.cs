using Microsoft.EntityFrameworkCore;

namespace GetAndFetchAPIs.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<CompetitorsSales>? CompetitorsSales { get; set; }
        public DbSet<CompetitorsStock>? CompetitorsStock { get; set; }
        public DbSet<Sales>? Sales { get; set; }
        public DbSet<Stock>? Stock { get; set; }
        public DbSet<StockSharePer>? StockSharePer { get; set; }
        public DbSet<ValueSharePer>? ValueSharePer { get; set; }
        
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

    }
}