using Microsoft.EntityFrameworkCore;
using Domain.Dto;

namespace DataAccess
{
    public class DBContext : DbContext
    {
        public DbSet<Bets> Bets { get; set; }
        public DbSet<BetQuotes> BetQuotes { get; set; }
        public DbSet<BetableEntity> BetableEntity { get; set; }
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

    }
}
