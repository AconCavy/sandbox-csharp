using System;
using System.Linq;
using NUnit.Framework;

namespace SandboxCSharp.Tests
{
    public class NumberTests
    {
        [Test]
        public void IsMultipleOfTest1([Range(1, 11)] int p)
        {
            for (var i = 0; i < 1 << 16; i++)
            {
                var str = i.ToString();
                var expected = i % p == 0;
                var actual = Number.IsMultipleOf(str, (uint) p);
                Assert.That(actual, Is.EqualTo(expected));
            }
        }

        [Test]
        public void IsMultipleOfTest2([Values(13, 17, 19, 23, 29)] int p)
        {
            for (var i = 0; i < 1 << 16; i++)
            {
                var str = i.ToString();
                var expected = i % p == 0;
                var actual = Number.IsMultipleOf(str, (uint) p);
                Assert.That(actual, Is.EqualTo(expected));
            }
        }

        [Test]
        public void IsMultipleOfTest3([Range(1, 11)] int p)
        {
            const long max = long.MaxValue;
            for (var i = max; i >= max - 100000; i--)
            {
                var str = i.ToString();
                var expected = i % p == 0;
                var actual = Number.IsMultipleOf(str, (uint) p);
                Assert.That(actual, Is.EqualTo(expected));
            }
        }

        [Test]
        public void IsMultipleOfTest4([Values(13, 17, 19, 23, 29)] int p)
        {
            const long max = long.MaxValue;
            for (var i = max; i >= max - 100000; i--)
            {
                var str = i.ToString();
                var expected = i % p == 0;
                var actual = Number.IsMultipleOf(str, (uint) p);
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
            const int limit = 1 << 16;
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
            const int limit = 1 << 16;
            for (var i = -limit; i <= limit; i++)
            {
                var str = Math.Abs(i).ToString();
                var expected = new int[10];
                foreach (var c in str) expected[c - '0']++;
                var actual = Number.GetNumberCounts(i);
                Assert.That(actual, Is.EqualTo(expected));
            }
        }
    }
}