using System;
using System.Linq;
using System.Security.Cryptography;

namespace Megaplan.API
{
    using System.Text;

//    using Raksha.Crypto;
//    using Raksha.Crypto.Digests;
//    using Raksha.Crypto.Macs;
//    using Raksha.Crypto.Parameters;
//    using Raksha.Utilities.Encoders;

    public static class Hashes
    {
        public static string MD5(string input)
        {
            var cryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] encriptedBytes = cryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(input));
            return encriptedBytes.Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
//            IDigest digest = new MD5Digest();
//            byte[] resBuf = new byte[digest.GetDigestSize()];
//            byte[] bytes = Encoding.UTF8.GetBytes(input);
//            digest.BlockUpdate(bytes, 0, bytes.Length);
//
//            digest.DoFinal(resBuf, 0);
//
//            return Hex.ToHexString(resBuf);
        }

        public static string HMACSHA1(string input, string key)
        {
            var cryptoProvider = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            byte[] encriptedBytes = cryptoProvider.ComputeHash(Encoding.UTF8.GetBytes(input));
            return encriptedBytes.Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);

//            HMac hmac = new HMac(new Sha1Digest());
//            byte[] resBuf = new byte[hmac.GetMacSize()];
//            byte[] bytes = Encoding.UTF8.GetBytes(input);
//
//            hmac.Init(new KeyParameter(Encoding.UTF8.GetBytes(key)));
//            hmac.BlockUpdate(bytes, 0, bytes.Length);
//            hmac.DoFinal(resBuf, 0);
//
//            return Hex.ToHexString(resBuf);
        }
    }
}