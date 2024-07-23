using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class BetQuotes
    {
        public Guid Id { get; private set; }

        public Guid BetId { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal QuoteA { get; set; }
        
        [Column(TypeName = "decimal(5, 2)")]
        public decimal QuoteB { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal QuoteX { get; set; }
    }
}
