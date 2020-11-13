using System;
using System.Collections.Generic;

namespace SandboxCSharp.Extensions
{
    public static partial class EnumerableExtension
    {
        public static bool TryGetFirst<TSource>(this IEnumerable<TSource> source, out TSource result)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            using var e = source.GetEnumerator();
            var exist = e.MoveNext();
            result = exist ? e.Current : default;
            return exist;
        }

        public static bool TryGetFirst<TSource>(this IEnumerable<TSource> source, out TSource result,
            Predicate<TSource> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            foreach (var current in source)
            {
                if (!predicate(current)) continue;
                result = current;
                return true;
            }

            result = default;
            return false;
        }
    }
}