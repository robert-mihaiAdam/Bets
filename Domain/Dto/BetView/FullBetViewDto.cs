using Domain.Dto.PlacedBet;
using Domain.Dto.BetQuote;
using Domain.Dto.Bets;
using Domain.Dto.BetableEntity;

namespace Domain.Dto.FullBetView
{
    public class FullBetViewDto
    {
        public PlacedBetsDto PlacedBets { get; set; }

        public BetQuoteDto BetQuote { get; set; }

        public BetsDto Bet { get; set; }

        public BetableEntityDto BetableEntityA { get; set; }

        public BetableEntityDto BetableEntityB { get; set; }
    }
}
