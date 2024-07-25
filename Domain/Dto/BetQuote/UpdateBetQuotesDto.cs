using System.ComponentModel.DataAnnotations;

namespace Domain.Dto.BetQuote
{
    public class UpdateBetQuotesDto
    {
        [Range(1.00, 100.00)]
        public decimal QuoteA { get; set; } = 1.00m;

        [Range(1.00, 100.00)]
        public decimal QuoteB { get; set; } = 1.00m;

        [Range(1.00, 100.00)]
        public decimal QuoteX { get; set; } = 1.00m;
    }
}
