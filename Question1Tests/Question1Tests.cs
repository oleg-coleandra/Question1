using Microsoft.VisualStudio.TestTools.UnitTesting;
using Question1;
using Question1.Exceptions;
using System;
using System.Linq;

namespace Question1Tests
{
    [TestClass]
    public class Question1Tests
    {
        private static string randomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [TestMethod]
        public void OkTest()
        {
            var codec = new Codec();
            var testString = randomString(16);

            var encodedString = codec.Encode(testString);
            var decodedString = codec.Decode(encodedString);

            Assert.AreEqual(0, String.Compare(testString, decodedString));
        }

        [TestMethod]
        public void InvalidCharacterTest()
        {
            var codec = new Codec();
            var testString = "TEST_Ф_TEST";

            Assert.ThrowsException<InvalidCharacterException>(() => codec.Encode(testString));
        }
    }
}
