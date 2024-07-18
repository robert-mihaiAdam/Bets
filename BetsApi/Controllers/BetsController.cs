using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Services;
using Domain.Command;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace BetsApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BetsController : Controller
    {
        private readonly IBetsService<Bets, UpdateBets> betService;
        private readonly IBetQuoteService<BetQuotes, UpdateBetQuotes> betQuoteService;
        private readonly IPlacedBetsService<PlacedBets, UpdatePlacedBets> placedBetsService;
        private readonly IBetableEntityService<BetableEntity, UpdateBetableEntity> betableEntityService;

        public BetsController(DBContext context,
                              IBetsService<Bets, UpdateBets> betService,
                              IBetableEntityService<BetableEntity, UpdateBetableEntity> betableEntityService,
                              IBetQuoteService<BetQuotes, UpdateBetQuotes> betQuoteService,
                              IPlacedBetsService<PlacedBets, UpdatePlacedBets> placedBetsService)
        {
            this.betService = betService;
            this.betableEntityService = betableEntityService;
            this.betQuoteService = betQuoteService;
            this.placedBetsService = placedBetsService;
        }

        [HttpPost("place")]
        public async Task<IActionResult> PlaceBet(CreateBetRequest betRequest)
        {
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
            PlacedBets newBet = await placedBetsService.Create(quotes);
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
            await placedBetsService.Update(id, newPlacedBet);
            return Ok();
        }

        [HttpGet()]
        public async Task<IEnumerable<Bets>> ListBets()
        {
            return await betService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SpecificBet(Guid id)
        {
            Bets currentBet = await betService.GetById(id);
            if (currentBet == null)
            {
                return NotFound("");
            }

            return Ok(currentBet);
        }
    }
}
