using System;
using System.Collections.Generic;

namespace SandboxCSharp
{
    public static class Mathematics
    {
        private static readonly Dictionary<long, long> Memo = new Dictionary<long, long> {{0, 1}, {1, 1}};
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
            r = Math.Min(r, n - r);
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

        public static long LeastCommonMultiple(long a, long b)
        {
            return a / GreatestCommonDivisor(a, b) * b;
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

        public static bool Intersect(long ax, long ay, long bx, long by, long cx, long cy, long dx, long dy)
        {
            var ta = (cx - dx) * (ay - cy) + (cy - dy) * (cx - ax);
            var tb = (cx - dx) * (by - cy) + (cy - dy) * (cx - bx);
            var tc = (ax - bx) * (cy - ay) + (ay - by) * (ax - cx);
            var td = (ax - bx) * (dy - ay) + (ay - by) * (ax - dx);
            return ta * tb < 0 && tc * td < 0;
        }
    }
}