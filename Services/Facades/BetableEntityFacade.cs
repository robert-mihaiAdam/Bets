using AutoMapper;
using Domain.Dto.BetableEntity;
using Domain.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Facades
{
    public class BetableEntityFacade : IBetableEntityFacade
    {
        private readonly IBetableEntityService _betableEntityService;
        private readonly IMapper _mapper;

        public BetableEntityFacade(IBetableEntityService betableEntityService,
                                IMapper mapper)
        {
            _betableEntityService = betableEntityService;
            _mapper = mapper;
        }

        public async Task<BetableEntityDto> CreateBetEntityAsync(CreateBetableEntityDto newEntity)
        {
            BetableEntityDto createdEntityDto = await _betableEntityService.CreateAsync(newEntity);
            return createdEntityDto;
        }

        public async Task<bool> DeleteBetEntityByIdAsync(Guid id)
        {
            bool checkEntityWasDeleted = await _betableEntityService.DeleteByIdAsync(id);
            return checkEntityWasDeleted;
        }

        public async Task<BetableEntityDto> EditBetEntityByIdAsync(Guid id, UpdateBetableEntityDto newEntity)
        {
            BetableEntityDto updatedEntity = await _betableEntityService.UpdateEntityByIdAsync(id, newEntity);
            return updatedEntity;
        }

        public async Task<IEnumerable<BetableEntityDto>> GetAllBetEntitiesAsync()
        {
            IEnumerable<BetableEntity> dtoEntities = await _betableEntityService.GetAllAsync();
            return _mapper.Map<IEnumerable<BetableEntityDto>>(dtoEntities);
        }

        public async Task<BetableEntityDto> GetBetEntityByIdAsync(Guid id)
        {
            BetableEntityDto foundEntity = await _betableEntityService.GetByIdAsync(id);
            return foundEntity;
        }
    }
}
