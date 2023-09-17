using BenchmarkDotNet.Running;

namespace Sandbox.Benchmark;

public static class Program
{
    private static void Main() =>
        BenchmarkRunner.Run<SortBenchmark>();
}