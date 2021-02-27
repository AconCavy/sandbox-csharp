using System;
using System.Collections.Generic;

namespace Sandbox.Mathematics
{
    public static class Mathematics
    {
        private static readonly Dictionary<long, long> Memo = new Dictionary<long, long> { { 0, 1 }, { 1, 1 } };
        private static long _max = 1;

        public static long Factorial(long n)
        {
            if (Memo.ContainsKey(n)) return Memo[n];
            if (n < 0) throw new ArgumentException();
            for (var i = _max; i < n; i++) Memo[i + 1] = Memo[i] * i;
            _max = n;
            return Memo[n];
        }

        public static long Permutation(long n, long r, bool useMemo = true)
        {
            if (n < 0) throw new ArgumentException(nameof(n));
            if (r < 0) throw new ArgumentException(nameof(r));
            if (n < r) return 0;
            if (useMemo) return Factorial(n) / Factorial(n - r);
            var ret = 1L;
            for (var i = 0L; i < r; i++) ret *= n - i;
            return ret;
        }

        public static long Combination(long n, long r, bool useMemo = true)
        {
            if (n < 0) throw new ArgumentException(nameof(n));
            if (r < 0) throw new ArgumentException(nameof(r));
            if (n < r) return 0;
            r = System.Math.Min(r, n - r);
            return Permutation(n, r, useMemo) / Factorial(r);
        }

        public static IEnumerable<long> GetDivisors(long n)
        {
            for (var i = 1L; i * i <= n; i++)
            {
                if (n % i != 0) continue;
                yield return i;
                if (n / i != i) yield return n / i;
            }
        }

        private static long GreatestCommonDivisor(long a, long b)
        {
            while (true)
            {
                if (b == 0) return a;
                (a, b) = (b, a % b);
            }
        }

        public static long LeastCommonMultiple(long a, long b) => a / GreatestCommonDivisor(a, b) * b;

        public static long ExtendedGreatestCommonDivisor(long a, long b, out long x, out long y)
        {
            if (b == 0)
            {
                x = 1;
                y = 0;
                return a;
            }

            var d = ExtendedGreatestCommonDivisor(b, a % b, out y, out x);
            y -= a / b * x;
            return d;
        }

        public static long Xor0To(long x)
        {
            if (x < 0) return 0;
            return (x % 4) switch
            {
                0 => x,
                1 => 1,
                2 => 1 ^ x,
                _ => 0
            };
        }

        public static IEnumerable<long> ConvertBaseNumber(long x, long n)
        {
            var y = x;
            var bits = new List<long>();
            while (y != 0)
            {
                bits.Add(y % n);
                y /= n;
            }

            bits.Reverse();
            return bits;
        }

        public static bool Intersect(double x1, double y1, double x2, double y2, double x3, double y3, double x4,
            double y4)
        {
            var t1 = (x3 - x4) * (y1 - y3) + (y3 - y4) * (x3 - x1);
            var t2 = (x3 - x4) * (y2 - y3) + (y3 - y4) * (x3 - x2);
            var t3 = (x1 - x2) * (y3 - y1) + (y1 - y2) * (x1 - x3);
            var t4 = (x1 - x2) * (y4 - y1) + (y1 - y2) * (x1 - x4);
            return t1 * t2 < 0 && t3 * t4 < 0;
        }

        public static double TriangleAria(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            var (dx1, dy1) = (x2 - x1, y2 - y1);
            var (dx2, dy2) = (x3 - x1, y3 - y1);
            return System.Math.Abs((dx1 * dy2 - dx2 * dy1) / 2);
        }

        public static double PerpendicularLineLength(double x, double y, double x1, double y1, double x2, double y2)
        {
            var (dx, dy) = (x2 - x1, y2 - y1);
            if (dx == 0) return System.Math.Abs(x1 - x);
            var m = dy / dx;
            var a = m;
            var c = y1 - m * x1;
            return System.Math.Abs(a * x - y + c) / System.Math.Sqrt(a * a + 1);
        }
    }
}