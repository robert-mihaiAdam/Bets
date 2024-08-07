using AutoMapper;
using Domain.Dto.BetableEntity;
using Domain.Dto.BetQuote;
using Domain.Dto.BetRequest;
using Domain.Dto.Bets;
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
    public class BetsUnitTests
    {
        private readonly ITestOutputHelper _output;
        private readonly IBetableEntityService _entityService;
        private readonly IBetsService _betsService;
        private readonly IBetQuoteService _betsQuoteService;
        private readonly IMapper _mapper;
        private Random random;
        private SharedFixture _sharedFixture;

        public BetsUnitTests(ITestOutputHelper output, SharedFixture sharedFixture)
        {
            _output = output;
            _sharedFixture = sharedFixture;
            DependencyInjectionFixture dependencyInjectionFixture = sharedFixture.dependencyInjection;
            _entityService = dependencyInjectionFixture.ServiceProvider.GetService<IBetableEntityService>();
            _betsService = dependencyInjectionFixture.ServiceProvider.GetService<IBetsService>();
            _betsQuoteService = dependencyInjectionFixture.ServiceProvider.GetService<IBetQuoteService>();
            _mapper = dependencyInjectionFixture.ServiceProvider.GetRequiredService<IMapper>();
            random = new Random();
        }


        [Fact]
        [TestPriority(1)]
        public async Task CreateFullBet()
        {
            IEnumerable<BetableEntityDto> entities = _sharedFixture.entities;
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

            BetRequestDto fullBet = new BetRequestDto { bet = betsDto, betQuote = betQuoteDto };

            betsDto.Should().NotBeNull("Bet Entity should be created successfully");
            betQuoteDto.Should().NotBeNull("Bet Quote should be created successfully");
            betsDto.Id.Should().NotBe(Guid.Empty, because: "Returned bet entity has id empty");
            betQuoteDto.Id.Should().NotBe(Guid.Empty, because: "Returned quote entity has id empty");

            _sharedFixture.fullBet = fullBet;
        }

        [Fact]
        [TestPriority(2)]
        public async Task GetById()
        {
            BetRequestDto fullBet = _sharedFixture.fullBet;
            BetsDto bets = fullBet.bet, getBet = null;
            BetQuoteDto betQuote = fullBet.betQuote, getBetQuote = null;

            Func<Task> act = async () =>
            {
                getBet = await _betsService.GetByIdAsync(bets.Id);
                getBetQuote = await _betsQuoteService.GetByIdAsync(betQuote.Id);
            };

            await act.Should().NotThrowAsync();
            bets.Should().BeEquivalentTo(getBet, because: "Bet Entities after create and get should be the same");
            betQuote.Should().BeEquivalentTo(getBetQuote, because: "Bet Quote Entities after create and get should be the same");
        }


        [Fact]
        [TestPriority(3)]
        public async Task UpdateFullBetById()
        {
            BetRequestDto fullBet = _sharedFixture.fullBet;
            BetsDto initialBet = fullBet.bet;
            BetQuoteDto initialBetQuote = fullBet.betQuote;

            IEnumerable<BetableEntityDto> entities = _sharedFixture.entities;
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
            updateFieldBet.Should().BeEquivalentTo(mappedUpdatedBet, because: "After update, entity should change values from fields");
            initialBetQuote.Id.Should().Be(updatedBetQuote.Id);
            mappedUpdatedBetQuotes.Should().BeEquivalentTo(updateFiledBetQuotes, because: "After update, entity should change values from fields");
        }

        [Fact]
        [TestPriority(4)]
        public async Task DeleteFullBetById()
        {
            BetRequestDto fullBet = _sharedFixture.fullBet;
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

        [Fact]
        [TestPriority(5)]
        public async Task GetAllBets()
        {
            Action act = () =>
            {
                _sharedFixture.bets = _betsService.GetAll().ToList();
            };
            act.Should().NotThrow();
        }

        [Fact]
        [TestPriority(6)]
        public async Task GetAllQuotes()
        {
            Action act = () =>
            {
                _sharedFixture.quotes = _betsQuoteService.GetAll().ToList();
            };
            act.Should().NotThrow();
        }
    }
}
