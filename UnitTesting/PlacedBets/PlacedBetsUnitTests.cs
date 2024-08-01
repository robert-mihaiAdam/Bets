using AutoMapper;
using Domain;
using Domain.Dto.PlacedBet;
using Domain.Entities;
using Domain.ErrorEntities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Interfaces;
using Xunit.Abstractions;

namespace UnitTesting.PlacedBets
{
    public class PlacedBetsUnitTests : IClassFixture<DependencyInjectionFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly IBetableEntityService _entityService;
        private readonly IBetsService _betsService;
        private readonly IBetQuoteService _betsQuoteService;
        private readonly IPlacedBetsService _placedBetsService;
        private readonly IMapper _mapper;
        private static Random random;

        public PlacedBetsUnitTests(ITestOutputHelper output, DependencyInjectionFixture fixture)
        {
            _output = output;
            _betsQuoteService = fixture.ServiceProvider.GetService<IBetQuoteService>();
            _placedBetsService = fixture.ServiceProvider.GetService<IPlacedBetsService>();
            random = new Random();
        }

        private async Task<PlacedBetsDto> CreatePlacedBetAsync()
        {
            IEnumerable<BetQuotes> entitiesQuotes = _betsQuoteService.GetAll().ToList();
            int noEntities = entitiesQuotes.Count();
            PlacedBetsDto placedBet= null;
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
            return placedBet;
        }


        [Fact]
        public async Task CreatedPlacedBet()
        {
            PlacedBetsDto placedBet = await CreatePlacedBetAsync();
            placedBet.Should().NotBeNull("Bet Entity should be created successfully");
            placedBet.Id.Should().NotBe(Guid.Empty, because: "Returned bet entity has id empty");
        }

        [Fact]
        public async Task CreatePlacedBetAndGetById()
        {
            PlacedBetsDto placedBet = await CreatePlacedBetAsync();
            PlacedBetsDto findPlacedBet= null;
            Func<Task> act = async () =>
            {
                findPlacedBet = await _placedBetsService.GetByIdAsync(placedBet.Id);
            };

            await act.Should().NotThrowAsync();
            Constants.CompareFields(placedBet, findPlacedBet).Should().BeTrue(because: "Entities should be the same");
        }

        [Fact]
        public async Task CreatePlacedBetAndUpdate()
        {
            PlacedBetsDto placedBet = await CreatePlacedBetAsync();

            IEnumerable<BetQuotes> entitiesQuotes = _betsQuoteService.GetAll().ToList();
            int noEntities = entitiesQuotes.Count();
            PlacedBetsDto updatedPlacedBet = null;
            UpdatePlacedBetDto mappedUpldatedPlacedBet = new UpdatePlacedBetDto();
            UpdatePlacedBetDto newPlacedBet = new UpdatePlacedBetDto
            {
                Type = Constants.GetRandomEnumValue<BetOptions>(),
                BetPrice = Constants.GetRandomDecimal(1.00m, 1000.00m)
            };

            Func<Task> updateAct = async () =>
            {
                updatedPlacedBet = await _placedBetsService.UpdateByIdAsync(placedBet.Id, newPlacedBet);
                _mapper.Map(updatedPlacedBet, mappedUpldatedPlacedBet);
            };
            await updateAct.Should().NotThrowAsync();
            placedBet.Id.Should().Be(updatedPlacedBet.Id);
            Constants.CompareFields<UpdatePlacedBetDto>(mappedUpldatedPlacedBet, newPlacedBet).Should().BeTrue(because: "After update, entity should change values from fields");
        }

        [Fact]
        public async Task CreatedPlaceBetAndDelete()
        {
            PlacedBetsDto placedBet = await CreatePlacedBetAsync();
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
