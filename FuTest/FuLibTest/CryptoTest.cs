using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace FuTest.FuLibTest
{
    public class CryptoTest
    {
        [Fact]
        public void EncryptStringAESTest()
        {
            string original="sa___123456";
            string sharekey="P@ssw0rd";
            string end = FuLib.Crypto.EncryptStringAES(original, sharekey);
            Console.WriteLine(end);
            string decrypted = FuLib.Crypto.DecryptStringAES(end, sharekey);
            Assert.Equal(original, decrypted);

            Console.Write(FuLib.Crypto.DecryptStringAES("EAAAAAMQNRg89ppa6Y+l9InCDPsTUv09Lg1FdCWS0FcCRg/3vip1jeAeZXHygs1m+JPxNnbm01v1/n8iD3X3iXueSUG6Qjxv5vJula5NsEfwdTZZ", sharekey));
        }
    }
}
