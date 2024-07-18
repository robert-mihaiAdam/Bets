using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class PlacedBets
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        public DateTime PlacedDate { get; private set; } = DateTime.UtcNow;

        [Required]
        public string Type { get; set; }

        [Required]
        public Guid QuoteId { get; set; }
    }
}
