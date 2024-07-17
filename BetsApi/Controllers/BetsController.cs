using DataAccess;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Services;
using Domain.Command;

namespace BetsApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BetsController : Controller
    {

        [HttpPost("place")]
        public async Task<IActionResult> PlaceBet(CreateBetRequest betRequest)
        {
            BetsServices betService = new ();
            BetQuoteServices betQuoteService = new();
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
            PlacedBetsService service = new();
            BetQuoteServices betQuoteService = new();
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
            PlacedBetsService service = new();
            await service.Update(id, newPlacedBet);
            return Ok();
        }

        [HttpGet()]
        public async Task<IEnumerable<Bets>> ListBets()
        {
            BetsServices betService = new BetsServices();

            return await betService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SpecificBet(Guid id)
        {
            BetsServices betService = new BetsServices();
            Bets currentBet = await betService.GetById(id);

            if (currentBet == null)
            {
                return NotFound("");
            }

            return Ok(currentBet);
        }
    }
}
