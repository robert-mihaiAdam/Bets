namespace Domain.ErrorEntities
{
    public class NotFoundException : Exception
    {
        public string message { get; set; }

        public NotFoundException(string message) : base(message) { }
    }
}
