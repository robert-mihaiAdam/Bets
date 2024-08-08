using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace DataAccess
{
    public class DBContext : DbContext
    {
        public DbSet<Bets> Bets { get; set; }
        public DbSet<BetQuotes> BetQuotes { get; set; }
        public virtual DbSet<BetableEntity> BetableEntity { get; set; }
        public DbSet<PlacedBets> PlacedBets { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DBContext(string connectionString): base(GetOptions(connectionString))
        {
        
        }

        private static DbContextOptions<DBContext> GetOptions(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return optionsBuilder.Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BetQuotes>()
                .Property(quote => quote.QuoteA)
                .HasPrecision(5,2);
            modelBuilder.Entity<BetQuotes>()
                .Property(quote => quote.QuoteB)
                .HasPrecision(5, 2);
            modelBuilder.Entity<BetQuotes>()
                .Property(quote => quote.QuoteX)
                .HasPrecision(5, 2);
        }

    }
}
