using NUnit.Framework;
using Sandbox.Extensions;

namespace Sandbox.Tests;

public class OrderedIndexesTests
{
    [Test]
    public void OrderedIndexesTest1()
    {
        const int length = 100;
        var items = Enumerable.Range(0, length).ToArray();
        var expected = items.ToArray();
        var actual = items.OrderedIndexes();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void OrderedIndexesTest2()
    {
        const int length = 100;
        var items = Enumerable.Range(0, length).Reverse().ToArray();
        var expected = Enumerable.Range(0, length).Reverse();
        var actual = items.OrderedIndexes();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void RandomTest()
    {
        const int length = 100;
        var random = new Random(0);
        var items = new int[length].Select(_ => random.Next()).ToArray();
        var expected = items.OrderBy(x => x);
        var actual = items.OrderedIndexes().Select(x => items[x]);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void RandomDescendingTest()
    {
        const int length = 100;
        var random = new Random(0);
        var items = new int[length].Select(_ => random.Next()).ToArray();
        var expected = items.OrderByDescending(x => x);
        var actual = items.OrderedIndexes((x, y) => y.CompareTo(x)).Select(x => items[x]);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void RandomWithComparerTest()
    {
        const int length = 100;
        var random = new Random(0);
        var items = new int[length].Select(_ => random.Next()).ToArray();
        var expected = items.OrderBy(x => x);
        var actual = items.OrderedIndexes(Comparer<int>.Default).Select(x => items[x]);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void RandomWithComparerDescendingTest()
    {
        const int length = 100;
        var random = new Random(0);
        var items = new int[length].Select(_ => random.Next()).ToArray();
        var expected = items.OrderByDescending(x => x);
        var actual = items.OrderedIndexes(Comparer<int>.Create((x, y) => y.CompareTo(x))).Select(x => items[x]);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void NullSourceTest()
    {
        IEnumerable<int> items = null;

        Assert.Throws<ArgumentNullException>(() => items.OrderedIndexes());
        Assert.Throws<ArgumentNullException>(() => items.OrderedIndexes(Comparer<int>.Default));
    }

    [Test]
    public void NullComparerTest()
    {
        var items = new int[10];

        Assert.Throws<ArgumentNullException>(() => items.OrderedIndexes(comparer: null));
    }
}