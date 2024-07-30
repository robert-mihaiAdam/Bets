using Domain.Validations;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dto.PlacedBet
{
    public class CreatePlacedBetDto
    {
        [Required]
        [EnumValidation(typeof(BetOptions))]
        public BetOptions Type { get; set; }

        [Required]
        [Range(1.00, 1000.00, ErrorMessage = "BetPrice must be a positive value and not higher than 1000.00.")]
        public decimal BetPrice { get; set; }

        [Required]
        public Guid QuoteId { get; set; }
    }
}
