using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Domain.Command;
using Services.Interfaces;

namespace BetsApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BetsController : Controller
    {
        private readonly IBetsService betService;
        private readonly IBetQuoteService betQuoteService;
        private readonly IPlacedBetsService placedBetsService;

        public BetsController(IBetsService betService,
                              IBetQuoteService betQuoteService,
                              IPlacedBetsService placedBetsService)
        {
            this.betService = betService;
            this.betQuoteService = betQuoteService;
            this.placedBetsService = placedBetsService;
        }

        [HttpPost()]
        public async Task<IActionResult> PlaceBetQuoteAsync(PlacedBets quotes)
        {
            //<TODO> User availability (or jwt token)
            PlacedBets newBet = await placedBetsService.CreateAsync(quotes);
            if (newBet == null)
                return BadRequest(ModelState);

            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateQuoteAsync(Guid id, UpdatePlacedBets newPlacedBet)
        {
            if (newPlacedBet == null)
            {
                return BadRequest("Body request is empty");
            }
            await placedBetsService.UpdateAsync(id, newPlacedBet);
            return Ok();
        }
    }
}
