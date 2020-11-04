using System.Collections.Generic;
using System.Numerics;

namespace SandboxCSharp
{
    public static class Prime
    {
        public static IDictionary<long, int> GetFactors(long value)
        {
            var factors = new Dictionary<long, int>();
            if (value < 2) return factors;
            for (var i = 2; i * i <= value; i++)
            {
                if (value % i != 0) continue;
                factors[i] = 0;
                while (value % i == 0)
                {
                    value /= i;
                    factors[i]++;
                }
            }

            if (value > 1) factors[value] = 1;
            return factors;
        }

        public static int[] GetPrimes(int value)
        {
            if (value < 2) return new int[0];
            if (value == 2) return new[] {2};
            const int bit = 32;
            const int limit = 1024;
            value = (value + 1) / 2;
            var length = (value + bit) / bit;
            var sieve = length < limit ? stackalloc uint[length] : new uint[length];
            for (var i = value % bit; i < bit; i++) sieve[^1] |= 1U << i;
            for (var i = 1; i * i <= value;)
            {
                for (var j = i; j <= value; j += i * 2 + 1) sieve[j / bit] |= 1U << (j % bit);
                sieve[i / bit] &= ~(1U << (i % bit));
                do i++;
                while (i * i <= value && (sieve[i / bit] >> (i % bit) & 1) == 1);
            }

            var count = bit * length;
            foreach (var flags in sieve) count -= BitOperations.PopCount(flags);
            var primes = count < limit ? stackalloc int[count] : new int[count];
            primes[0] = 2;
            var index = 1;
            for (var i = 1; index < count && i <= value; i++)
                if ((sieve[i / bit] >> (i % bit) & 1U) == 0)
                    primes[index++] = i * 2 + 1;
            return primes.ToArray();
        }

        public static bool IsPrime(int value)
        {
            if (value == 2) return true;
            if (value < 2 || value % 2 == 0) return false;
            if (value < 2e5)
            {
                for (var i = 3; i * i <= value; i += 2)
                    if (value % i == 0)
                        return false;
                return true;
            }

            long d = value - 1;
            d /= d & -d;
            foreach (var w in stackalloc long[] {2, 7, 61})
            {
                var a = 1L;
                var t = d;
                var m = w;
                while (t > 0)
                {
                    if (t % 2 == 1) a = a * m % value;
                    m = m * m % value;
                    t >>= 1;
                }

                t = d;
                while (t != value - 1 && a != 1 && a != value - 1)
                {
                    a = a * a % value;
                    t <<= 1;
                }

                if (w % value != 0 && a != value - 1 && t % 2 == 0) return false;
            }

            return true;
        }
    }
}