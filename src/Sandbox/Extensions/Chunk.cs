namespace Sandbox.Extensions;

public static partial class EnumerableExtension
{
    public static IEnumerable<T[]> Chunk<T>(this IEnumerable<T> source, int size)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (size <= 0) throw new ArgumentException(nameof(size));

        IEnumerable<T[]> Inner()
        {
            var idx = 0;
            var result = new T[size];
            foreach (var x in source)
            {
                result[idx++] = x;
                if (idx != size) continue;
                yield return result;
                result = new T[size];
                idx %= size;
            }

            if (idx > 0) yield return result[..idx];
        }

        return Inner();
    }
}