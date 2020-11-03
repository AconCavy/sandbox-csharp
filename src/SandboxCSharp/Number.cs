using System;

namespace SandboxCSharp
{
    public static class Number
    {
        public static bool IsMultipleOf(ReadOnlySpan<char> value, uint p, bool checkValues = false)
        {
            if (value.Length == 0) throw new ArgumentException(nameof(value));
            if (checkValues)
            {
                foreach (var c in value)
                    if (c < '0' || '9' < c)
                        throw new ArgumentException(nameof(value));
            }
#if DEBUG
            const int parsableDigit = 3;
#else
            const int parsableDigit = 18;
#endif
            const int stackSize = 1 << 12;
            switch (p)
            {
                case 0: throw new ArgumentException(nameof(p));
                case 1: return true;
                case 2:
                case 5:
                case 10: return (value[^1] - '0') % p == 0;
            }

            if (value.Length <= parsableDigit) return ulong.Parse(value) % p == 0;
            var v1 = value.Length <= stackSize ? stackalloc sbyte[value.Length] : new sbyte[value.Length];
            for (var i = 0; i < value.Length; i++) v1[i] = (sbyte) (value[i] - '0');
            var x = 0L;
            switch (p)
            {
                case 3:
                case 9:
                    foreach (var v in v1) x += v;
                    return x % p == 0;
                case 6:
                    foreach (var v in v1) x += v;
                    return v1[^1] % 2 == 0 && x % 3 == 0;
                case 4:
                case 8:
                    x = v1[^2] * 10 + v1[^1];
                    if (p == 8) x += v1[^3] * 100;
                    return x % p == 0;
            }

            var n = 0;
            while ((10 * n + 1) % p != 0) n++;
            var idx = v1.Length - 1;
            while (idx >= parsableDigit)
            {
                var size = 0;
                var y = v1[idx] * n;
                while (y > 0)
                {
                    v1[idx - size - 1] -= (sbyte) (y % 10);
                    y /= 10;
                    size++;
                }

                for (var i = 0; i <= size; i++)
                {
                    if (v1[idx - i] >= 0) continue;
                    v1[idx - i - 1]--;
                    v1[idx - i] += 10;
                }

                idx--;
            }

            x = 0;
            foreach (var v in v1[..(idx + 1)])
            {
                x *= 10;
                x += v;
            }

            return x % p == 0;
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
                ret[^idx] = (int) (value % 10);
                value /= 10;
                idx++;
            }

            return ret.Slice(size - idx + 1).ToArray();
        }
    }
}