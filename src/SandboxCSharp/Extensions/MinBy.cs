using System;
using System.Collections.Generic;

namespace SandboxCSharp.Extensions
{
    public static partial class EnumerableExtension
    {
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector, bool updateEquals = false) where TKey : IComparable<TKey>
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            TSource Inner()
            {
                using var e = source.GetEnumerator();
                if (!e.MoveNext()) throw new InvalidOperationException();
                var ret = e.Current;
                var min = selector(ret);
                while (e.MoveNext())
                {
                    var current = e.Current;
                    var value = selector(current);
                    if (value.CompareTo(min) > 0) continue;
                    if (!updateEquals && value.CompareTo(min) == 0) continue;
                    ret = current;
                    min = value;
                }

                return ret;
            }

            return Inner();
        }
    }
}