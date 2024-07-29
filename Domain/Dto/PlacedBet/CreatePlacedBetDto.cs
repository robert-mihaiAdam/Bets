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
        [Range(1, long.MaxValue, ErrorMessage = "BetPrice must be a positive value.")]
        public long BetPrice { get; set; }

        [Required]
        public Guid QuoteId { get; set; }
    }
}
