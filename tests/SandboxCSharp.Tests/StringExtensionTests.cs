using System;
using NUnit.Framework;
using SandboxCSharp.Extensions;

namespace SandboxCSharp.Tests
{
    public class StringExtensionTests
    {
        [Test]
        public void GetCharacterCountsTest()
        {
            const string str =
                @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis tempus quam ipsum. Sed ut porta lectus. Duis fermentum sem sed leo rutrum, a efficitur metus hendrerit. Ut faucibus, urna in tincidunt elementum, purus libero rutrum magna, quis porta erat ligula eu elit. Donec nulla libero, efficitur ut erat sed, consequat malesuada quam. Ut sed iaculis purus, ut auctor libero. Aenean efficitur libero tortor, sit amet efficitur lectus varius sed. Nullam scelerisque eleifend imperdiet.";
            var expected = new int[128];
            foreach (var c in str) expected[c]++;
            var actual = str.GetCharacterCounts();
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetAlphabetCountsLowerTest()
        {
            const string str = "abcdefghijklmanozpqrstuvwxyz";
            const char start = 'a';
            var expected = new int[26];
            foreach (var c in str) expected[c - start]++;
            var actual = str.GetAlphabetCounts();
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetAlphabetCountsUpperTest()
        {
            const string str = "ABCDEFGHIJKZLMNOPQARSTUVWXYZ";
            const char start = 'A';
            var expected = new int[26];
            foreach (var c in str) expected[c - start]++;
            var actual = str.GetAlphabetCounts(true);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetNumberCountsTest()
        {
            const string str = "102345678909";
            const char start = '0';
            var expected = new int[10];
            foreach (var c in str) expected[c - start]++;
            var actual = str.GetNumberCounts();
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void NullTest()
        {
            const string str = null;
            Assert.Throws<ArgumentNullException>(() => str.GetAlphabetCounts());
            Assert.Throws<ArgumentNullException>(() => str.GetAlphabetCounts(true));
            Assert.Throws<ArgumentNullException>(() => str.GetNumberCounts());
        }

        [Test]
        public void StartOutOfRangeTest()
        {
            const string str = "";
            Assert.Throws<ArgumentOutOfRangeException>(() => str.GetCharacterCounts(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => str.GetCharacterCounts(128));
        }

        [Test]
        public void LengthOutOfRangeTest()
        {
            const string str = "";
            Assert.Throws<ArgumentOutOfRangeException>(() => str.GetCharacterCounts(0, 129));
            Assert.Throws<ArgumentOutOfRangeException>(() => str.GetCharacterCounts(127, 2));
        }
    }
}