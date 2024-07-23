using System.ComponentModel.DataAnnotations;

namespace Domain.Dto

{
    public class PlacedBetsDto
    {
        [Required]
        public BetOptions Type { get; set; }

        [Required]
        public Guid QuoteId { get; set; }
    }
}
