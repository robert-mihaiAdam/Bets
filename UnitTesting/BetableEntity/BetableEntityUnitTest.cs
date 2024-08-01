using Xunit.Abstractions;
using FluentAssertions;
using Services.Interfaces;
using Domain.Dto.BetableEntity;
using Domain.ErrorEntities;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTesting.BetableEntity
{
    public class BetableEntityUnitTest : IClassFixture<DependencyInjectionFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly IBetableEntityService _service;

        public BetableEntityUnitTest(ITestOutputHelper output, DependencyInjectionFixture fixture)
        {
            _output = output;
            _service = fixture.ServiceProvider.GetService<IBetableEntityService>();
        }

        private bool CompareEntities(BetableEntityDto entity1, BetableEntityDto entity2)
        {
            return entity1.Id == entity2.Id && entity1.Name == entity2.Name;
        }

        private async Task<BetableEntityDto> CreateBetableEntity()
        {
            CreateBetableEntityDto createBetableEntityDto = new CreateBetableEntityDto { Name = $"BetableEntity {Guid.NewGuid()}" };
            BetableEntityDto betableEntityDto = null;
            Func<Task> act = async () =>
            {
                betableEntityDto = await _service.CreateAsync(createBetableEntityDto);
            };
            await act.Should().NotThrowAsync();
            return betableEntityDto;
        }

        [Fact]
        public async Task CreateEntity()
        {
            BetableEntityDto betableEntityDto = await CreateBetableEntity();
            betableEntityDto.Should().NotBeNull("Entity should be created successfully");
            Guid betableId = betableEntityDto.Id;
            betableId.Should().NotBe(Guid.Empty, because: "Returned entity has id empty");
        }

        [Fact]
        public async Task CreateEntityAndGetById()
        {
            BetableEntityDto betableEntityDto = await CreateBetableEntity();
            BetableEntityDto findEntity = null;
            Func<Task> act = async () =>
            {
                findEntity = await _service.GetByIdAsync(betableEntityDto.Id);
            };

            await act.Should().NotThrowAsync();
            CompareEntities(betableEntityDto, findEntity).Should().BeTrue(because: "Entities should be the same");
        }

        [Fact]
        public async Task CreateEntityAndGetAllEntities()
        {
            BetableEntityDto betableEntityDto = null;
            long initialNoEntities = 0, finalNoEntities = 0;

            Func<Task> act = async () =>
            {
                IEnumerable<BetableEntityDto> entities = await _service.GetAllAsync();
                initialNoEntities = entities.LongCount();
                betableEntityDto = await CreateBetableEntity();
                entities = await _service.GetAllAsync();
                finalNoEntities = entities.LongCount();
            };

            await act.Should().NotThrowAsync();
            finalNoEntities.Should().Be(initialNoEntities + 1, because: "New entity was inserted");
        }

        [Fact]
        public async Task CreateAndUpdateEntity()
        {
            UpdateBetableEntityDto updateBetableEntityDto = new UpdateBetableEntityDto { Name = $"BetableEntity {Guid.NewGuid()}" };
            BetableEntityDto updatedEntityDto = null;
            BetableEntityDto betableEntityDto = await CreateBetableEntity();

            Func<Task> act = async () =>
            {
                updatedEntityDto = await _service.UpdateEntityByIdAsync(betableEntityDto.Id, updateBetableEntityDto);
            };

            await act.Should().NotThrowAsync();
            updatedEntityDto.Should().NotBeNull("Entity should be created successfully");
            updatedEntityDto.Id.Should().Be(betableEntityDto.Id, because: "After update ID should be same");
            updatedEntityDto.Name.Should().NotBe(betableEntityDto.Name, because: "After update Name shouldn't be same");
            updatedEntityDto.Name.Should().Be(updateBetableEntityDto.Name, because: "After update Name be changed");
        }

        [Fact]
        public async Task CreateAndDeleteEntity()
        {
            BetableEntityDto betableEntityDto = await CreateBetableEntity();
            long initialNoEntities = 0, afterInsertNoEntities = 0, finalNoEntities = 0;

            Func<Task> deleteAct = async () =>
            {
                await _service.DeleteByIdAsync(betableEntityDto.Id);
            };

            await deleteAct.Should().NotThrowAsync();

            Func<Task> getAct = async () =>
            {
                await _service.GetByIdAsync(betableEntityDto.Id);
            };
            await getAct.Should().ThrowAsync<NotFoundException>();
        }

    }
}
