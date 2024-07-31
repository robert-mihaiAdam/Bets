using Xunit.Abstractions;
using Xunit.Sdk;

namespace UnitTesting
{
    public class PriorityOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            var sortedTestCases = testCases.OrderBy(testCase =>
            {
                var priorityAttribute = testCase.TestMethod.Method.GetCustomAttributes(typeof(PriorityAttribute)).FirstOrDefault();
                return priorityAttribute == null ? int.MaxValue : priorityAttribute.GetNamedArgument<int>("Priority");
            });

            return sortedTestCases;
        }
    }
}
