using Domain.Dto.BetRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                BetRequestDto betRequestDto = await _betFacade.CreateBetAsync(betRequest);
                return Ok(betRequestDto);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, "An error occurred while saving changes to the database");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBetByIdAsync(Guid id)
        {
            try
            {
                BetRequestDto entity = await _betFacade.GetBetByIdAsync(id);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                IEnumerable<BetRequestDto> betsDtosEntities = await _betFacade.GetAllBetsAsync();
                return Ok(betsDtosEntities);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> EditBetAsync(Guid id, UpdateBetRequestDto newEntity)
        {
            try
            {
                BetRequestDto updatedBet = await _betFacade.UpdateBetAsync(id, newEntity);
                return Ok(updatedBet);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, "An error occurred while saving changes to the database");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                await _betFacade.DeleteByIdAsync(id);
                return Ok();
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, "An error occurred while saving changes to the database");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
