using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using SandboxCSharp.Extensions;

namespace SandboxCSharp.Benchmark
{
    public class PermuteBenchmark
    {
        [ParamsSource(nameof(TestCount))] public int N;
        public static IEnumerable<int> TestCount() => Enumerable.Range(1, 10);
        private readonly Consumer _consumer = new Consumer();

        [Benchmark]
        public void Permute() => Enumerable.Range(0, 10).Permute(N).Consume(_consumer);
    }
}