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
    public void NullSourceTest()
    {
        IEnumerable<int> items = null;
        Assert.Throws<ArgumentNullException>(() => items.Permute());
    }
}