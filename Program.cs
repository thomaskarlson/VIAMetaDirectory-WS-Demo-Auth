using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using ServiceReference1;

namespace MetadirDemo
{
    class Program
    {

        private CookieContainer _cookieContainer = new CookieContainer();
        private const string _publicKey = @"<RSAKeyValue>
                <Modulus>
                    OPDATER MED DIN EGEN PUBLIC KEY
                </Modulus>

                <Exponent>
                OPDATER MED DIN EGEN PUBLIC KEY
                </Exponent>

                </RSAKeyValue>";
        private const string _connectionIdent = @"OPDATER-MED-DIN-EGEN-IDENT";

        static void Main(string[] args)
        {

            // Opret en ny instans af soap klienten
            var metadir = new MetaDirectorySoapClient(MetaDirectorySoapClient.EndpointConfiguration.MetaDirectorySoap12);


            // Indlæs public key
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(_publicKey);


            // Kald AuthenticateConnectionAsync (skal altid kaldes for at starte kommunikation med metadir)
            metadir.AuthenticateConnectionAsync(_connectionIdent, rsa.Encrypt(Encoding.Unicode.GetBytes(_connectionIdent), false)).Wait();


            // Lav et kald til webservicen (henter i dette tilfælde person med id 2121)
            Console.WriteLine(metadir.GetPerson1Async(2121).Result.PersonByPersonIDResult.DisplayName);

        }
    }
}
