using AutoMapper;
using BenchmarkDotNet.Attributes;
using DataAccess;
using Domain;
using Domain.Dto.BetableEntity;
using Domain.Dto.BetQuote;
using Domain.Dto.Bets;
using Services;
using Services.Interfaces;
using System.Diagnostics;

namespace Benchmark
{
    public class DbPerformance
    {
        private readonly string benchmarkConnectionString = "Server=localhost;Database=BenchmarkBetsDatabase;TrustServerCertificate=true;Trusted_Connection=true;";
        private readonly int noEntities = 2;
        private readonly int noBets = 10;
        private List<Guid> entitiesId, betsId;
        public DBContext _dBContext;
        public IBetableEntityService _entityService;
        public IBetsService _betsService;
        public IBetQuoteService _betQuoteService;

        private void PrepareDb()
        {
            _dBContext = new DBContext(benchmarkConnectionString);
            DbDatabaseCreation contextCreation = new DbDatabaseCreation(benchmarkConnectionString);
            contextCreation.Database.EnsureCreated();
            DatabaseMigrator.MigrateDb(benchmarkConnectionString);
        }

        private async Task SeedDB()
        {
            entitiesId = new List<Guid>();
            for (int i = 0; i < noEntities; i++)
            {
                CreateBetableEntityDto createBetableEntityDto = new CreateBetableEntityDto { Name = $"BetableEntity {Guid.NewGuid()}" };
                BetableEntityDto betableEntityDto = await _entityService.CreateAsync(createBetableEntityDto);
                entitiesId.Add(betableEntityDto.Id);
            }
        }
        private decimal GetRandomDecimal(decimal minValue, decimal maxValue, Random random)
        {
            double randomDouble = random.NextDouble();
            decimal scaledValue = (decimal)randomDouble * (maxValue - minValue) + minValue;

            return scaledValue;
        }

        [IterationSetup]
        public async Task Setup()
        {
            PrepareDb();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperConfig>());
            IMapper mapper = config.CreateMapper();
            _entityService = new BetableEntityService(_dBContext, mapper);
            _betsService = new BetsService(_dBContext, TimeProvider.System, _entityService, mapper);
            _betQuoteService = new BetQuoteService(_dBContext, mapper);
            await SeedDB();
        }

        [IterationCleanup]
        public async Task CleanUp()
        {
            await _dBContext.Database.EnsureDeletedAsync();
        }
        
        public async Task CreateBets()
        {
            Random random = new Random();
            betsId = new List<Guid>();
            Guid betableEntityA = entitiesId.First();
            Guid betableEntityB = entitiesId.Last();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < noBets; i++)
            {
                CreateBetsDto newBet = new CreateBetsDto
                {
                    Name = $"Bet {Guid.NewGuid()}",
                    BetableEntityA = betableEntityA,
                    BetableEntityB = betableEntityB,
                };
                CreateBetQuotesDto newBetQuote = new CreateBetQuotesDto
                {
                    QuoteA = 2.34m,
                    QuoteB = 82.2m,
                    QuoteX = 34.79m
                };
                BetsDto betsDto = await _betsService.CreateAsync(newBet);
                BetQuoteDto betQuoteDto = await _betQuoteService.CreateAsync(newBetQuote, betsDto.Id);
                betsId.Add(betsDto.Id);
            }

            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;
            Console.WriteLine($"Took {elapsedTime.TotalSeconds} s to CREATE {noBets} bets");

        }

        public async Task GetQuoteByBetsId()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            foreach (Guid id in betsId.AsEnumerable())
            {
                await _betQuoteService.GetByBetIdAsync(id);
            }

            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;
            Console.WriteLine($"Took {elapsedTime.TotalSeconds} s to GET {noBets} bets");
        }
    }
}
