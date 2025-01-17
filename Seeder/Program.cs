﻿using Services;
using DataAccess;
using Domain.Entities;
using Domain;
using AutoMapper;

namespace Seeder
{
    public class Program
    {
        private static readonly long noEntities = 20;
        private static readonly long noBets = 10;
        private static readonly string connection_data = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";
        private static readonly IMapper mapper;

        public static void Main(string[] args)
        {
            populateBetableEntity();
            populateBets();
            populateBetsQuote();
        }

        private static void populateBetableEntity()
        {
            List<Task<BetableEntity>> tasks = new();
            DBContext context = new(connection_data);

            for (long i = 1; i <= noEntities; i++)
            {

                BetableEntityService service = new(context, mapper);
                BetableEntity entity = new BetableEntity { Name = $"Echipa {i * 12}" };
                context.BetableEntity.Add(entity);
            }
            context.SaveChanges();
        }

        private static void populateBets()
        {
            Random random = new Random();
            DBContext context = new(connection_data);
            BetableEntityService BetableEntityService = new(context, mapper);
            IEnumerable<BetableEntity> teams = context.BetableEntity.ToList();
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
                context.Bets.Add(newBet);
            }
            context.SaveChanges();
        }

        private static void populateBetsQuote()
        {
            DBContext context = new(connection_data);
            IEnumerable<Bets> bets = context.Bets.ToList();
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
                context.BetQuotes.Add(quote);
            }
            context.SaveChanges();
        }
    }
}