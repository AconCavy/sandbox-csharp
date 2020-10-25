using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SandboxCSharp.Extensions;

namespace SandboxCSharp.Tests
{
    public class DivideByTests
    {
        [Test]
        public void DivideByTest()
        {
            var items = Enumerable.Range(0, 9);

            var expected = new[] {new[] {0, 1, 2}, new[] {3, 4, 5}, new[] {6, 7, 8}};
            var actual = items.DivideBy(3);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void FragmentTest()
        {
            var items = Enumerable.Range(0, 10);

            var expected = new[] {new[] {0, 1, 2}, new[] {3, 4, 5}, new[] {6, 7, 8}, new[] {9}};
            var actual = items.DivideBy(3);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void LessThanSizeTest()
        {
            var items = Enumerable.Range(0, 2);

            var expected = new[] {new[] {0, 1}};
            var actual = items.DivideBy(3);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void NullSourceTest()
        {
            IEnumerable<int> items = null;
            Assert.Throws<ArgumentNullException>(() => items.DivideBy(1));
        }

        [Test]
        public void SizeLessThanOrEquals0Test()
        {
            var items = Enumerable.Range(0, 5);
            Assert.Throws<ArgumentException>(() => items.DivideBy(0));
            Assert.Throws<ArgumentException>(() => items.DivideBy(-1));
        }
    }
}