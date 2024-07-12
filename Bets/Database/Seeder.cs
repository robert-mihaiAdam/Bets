using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
using Bets.Constants;
using Bets.Utils;


namespace Bets.Database
{
    [Migration(121)]
    public class Seeder : Migration
    {

        public override void Up()
        {
            createTables();
            populateDB();
        }

        public override void Down()
        {
            dropTables();
        }

        private void populateDB()
        {
            seedBetableEntity();
            seedBets();
            seedBetQuotes();
        }
        private void createTables()
        {
            createBetableEntity();
            createBets();
            createBetQuotes();
            createPlacedBets();
        }

        private void dropTables()
        {
            Delete.Table("BetableEntity");
            Delete.Table("Bets");
            Delete.Table("BetQuotes");
            Delete.Table("PlacedBets");
        }

        private void createBetableEntity()
        {
            Create.Table("BetableEntity")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString();

        }

        private void createBets()
        {
            Create.Table("Bets")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Date").AsDateTime().WithDefaultValue(DateTime.Now)
                .WithColumn("BetableEntityA").AsInt64()
                .WithColumn("BetableEntityB").AsInt64();
        }

        private void createBetQuotes()
        {
            Create.Table("BetQuotes")
                 .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                 .WithColumn("BetId").AsInt64()
                 .WithColumn("QuoteA").AsDecimal()
                 .WithColumn("QuoteB").AsDecimal()
                 .WithColumn("QuoteX").AsDecimal();
        }

        private void createPlacedBets()
        {
            Create.Table("PlacedBets")
                 .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                 .WithColumn("UserId ").AsInt64()
                 .WithColumn("PlacedDate").AsDateTime().WithDefaultValue(DateTime.Now)
                 .WithColumn("Type").AsString()
                 .WithColumn("QuoteId").AsInt64();
        }

        private void seedBetableEntity()
        {
            long noEntities = Constants.Constants.noEntities;

            for (long i = 1; i <= noEntities; i++)
            {
                Insert.IntoTable("BetableEntity").Row(new
                {
                    Name = $"Echipa {i}"
                });
            }
        }

        private void seedBets()
        {
            long noBets = Constants.Constants.noBets;
            long noEntities = Constants.Constants.noEntities;
            Random random = new Random();

            for (long i = 1; i <= noBets; i++)
            {
                long firstTeamId = random.NextInt64(noEntities) + 1;
                long secondTeamId = random.NextInt64(noEntities) + 1;

                while (firstTeamId == secondTeamId)
                {
                    secondTeamId = random.NextInt64(noEntities);
                }

                Insert.IntoTable("Bets").Row(new
                {
                    Name = $"Meciul {i}",
                    BetableEntityA = firstTeamId,
                    BetableEntityB = secondTeamId
                });
            }
        }

        private void seedBetQuotes()
        {
            long noBets = Constants.Constants.noBets;
            Random random = new Random();

            for (long i = 1; i <= noBets; i++)
            {
                decimal quoteA = random.Next(100, 1000) / 100m;
                decimal quoteB = random.Next(100, 1000) / 100m;
                decimal quoteX = random.Next(100, 1000) / 100m;
                Insert.IntoTable("BetQuotes").Row(new
                {
                    BetId = i,
                    QuoteA = quoteA,
                    QuoteB = quoteB,
                    QuoteX = quoteX
                });
            }
        }

    }
}
