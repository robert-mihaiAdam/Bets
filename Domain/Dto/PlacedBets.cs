using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Dto
{
    public class PlacedBets
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        public DateTime PlacedDate { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public Guid QuoteId { get; set; }

        public void SetTime(TimeProvider timeProvider)
        {
            PlacedDate = timeProvider.GetUtcNow().DateTime;
        }
    }
}
