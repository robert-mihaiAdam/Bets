using Xunit.Abstractions;
using Xunit.Sdk;

namespace UnitTesting
{
    public class TestPriorityAttribute : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            var sortedMethods = testCases.OrderBy(testCase => testCase.TestMethod.Method.Name).ToList();
            return sortedMethods;
        }
    }
}
