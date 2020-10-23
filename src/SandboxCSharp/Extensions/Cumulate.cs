using System;
using System.Collections.Generic;

namespace SandboxCSharp.Extensions
{
    public static partial class LinqExtensions
    {
        public static IEnumerable<TAccumulate> Cumulate<TSource, TAccumulate>(this IEnumerable<TSource> source,
            TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));
            yield return seed;
            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext()) yield return seed = func(seed, enumerator.Current);
        }

        public static IEnumerable<TAccumulate> Cumulate<TSource, TAccumulate>(this IEnumerable<TSource> source,
            Func<TAccumulate, TSource, TAccumulate> func) => source.Cumulate(default, func);

        public static IEnumerable<TSource> Cumulate<TSource>(this IEnumerable<TSource> source,
            Func<TSource, TSource, TSource> func) => source.Cumulate(default, func);
    }
}