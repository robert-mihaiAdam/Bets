using Domain.Dto.BetableEntity;
using Domain.Dto.BetQuote;
using Domain.Dto.Bets;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Interfaces;
using System.Text;
using UnitTesting.Helpers;
using UnitTesting.SetupUnitTests;
using Xunit.Abstractions;

namespace UnitTesting.BetsUnitTests
{
    public class Seed
    {
        private readonly int noEntities = 20;
        private readonly int noBets = 5;
        private readonly IBetableEntityService _betableEntityService;
        private readonly IBetQuoteService _betQuoteService;
        private readonly IBetsService _betsService;
        private List<Guid> entitiesId;
        private ITestOutputHelper output = null;

        public Seed(DependencyInjectionFixture fixture)
        {
            _betableEntityService = fixture.ServiceProvider.GetService<IBetableEntityService>();
            _betsService = fixture.ServiceProvider.GetService<IBetsService>();
            _betQuoteService = fixture.ServiceProvider.GetService<IBetQuoteService>();
            entitiesId = new List<Guid>();
        }

        public async Task PopulateDB()
        {
            await CreateBetableEntities();
            await CreateBets();
        }

        private async Task CreateBetableEntities()
        {
            for (int i = 0; i < noEntities; i++)
            {
                CreateBetableEntityDto createBetableEntityDto = new CreateBetableEntityDto { Name = $"BetableEntity {Guid.NewGuid()}" };
                BetableEntityDto betableEntityDto = await _betableEntityService.CreateAsync(createBetableEntityDto);
                entitiesId.Add(betableEntityDto.Id);
            }
        }

        private async Task CreateBets()
        {
            Random random = new Random();
            for (int i = 0; i < noBets; i++)
            {
                CreateBetsDto newBet = new CreateBetsDto
                {
                    Name = $"Bet {Guid.NewGuid()}",
                    BetableEntityA = entitiesId.ElementAt(random.Next(noEntities)),
                    BetableEntityB = entitiesId.ElementAt(random.Next(noEntities)),
                };
                CreateBetQuotesDto newBetQuote = new CreateBetQuotesDto
                {
                    QuoteA = Constants.GetRandomDecimal(1.00m, 100.00m),
                    QuoteB = Constants.GetRandomDecimal(1.00m, 100.00m),
                    QuoteX = Constants.GetRandomDecimal(1.00m, 100.00m)
                };

                BetsDto betsDto = await _betsService.CreateAsync(newBet);
                BetQuoteDto betQuoteDto = await _betQuoteService.CreateAsync(newBetQuote, betsDto.Id);
            }
        }
    }
}
