using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Sandbox.Extensions;

namespace Sandbox.Benchmark
{
    [MemoryDiagnoser]
    public class CombineBenchmark
    {
        private readonly Consumer _consumer = new Consumer();
        [ParamsSource(nameof(TestCount))] public int N;

        public static IEnumerable<int> TestCount() => Enumerable.Range(1, 10);

        [Benchmark]
        public void Combine() => Enumerable.Range(0, 10).Combine(N).Consume(_consumer);
    }
}