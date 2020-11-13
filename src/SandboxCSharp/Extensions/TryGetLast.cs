using System;
using System.Collections.Generic;

namespace SandboxCSharp.Extensions
{
    public static partial class EnumerableExtension
    {
        public static bool TryGetLast<TSource>(this IEnumerable<TSource> source, out TSource result)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            using var e = source.GetEnumerator();
            var exist = false;
            result = default;
            while (e.MoveNext())
            {
                result = e.Current;
                exist = true;
            }

            return exist;
        }

        public static bool TryGetLast<TSource>(this IEnumerable<TSource> source, out TSource result,
            Predicate<TSource> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            using var e = source.GetEnumerator();
            var exist = false;
            result = default;
            while (e.MoveNext())
            {
                var current = e.Current;
                if (!predicate(current)) continue;
                result = current;
                exist = true;
            }

            return exist;
        }
    }
}