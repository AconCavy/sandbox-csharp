using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox.Extensions
{
    public static partial class EnumerableExtension
    {
        public static IEnumerable<int> OrderedIndexes<T>(this IEnumerable<T> source, Comparison<T> comparison = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            comparison ??= Comparer<T>.Default.Compare;
            var tmp = source.ToArray();
            var order = Enumerable.Range(0, tmp.Length).ToArray();
            Array.Sort(order, (x, y) => comparison(tmp[x], tmp[y]));
            return order;
        }

        public static IEnumerable<int> OrderedIndexes<T>(this IEnumerable<T> source, IComparer<T> comparer)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            return OrderedIndexes(source, comparer.Compare);
        }
    }
}