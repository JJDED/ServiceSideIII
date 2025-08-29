using System.Security.Cryptography;

namespace ServerSideIII.Codes
{
    public class AsymmetricEncryption
    {
        private string _publicKey;
        private string _privateKey;

        public AsymmetricEncryption()
        {
            using (RSA rsa = RSA.Create(2048))
            {
                byte[] privateKeyBytes = rsa.ExportRSAPrivateKey();
                _privateKey =
                    "-----BEGIN PRIVATE KEY-----\n" +
                    Convert.ToBase64String(privateKeyBytes, Base64FormattingOptions.InsertLineBreaks) +
                    "-----END PRIVATE KEY-----\n";

                byte[] publicKeyBytes = rsa.ExportSubjectPublicKeyInfo();
                _publicKey =
                    "-----BEGIN PUBLIC KEY-----\n" +
                    Convert.ToBase64String(publicKeyBytes, Base64FormattingOptions.InsertLineBreaks) +
                    "-----END PUBLIC KEY-----\n";
            }
        }

        public string EncryptAsymmetric(string dataToEncrypt)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                string privateKey = _privateKey
                    .Replace("-----BEGIN PRIVATE KEY-----", "")
                    .Replace("-----END PRIVATE KEY-----", "")
                    .Replace("\n", "").Replace("\r", "").Trim();

                byte[] privateKeyBytes = Convert.FromBase64String(privateKey);
                rsa.ImportSubjectPublicKeyInfo(privateKeyBytes, out _);

                byte[] byteArrayTextToDecrpyt = Convert.FromBase64String(dataToEncrypt);
                byte[] decryptedDataAsByteArray = rsa.Decrypt(byteArrayTextToDecrpyt, true);
                string decryptedDataAsString = System.Text.Encoding.UTF8.GetString(decryptedDataAsByteArray);

                return decryptedDataAsString;
            }
        }

        public string DecryptAsymmetric(string textToDecrypt)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                string privateKey = _privateKey
                    .Replace("-----BEGIN PRIVATE KEY-----", "")
                    .Replace("-----END PRIVATE KEY-----", "")
                    .Replace("\n", "").Replace("\r", "").Trim();

                byte[] privateKeyBytes = Convert.FromBase64String(privateKey);
                rsa.ImportRSAPrivateKey(privateKeyBytes, out _);

                byte[] byteArrayTextToDecrpyt = Convert.FromBase64String(textToDecrypt);
                byte[] decryptedDataAsByteArray = rsa.Decrypt(byteArrayTextToDecrpyt, true);
                string decryptedDataAsString = System.Text.Encoding.UTF8.GetString(decryptedDataAsByteArray);

                return decryptedDataAsString;
            }
        }

        public async Task<string> EncryptAsymetric_webApi(string dataToEncrypt)
        {
            string? responseMessage = null;

            string[] ar = new string[2] { _publicKey, dataToEncrypt  };
            string arSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(ar);
            StringContent sc = new StringContent(
                arSerialized,
                System.Text.Encoding.UTF8,
                "application/json"
            );

            using (HttpClient _httpClient = new HttpClient())
            {
                var response = await _httpClient.PostAsync(
                    "https://localhost:7270/encryptor",
                    sc
                    );
                responseMessage = response.Content.ReadAsStringAsync().Result;
            }

            return responseMessage;
        }

    }
}

