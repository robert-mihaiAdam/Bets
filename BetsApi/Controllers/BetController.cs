using Domain.Dto.BetRequest;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BetsApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BetController : Controller
    {
        private readonly IBetFacade _betFacade;

        public BetController(IBetFacade betFacade)
        {
            _betFacade = betFacade;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddBetAsync(CreateBetRequestDto betRequest)
        {
            BetRequestDto betRequestDto = await _betFacade.CreateBetAsync(betRequest);
            return Ok(betRequestDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBetByIdAsync(Guid id)
        {
            BetRequestDto entity = await _betFacade.GetBetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<BetRequestDto> betsDtosEntities = await _betFacade.GetAllBetsAsync();
            return Ok(betsDtosEntities);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> EditBetAsync(Guid id, UpdateBetRequestDto newEntity)
        {
            BetRequestDto updatedBet = await _betFacade.UpdateBetAsync(id, newEntity);
            return Ok(updatedBet);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            bool checkRemove = await _betFacade.DeleteByIdAsync(id);
            if(!checkRemove)
            {
                return NotFound($"Bet Quote with id: {id} doesn't exists");
            }
            return Ok();
        }
    }
}
