using System;
using System.Collections.Generic;

namespace Sandbox.Extensions
{
    public static partial class EnumerableExtension
    {
        public static bool TryGetLast<TSource>(this IEnumerable<TSource> source, out TSource result)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var exist = false;
            result = default;
            foreach (var current in source)
            {
                result = current;
                exist = true;
            }

            return exist;
        }

        public static bool TryGetLast<TSource>(this IEnumerable<TSource> source, out TSource result,
            Predicate<TSource> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            var exist = false;
            result = default;
            foreach (var current in source)
            {
                if (!predicate(current)) continue;
                result = current;
                exist = true;
            }

            return exist;
        }
    }
}