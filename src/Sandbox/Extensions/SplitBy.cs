using System;
using System.Collections.Generic;

namespace Sandbox.Extensions
{
    public static partial class EnumerableExtension
    {
        public static IEnumerable<IEnumerable<T>> SplitBy<T>(this IEnumerable<T> source, int size)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (size <= 0) throw new ArgumentException(nameof(size));

            IEnumerable<IEnumerable<T>> Inner()
            {
                var idx = 0;
                var ret = new T[size];
                foreach (var x in source)
                {
                    ret[idx++] = x;
                    if (idx == size) yield return ret;
                    idx %= size;
                }

                if (idx > 0) yield return ret[..idx];
            }

            return Inner();
        }
    }
}