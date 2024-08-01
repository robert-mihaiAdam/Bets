using System;
using System.Reflection;

namespace UnitTesting
{
    public static class Constants
    {
        public static string connection_string = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";
        private static Random random = new Random();

        public static decimal GetRandomDecimal(decimal minValue, decimal maxValue)
        {
            double randomDouble = random.NextDouble();
            decimal scaledValue = (decimal)randomDouble * (maxValue - minValue) + minValue;

            return scaledValue;
        }

        public static bool CompareFields<T>(T obj1, T obj2)
        {
            if (obj1 == null || obj2 == null)
            {
                return obj1 == null && obj2 == null;
            }

            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (FieldInfo field in fields)
            {
                object value1 = field.GetValue(obj1);
                object value2 = field.GetValue(obj2);

                if (!object.Equals(value1, value2))
                {
                    return false;
                }
            }
            return true;
        }

        public static T GetRandomEnumValue<T>() where T : Enum
        {
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(random.Next(values.Length));
        }
    }
}
