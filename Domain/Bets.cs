using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Bets
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime Date { get; private set; } = DateTime.UtcNow;

        [Required]
        public Guid BetableEntityA { get; set; }

        [Required]
        public Guid BetableEntityB { get; set; }
    }

    public class UpdateBets
    {
        [StringLength(100)]
        public string? Name { get; set; }
        public long BetableEntityA { get; set; }
        public long BetableEntityB { get; set; }

    }
}
