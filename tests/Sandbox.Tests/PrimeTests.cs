using NUnit.Framework;

namespace Sandbox.Tests
{
    [Parallelizable]
    public class PrimeTests
    {
        [Test]
        [Parallelizable]
        public void GetFactorsTest([Range(-1, 1000)] int value)
        {
            var expected = Naives.Prime.GetFactors(value);
            var actual = Prime.GetFactors(value);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [Parallelizable]
        public void GetPrimesTest([Range(-1, 1000)] int value)
        {
            var expected = Naives.Prime.GetPrimes(value);
            var actual = Prime.GetPrimes(value);

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
}