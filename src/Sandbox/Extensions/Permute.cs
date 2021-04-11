using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox.Extensions
{
    public static partial class EnumerableExtension
    {
        public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            IEnumerable<IEnumerable<T>> Inner()
            {
                var items = source.ToArray();
                var ret = new T[items.Length];
                foreach (var indices in PermuteIndices(items.Length))
                {
                    var idx = 0;
                    foreach (var index in indices) ret[idx++] = items[index];
                    yield return ret;
                }
            }

            return Inner();
        }

        private static IEnumerable<IEnumerable<int>> PermuteIndices(int n)
        {
            var indices = Enumerable.Range(0, n).ToArray();
            yield return indices;
            while (true)
            {
                var (i, j) = (indices.Length - 2, indices.Length - 1);
                while (i >= 0)
                {
                    if (indices[i] < indices[i + 1]) break;
                    i--;
                }

                if (i == -1) yield break;
                while (true)
                {
                    if (indices[j] > indices[i]) break;
                    j--;
                }

                (indices[i], indices[j]) = (indices[j], indices[i]);
                Array.Reverse(indices, i + 1, indices.Length - 1 - i);
                yield return indices;
            }
        }
    }
}