using Domain.Dto.BetableEntity;
using Domain.Dto.BetRequest;
using Domain.Dto.PlacedBet;
using Domain.Entities;
using UnitTesting.SetupUnitTests;

namespace UnitTesting.BetsUnitTests
{
    public class SharedFixture : IAsyncLifetime
    {
        public DependencyInjectionFixture dependencyInjection;
        public BetableEntityDto createdBetableEntity;
        public IEnumerable<BetableEntityDto> entities;
        public BetRequestDto fullBet;
        public IEnumerable<BetQuotes> quotes;
        public IEnumerable<Bets> bets;
        public PlacedBetsDto placedBets;

        public SharedFixture() 
        {
            dependencyInjection = new DependencyInjectionFixture();
            Seed seeder = new Seed(dependencyInjection);
            Task.Run(async () => await seeder.PopulateDB()).Wait();
        }

        public async Task InitializeAsync()
        {
            await Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
           await dependencyInjection.DisposeAsync();
        }

        
    }
}
