using AutoMapper;
using DataAccess;
using Domain;
using Domain.Dto.BetableEntity;
using Domain.Dto.BetQuote;
using Domain.Dto.BetRequest;
using Domain.Dto.Bets;
using Domain.ErrorEntities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using Xunit.Abstractions;

namespace UnitTesting.Bets
{
    public class BetsUnitTests : IClassFixture<DependencyInjectionFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly IBetableEntityService _entityService;
        private readonly IBetsService _betsService;
        private readonly IBetQuoteService _betsQuoteService;
        private readonly IMapper _mapper;
        private static Random random;

        public BetsUnitTests(ITestOutputHelper output, DependencyInjectionFixture fixture)
        {
            _output = output;
            _entityService = fixture.ServiceProvider.GetService<IBetableEntityService>();
            _betsService = fixture.ServiceProvider.GetService<IBetsService>();
            _betsQuoteService = fixture.ServiceProvider.GetService<IBetQuoteService>();
            random = new Random();
        }

        private async Task<BetRequestDto> CreateFullBetAsync()
        {
            IEnumerable<BetableEntityDto> entities = await _entityService.GetAllAsync();
            int noElements = entities.Count();
            BetQuoteDto betQuoteDto = null;
            BetsDto betsDto = null;
            CreateBetsDto newBet = new CreateBetsDto
            {
                Name = $"Bet {Guid.NewGuid()}",
                BetableEntityA = entities.ElementAt(random.Next(noElements)).Id,
                BetableEntityB = entities.ElementAt(random.Next(noElements)).Id,
            };
            CreateBetQuotesDto newBetQuote = new CreateBetQuotesDto
            {
                QuoteA = Constants.GetRandomDecimal(1.00m, 100.00m),
                QuoteB = Constants.GetRandomDecimal(1.00m, 100.00m),
                QuoteX = Constants.GetRandomDecimal(1.00m, 100.00m)
            };

            Func<Task> act = async () =>
            {
                betsDto = await _betsService.CreateAsync(newBet);
                betQuoteDto = await _betsQuoteService.CreateAsync(newBetQuote, betsDto.Id);
            };

            await act.Should().NotThrowAsync();
            return new BetRequestDto { bet = betsDto, betQuote = betQuoteDto };
        }


        [Fact]
        public async Task CreateFullBet()
        {
            BetRequestDto fullBet = await CreateFullBetAsync();
            BetsDto bets = fullBet.bet;
            BetQuoteDto betQuote = fullBet.betQuote;

            bets.Should().NotBeNull("Bet Entity should be created successfully");
            betQuote.Should().NotBeNull("Bet Quote should be created successfully");
            bets.Id.Should().NotBe(Guid.Empty, because: "Returned bet entity has id empty");
            betQuote.Id.Should().NotBe(Guid.Empty, because: "Returned quote entity has id empty");
        }

        [Fact]
        public async Task CreateFullBetAndGetById()
        {
            BetRequestDto fullBet = await CreateFullBetAsync();
            BetsDto bets = fullBet.bet, getBet = null;
            BetQuoteDto betQuote = fullBet.betQuote, getBetQuote = null;

            Func<Task> act = async () =>
            {
                getBet = await _betsService.GetByIdAsync(bets.Id);
                getBetQuote = await _betsQuoteService.GetByIdAsync(betQuote.Id);
            };

            await act.Should().NotThrowAsync();
            Constants.CompareFields<BetsDto>(bets, getBet).Should().BeTrue(because: "Bet Entities after create and get should be the same");
            Constants.CompareFields<BetQuoteDto>(betQuote, getBetQuote).Should().BeTrue(because: "Bet Quote Entities after create and get should be the same");
        }


        [Fact]
        public async Task CreateFullBetAndUpdate()
        {
            BetRequestDto fullBet = await CreateFullBetAsync();
            BetsDto initialBet = fullBet.bet;
            BetQuoteDto initialBetQuote = fullBet.betQuote;

            IEnumerable<BetableEntityDto> entities = await _entityService.GetAllAsync();
            int noElements = entities.Count();
            BetQuoteDto updatedBetQuote = null;
            BetsDto updatedBet = null;
            UpdateBetsDto mappedUpdatedBet = new UpdateBetsDto();
            UpdateBetQuotesDto mappedUpdatedBetQuotes = new UpdateBetQuotesDto();

            UpdateBetsDto updateFieldBet = new UpdateBetsDto
            {
                Name = $"Bet {Guid.NewGuid()}",
                BetableEntityA = entities.ElementAt(random.Next(noElements)).Id,
                BetableEntityB = entities.ElementAt(random.Next(noElements)).Id,
            };

            UpdateBetQuotesDto updateFiledBetQuotes = new UpdateBetQuotesDto
            {
                QuoteA = Constants.GetRandomDecimal(1.00m, 100.00m),
                QuoteB = Constants.GetRandomDecimal(1.00m, 100.00m),
                QuoteX = Constants.GetRandomDecimal(1.00m, 100.00m)
            };

            Func<Task> act = async () =>
            {
                updatedBet = await _betsService.UpdateById(initialBet.Id, updateFieldBet);
                updatedBetQuote = await _betsQuoteService.UpdateById(initialBetQuote.Id, updateFiledBetQuotes);
                _mapper.Map(updatedBet, mappedUpdatedBet);
                _mapper.Map(updatedBetQuote, mappedUpdatedBetQuotes);
            };

            await act.Should().NotThrowAsync();
            initialBet.Id.Should().Be(updatedBet.Id);
            Constants.CompareFields<UpdateBetsDto>(updateFieldBet, mappedUpdatedBet).Should().BeTrue(because: "After update, entity should change values from fields");
            initialBetQuote.Id.Should().Be(updatedBetQuote.Id);
            Constants.CompareFields<UpdateBetQuotesDto>(mappedUpdatedBetQuotes, updateFiledBetQuotes).Should().BeTrue(because: "After update, entity should change values from fields");
        }

        [Fact]
        public async Task CreateFullBetAndDeleteIt()
        {
            BetRequestDto fullBet = await CreateFullBetAsync();
            BetsDto bet = fullBet.bet;
            BetQuoteDto betQuote = fullBet.betQuote;

            Func<Task> act = async () =>
            {
                await _betsService.DeleteByIdAsync(bet.Id);
                await _betsQuoteService.DeleteByIdAsync(betQuote.Id);

            };
            await act.Should().NotThrowAsync();

            Func<Task> deleteBet = async () =>
            {
                await _betsService.GetByIdAsync(bet.Id);
            };
            await deleteBet.Should().ThrowAsync<NotFoundException>();

            Func<Task> deleteBetQuote = async () =>
            {
                await _betsQuoteService.GetByIdAsync(betQuote.Id);
            };
            await deleteBetQuote.Should().ThrowAsync<NotFoundException>();

        }

    }
}
