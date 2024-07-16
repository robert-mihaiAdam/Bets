using Microsoft.EntityFrameworkCore;
using Domain;

namespace DataAccess
{
    public class DBContext : DbContext
    {
        public DbSet<Bets> bets { get; set; }
        public DbSet<BetQuotes> betQuotes { get; set; }
        public DbSet<BetableEntity> betableEntity { get; set; }
        public DbSet<PlacedBets> placedBets { get; set; }

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

    }
}
