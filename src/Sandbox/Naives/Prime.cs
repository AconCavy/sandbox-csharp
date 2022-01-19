namespace Sandbox.Naives;

public static class Prime
{
    public static IDictionary<long, int> GetFactorDictionary(long value)
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

    public static IEnumerable<int> Sieve(int value)
    {
        if (value < 2) yield break;
        var sieve = new bool[value + 1];
        for (var i = 2; i < sieve.Length; i++)
        {
            if (sieve[i]) continue;
            yield return i;
            for (var j = i; j < sieve.Length; j += i) sieve[j] = true;
        }
    }

    public static bool IsPrime(long value)
    {
        if (value < 2) return false;
        if (value == 2) return true;
        if (value % 2 == 0) return false;
        for (var i = 3L; i * i <= value; i += 2)
        {
            if (value % i == 0) return false;
        }

        return true;
    }
}