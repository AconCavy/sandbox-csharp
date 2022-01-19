using NUnit.Framework;
using Sandbox.Mathematics;
using Math = System.Math;

namespace Sandbox.Tests;

public class NumberTests
{
    [Test]
    public void IsMultipleOfTest1([Range(1, 30)] int p)
    {
        for (var i = 0; i < 1 << 14; i++)
        {
            var str = i.ToString();
            var expected = i % p == 0;
            var actual = Number.IsMultipleOf(str, (uint)p);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }

    [Test]
    public void IsMultipleOfTest2([Range(1, 30)] int p)
    {
        const long max = long.MaxValue;
        for (var i = max; i >= max - 10000; i--)
        {
            var str = i.ToString();
            var expected = i % p == 0;
            var actual = Number.IsMultipleOf(str, (uint)p);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }

    [Test]
    public void IsMultipleOfTest3()
    {
        const string value =
            "777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777";
        const bool expected = true;
        var actual = Number.IsMultipleOf(value, 7);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void IsMultipleOf2ETest([Values(2, 4, 8, 16, 32, 64, 128, 256, 512)] int p)
    {
        const long max = long.MaxValue;
        for (var i = max; i >= max - 10000; i--)
        {
            var str = i.ToString();
            var expected = i % p == 0;
            var actual = Number.IsMultipleOf(str, (uint)p);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }

    [Test]
    public void IsMultipleOf3ETest([Values(3, 9, 27, 81, 243, 729)] int p)
    {
        const long max = long.MaxValue;
        for (var i = max; i >= max - 10000; i--)
        {
            var str = i.ToString();
            var expected = i % p == 0;
            var actual = Number.IsMultipleOf(str, (uint)p);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }

    [Test]
    public void IsMultipleOf5ETest([Values(5, 25, 125, 625)] int p)
    {
        const long max = long.MaxValue;
        for (var i = max; i >= max - 10000; i--)
        {
            var str = i.ToString();
            var expected = i % p == 0;
            var actual = Number.IsMultipleOf(str, (uint)p);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }

    [Test]
    public void IsMultipleOfZeroTest()
    {
        const string str = "1234567890";
        Assert.Throws<ArgumentException>(() => Number.IsMultipleOf(str, 0));
    }

    [Test]
    public void IsMultipleOfStringIncludesOutOfNumberTest()
    {
        const string str = "1234a67890";
        Assert.Throws<ArgumentException>(() => Number.IsMultipleOf(str, 2, true));
    }

    [Test]
    public void IsMultipleOfEmptyStringTest()
    {
        const string str = "";
        Assert.Throws<ArgumentException>(() => Number.IsMultipleOf(str, 2, true));
    }

    [Test]
    public void IsMultipleOfNullStringTest()
    {
        const string str = null;
        Assert.Throws<ArgumentException>(() => Number.IsMultipleOf(str, 2, true));
    }

    [Test]
    public void ToDigitsTest1()
    {
        const int limit = 1 << 14;
        for (var i = -limit; i < limit; i++)
        {
            var expected = Math.Abs(i).ToString().Select(x => x - '0');
            var actual = Number.ToDigits(i);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }

    [Test]
    public void ToDigitsTest2()
    {
        for (var i = 0; i < 63; i++)
        {
            var n = 1L << i;
            var expected = n.ToString().Select(x => x - '0');
            var actual = Number.ToDigits(n);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }

    [Test]
    public void GetNumberCountsTest()
    {
        const int limit = 1 << 14;
        for (var i = -limit; i <= limit; i++)
        {
            var str = Math.Abs(i).ToString();
            var expected = new int[10];
            foreach (var c in str) expected[c - '0']++;
            var actual = Number.GetNumberCounts(i);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }

    [Test]
    public void GetModuloTest([Range(1, 30)] int p)
    {
        for (var i = 0; i < 1 << 14; i++)
        {
            var str = i.ToString();
            var expected = i % p;
            var actual = Number.GetModulo(str, (uint)p);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}