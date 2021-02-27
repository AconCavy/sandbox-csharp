using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sandbox.Extensions;

namespace Sandbox.Tests
{
    public class GetOrderedIndexesTests
    {
        [Test]
        public void GetOrderedIndexesTest1()
        {
            const int length = 100;
            var items = Enumerable.Range(0, length).ToArray();
            var expected = items.ToArray();
            var actual = items.GetOrderedIndexes();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetOrderedIndexesTest2()
        {
            const int length = 100;
            var items = Enumerable.Range(0, length).Reverse().ToArray();
            var expected = Enumerable.Range(0, length).Reverse();
            var actual = items.GetOrderedIndexes();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void RandomTest()
        {
            const int length = 100;
            var random = new Random(0);
            var items = new int[length].Select(_ => random.Next()).ToArray();
            var expected = new int[length];
            var indexed = new (int index, int value)[length];
            for (var i = 0; i < length; i++) indexed[i] = (i, items[i]);
            Array.Sort(indexed, (x, y) => x.value.CompareTo(y.value));
            for (var i = 0; i < length; i++) expected[indexed[i].index] = i;
            var actual = items.GetOrderedIndexes();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void RandomDescendingTest()
        {
            const int length = 100;
            var random = new Random(0);
            var items = new int[length].Select(_ => random.Next()).ToArray();
            var expected = new int[length];
            var indexed = new (int index, int value)[length];
            for (var i = 0; i < length; i++) indexed[i] = (i, items[i]);
            Array.Sort(indexed, (x, y) => y.value.CompareTo(x.value));
            for (var i = 0; i < length; i++) expected[indexed[i].index] = i;
            var actual = items.GetOrderedIndexes((x, y) => y.CompareTo(x));

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void RandomWithComparerTest()
        {
            const int length = 100;
            var random = new Random(0);
            var items = new int[length].Select(_ => random.Next()).ToArray();
            var expected = new int[length];
            var indexed = new (int index, int value)[length];
            for (var i = 0; i < length; i++) indexed[i] = (i, items[i]);
            Array.Sort(indexed, (x, y) => x.value.CompareTo(y.value));
            for (var i = 0; i < length; i++) expected[indexed[i].index] = i;
            var actual = items.GetOrderedIndexes(Comparer<int>.Default);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void RandomWithComparerDescendingTest()
        {
            const int length = 100;
            var random = new Random(0);
            var items = new int[length].Select(_ => random.Next()).ToArray();
            var expected = new int[length];
            var indexed = new (int index, int value)[length];
            for (var i = 0; i < length; i++) indexed[i] = (i, items[i]);
            Array.Sort(indexed, (x, y) => y.value.CompareTo(x.value));
            for (var i = 0; i < length; i++) expected[indexed[i].index] = i;
            var actual = items.GetOrderedIndexes(Comparer<int>.Create((x, y) => y.CompareTo(x)));

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void NullSourceTest()
        {
            IEnumerable<int> items = null;

            Assert.Throws<ArgumentNullException>(() => items.GetOrderedIndexes());
            Assert.Throws<ArgumentNullException>(() => items.GetOrderedIndexes(Comparer<int>.Default));
        }

        [Test]
        public void NullComparerTest()
        {
            var items = new int[10];

            Assert.Throws<ArgumentNullException>(() => items.GetOrderedIndexes(comparer: null));
        }
    }
}