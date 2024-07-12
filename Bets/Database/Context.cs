using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bets.Constants;

namespace Bets.Database
{
    public class Context : DbContext
    {
        public DbSet<BetableEntity> BetableEntity { get; set; }
        public DbSet<Bets> Bets{ get; set;}
        public DbSet<BetQuotes> BetQuotes { get; set; }
        public DbSet<PlacedBets> PlacedBets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Constants.Constants.connection_data);
        }
    }

    public class BetableEntity
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
    }

    public class Bets
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Int64 BetableEntityA { get; set; }
        public Int64 BetableEntityB { get; set; }
    }

    public class BetQuotes
    {
        public Int64 Id { get; set; }
        public Int64 BetId { get; set; }
        public decimal QuoteA { get; set; } 
        public decimal QuoteB { get; set; }
        public decimal QuoteX { get; set; }
    }

    public class PlacedBets
    {
        public Int64 Id { get; set; }
        public Int64 UserId { get; set; }
        public DateTime PlacedDate { get; set; } = DateTime.Now;

        public string Type { get; set; }
        public Int64 QuoteId { get; set; }
    }
}
