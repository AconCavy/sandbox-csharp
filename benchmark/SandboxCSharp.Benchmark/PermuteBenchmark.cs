using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using SandboxCSharp.Extensions;

namespace SandboxCSharp.Benchmark
{
    [MemoryDiagnoser]
    public class PermuteBenchmark
    {
        private readonly Consumer _consumer = new Consumer();
        [ParamsSource(nameof(TestCount))] public int N;

        public static IEnumerable<int> TestCount() => Enumerable.Range(1, 10);

        [Benchmark]
        public void Permute() => Enumerable.Range(0, N).Permute(N).Consume(_consumer);
    }
}