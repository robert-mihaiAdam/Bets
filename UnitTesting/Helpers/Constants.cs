namespace UnitTesting.Helpers
{
    public static class Constants
    {
        private static Random random = new Random();

        public static decimal GetRandomDecimal(decimal minValue, decimal maxValue)
        {
            double randomDouble = random.NextDouble();
            decimal scaledValue = (decimal)randomDouble * (maxValue - minValue) + minValue;

            return scaledValue;
        }

        public static T GetRandomEnumValue<T>() where T : Enum
        {
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(random.Next(values.Length));
        }
    }
}
