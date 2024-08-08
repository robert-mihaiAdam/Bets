using AutoMapper;
using Domain;
using Domain.Dto.BetableEntity;
using Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using DataAccess;
using Services;
using Services.Interfaces;
using UnitTesting.Scheduler;
using Xunit.Abstractions;
using UnitTesting.BetsUnitTests;
using Domain.ErrorEntities;

namespace UnitTesting.UnitTests
{
    public class EntityUnitTests
    {
        private readonly ITestOutputHelper _output;
        private readonly IBetableEntityService _entityService;
        private DBContext _dbContext;
        private readonly IMapper _mapper;


        public EntityUnitTests(ITestOutputHelper output)
        {
            _output = output;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperConfig>();
            });
            _mapper = config.CreateMapper();
            var options = new DbContextOptionsBuilder<DBContext>()
                                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                                .Options;
            _dbContext = new DBContext(options);
            _entityService = new BetableEntityService(_dbContext, _mapper);
        }

        private async Task<BetableEntityDto> CreateEntity()
        {
            CreateBetableEntityDto createBetableEntityDto = new CreateBetableEntityDto { Name = $"BetableEntity {Guid.NewGuid()}" };
            BetableEntityDto betableEntityDto = null;
            betableEntityDto = await _entityService.CreateAsync(createBetableEntityDto);
            return betableEntityDto;
        }

        private void ValidateEntityWithDb(BetableEntityDto entity)
        {
            BetableEntity dbBetableEntity = _dbContext.BetableEntity.Find(entity.Id);
            dbBetableEntity.Should().NotBeNull("Entity should be in the dbContext");
            BetableEntityDto dbBetableEntityDto = _mapper.Map<BetableEntityDto>(dbBetableEntity);
            dbBetableEntityDto.Should().BeEquivalentTo(entity, "Entity from DB and the returned one should be the same");
        }


        [Fact]
        public async Task CreateAsync()
        {
            BetableEntityDto betableEntityDto = await CreateEntity();
            betableEntityDto.Should().NotBeNull("Entity should be created successfully");
            Guid betableId = betableEntityDto != null ? betableEntityDto.Id : Guid.Empty;
            betableId.Should().NotBe(Guid.Empty, because: "Returned entity has id empty");
            ValidateEntityWithDb(betableEntityDto);
        }

        [Fact]
        public async Task GetAllEntities()
        {
            BetableEntityDto betableEntityDto = await CreateEntity();
            Random random = new();
            int initialNoEntities = 0, finalNoEntities = 0, createdEntities = random.Next(10) + 1;

            IEnumerable<BetableEntity> entities = null;
            entities = _entityService.GetAll();
            initialNoEntities = entities.Count();
            for (int i = 0; i < createdEntities; i++)
            {
                await CreateEntity();
            }
            entities = _entityService.GetAll();
            finalNoEntities = entities.Count();


            initialNoEntities.Should().NotBe(finalNoEntities, because: "After insertions, number of total entities should be modified");
            finalNoEntities.Should().Be(createdEntities + initialNoEntities, because: "All entities wasn't inserted succesfully");
        }

        [Fact]
        public async Task GetById()
        {
            BetableEntityDto createdBetableEntity = await CreateEntity();
            BetableEntityDto findEntity = null;
            findEntity = await _entityService.GetByIdAsync(createdBetableEntity.Id);
            createdBetableEntity.Should().BeEquivalentTo(findEntity, because: "Entities should be the same");
        }


        [Fact]
        public async Task UpdateEntity()
        {
            BetableEntityDto createdBetableEntity = await CreateEntity();
            UpdateBetableEntityDto updateBetableEntityDto = new UpdateBetableEntityDto { Name = $"BetableEntity {Guid.NewGuid()}" };
            BetableEntityDto updatedEntityDto = null;
            updatedEntityDto = await _entityService.UpdateEntityByIdAsync(createdBetableEntity.Id, updateBetableEntityDto);
            updatedEntityDto.Should().NotBeNull("Entity should be created successfully");
            updatedEntityDto.Id.Should().Be(createdBetableEntity.Id, because: "After update ID should be same");
            updatedEntityDto.Name.Should().NotBe(createdBetableEntity.Name, because: "After update Name shouldn't be same");
            updatedEntityDto.Name.Should().Be(updateBetableEntityDto.Name, because: "After update Name be changed");
            ValidateEntityWithDb(updatedEntityDto);
        }

        [Fact]
        public async Task DeleteEntity()
        {
            BetableEntityDto createdBetableEntity = await CreateEntity();
            await _entityService.DeleteByIdAsync(createdBetableEntity.Id);
            BetableEntity entity = _dbContext.BetableEntity.Find(createdBetableEntity.Id);
            entity.Should().BeNull(because:"After delete operation entity shouldn't be in the database");
            Func<Task> getAct = async () =>
            {
                await _entityService.GetByIdAsync(createdBetableEntity.Id);
            };
            await getAct.Should().ThrowAsync<NotFoundException>();
        }

    }
}
