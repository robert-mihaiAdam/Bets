using BenchmarkDotNet.Running;

namespace Benchmark
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DbPerformance dbPerformance = new DbPerformance();
            await dbPerformance.Setup();

            await dbPerformance.CreateBets();
            await dbPerformance.GetQuoteByBetsId();

            await dbPerformance.CleanUp();
        }
    }
}