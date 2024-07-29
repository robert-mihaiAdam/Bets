namespace Domain.Dto.Bets
{
    public class BetsDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid BetableEntityA { get; set; }

        public Guid BetableEntityB { get; set; }
    }
}
