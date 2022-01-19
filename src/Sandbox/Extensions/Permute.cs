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

            var result = new T[n];

            void Fill()
            {
                for (var i = 0; i < n; i++)
                {
                    result[i] = items[indices[i]];
                }
            }

            Fill();
            yield return result;
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
                Fill();
                yield return result;
            }
        }

        return Inner();
    }

    public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> source, int count)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));

        IEnumerable<T[]> Inner()
        {
            var items = source.ToArray();
            if (count <= 0 || items.Length < count) throw new ArgumentOutOfRangeException(nameof(count));
            var n = items.Length;
            var indices = new int[n];
            for (var i = 0; i < indices.Length; i++)
            {
                indices[i] = i;
            }

            var cycles = new int[count];
            for (var i = 0; i < cycles.Length; i++)
            {
                cycles[i] = n - i;
            }

            var result = new T[count];

            void Fill()
            {
                for (var i = 0; i < count; i++)
                {
                    result[i] = items[indices[i]];
                }
            }

            Fill();
            yield return result;
            while (true)
            {
                var done = true;
                for (var i = count - 1; i >= 0; i--)
                {
                    cycles[i]--;
                    if (cycles[i] == 0)
                    {
                        for (var j = i; j + 1 < indices.Length; j++)
                        {
                            (indices[j], indices[j + 1]) = (indices[j + 1], indices[j]);
                        }

                        cycles[i] = n - i;
                    }
                    else
                    {
                        (indices[i], indices[^cycles[i]]) = (indices[^cycles[i]], indices[i]);
                        Fill();
                        yield return result;
                        done = false;
                        break;
                    }
                }

                if (done) yield break;
            }
        }

        return Inner();
    }
}