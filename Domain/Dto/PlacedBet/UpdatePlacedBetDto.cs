using System.ComponentModel.DataAnnotations;
using Domain.Validations;

namespace Domain.Dto.PlacedBet
{
    public class UpdatePlacedBetDto
    {
        [Required]
        [EnumValidation(typeof(BetOptions))]
        public BetOptions Type { get; set; }

        [Required]
        [Range(1.00, 1000.00, ErrorMessage = "BetPrice must be a positive value and not higher than 1000.00.")]
        public long BetPrice { get; set; }
    }
}
