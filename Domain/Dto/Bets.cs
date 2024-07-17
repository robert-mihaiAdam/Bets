using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class Bets
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime Date { get; private set; } = TimeProvider.System.GetUtcNow().DateTime;

        [Required]
        public Guid BetableEntityA { get; set; }

        [Required]
        public Guid BetableEntityB { get; set; }
    }
}
