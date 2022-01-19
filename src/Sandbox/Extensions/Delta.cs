namespace Sandbox.Extensions;

public static partial class EnumerableExtension
{
    public static IEnumerable<T> Delta<T>(this IEnumerable<T> source, Func<T, T, T> func)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (func == null) throw new ArgumentNullException(nameof(func));

        IEnumerable<T> Inner()
        {
            using var e = source.GetEnumerator();
            if (!e.MoveNext()) yield break;
            var prev = e.Current;
            while (e.MoveNext())
            {
                yield return func(prev, e.Current);
                prev = e.Current;
            }
        }

        return Inner();
    }
}