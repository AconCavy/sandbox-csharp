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
            return CumulateImpl(source, seed, func);
        }

        public static IEnumerable<TAccumulate> Cumulate<TSource, TAccumulate>(this IEnumerable<TSource> source,
            Func<TAccumulate, TSource, TAccumulate> func)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));
            return CumulateImpl(source, default, func);
        }

        public static IEnumerable<TSource> Cumulate<TSource>(this IEnumerable<TSource> source,
            Func<TSource, TSource, TSource> func)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));
            return CumulateImpl(source, default, func);
        }

        private static IEnumerable<TAccumulate> CumulateImpl<TSource, TAccumulate>(this IEnumerable<TSource> source,
            TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
        {
            yield return seed;
            foreach (var item in source) yield return seed = func(seed, item);
        }
    }
}