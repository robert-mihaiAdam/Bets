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
            BetsDto createdBet = await _betService.CreateAsync(bet);
            Console.WriteLine("Pana aici e ok");
            if (createdBet == null)
                return BadRequest(ModelState);

            BetQuoteDto createQuote = await _betQuoteService.CreateAsync(quote, createdBet.Id);
            BetRequestDto betRequestDto = new BetRequestDto{bet = createdBet ,betQuote = createQuote};
            return Ok(betRequestDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBetQuoteByIdAsync(Guid id)
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

        [HttpGet("betQuote/all")]
        public async Task<IActionResult> GetAllBetQuotesAsync()
        {
            IEnumerable<BetQuoteDto> betQuoteEntities = await _betQuoteService.GetAllAsync();
            return Ok(betQuoteEntities);
        }

        [HttpGet("bet/all")]
        public async Task<IActionResult> GetAllBetAsync()
        {
            IEnumerable<BetsDto> betsDtosEntities = await _betService.GetAllAsync();
            return Ok(betsDtosEntities);
        }

        [HttpGet("fullBets/all")]
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<BetRequestDto> betsDtosEntities = await _betQuoteService.GetAllFullBetsAsync();
            return Ok(betsDtosEntities);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> EditFullBetAsync(Guid id, UpdateBetRequestDto newEntity)
        {
            UpdateBetQuotesDto newBetQuote = newEntity.betQuote;
            UpdateBetsDto newBet = newEntity.bets;
            BetQuoteDto updatedBetQuote = await _betQuoteService.UpdateById(id, newBetQuote);
            if (updatedBetQuote == null)
            {
                return NotFound($"Bet Quote with id: {id} doesn't exists");
            }

            Guid betId = updatedBetQuote.BetId;
            BetsDto updatedBetDto = await _betService.UpdateById(betId, newBet);
            if (updatedBetDto == null)
            {
                return NotFound($"Bet with id: {betId} doesn't exists");
            }

            BetRequestDto updatedFullBet = new BetRequestDto { bet = updatedBetDto, betQuote = updatedBetQuote };

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
