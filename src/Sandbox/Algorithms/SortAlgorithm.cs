using System;
using System.Collections.Generic;

namespace Sandbox.Algorithms
{
    public static class SortAlgorithm
    {
        public static void Quick<T>(Span<T> source, int l, int r, Comparer<T> comparer = null) =>
            Quick(source, l, r, (comparer ?? Comparer<T>.Default).Compare);

        public static void Quick<T>(Span<T> source, int l, int r, Comparison<T> comparison)
        {
            if (l < 0 || source.Length < r) throw new ArgumentOutOfRangeException();
            if (r <= l) throw new ArgumentException();
            comparison ??= Comparer<T>.Default.Compare;

            var pivot = source[(l + r) / 2];

            var (ll, rr) = (l, r - 1);
            while (true)
            {
                while (comparison(pivot, source[ll]) > 0) ll++;
                while (comparison(pivot, source[rr]) < 0) rr--;
                if (ll >= rr) break;
                (source[ll], source[rr]) = (source[rr], source[ll]);
            }

            if (ll - l > 1) Quick(source, l, ll, comparison);
            if (r - rr > 1) Quick(source, rr, r, comparison);
        }

        public static void Bubble<T>(Span<T> source, int l, int r, Comparer<T> comparer = null) =>
            Bubble(source, l, r, (comparer ?? Comparer<T>.Default).Compare);

        public static void Bubble<T>(Span<T> source, int l, int r, Comparison<T> comparison)
        {
            if (l < 0 || source.Length < r) throw new ArgumentOutOfRangeException();
            if (r <= l) throw new ArgumentException();
            comparison ??= Comparer<T>.Default.Compare;

            for (var i = 0; i < r - l; i++)
            {
                var ok = true;
                for (var j = l; j + 1 < r - i; j++)
                {
                    if (comparison(source[j], source[j + 1]) <= 0) continue;

                    (source[j], source[j + 1]) = (source[j + 1], source[j]);
                    ok = false;
                }

                if (ok) return;
            }
        }

        public static void Merge<T>(Span<T> source, int l, int r, Comparer<T> comparer = null) =>
            Merge(source, l, r, (comparer ?? Comparer<T>.Default).Compare);

        public static void Merge<T>(Span<T> source, int l, int r, Comparison<T> comparison)
        {
            if (l < 0 || source.Length < r) throw new ArgumentOutOfRangeException();
            if (r <= l) throw new ArgumentException();
            comparison ??= Comparer<T>.Default.Compare;

            var n = r - l;
            if (n < 2) return;

            var m = (l + r) / 2;
            Merge(source, l, m, comparison);
            Merge(source, m, r, comparison);

            var buffer = new T[n];
            var (ll, rr) = (l, m);
            var idx = 0;
            while (ll < m && rr < r)
            {
                if (comparison(source[ll], source[rr]) <= 0) buffer[idx++] = source[ll++];
                else buffer[idx++] = source[rr++];
            }

            source[ll..m].CopyTo(buffer.AsSpan(idx..));
            source[rr..r].CopyTo(buffer.AsSpan((idx + m - ll)..));
            buffer.AsSpan().CopyTo(source[l..]);
        }

        public static void Insertion<T>(Span<T> source, int l, int r, Comparer<T> comparer = null) =>
            Insertion(source, l, r, (comparer ?? Comparer<T>.Default).Compare);

        public static void Insertion<T>(Span<T> source, int l, int r, Comparison<T> comparison)
        {
            if (l < 0 || source.Length < r) throw new ArgumentOutOfRangeException();
            if (r <= l) throw new ArgumentException();
            comparison ??= Comparer<T>.Default.Compare;

            for (var i = l + 1; i < r; i++)
            {
                for (var j = i; j - 1 >= l; j--)
                {
                    if (comparison(source[j - 1], source[j]) > 0)
                        (source[j - 1], source[j]) = (source[j], source[j - 1]);
                    else break;
                }
            }
        }

        public static void Shell<T>(Span<T> source, int l, int r, Comparer<T> comparer = null) =>
            Shell(source, l, r, (comparer ?? Comparer<T>.Default).Compare);

        public static void Shell<T>(Span<T> source, int l, int r, Comparison<T> comparison)
        {
            if (l < 0 || source.Length < r) throw new ArgumentOutOfRangeException();
            if (r <= l) throw new ArgumentException();
            comparison ??= Comparer<T>.Default.Compare;

            var max = -(-(r - l)) / 3;
            var k = 1;
            while (k * 3 - 1 <= max * 2) k *= 3;
            var h = (k - 1) / 2;

            while (h > 0)
            {
                for (var d = 0; d < h; d++)
                {
                    for (var i = l + d + h; i < r; i++)
                    {
                        for (var j = i; j - h >= l; j -= h)
                        {
                            if (comparison(source[j - h], source[j]) > 0)
                                (source[j - h], source[j]) = (source[j], source[j - h]);
                            else break;
                        }
                    }
                }

                h /= 3;
            }
        }
    }
}