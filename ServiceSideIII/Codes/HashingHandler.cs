﻿using System.Security.Cryptography;
using System.Text;

namespace ServerSideIII.Codes
{
    public class HashingHandler
    {
        public dynamic MD5Hashing(dynamic valueToHash) =>
            valueToHash is byte[]? MD5.Create().ComputeHash(valueToHash)
            : Convert.ToBase64String(MD5.Create().
                ComputeHash(System.Text.Encoding.UTF8.GetBytes(valueToHash.ToString())));

        public dynamic SHAHashing(dynamic valueToHash) =>
            valueToHash is byte[]? SHA256.Create().ComputeHash(valueToHash)
            : Convert.ToBase64String(SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(valueToHash.ToString())));

        public dynamic HMACHashing(dynamic valueToHash) =>
            valueToHash is byte[]? new HMACSHA256(Encoding.ASCII.GetBytes("NielsErMinFavoritLærer")).ComputeHash(valueToHash)
            : Convert.ToBase64String(new HMACSHA256(Encoding.ASCII.GetBytes("NielsErMinFavoritLærer"))
                .ComputeHash(valueToHash));

        //public byte[] PBKDF2Hashing(dynamic valueToHash) =>
        //    Rfc2898DeriveBytes.Pbkdf2(
        //        valueToHash is byte[]
        //        ? valueToHash
        //        : Encoding.UTF8.GetBytes(valueToHash),
        //        Encoding.UTF8.GetBytes("230271"), 10, HashAlgorithmName.SHA256, 32
        //    );

        public byte[] PBKDF2Hashing(dynamic valueToHash) =>
            Rfc2898DeriveBytes.Pbkdf2(
                valueToHash,
                Encoding.UTF8.GetBytes("Niels"),
                10,
                HashAlgorithmName.SHA256,
                32
            );

        #region BCrypt
        public string BCryptHashing1(string textToHash) =>
            BCrypt.Net.BCrypt.HashPassword(textToHash);

        public bool BCryptVerifyHashing1(string textToHash, string hashedValue) =>
            BCrypt.Net.BCrypt.Verify(textToHash, hashedValue);
        #endregion

        #region Bcrypt v2
        public string BCryptHashing2(string textToHash) =>
            BCrypt.Net.BCrypt.HashPassword
            (
                textToHash,
                BCrypt.Net.BCrypt.GenerateSalt(10),
                true,
                BCrypt.Net.HashType.SHA256
            );

        public bool BCryptVerifyHashing2(string textToHash, string hashedValue) =>
            BCrypt.Net.BCrypt.Verify
            (
                textToHash,
                hashedValue,
                true,
                BCrypt.Net.HashType.SHA256
            );
        #endregion
    }

}
