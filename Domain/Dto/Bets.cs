using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class Bets
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public Guid BetableEntityA { get; set; }

        [Required]
        public Guid BetableEntityB { get; set; }
    }
}
