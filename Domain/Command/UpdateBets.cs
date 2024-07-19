using System.ComponentModel.DataAnnotations;

namespace Domain.Command
{
    public class UpdateBets
    {
        [StringLength(100)]
        public string? Name { get; set; }
        public long BetableEntityA { get; set; }
        public long BetableEntityB { get; set; }

    }
}
