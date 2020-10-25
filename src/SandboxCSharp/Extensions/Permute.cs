using System;
using System.Collections.Generic;
using System.Linq;

namespace SandboxCSharp.Extensions
{
    public static partial class EnumerableExtension
    {
        public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> source, int count)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            IEnumerable<IEnumerable<T>> Inner()
            {
                var items = source.ToArray();
                if (count <= 0 || items.Length < count) throw new ArgumentException(nameof(count));
                var idx = 0;
                var ret = new T[count];
                foreach (var x in Permutation(items.Length, count))
                {
                    ret[idx++] = items[x];
                    if (idx == count) yield return ret;
                    idx %= count;
                }
            }

            return Inner();
        }

        private static IEnumerable<int> Permutation(int n, int r)
        {
            var items = new int[n];
            for (var i = 0; i < n; i++) items[i] = i;

            IEnumerable<int> Inner(int step = 0)
            {
                if (step >= r)
                {
                    for (var i = 0; i < r; i++) yield return items[i];
                    yield break;
                }

                foreach (var x in Inner(step + 1)) yield return x;
                for (var i = step + 1; i < n; i++)
                {
                    (items[step], items[i]) = (items[i], items[step]);
                    foreach (var x in Inner(step + 1)) yield return x;
                    (items[step], items[i]) = (items[i], items[step]);
                }
            }

            return Inner();
        }
    }
}