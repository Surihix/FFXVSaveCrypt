using System.Collections.Generic;
using System;
using System.IO;
using System.Security.Cryptography;

namespace FFXVSaveCrypt.Crypto
{
    internal class Encrypt
    {
        public static void BeginEncryption(string inFile)
        {
            // Get crypto related variables 
            // and the data to encrypt
            var cryptoVars = new CryptoVariables();
            byte[] dataToEncrypt = new byte[] { };

            using (var inFileReader = new BinaryReader(File.Open(inFile, FileMode.Open, FileAccess.Read)))
            {
                var cryptoOffset = inFileReader.BaseStream.Length - 53;

                inFileReader.BaseStream.Position = cryptoOffset;
                inFileReader.AssignByteValuesInClass(cryptoVars);

                inFileReader.BaseStream.Position = 0;
                dataToEncrypt = new byte[(int)cryptoOffset];
                dataToEncrypt = inFileReader.ReadBytes((int)cryptoOffset);
            }

            // Encrypt the data with AES algorithm
            var ivValList = new List<byte>();
            ivValList.AddRange(BitConverter.GetBytes(cryptoVars.IV1));
            ivValList.AddRange(BitConverter.GetBytes(cryptoVars.IV2));

            var encryptedData = new byte[dataToEncrypt.Length];

            using (var aes = Aes.Create())
            {
                aes.Key = CryptoVariables.ConstantKey;
                aes.IV = ivValList.ToArray();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.None;

                using (var encryptor = aes.CreateEncryptor())
                {
                    encryptedData = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
                }
            }

            // Shuffle the encrypted data
            ShuffleData(cryptoVars, encryptedData);

            // Tweak the encrypted data
            var tweakBytesList = new List<byte>();
            tweakBytesList.AddRange(BitConverter.GetBytes(cryptoVars.Tweak1));
            tweakBytesList.AddRange(BitConverter.GetBytes(cryptoVars.Tweak2));


        }


        private static uint ComputeChecksum()
        {
            return 0;
        }


        private static void ShuffleData(CryptoVariables cryptoVars, byte[] encryptedData)
        {
            var bufferSize = encryptedData.Length >> 4;
            var keyStack = SharedFunctions.GenerateKeyStack(cryptoVars, bufferSize);

            for (var shuffleIterator = 1; shuffleIterator < bufferSize; shuffleIterator++)
            {
                var destination = new List<ulong>();
                var source = new List<ulong>();


            }
        }
    }
}