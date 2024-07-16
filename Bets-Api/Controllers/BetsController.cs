using DataAccess;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Bets_Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class betsController : Controller
    {
        private readonly DBContext _context;

        public betsController(DBContext context)
        {
            _context = context;
        }


        [HttpPost("place")]
        public async Task<IActionResult> PlaceBet(CreateBetRequest betRequest)
        {
            BetsServices betService = new BetsServices(_context);
            BetQuoteServices betQuoteService = new(_context);
            Bets bet = betRequest.Bet;
            BetQuotes quote = betRequest.BetQuote;
            bet = await betService.Create(bet);
            if (bet == null)
                return BadRequest(ModelState);

            quote.BetId = bet.Id;
            await betQuoteService.Create(quote);

            return Ok();
        }

        [HttpPost()]
        public async Task<IActionResult> PlaceBetQuote(PlacedBets quotes)
        {
            //<TODO> User availability (or jwt token)
            PlacedBetsService service = new(_context);
            BetQuoteServices betQuoteService = new(_context);
            PlacedBets newBet = await service.Create(quotes);
            if (newBet == null)
                return BadRequest(ModelState);

            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateQuote(Guid id, UpdatePlacedBets newPlacedBet)
        {
            if (newPlacedBet == null)
            {
                return BadRequest("Body request is empty");
            }
            PlacedBetsService service = new(_context);
            await service.Update(id, newPlacedBet);
            return Ok();
        }

        [HttpGet()]
        public async Task<IEnumerable<Bets>> ListBets()
        {
            BetsServices betService = new BetsServices(_context);

            return await betService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SpecificBet(Guid id)
        {
            BetsServices betService = new BetsServices(_context);
            Bets currentBet = await betService.GetById(id);

            if (currentBet == null)
            {
                return NotFound("");
            }

            return Ok(currentBet);
        }
    }
}
