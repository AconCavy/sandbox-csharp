using System;

namespace Sandbox
{
    public static class Number
    {
#if DEBUG
        private const int ParsableDigit = 4;
#else
        private const int ParsableDigit = 18;
#endif
        public static bool IsMultipleOf(ReadOnlySpan<char> value, uint divisor, bool checkValues = false)
        {
            if (value.Length == 0) throw new ArgumentException(nameof(value));
            if (divisor == 0) throw new ArgumentException(nameof(divisor));
            if (checkValues)
                foreach (var c in value)
                    if (c < '0' || '9' < c)
                        throw new ArgumentException(nameof(value));

            if (divisor == 1) return true;
            foreach (var (p, c) in Prime.GetFactors(divisor))
                if (!IsMultipleOf_(value, (uint)Math.Pow(p, c)))
                    return false;

            return true;
        }

        private static bool IsMultipleOf_(ReadOnlySpan<char> value, uint divisor)
        {
            const int stackSize = 1 << 12;
            if (value.Length <= ParsableDigit) return ulong.Parse(value) % divisor == 0;
            var x = 0L;
            if (divisor % 2 == 0)
                for (var i = 1; i < 32; i++)
                {
                    if (divisor != 1UL << i) continue;
                    for (var j = i; j >= 1; j--)
                    {
                        x *= 10;
                        x += value[^j] - '0';
                    }

                    return x % divisor == 0;
                }

            if (divisor % 5 == 0)
            {
                var d = divisor;
                var count = 1;
                while (d > 1)
                {
                    d /= 5;
                    count++;
                }

                for (var i = count; i >= 1; i--)
                {
                    x *= 10;
                    x += value[^i] - '0';
                }

                return x % divisor == 0;
            }

            var v1 = value.Length <= stackSize ? stackalloc sbyte[value.Length] : new sbyte[value.Length];
            for (var i = 0; i < value.Length; i++) v1[i] = (sbyte)(value[i] - '0');
            if (divisor % 3 == 0)
            {
                foreach (var v in v1) x += v;
                if (divisor <= 9) return x % divisor == 0;
                if (x % 9 != 0) return false;
            }

            var n = 0;
            while ((10 * n + 1) % divisor != 0) n++;
            int idx;
            for (idx = v1.Length - 1; idx > ParsableDigit; idx--)
            {
                int size;
                var y = v1[idx] * n;
                for (size = 1; y > 0; size++)
                {
                    v1[idx - size] -= (sbyte)(y % 10);
                    y /= 10;
                }

                for (var i = 0; i <= size; i++)
                {
                    if (v1[idx - i] >= 0) continue;
                    v1[idx - i - 1]--;
                    v1[idx - i] += 10;
                }
            }

            x = 0;
            foreach (var v in v1[..(idx + 1)]) x = x * 10 + v;
            return x % divisor == 0;
        }

        public static int[] ToDigits(long value)
        {
            if (value == 0) return new[] {0};
            value = Math.Abs(value);
            const int size = 32;
            Span<int> ret = stackalloc int[size];
            var idx = 1;
            while (value > 0)
            {
                ret[^idx] = (int)(value % 10);
                value /= 10;
                idx++;
            }

            return ret.Slice(size - idx + 1).ToArray();
        }

        public static int[] GetNumberCounts(long value)
        {
            value = Math.Abs(value);
            Span<int> ret = stackalloc int[10];
            if (value == 0) ret[0]++;
            while (value > 0)
            {
                ret[(int)value % 10]++;
                value /= 10;
            }

            return ret.ToArray();
        }

        public static int GetModulo(ReadOnlySpan<char> value, uint modulo)
        {
            if (value.Length <= ParsableDigit) return (int)(ulong.Parse(value) % modulo);
            if (modulo == 1) return 0;
            var ret = 0U;
            foreach (var d in value) ret = (ret * 10 + (uint)(d - '0')) % modulo;
            return (int)ret;
        }
    }
}