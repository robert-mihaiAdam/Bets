namespace Domain.Entities
{
    public class PlacedBets
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime PlacedDate { get; set; }

        public decimal BetPrice { get; set; }

        public BetOptions Type { get; set; }

        public Guid QuoteId { get; set; }
    }
}
