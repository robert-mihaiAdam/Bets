using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class PlacedBets
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public DateTime PlacedDate { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public Guid QuoteId { get; set; }
    }
}
