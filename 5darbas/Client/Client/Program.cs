using System;
using System.IO;
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
using Org.BouncyCastle.X509;

namespace Client
{
    public class valid
    {
        public byte[] key;
        public byte[] data;
        public byte[] signature;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            using (WebSocket socket = new WebSocket("ws://127.0.0.1:7890/Echo"))
            {
                valid val = new valid();
                socket.OnMessage += Socket_OnMessage;
                socket.Connect();
                Console.WriteLine("Connected to server");
                Console.Write("Enter text: ");
                string text = Console.ReadLine();
                byte[] data = Encoding.ASCII.GetBytes(text);
                RsaKeyPairGenerator generator = new RsaKeyPairGenerator();
                generator.Init(new KeyGenerationParameters(new SecureRandom(), 2048));
                AsymmetricCipherKeyPair keyPair = generator.GenerateKeyPair();
                RsaKeyParameters privatekey = (RsaKeyParameters)keyPair.Private;
                RsaKeyParameters publickey = (RsaKeyParameters)keyPair.Public;
                ISigner signer = SignerUtilities.GetSigner(PkcsObjectIdentifiers.Sha1WithRsaEncryption.Id);
                signer.Init(true, privatekey);
                signer.BlockUpdate(data, 0, data.Length);
                byte[] signature = signer.GenerateSignature();
                val.key = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publickey).GetDerEncoded();
                Console.WriteLine(val.key.Length);

                RsaKeyPairGenerator generator1 = new RsaKeyPairGenerator();
                generator1.Init(new KeyGenerationParameters(new SecureRandom(), 2048));
                AsymmetricCipherKeyPair keyPair1 = generator1.GenerateKeyPair();
                RsaKeyParameters privatekey1 = (RsaKeyParameters)keyPair1.Private;
                RsaKeyParameters publickey1 = (RsaKeyParameters)keyPair1.Public;
                val.key = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publickey1).GetDerEncoded();

                val.data = data;
                val.signature = signature;
                String tmp = JsonConvert.SerializeObject(val);
                socket.Send(tmp);
                Console.ReadKey();
            }
        }

        static void Socket_OnMessage(object sender, MessageEventArgs e)
        {
        }
    }
}
