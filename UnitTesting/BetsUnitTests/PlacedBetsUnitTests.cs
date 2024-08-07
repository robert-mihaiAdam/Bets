using AutoMapper;
using Domain;
using Domain.Dto.PlacedBet;
using Domain.Entities;
using Domain.ErrorEntities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using UnitTesting.Helpers;
using UnitTesting.SetupTests;
using Xunit.Abstractions;
using UnitTesting.Scheduler;

namespace UnitTesting.BetsUnitTests
{
    [TestCaseOrderer("UnitTesting.Scheduler.PriorityOrderer", "UnitTesting")]
    [Collection("Bets Unit Tests")]
    public class PlacedBetsUnitTests
    {
        private readonly ITestOutputHelper _output;
        private readonly IBetableEntityService _entityService;
        private readonly IBetsService _betsService;
        private readonly IBetQuoteService _betsQuoteService;
        private readonly IPlacedBetsService _placedBetsService;
        private readonly IMapper _mapper;
        private Random random;
        private SharedFixture _sharedFixture;

        public PlacedBetsUnitTests(ITestOutputHelper output, SharedFixture sharedFixture)
        {
            _output = output;
            _sharedFixture = sharedFixture;
            DependencyInjectionFixture fixture = sharedFixture.dependencyInjection;
            _betsQuoteService = fixture.ServiceProvider.GetService<IBetQuoteService>();
            _placedBetsService = fixture.ServiceProvider.GetService<IPlacedBetsService>();
            _mapper = fixture.ServiceProvider.GetRequiredService<IMapper>();
            random = new Random();
        }

        [Fact]
        [TestPriority(1)]
        public async Task CreatedPlacedBet()
        {
            IEnumerable<BetQuotes> entitiesQuotes = _sharedFixture.quotes;
            int noEntities = entitiesQuotes.Count();
            PlacedBetsDto placedBet = null;
            CreatePlacedBetDto newPlacedBet = new CreatePlacedBetDto
            {
                Type = Constants.GetRandomEnumValue<BetOptions>(),
                BetPrice = Constants.GetRandomDecimal(1.00m, 1000.00m),
                QuoteId = entitiesQuotes.ElementAt(random.Next(noEntities)).Id
            };

            Func<Task> act = async () =>
            {
                placedBet = await _placedBetsService.CreateAsync(newPlacedBet);
            };

            await act.Should().NotThrowAsync();
            placedBet.Should().NotBeNull("Bet Entity should be created successfully");
            placedBet.Id.Should().NotBe(Guid.Empty, because: "Returned bet entity has id empty");

            _sharedFixture.placedBets = placedBet;
        }

        [Fact]
        [TestPriority(2)]
        public async Task GetById()
        {
            PlacedBetsDto placedBet = _sharedFixture.placedBets;
            PlacedBetsDto findPlacedBet = null;
            Func<Task> act = async () =>
            {
                findPlacedBet = await _placedBetsService.GetByIdAsync(placedBet.Id);
            };

            await act.Should().NotThrowAsync();
            placedBet.Should().BeEquivalentTo(findPlacedBet, because: "Entities should be the same");
        }

        [Fact]
        [TestPriority(3)]
        public async Task UpdatePlacedBetById()
        {
            PlacedBetsDto placedBet = _sharedFixture.placedBets;
            IEnumerable<BetQuotes> entitiesQuotes = _sharedFixture.quotes;
            int noEntities = entitiesQuotes.Count();
            PlacedBetsDto updatedPlacedBet = null;
            UpdatePlacedBetDto mappedUpldatedPlacedBet = new UpdatePlacedBetDto();
            UpdatePlacedBetDto newPlacedBet = new UpdatePlacedBetDto
            {
                Type = Constants.GetRandomEnumValue<BetOptions>(),
                BetPrice = Constants.GetRandomDecimal(1.00m, 1000.00m)
            };

            _output.WriteLine($"{placedBet.Id}");
            Func<Task> updateAct = async () =>
            {
                updatedPlacedBet = await _placedBetsService.UpdateByIdAsync(placedBet.Id, newPlacedBet);
                _mapper.Map(updatedPlacedBet, mappedUpldatedPlacedBet);
            };
            await updateAct.Should().NotThrowAsync();
            placedBet.Id.Should().Be(updatedPlacedBet.Id);
            mappedUpldatedPlacedBet.Should().BeEquivalentTo(newPlacedBet, because: "After update, entity should change values from fields");
        }

        [Fact]
        [TestPriority(4)]
        public async Task DeletePlacedBetById()
        {
            PlacedBetsDto placedBet = _sharedFixture.placedBets;
            Func<Task> deleteAct = async () =>
            {
                await _placedBetsService.DeletePlacedBetByIdAsync(placedBet.Id);
            };
            await deleteAct.Should().NotThrowAsync();

            Func<Task> getAct = async () =>
            {
                await _placedBetsService.GetByIdAsync(placedBet.Id);
            };
            await getAct.Should().ThrowAsync<NotFoundException>();
        }
    }
}
