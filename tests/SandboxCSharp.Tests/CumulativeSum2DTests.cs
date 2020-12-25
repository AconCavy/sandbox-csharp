using System;
using NUnit.Framework;

namespace SandboxCSharp.Tests
{
    public class CumulativeSum2DTests
    {
        [Test]
        public void InitializeTest()
        {
            Assert.DoesNotThrow(() => _ = new CumulativeSum2D(1, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new CumulativeSum2D(0, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new CumulativeSum2D(1, 0));
        }

        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        public void ArgumentOutOfRangeTest(int h, int w)
        {
            var cum = new CumulativeSum2D(1, 1);
            Assert.Throws<ArgumentOutOfRangeException>(() => cum.Add(h, w, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => cum.Set(h, w, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => cum.Get(h, w));
            Assert.Throws<ArgumentOutOfRangeException>(() => cum.Sum(h, w));
            Assert.Throws<ArgumentOutOfRangeException>(() => cum.Sum(h, w, 0, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => cum.Sum(0, 0, h, w));
        }

        [Test]
        public void SetAndAddAndGetTest()
        {
            var cum = new CumulativeSum2D(1, 1);
            var expected = 1;
            cum.Set(0, 0, expected);
            var actual = cum.Get(0, 0);
            Assert.That(actual, Is.EqualTo(expected));

            cum.Add(0, 0, expected);
            expected += expected;
            actual = cum.Get(0, 0);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void SumTest()
        {
            const int height = 3;
            const int width = 3;
            var cum = new CumulativeSum2D(height, width);
            for (var i = 0; i < height; i++)
            for (var j = 0; j < width; j++)
                cum.Set(i, j, 1);

            for (var i = 0; i < height; i++)
            for (var j = 0; j < width; j++)
            {
                var expected = (i + 1) * (j + 1);
                var actual = cum.Sum(i, j);
                Assert.That(actual, Is.EqualTo(expected));
            }
        }

        [Test]
        public void MatchTest()
        {
            var random = new Random(0);
            const int height = 50;
            const int width = 50;
            var ft = new FenwickTree2D(height, width);
            var cum = new CumulativeSum2D(height, width);

            for (var i = 0; i < height; i++)
            for (var j = 0; j < width; j++)
            {
                var value = random.Next(0, 10);
                ft.Add(i, j, value);
                cum.Add(i, j, value);
            }

            for (var h1 = 0; h1 < height; h1++)
            for (var w1 = 0; w1 < width; w1++)
            for (var h2 = 0; h2 <= h1; h2++)
            for (var w2 = 0; w2 <= w1; w2++)
            {
                var sum1 = ft.Sum(h1, w1, h2, w2);
                var sum2 = cum.Sum(h1, w1, h2, w2);
                Assert.That(sum1, Is.EqualTo(sum2));
            }
        }
    }
}