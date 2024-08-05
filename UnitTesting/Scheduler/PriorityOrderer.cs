using Xunit.Abstractions;
using Xunit.Sdk;

namespace UnitTesting.Scheduler
{
    public class PriorityOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            var sortedTestCases = testCases.OrderBy(tc =>
            {
                var priorityAttribute = tc.TestMethod.Method
                    .GetCustomAttributes(typeof(TestPriorityAttribute).AssemblyQualifiedName)
                    .FirstOrDefault();

                return priorityAttribute == null ? 0 : priorityAttribute.GetNamedArgument<int>("Priority");
            });

            return sortedTestCases;
        }
    }
}
