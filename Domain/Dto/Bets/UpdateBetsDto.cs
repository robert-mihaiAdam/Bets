using System.ComponentModel.DataAnnotations;

namespace Domain.Dto.Bets
{
    public class UpdateBetsDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public Guid BetableEntityA { get; set; }

        [Required]
        public Guid BetableEntityB { get; set; }
    }
}
