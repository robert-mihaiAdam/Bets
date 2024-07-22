namespace Domain.Entities
{
    public class CreateBetRequest
    {
        public Bets Bet { get; set; }
        public BetQuotes BetQuote { get; set; }
    }
}
