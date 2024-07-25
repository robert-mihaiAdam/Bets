namespace Domain.Dto.Bets
{
    public class UpdateBetsDto
    {
        public string Name { get; set; }

        public Guid BetableEntityA { get; set; }

        public Guid BetableEntityB { get; set; }
    }
}
