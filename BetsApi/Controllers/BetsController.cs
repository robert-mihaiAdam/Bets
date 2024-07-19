﻿using Domain.Dto;
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

        [HttpPost("place")]
        public async Task<IActionResult> PlaceBetAsync(CreateBetRequest betRequest)
        {
            Bets bet = betRequest.Bet;
            BetQuotes quote = betRequest.BetQuote;
            bet = await betService.CreateAsync(bet);
            if (bet == null)
                return BadRequest(ModelState);

            quote.BetId = bet.Id;
            await betQuoteService.CreateAsync(quote);

            return Ok();
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

        [HttpGet()]
        public async Task<IEnumerable<Bets>> ListBetsAsync()
        {
            return await betService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SpecificBetAsync(Guid id)
        {
            Bets currentBet = await betService.GetByIdAsync(id);
            if (currentBet == null)
            {
                return NotFound("");
            }

            return Ok(currentBet);
        }
    }
}
