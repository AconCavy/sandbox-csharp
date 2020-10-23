using BenchmarkDotNet.Attributes;

namespace SandboxCSharp.Benchmark
{
    [MemoryDiagnoser]
    public class PrimeBenchmark
    {
        [Params(100, 10000, 1000000)]
        public int N;

        [Benchmark]
        public void GetPrimes() => Prime.GetPrimes(N);

        [Benchmark]
        public void GetPrimesNaive() => NaivePrime.GetPrimes(N);

        [Benchmark]
        public void IsPrime()
        {
            for (var i = 0; i <= N; i++) Prime.IsPrime(i);
        }

        [Benchmark]
        public void IsPrimeNaive()
        {
            for (var i = 0; i <= N; i++) NaivePrime.IsPrime(i);
        }
    }
}