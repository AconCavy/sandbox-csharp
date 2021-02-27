using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox.Extensions
{
    public static partial class EnumerableExtension
    {
        public static IEnumerable<int> GetOrderedIndexes<T>(this IEnumerable<T> source, Comparison<T> comparison = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            comparison ??= Comparer<T>.Default.Compare;
            var items = source.Select((x, i) => (x, i)).ToArray();
            Array.Sort(items, (l, r) => comparison(l.x, r.x));
            var indexes = new int[items.Length];
            for (var i = 0; i < items.Length; i++) indexes[items[i].i] = i;
            return indexes;
        }

        public static IEnumerable<int> GetOrderedIndexes<T>(this IEnumerable<T> source, IComparer<T> comparer)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            return GetOrderedIndexes(source, comparer.Compare);
        }
    }
}