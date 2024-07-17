namespace Domain.Dto
{
    public static class Abstraction
    {
        public const long noEntities = 1000000;
        public const long noBets = 10000000;
        public const long noClientBets = 10;
        public const string connection_data = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";

        public enum BetsOptions
        {
            A = 'A',
            B = 'B',
            X = 'X'
        }

    }
}
