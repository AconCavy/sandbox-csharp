using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using SandboxCSharp.Extensions;

namespace SandboxCSharp.Benchmark
{
    [MemoryDiagnoser]
    public class CombineBenchmark
    {
        private readonly Consumer _consumer = new Consumer();
        [ParamsSource(nameof(TestCount))] public int N;

        public static IEnumerable<int> TestCount()
        {
            return Enumerable.Range(1, 10);
        }

        [Benchmark]
        public void Combine()
        {
            Enumerable.Range(0, 10).Combine(N).Consume(_consumer);
        }
    }
}