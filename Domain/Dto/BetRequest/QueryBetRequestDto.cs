using Domain.Entities;

namespace Domain.Dto.BetRequest
{
    public class QueryBetRequestDto
    {
        public Entities.Bets bet {  get; set; }
        public BetQuotes betQuote { get; set; }
    }
}
