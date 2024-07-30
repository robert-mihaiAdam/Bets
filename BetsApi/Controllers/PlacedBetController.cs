using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Domain.Dto.PlacedBet;
using Domain.Dto.FullBetView;

namespace BetsApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PlacedBetController : Controller
    {
        private readonly IPlacedBetFacade _placedBetFacade;

        public PlacedBetController(IPlacedBetFacade placedBetFacade)
        {
            _placedBetFacade = placedBetFacade;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddPlacedBetAsync(CreatePlacedBetDto newPlacedBetBody)
        {
            PlacedBetsDto newPlacedBetDto = await _placedBetFacade.CreatePlacedBetAsync(newPlacedBetBody);
            if (newPlacedBetDto == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlacedBetByIdAsync(Guid id)
        {
            FullBetViewDto betEntities = await _placedBetFacade.GetPlacedBetByIdAsync(id);
            return Ok(betEntities);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPlacedBetsAsync()
        {
            IEnumerable<FullBetViewDto> betEntities = await _placedBetFacade.GetAllPlacedBetAsync();
            return Ok(betEntities);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePlacedBetAsync(Guid id, UpdatePlacedBetDto updatePlacedBet)
        {
            PlacedBetsDto updatedPlacedBetDto = await _placedBetFacade.UpdatePlacedBetByIdAsync(id, updatePlacedBet);
            return Ok(updatedPlacedBetDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlacedBetByIdAsync(Guid id)
        {
            bool checkPlacedBetWasDeleted = await _placedBetFacade.DeletePlacedBetByIdAsync(id);
            if(!checkPlacedBetWasDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
