using NUnit.Framework;
using Sandbox.Algorithms;

namespace Sandbox.Tests;

public class SortTests
{
    private const int N = 10000;
    private readonly int[] _items = Enumerable.Range(0, N).Reverse().ToArray();

    [Test]
    public void QuickSortTest()
    {
        var expected = _items.ToArray();
        Array.Sort(expected);

        var actual = _items.ToArray();
        SortAlgorithm.Quick(actual.AsSpan());

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void BubbleSortTest()
    {
        var expected = _items.ToArray();
        Array.Sort(expected);

        var actual = _items.ToArray();
        SortAlgorithm.Bubble(actual.AsSpan());

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MergeSortTest()
    {
        var expected = _items.ToArray();
        Array.Sort(expected);

        var actual = _items.ToArray();
        SortAlgorithm.Merge(actual.AsSpan());

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void InsertionSortTest()
    {
        var expected = _items.ToArray();
        Array.Sort(expected);

        var actual = _items.ToArray();
        SortAlgorithm.Insertion(actual.AsSpan());

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ShellSortTest()
    {
        var expected = _items.ToArray();
        Array.Sort(expected);

        var actual = _items.ToArray();
        SortAlgorithm.Shell(actual.AsSpan());

        Assert.That(actual, Is.EqualTo(expected));
    }
}