using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto.Generators;
using Newtonsoft.Json;

namespace validator
{
    internal class Program
    {
        public class valid
        {
            public byte[] key;
            public byte[] data;
            public byte[] signature;
        }
        static void Main(string[] args)
        {
            using (WebSocket socket = new WebSocket("ws://127.0.0.1:7890/Echo"))
            {
                socket.OnMessage += Socket_OnMessage;
                socket.Connect();
                Console.WriteLine("Connected to server");
                Console.ReadKey();
            }
        }

        private static void Socket_OnMessage(object sender, MessageEventArgs e)
        {
            valid val = JsonConvert.DeserializeObject<valid>(e.Data);
            RsaKeyParameters publicKeyRestored = (RsaKeyParameters)PublicKeyFactory.CreateKey(val.key);
            ISigner signer = SignerUtilities.GetSigner(PkcsObjectIdentifiers.Sha1WithRsaEncryption.Id);
            signer.Init(false,publicKeyRestored);
            signer.BlockUpdate(val.data,0, val.data.Length);
            bool ver = signer.VerifySignature(val.signature);
            if (ver == true)
                Console.WriteLine("Signature verified");
            else
                Console.WriteLine("Signature not verified");
        }
    }
}
