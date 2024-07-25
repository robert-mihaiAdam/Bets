using AutoMapper;
using Domain.Dto.BetQuote;
using Domain.Dto.Bets;
using Domain.Dto.BetRequest;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Facades;

//{
//    "name": "O mers",
//  "betableEntityA": "492d94c5-c969-4ce0-8749-08dcab19fa72",
//  "betableEntityB": "ae3df49f-3751-470c-aea9-08dcabb6ff6b",
//  "quoteA": 2.34,
//  "quoteB": 19.73,
//  "quoteX": 58.9
//}

namespace BetsApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BetController : Controller
    {
        private readonly IBetsService _betService;
        private readonly IBetQuoteService _betQuoteService;
        private readonly IMapper _mapper;

        public BetController(IBetsService betService, IBetQuoteService betQuoteService, IMapper mapper)
        {
            _betService = betService;
            _betQuoteService = betQuoteService;
            _mapper = mapper;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddBetAsync(CreateBetRequestDto betRequest)
        {
            CreateBetFacade facade = new CreateBetFacade(_mapper, _betService, _betQuoteService, betRequest);
            BetRequestDto betRequestDto = await facade.CreateBet();
            return Ok(betRequestDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFullBetByIdAsync(Guid id)
        {
            BetQuoteDto currentQuote = await _betQuoteService.GetByIdAsync(id);
            if (currentQuote == null)
            {
                return NotFound($"Error: Bet quote with id:{id} doesn't exists");
            }

            if (currentQuote.BetId == null)
            {
                return NotFound("Error: Bet quote doesn't has an bet");
            }

            BetsDto currentBet = await _betService.GetByIdAsync(currentQuote.BetId);
            BetRequestDto betQuoteEntity = new BetRequestDto { bet = currentBet, betQuote = currentQuote };
            return Ok(betQuoteEntity);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<BetRequestDto> betsDtosEntities = await _betQuoteService.GetAllFullBetsAsync();
            return Ok(betsDtosEntities);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> EditFullBetAsync(Guid id, UpdateBetRequestDto newEntity)
        {
            UpdateBetFacade facade = new UpdateBetFacade(_mapper, _betService, _betQuoteService, newEntity);
            BetRequestDto updatedFullBet = await facade.UpdateFullBet(id);
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
