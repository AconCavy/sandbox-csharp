﻿using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using SandboxCSharp.Extensions;

namespace SandboxCSharp.Benchmark
{
    [MemoryDiagnoser]
    public class CombineBenchmark
    {
        [ParamsSource(nameof(TestCount))] public int N;
        public static IEnumerable<int> TestCount() => Enumerable.Range(1, 10);
        private readonly Consumer _consumer = new Consumer();

        [Benchmark]
        public void Combine() => Enumerable.Range(0, 10).Combine(N).Consume(_consumer);
    }
}