using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class PlacedBets
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        public DateTime PlacedDate { get; private set; } = DateTime.UtcNow;

        [Required]
        public String Type { get; set; }

        [Required]
        public Guid QuoteId { get; set; }
    }

    public class UpdatePlacedBets
    {
        public String Type { get; set; }
    }
}
