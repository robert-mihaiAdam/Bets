using Services;
using DataAccess;
using Domain;
using Constants;

namespace Seeder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            populateBetableEntity();
            populateBets();
            populateBetsQuote();
        }

        private static void populateBetableEntity()
        {
            long noEntities = Constants.Constants.noEntities;
            List<Task<BetableEntity>> tasks = new();
            DBContext context = new(Constants.Constants.connection_data);

            for (long i = 1; i <= noEntities; i++)
            {
                BetableEntityServices service = new(context);
                BetableEntity entity = new BetableEntity { Name = $"Echipa {i * 12}" };
                context.betableEntity.Add(entity);
            }
            context.SaveChanges();
        }

        private static void populateBets()
        {
            long noBets = Constants.Constants.noBets;
            Random random = new Random();
            DBContext context = new(Constants.Constants.connection_data);
            BetableEntityServices BetableEntityService = new(context);
            IEnumerable<BetableEntity> teams = context.betableEntity.ToList();
            int noEntities = teams.Count();

            for (long i = 1; i <= noBets; i++)
            {
                int firstTeamindex = random.Next(noEntities);
                Guid firstTeamId = teams.ElementAt(firstTeamindex).Id;
                int secondTeamindex = random.Next(noEntities);
                Guid secondTeamId = teams.ElementAt(secondTeamindex).Id;

                while (firstTeamId == secondTeamId)
                {
                    secondTeamindex = random.Next(noEntities);
                    secondTeamId = teams.ElementAt(secondTeamindex).Id;
                }

                Bets newBet = new Bets
                {
                    Name = $"Meciul {i}",
                    BetableEntityA = firstTeamId,
                    BetableEntityB = secondTeamId
                };
                context.bets.Add(newBet);
            }
            context.SaveChanges();
        }

        private static void populateBetsQuote()
        {
            DBContext context = new(Constants.Constants.connection_data);
            IEnumerable<Bets> bets = context.bets.ToList();
            long noBets = bets.Count();
            Random random = new Random();

            foreach (Bets bet in bets)
            {
                decimal quoteA = random.Next(100, 1000) / 100m;
                decimal quoteB = random.Next(100, 1000) / 100m;
                decimal quoteX = random.Next(100, 1000) / 100m;
                BetQuotes quote = new BetQuotes { 
                    BetId = bet.Id,
                    QuoteA = quoteA,
                    QuoteB = quoteB,
                    QuoteX = quoteX
                };
                context.betQuotes.Add(quote);
            }
            context.SaveChanges();
        }
    }
}