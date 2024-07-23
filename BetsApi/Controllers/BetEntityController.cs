using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Dto.BetableEntity;

namespace BetsApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BetEntityController : Controller
    {
        private readonly IBetableEntityService _betableEntityService;
        private readonly IMapper _mapper;

        public BetEntityController(IBetableEntityService betableEntityService,
                                   IMapper mapper)
        {
            _betableEntityService = betableEntityService;
            _mapper = mapper;
        }

        [HttpPost("")]
        public async Task<IActionResult> PlaceBetEntityAsync(PlaceBetableEntityDto newEntity)
        {
            BetableEntity createdEntity = await _betableEntityService.CreateAsync(newEntity);
            BetableEntityDto createdEntityDto = _mapper.Map<BetableEntityDto>(createdEntity);
            return Ok(createdEntityDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBetEntityByIdAsync(Guid id)
        {
            BetableEntity? foundEntity = await _betableEntityService.GetByIdAsync(id);

            if (foundEntity == null)
            {
                return NotFound($"The entity with id: {id} wasn't found");
            }

            BetableEntityDto dtoEntity = _mapper.Map<BetableEntityDto>(foundEntity);
            return Ok(dtoEntity);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBetEntityByIdAsync()
        {
            IEnumerable<BetableEntity?> entities = await _betableEntityService.GetAllAsync();
            IEnumerable<BetableEntityDto> dtoEntities = _mapper.Map< IEnumerable<BetableEntityDto>>(entities);
            return Ok(dtoEntities);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> EditBetEntityByIdAsync(Guid id, UpdateBetableEntityDto newEntity)
        {
            BetableEntity? updatedEntity = await _betableEntityService.UpdateEntityByIdAsync(id, newEntity);
            if (updatedEntity == null)
            {
                return NotFound($"The entity with id: {id} wasn't found");
            }

            BetableEntityDto entityDto = _mapper.Map<BetableEntityDto>(updatedEntity);

            return Ok(entityDto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            BetableEntity? foundEntity = await _betableEntityService.DeleteByIdAsync(id);

            if (foundEntity == null)
            {
                return NotFound($"The entity with id: {id} wasn't found");
            }

            BetableEntityDto dtoEntity = _mapper.Map<BetableEntityDto>(foundEntity);
            return Ok(dtoEntity);
        }
    }
}
