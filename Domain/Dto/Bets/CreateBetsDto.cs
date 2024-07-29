namespace Domain.Dto.Bets
{
    public class CreateBetsDto
    {
        public string Name { get; set; }

        public Guid BetableEntityA { get; set; }

        public Guid BetableEntityB { get; set; }
    }
}
