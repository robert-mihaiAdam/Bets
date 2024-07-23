using AutoMapper;
using Domain.Dto.BetQuote;
using Domain.Dto.Bets;
using Domain.Dto.BetRequest;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Domain.Entities;

namespace BetsApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BetQuoteController : Controller
    {
        private readonly IBetsService _betService;
        private readonly IBetQuoteService _betQuoteService;
        private readonly IMapper _mapper;

        public BetQuoteController(IBetsService betService, IBetQuoteService betQuoteService, IMapper mapper)
        {
            _betService = betService;
            _betQuoteService = betQuoteService;
            _mapper = mapper;
        }

        [HttpPost("")]
        public async Task<IActionResult> PlaceBetAsync(CreateBetRequest betRequest)
        {
            CreateBetsDto bet = betRequest.Bet;
            CreateBetQuotesDto quote = betRequest.BetQuote;
            Bets createdBet = await _betService.CreateAsync(bet);
            if (createdBet == null)
                return BadRequest(ModelState);

            BetQuotes createdQuote = _mapper.Map<BetQuotes>(quote);
            createdQuote.BetId = createdBet.Id;
            await _betQuoteService.CreateAsync(createdQuote);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBetQuoteByIdAsync(Guid id)
        {
            BetQuotes currentQuote = await _betQuoteService.GetByIdAsync(id);
            if (currentQuote == null)
            {
                return NotFound($"Error: Bet quote with id:{id} doesn't exists");
            }

            if (currentQuote.BetId == null)
            {
               return NotFound($"Error: Bet quote doesn't has an bet");
            }

            Bets currentBet = await _betService.GetByIdAsync(currentQuote.BetId);
            GetBetQuoteDto getQuotesDto = _mapper.Map<GetBetQuoteDto>(currentQuote);
            GetBetsDto getBetsDto = _mapper.Map<GetBetsDto>(currentBet);
            GetBetRequest betQuoteEntity = new GetBetRequest { bet = getBetsDto, betQuote = getQuotesDto };
            return Ok(betQuoteEntity);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBetQuotesAsync()
        {
            return Ok();
        }
    
    }
}
