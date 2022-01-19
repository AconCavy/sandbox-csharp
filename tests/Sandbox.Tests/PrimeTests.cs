using NUnit.Framework;
using Sandbox.Mathematics;

namespace Sandbox.Tests;

[Parallelizable]
public class PrimeTests
{
    [Test]
    [Parallelizable]
    public void GetFactorsTest([Range(-1, 1000)] int value)
    {
        var expected = Naives.Prime.GetFactorDictionary(value);
        var actual = Prime.GetFactorDictionary(value);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    [Parallelizable]
    public void GetPrimesTest([Range(-1, 1000)] int value)
    {
        var expected = Naives.Prime.Sieve(value);
        var actual = Prime.Sieve(value);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    [Parallelizable]
    public void IsPrimeTest([Values(0, 100000, 100000000)] int value, [Range(-500, 500)] int range)
    {
        var expected = Naives.Prime.IsPrime(value + range);
        var actual = Prime.IsPrime(value + range);

        Assert.That(actual, Is.EqualTo(expected));
    }
}