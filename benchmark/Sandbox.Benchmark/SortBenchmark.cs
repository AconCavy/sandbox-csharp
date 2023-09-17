using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Sandbox.Algorithms;

namespace Sandbox.Benchmark;

[MemoryDiagnoser]
public class SortBenchmark
{
    private const int N = 10000;
    private readonly int[] _items = Enumerable.Range(0, N).Reverse().ToArray();
    private readonly Consumer _consumer = new();

    [Benchmark]
    public void ArraySort()
    {
        var items = _items.ToArray();
        Array.Sort(items);
        _consumer.Consume(items);
    }

    [Benchmark]
    public void OrderSort()
    {
        var items = _items.Order().ToArray();
        _consumer.Consume(items);
    }

    [Benchmark]
    public void QuickSort()
    {
        var items = _items.ToArray();
        SortAlgorithm.Quick(items.AsSpan());
        _consumer.Consume(items);
    }

    [Benchmark]
    public void BubbleSort()
    {
        var items = _items.ToArray();
        SortAlgorithm.Bubble(items.AsSpan());
        _consumer.Consume(items);
    }

    [Benchmark]
    public void MergeSort()
    {
        var items = _items.ToArray();
        SortAlgorithm.Merge(items.AsSpan());
        _consumer.Consume(items);
    }

    [Benchmark]
    public void InsertionSort()
    {
        var items = _items.ToArray();
        SortAlgorithm.Insertion(items.AsSpan());
        _consumer.Consume(items);
    }

    [Benchmark]
    public void ShellSort()
    {
        var items = _items.ToArray();
        SortAlgorithm.Shell(items.AsSpan());
        _consumer.Consume(items);
    }
}