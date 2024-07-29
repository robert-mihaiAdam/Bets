using System.ComponentModel.DataAnnotations;

namespace Domain.Dto.PlacedBet
{
    public class CreatePlacedBetDto
    {
        [Required]
        public BetOptions Type { get; set; }

        [Required]
        public Guid QuoteId { get; set; }
    }
}
