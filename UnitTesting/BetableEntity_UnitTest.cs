using Xunit.Abstractions;
using FluentAssertions;
using Services.Interfaces;
using Services;
using Domain.Dto.BetableEntity;
using DataAccess;
using AutoMapper;
using Domain;

namespace UnitTesting
{
    [TestCaseOrderer("UnitTesting.PriorityOrderer", "1")]
    public class BetableEntity_UnitTest
    {
        private readonly ITestOutputHelper _output;
        private readonly IBetableEntityService _service;
        private long noEntities = 0;
        public int testNo = 0;
        private BetableEntityDto testEntity = null;

        public BetableEntity_UnitTest(ITestOutputHelper output)
        {
            _output = output;
            DBContext context = new DBContext(Constants.connection_string);
            IMapper mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperConfig>();
            }).CreateMapper();
            _service = new BetableEntityService(context, mapper);
        }

        [Fact, Priority(0)]
        public async Task GetAllEntitiesAsync()
        {
            Thread.Sleep(3000);
            IEnumerable<BetableEntityDto> entities = null;
            Func<Task> act = async () =>
            {
                entities = await _service.GetAllAsync();
                noEntities = entities.LongCount();
            };
            await act.Should().NotThrowAsync();
        }

        [Fact, Priority(1)]
        public async Task CreateEntityAsync()
        {
            CreateBetableEntityDto createBetableEntityDto = new CreateBetableEntityDto { Name = $"BetableEntity {Guid.NewGuid()}" };
        }
    }
}
