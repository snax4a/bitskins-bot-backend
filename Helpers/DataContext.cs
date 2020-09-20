using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public class DataContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<WhitelistedItem> WhitelistedItems { get; set; }
        public DbSet<Price> Prices { get; set; }

        private readonly IConfiguration Configuration;
        private readonly ILoggerFactory _loggerFactory;

        public DataContext(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connest to mysql database
            options.UseMySQL(Configuration.GetConnectionString("MYSQLDatabase")).UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Price>()
                .HasIndex(p => p.ItemName)
                .IsUnique();
        }
    }
}
