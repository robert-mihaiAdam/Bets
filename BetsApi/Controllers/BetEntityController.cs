﻿using Microsoft.AspNetCore.Mvc;
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
            BetableEntityDto updatedEntity = await _betableEntityService.UpdateEntityByIdAsync(id, newEntity);
            return Ok(updatedEntity);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            await _betableEntityService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
