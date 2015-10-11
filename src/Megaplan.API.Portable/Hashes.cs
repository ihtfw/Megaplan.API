using System;
using System.Linq;

#if !PCL
using System.Security.Cryptography;
#endif

namespace Megaplan.API
{
    using System.Text;

#if PCL
    using Raksha.Crypto;
    using Raksha.Crypto.Digests;
    using Raksha.Crypto.Macs;
    using Raksha.Crypto.Parameters;
    using Raksha.Utilities.Encoders;
#endif

    public static class Hashes
    {
        public static string MD5(string input)
        {
#if PCL
            IDigest digest = new MD5Digest();
            byte[] resBuf = new byte[digest.GetDigestSize()];
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            digest.BlockUpdate(bytes, 0, bytes.Length);

            digest.DoFinal(resBuf, 0);

            return Hex.ToHexString(resBuf);
#else
            var cryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] encriptedBytes = cryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(input));
            return encriptedBytes.Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
#endif
        }

        public static string HMACSHA1(string input, string key)
        {
#if PCL 
            HMac hmac = new HMac(new Sha1Digest());
            byte[] resBuf = new byte[hmac.GetMacSize()];
            byte[] bytes = Encoding.UTF8.GetBytes(input);

            hmac.Init(new KeyParameter(Encoding.UTF8.GetBytes(key)));
            hmac.BlockUpdate(bytes, 0, bytes.Length);
            hmac.DoFinal(resBuf, 0);

            return Hex.ToHexString(resBuf);
#else
            var cryptoProvider = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            byte[] encriptedBytes = cryptoProvider.ComputeHash(Encoding.UTF8.GetBytes(input));
            return encriptedBytes.Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
#endif
        }
    }
}