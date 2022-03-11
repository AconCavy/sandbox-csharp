namespace Sandbox.Extensions;

public static partial class EnumerableExtension
{
    public static IEnumerable<T[]> Permute<T>(this IEnumerable<T> source)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));

        IEnumerable<T[]> Inner()
        {
            var items = source.ToArray();
            var n = items.Length;
            var indices = new int[n];
            for (var i = 0; i < indices.Length; i++)
            {
                indices[i] = i;
            }

            T[] Result()
            {
                var result = new T[n];
                for (var i = 0; i < n; i++)
                {
                    result[i] = items[indices[i]];
                }

                return result;
            }

            yield return Result();
            while (true)
            {
                var (i, j) = (n - 2, n - 1);
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
                Array.Reverse(indices, i + 1, n - 1 - i);
                yield return Result();
            }
        }

        return Inner();
    }
}