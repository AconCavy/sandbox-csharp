using System.Collections.Generic;

namespace SandboxCSharp.Naives
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
            var sieve = new bool[value + 1];
            sieve[2] = true;
            for (var i = 3; i <= value; i += 2) sieve[i] = true;
            for (var i = 3; i * i <= value;)
            {
                for (var j = i * 2; j <= value; j += i) sieve[j] = false;
                do i++;
                while (i * i <= value && !sieve[i]);
            }

            var primes = new int[value + 1];
            var index = 0;
            for (var i = 0; i <= value; i++)
            {
                if (!sieve[i]) continue;
                primes[index++] = i;
            }

            return primes[..index];
        }

        public static bool IsPrime(int value)
        {
            if (value < 2) return false;
            if (value == 2) return true;
            if (value % 2 == 0) return false;
            for (var i = 3; i * i <= value; i += 2)
                if (value % i == 0)
                    return false;

            return true;
        }
    }
}