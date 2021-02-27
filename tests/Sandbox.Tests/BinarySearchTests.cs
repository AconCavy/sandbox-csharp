using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sandbox.Extensions;

namespace Sandbox.Tests
{
    public class BinarySearchTests
    {
        [Test]
        public void BinarySearchTest([Range(-1, 11)] int key)
        {
            const int count = 10;
            var items = Enumerable.Range(1, count).ToArray();
            var expected = SearchNaive(items, key);
            var actual = items.BinarySearch(key);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void BinarySearchMultipleValuesTest()
        {
            const int count = 5;
            var items = new int[count * 2];
            for (var i = 0; i < count; i++) items[i * 2] = items[i * 2 + 1] = i + 1;
            var expected = SearchNaive(items, 2);
            var actual = items.BinarySearch(2);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void LowerBoundTest([Values(0, 1, 99, 101)] int key)
        {
            const int count = 10;
            var items = Enumerable.Range(1, count).Select(x => x * x).ToArray();
            var expected = LowerBoundNaive(items, key);
            var actual = items.LowerBound(key);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void LowerBoundMultipleValuesTest()
        {
            const int count = 5;
            var items = new int[count * 2];
            for (var i = 0; i < count; i++) items[i * 2] = items[i * 2 + 1] = i + 1;
            var expected = LowerBoundNaive(items, 2);
            var actual = items.LowerBound(2);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void UpperBoundTest([Values(0, 1, 99, 101)] int key)
        {
            const int count = 10;
            var items = Enumerable.Range(1, count).Select(x => x * x).ToArray();
            var expected = UpperBoundNaive(items, key);
            var actual = items.UpperBound(key);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void UpperBoundMultipleValuesTest()
        {
            const int count = 5;
            var items = new int[count * 2];
            for (var i = 0; i < count; i++) items[i * 2] = items[i * 2 + 1] = i + 1;
            var expected = UpperBoundNaive(items, 2);
            var actual = items.UpperBound(2);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void NoElementTest()
        {
            var items = new int[0];
            Assert.That(items.BinarySearch(0), Is.EqualTo(SearchNaive(items, 0)));
            Assert.That(items.LowerBound(0), Is.EqualTo(LowerBoundNaive(items, 0)));
            Assert.That(items.UpperBound(0), Is.EqualTo(UpperBoundNaive(items, 0)));
        }

        [Test]
        public void OneElementTest([Range(-1, 1)] int value)
        {
            var items = new[] {value};
            Assert.That(items.BinarySearch(0), Is.EqualTo(SearchNaive(items, 0)));
            Assert.That(items.LowerBound(0), Is.EqualTo(LowerBoundNaive(items, 0)));
            Assert.That(items.UpperBound(0), Is.EqualTo(UpperBoundNaive(items, 0)));
        }

        [Test]
        public void NullSourceTest()
        {
            int[] items = null;
            Assert.Throws<ArgumentNullException>(() => items.BinarySearch(0));
            Assert.Throws<ArgumentNullException>(() => items.LowerBound(0));
            Assert.Throws<ArgumentNullException>(() => items.UpperBound(0));
        }

        private static int SearchNaive<T>(IReadOnlyList<T> source, T key, Comparison<T> comparison = null)
        {
            if (source.Count == 0) return -1;
            comparison ??= Comparer<T>.Default.Compare;
            for (var i = 0; i < source.Count; i++)
                if (comparison(source[i], key) == 0)
                    return i;

            return -1;
        }

        private static int LowerBoundNaive<T>(IReadOnlyList<T> source, T key, Comparison<T> comparison = null)
        {
            if (source.Count == 0) return 0;
            comparison ??= Comparer<T>.Default.Compare;
            int ret;
            for (ret = 0; ret < source.Count; ret++)
                if (comparison(source[ret], key) >= 0)
                    break;

            return ret < source.Count ? ret : ret - 1;
        }

        private static int UpperBoundNaive<T>(IReadOnlyList<T> source, T key, Comparison<T> comparison = null)
        {
            if (source.Count == 0) return 0;
            comparison ??= Comparer<T>.Default.Compare;
            int ret;
            for (ret = 0; ret < source.Count; ret++)
                if (comparison(source[ret], key) > 0)
                    break;

            return ret < source.Count ? ret : ret - 1;
        }
    }
}