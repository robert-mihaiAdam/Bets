namespace Domain
{
    public class BetableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }

    public class UpdateBetableEntity
    {
        public string Name { get; set; }
    }
}
