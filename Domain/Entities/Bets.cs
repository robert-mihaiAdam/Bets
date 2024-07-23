using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Bets
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public Guid BetableEntityA { get; set; }

        public Guid BetableEntityB { get; set; }
    }
}
