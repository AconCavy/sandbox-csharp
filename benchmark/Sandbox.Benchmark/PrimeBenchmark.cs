using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Sandbox.Mathematics;

namespace Sandbox.Benchmark
{
    [MemoryDiagnoser]
    public class PrimeBenchmark
    {
        [Params(100, 10000, 1000000)] public int N;
        private readonly Consumer _consumer = new Consumer();

        [Benchmark]
        public void GetFactorDictionary()
        {
            for (var i = 0; i <= N; i++) Prime.GetFactorDictionary(i);
        }

        [Benchmark]
        public void GetFactorDictionaryNaive()
        {
            for (var i = 0; i <= N; i++) Naives.Prime.GetFactorDictionary(i);
        }

        [Benchmark]
        public void Sieve() => Prime.Sieve(N).Consume(_consumer);

        [Benchmark]
        public void SieveNaive() => Naives.Prime.Sieve(N).Consume(_consumer);

        [Benchmark]
        public void IsPrime()
        {
            for (var i = 0; i <= N; i++) Prime.IsPrime(i);
        }

        [Benchmark]
        public void IsPrimeNaive()
        {
            for (var i = 0; i <= N; i++) Naives.Prime.IsPrime(i);
        }
    }
}