using NUnit.Framework;
using Sandbox.Extensions;

namespace Sandbox.Tests;

public class PermuteTests
{
    [Test]
    public void PermuteAllTest()
    {
        const int count = 3;
        var items = Enumerable.Range(1, count);

        var expected = new List<int[]>();
        for (var a = 1; a <= count; a++)
            for (var b = 1; b <= count; b++)
                for (var c = 1; c <= count; c++)
                {
                    if (a == b || b == c || c == a) continue;
                    expected.Add(new[] { a, b, c });
                }

        var actual = items.Permute();

        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void PermuteToArrayTest()
    {
        var sut = new[] { 1, 2, 3 };
        var expected = new[]
        {
            new[] { 1, 2, 3 }, new[] { 1, 3, 2 }, new[] { 2, 1, 3 }, new[] { 2, 3, 1 }, new[] { 3, 1, 2 },
            new[] { 3, 2, 1 }
        };
        var actual1 = sut.Permute(3).ToArray();
        var actual2 = sut.Permute().ToArray();

        Assert.That(actual1, Is.EqualTo(expected));
        Assert.That(actual2, Is.EqualTo(expected));
    }

    [Test]
    public void NullSourceTest()
    {
        IEnumerable<int> items = null;
        Assert.Throws<ArgumentNullException>(() => items.Permute());
    }
}