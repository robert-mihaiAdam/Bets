using System.ComponentModel.DataAnnotations;

namespace Domain.Dto.PlacedBet
{
    public class UpdatePlacedBetDto
    {
        [Required]
        public BetOptions Type { get; set; }
    }
}
