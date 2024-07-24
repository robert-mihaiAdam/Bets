using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Dto.BetableEntity;
using Microsoft.AspNetCore.JsonPatch;
using System;

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

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBetEntitiesAsync()
        {
            IEnumerable<BetableEntityDto> dtoEntities = await _betableEntityService.GetAllAsync();
            return Ok(dtoEntities);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> EditBetEntityByIdAsync(Guid id, UpdateBetableEntityDto newEntity)
        {
            if (newEntity == null)
            {
                return BadRequest();
            }
            Console.WriteLine("aM AJUNS SI AICI");
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
