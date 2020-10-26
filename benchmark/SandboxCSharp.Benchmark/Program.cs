using BenchmarkDotNet.Running;

namespace SandboxCSharp.Benchmark
{
    public static class Program
    {
        private static void Main()
        {
            // BenchmarkRunner.Run<PrimeBenchmark>();
            // BenchmarkRunner.Run<PermuteBenchmark>();
            BenchmarkRunner.Run<CombineBenchmark>();
        }
    }
}