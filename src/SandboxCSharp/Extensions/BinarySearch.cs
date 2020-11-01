using System;
using System.Collections.Generic;

namespace SandboxCSharp.Extensions
{
    public static class IndexedListExtension
    {
        public static int BinarySearch<T>(this IReadOnlyList<T> source, T key, Comparison<T> comparison = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (source.Count == 0) return -1;
            comparison ??= Comparer<T>.Default.Compare;
            var (l, r) = (-1, source.Count);
            while (r - l > 1)
            {
                var m = l + (r - l) / 2;
                var result = comparison(source[m], key);
                if (result < 0) l = m;
                else if (result > 0) r = m;
                else return m;
            }

            return -1;
        }

        public static int LowerBound<T>(this IReadOnlyList<T> source, T key, Comparison<T> comparison = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (source.Count == 0) return 0;
            comparison ??= Comparer<T>.Default.Compare;
            var (l, r) = (-1, source.Count - 1);
            while (r - l > 1)
            {
                var m = l + (r - l) / 2;
                if (comparison(source[m], key) >= 0) r = m;
                else l = m;
            }

            return r;
        }

        public static int UpperBound<T>(this IReadOnlyList<T> source, T key, Comparison<T> comparison = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (source.Count == 0) return 0;
            comparison ??= Comparer<T>.Default.Compare;
            var (l, r) = (-1, source.Count - 1);
            while (r - l > 1)
            {
                var m = l + (r - l) / 2;
                if (comparison(source[m], key) > 0) r = m;
                else l = m;
            }

            return r;
        }
    }
}