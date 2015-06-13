namespace Megaplan.API.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    
    using NUnit.Framework;

    [TestFixture]
    public class HashesTests
    {
        [Test]
        public void MD5Test()
        {
            var input = "some string to test";

            var hash = Hashes.MD5(input);

            var cryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] encriptedBytes = cryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(input));
            var normalHash = encriptedBytes.Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);

            Assert.That(hash, Is.EqualTo(normalHash));
        }

        [Test]
        public void HMACSHA1Test()
        {
            var input = "some string to test";
            var key = "some string to test";

            var hash = Hashes.HMACSHA1(input, key);

            var cryptoProvider = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            byte[] encriptedBytes = cryptoProvider.ComputeHash(Encoding.UTF8.GetBytes(input));
            string encryptedString = encriptedBytes.Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);

            Assert.That(hash, Is.EqualTo(encryptedString));
        }
    }
}