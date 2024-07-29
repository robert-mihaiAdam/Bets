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
        [Range(1, long.MaxValue, ErrorMessage = "BetPrice must be a positive value.")]
        public long BetPrice { get; set; }
    }
}
