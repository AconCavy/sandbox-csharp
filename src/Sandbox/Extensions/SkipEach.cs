using System;
using System.Collections.Generic;

namespace Sandbox.Extensions
{
    public static partial class EnumerableExtension
    {
        public static IEnumerable<T> SkipEach<T>(this IEnumerable<T> source, int count)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (count < 0) throw new ArgumentException(nameof(count));
            if (count == 0) return source;

            IEnumerable<T> Inner()
            {
                var idx = 0;
                count++;
                foreach (var item in source)
                {
                    if (idx == 0) yield return item;
                    idx++;
                    idx %= count;
                }
            }

            return Inner();
        }
    }
}