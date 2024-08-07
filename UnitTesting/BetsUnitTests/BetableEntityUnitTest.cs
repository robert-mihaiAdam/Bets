using Xunit.Abstractions;
using FluentAssertions;
using Services.Interfaces;
using Domain.Dto.BetableEntity;
using Domain.ErrorEntities;
using Microsoft.Extensions.DependencyInjection;
using UnitTesting.Scheduler;

namespace UnitTesting.BetsUnitTests
{
    [TestCaseOrderer(" UnitTesting.Scheduler.PriorityOrderer", "UnitTesting")]
    [Collection("Bets Unit Tests")]
    public class BetableEntityUnitTest
    {
        private readonly ITestOutputHelper _output;
        private readonly IBetableEntityService _service;
        private SharedFixture _sharedFixture;

        public BetableEntityUnitTest(ITestOutputHelper output, SharedFixture sharedFixture)
        {
            _output = output;
            _sharedFixture = sharedFixture;
            _service = _sharedFixture.dependencyInjection.ServiceProvider.GetService<IBetableEntityService>();
        }

        [Fact]
        [TestPriority(1)]
        public async Task CreateEntity()
        {
            CreateBetableEntityDto createBetableEntityDto = new CreateBetableEntityDto { Name = $"BetableEntity {Guid.NewGuid()}" };
            BetableEntityDto betableEntityDto = null;
            Func<Task> act = async () =>
            {
                betableEntityDto = await _service.CreateAsync(createBetableEntityDto);
            };
            await act.Should().NotThrowAsync();
            betableEntityDto.Should().NotBeNull("Entity should be created successfully");
            Guid betableId = betableEntityDto.Id;
            betableId.Should().NotBe(Guid.Empty, because: "Returned entity has id empty");
            _sharedFixture.createdBetableEntity = betableEntityDto;
        }

        [Fact]
        [TestPriority(2)]
        public async Task GetById()
        {
            BetableEntityDto createdBetableEntity = _sharedFixture.createdBetableEntity;
            BetableEntityDto findEntity = null;
            Func<Task> act = async () =>
            {
                findEntity = await _service.GetByIdAsync(createdBetableEntity.Id);
            };

            await act.Should().NotThrowAsync();
            createdBetableEntity.Should().BeEquivalentTo(findEntity, because: "Entities should be the same");
        }

        [Fact]
        [TestPriority(3)]
        public async Task UpdateEntity()
        {
            BetableEntityDto createdBetableEntity = _sharedFixture.createdBetableEntity;
            UpdateBetableEntityDto updateBetableEntityDto = new UpdateBetableEntityDto { Name = $"BetableEntity {Guid.NewGuid()}" };
            BetableEntityDto updatedEntityDto = null;

            Func<Task> act = async () =>
            {
                updatedEntityDto = await _service.UpdateEntityByIdAsync(createdBetableEntity.Id, updateBetableEntityDto);
            };

            await act.Should().NotThrowAsync();
            updatedEntityDto.Should().NotBeNull("Entity should be created successfully");
            updatedEntityDto.Id.Should().Be(createdBetableEntity.Id, because: "After update ID should be same");
            updatedEntityDto.Name.Should().NotBe(createdBetableEntity.Name, because: "After update Name shouldn't be same");
            updatedEntityDto.Name.Should().Be(updateBetableEntityDto.Name, because: "After update Name be changed");
        }

        [Fact]
        [TestPriority(4)]
        public async Task DeleteEntity()
        {
            BetableEntityDto createdBetableEntity = _sharedFixture.createdBetableEntity;

            Func<Task> deleteAct = async () =>
            {
                await _service.DeleteByIdAsync(createdBetableEntity.Id);
            };

            await deleteAct.Should().NotThrowAsync();

            Func<Task> getAct = async () =>
            {
                await _service.GetByIdAsync(createdBetableEntity.Id);
            };
            await getAct.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        [TestPriority(5)]
        public async Task GetAllEntities()
        {
            Func<Task> act = async () =>
            {
                _sharedFixture.entities = await _service.GetAllAsync();
            };
            await act.Should().NotThrowAsync();
        }

    }
}
