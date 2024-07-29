using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using AutoMapper;
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
        public async Task<IActionResult> AddBetEntityAsync(CreateBetableEntityDto newEntity)
        {
            BetableEntityDto createdEntityDto = await _betableEntityService.CreateAsync(newEntity);
            return Ok(createdEntityDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBetEntityByIdAsync(Guid id)
        {
            BetableEntityDto foundEntity = await _betableEntityService.GetByIdAsync(id);

            if (foundEntity == null)
            {
                return NotFound($"The entity with id: {id} wasn't found");
            }

            return Ok(foundEntity);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> EditBetEntityByIdAsync(Guid id, UpdateBetableEntityDto newEntity)
        {
            BetableEntityDto updatedEntity = await _betableEntityService.UpdateEntityByIdAsync(id, newEntity);
            if (updatedEntity == null)
            {
                return NotFound($"The entity with id: {id} wasn't found");
            }

            return Ok(updatedEntity);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            bool foundEntity = await _betableEntityService.DeleteByIdAsync(id);

            if (!foundEntity)
            {
                return NotFound($"The entity with id: {id} wasn't found");
            }

            return Ok();
        }
    }
}
