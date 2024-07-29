using System.ComponentModel.DataAnnotations;

namespace Domain.Dto.BetRequest
{
    public class CreateBetRequestDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public Guid BetableEntityA { get; set; }

        [Required]
        public Guid BetableEntityB { get; set; }

        [Range(1.00, 100.00)]
        public decimal QuoteA { get; set; } = 1.00m;

        [Range(1.00, 100.00)]
        public decimal QuoteB { get; set; } = 1.00m;

        [Range(1.00, 100.00)]
        public decimal QuoteX { get; set; } = 1.00m;
    }
}
