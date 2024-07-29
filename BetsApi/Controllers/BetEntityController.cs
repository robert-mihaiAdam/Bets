using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using AutoMapper;
using Domain.Dto.BetableEntity;
using Domain.Entities;

namespace BetsApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BetEntityController : Controller
    {
        private readonly IBetableEntityFacade _entityFacade;  

        public BetEntityController(IBetableEntityFacade entityFacade)
        {
            _entityFacade = entityFacade;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddBetEntityAsync(CreateBetableEntityDto newEntity)
        {
            BetableEntityDto createdEntityDto = await _entityFacade.CreateBetEntityAsync(newEntity);
            return Ok(createdEntityDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBetEntityByIdAsync(Guid id)
        {
            BetableEntityDto foundEntity = await _entityFacade.GetBetEntityByIdAsync(id);

            if (foundEntity == null)
            {
                return NotFound($"The entity with id: {id} wasn't found");
            }

            return Ok(foundEntity);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBetEntitiesAsync()
        {
            IEnumerable<BetableEntityDto> dtoEntities = await _entityFacade.GetAllBetEntitiesAsync();
            return Ok(dtoEntities);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> EditBetEntityByIdAsync(Guid id, UpdateBetableEntityDto newEntity)
        {
            BetableEntityDto updatedEntity = await _entityFacade.EditBetEntityByIdAsync(id, newEntity);
            if (updatedEntity == null)
            {
                return NotFound($"The entity with id: {id} wasn't found");
            }

            return Ok(updatedEntity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            bool foundEntity = await _entityFacade.DeleteBetEntityByIdAsync(id);

            if (!foundEntity)
            {
                return NotFound($"The entity with id: {id} wasn't found");
            }

            return Ok();
        }
    }
}
