namespace Sandbox.Mathematics;

public static class Prime
{
    public static IEnumerable<long> GetFactors(long value)
    {
        if (value < 2) yield break;
        while (value % 2 == 0)
        {
            yield return 2;
            value /= 2;
        }

        for (var i = 3L; i * i <= value; i++)
        {
            while (value % i == 0)
            {
                yield return i;
                value /= i;
            }
        }

        if (value > 1) yield return value;
    }

    public static IDictionary<long, int> GetFactorDictionary(long value)
    {
        var factors = new Dictionary<long, int>();
        if (value < 2) return factors;

        void CountUp(long n)
        {
            if (value % n != 0) return;
            factors[n] = 0;
            while (value % n == 0)
            {
                value /= n;
                factors[n]++;
            }
        }

        CountUp(2);
        for (var i = 3L; i * i <= value; i += 2) CountUp(i);

        if (value > 1) factors[value] = 1;
        return factors;
    }

    public static IEnumerable<int> Sieve(int value)
    {
        if (value < 2) yield break;
        yield return 2;
        var sieve = new bool[(value + 1) / 2];
        for (var i = 1; i < sieve.Length; i++)
        {
            if (sieve[i]) continue;
            yield return i * 2 + 1;
            for (var j = i; j < sieve.Length; j += i * 2 + 1) sieve[j] = true;
        }
    }

    public static bool IsPrime(long value)
    {
        if (value == 2) return true;
        if (value < 2 || value % 2 == 0) return false;
        for (var i = 3L; i * i <= value; i += 2)
        {
            if (value % i == 0) return false;
        }

        return true;
    }
}