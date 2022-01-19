namespace Sandbox.Extensions;

public static partial class EnumerableExtension
{
    public static IEnumerable<T[]> Combine<T>(this IEnumerable<T> source, int count)
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
                var idx = 0;
                for (var i = count - 1; i >= 0; i--)
                {
                    if (indices[i] == i + n - count) continue;
                    idx = i;
                    done = false;
                    break;
                }

                if (done) yield break;

                indices[idx]++;
                for (var i = idx; i + 1 < count; i++)
                {
                    indices[i + 1] = indices[i] + 1;
                }

                Fill();
                yield return result;
            }
        }

        return Inner();
    }
}