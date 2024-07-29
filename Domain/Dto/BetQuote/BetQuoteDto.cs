namespace Domain.Dto.BetQuote
{
    public class BetQuoteDto
    {
        public Guid Id { get; private set; }

        public Guid BetId { get; set; }

        public decimal QuoteA { get; set; }

        public decimal QuoteB { get; set; }

        public decimal QuoteX { get; set; }
    }
}
