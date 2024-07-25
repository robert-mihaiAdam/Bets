using AutoMapper;
using Domain.Dto.BetQuote;
using Domain.Dto.Bets;
using Domain.Dto.BetRequest;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BetsApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BetController : Controller
    {
        private readonly IBetsService _betService;
        private readonly IBetQuoteService _betQuoteService;
        private readonly IMapper _mapper;
        private readonly IBetFacade _betFacade;

        public BetController(IBetsService betService, IBetQuoteService betQuoteService, IMapper mapper, IBetFacade betFacade)
        {
            Console.WriteLine("Face constructorul?");
            _betService = betService;
            _betQuoteService = betQuoteService;
            _mapper = mapper;
            _betFacade = betFacade;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddBetAsync(CreateBetRequestDto betRequest)
        {
            BetRequestDto betRequestDto = await _betFacade.CreateBetAsync(betRequest);
            return Ok(betRequestDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFullBetByIdAsync(Guid id)
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
            Console.WriteLine("INtra aici?");
            IEnumerable<BetRequestDto> betsDtosEntities = await _betFacade.GetAllBetsAsync();
            return Ok(betsDtosEntities);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> EditFullBetAsync(Guid id, UpdateBetRequestDto newEntity)
        {
            BetRequestDto updatedFullBet = await _betFacade.UpdateFullBetAsync(id, newEntity);
            return Ok(updatedFullBet);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByQuoteIdAsync(Guid id)
        {
            bool checkRemove = await _betQuoteService.DeleteFullBetAsync(id);
            if(!checkRemove)
            {
                return NotFound($"Bet Quote with id: {id} doesn't exists");
            }
            return Ok();
        }
    }
}
