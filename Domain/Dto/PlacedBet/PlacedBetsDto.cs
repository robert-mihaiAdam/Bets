using System.ComponentModel.DataAnnotations;

namespace Domain.Dto.PlacedBet

{
    public class PlacedBetsDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime PlacedDate { get; set; }

        public BetOptions Type { get; set; }

        public Guid QuoteId { get; set; }
    }
}
