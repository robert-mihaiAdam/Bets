using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class BetQuotes
    {
        public Guid Id { get; private set; }

        public Guid BetId { get; set; }

        public decimal QuoteA { get; set; }
        
        public decimal QuoteB { get; set; }

        public decimal QuoteX { get; set; }
    }
}
