using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Sandbox.Mathematics;

namespace Sandbox.Benchmark;

[MemoryDiagnoser]
public class PermutationBenchmark
{
    private const int N = 8;
    private readonly int[] _source = Enumerable.Range(0, N).ToArray();
    private readonly Consumer _consumer = new();

    [Benchmark]
    public void GeneratePermutationWithLength()
    {
        var buffer = new int[N];
        foreach (var indices in Permutation.Generate(N))
        {
            for (var i = 0; i < N; i++)
            {
                buffer[i] = _source[indices[i]];
            }

            _consumer.Consume(buffer);
        }
    }
}