using System;
using System.Collections.Generic;
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

            File.WriteAllBytes("enc_1", encryptedData);


            // Shuffle the encrypted data
            ShuffleData(cryptoVars, encryptedData);
            File.WriteAllBytes("enc_2", encryptedData);

            // Tweak the encrypted data
            var tweakBytesList = new List<byte>();
            tweakBytesList.AddRange(BitConverter.GetBytes(cryptoVars.Tweak1));
            tweakBytesList.AddRange(BitConverter.GetBytes(cryptoVars.Tweak2));

            for (int i = 0; i < encryptedData.Length; i++)
            {
                encryptedData[i] = (byte)(encryptedData[i] + tweakBytesList[i & 0xF]);
            }

            // Create the final encrypted file
            var outFile = Path.Combine(Path.GetDirectoryName(inFile), Path.GetFileNameWithoutExtension(inFile) + ".enc");

            if (File.Exists(outFile))
            {
                File.Delete(outFile);
            }

            using (var outFileWriter = new BinaryWriter(File.Open(outFile, FileMode.Append, FileAccess.Write)))
            {
                outFileWriter.Write(encryptedData);

                outFileWriter.Write(cryptoVars.IV1);
                outFileWriter.Write(cryptoVars.IV2);
                outFileWriter.Write(cryptoVars.Tweak1);
                outFileWriter.Write(cryptoVars.Tweak2);
                outFileWriter.Write(cryptoVars.Seed);
                outFileWriter.Write(cryptoVars.NullPaddingA);
                outFileWriter.Write(cryptoVars.NullPaddingB);
                outFileWriter.Write(cryptoVars.End2Value);
            }
        }


        private static uint ComputeChecksum()
        {
            return 0;
        }


        private static void ShuffleData(CryptoVariables cryptoVars, byte[] encryptedData)
        {
            var bufferSize = encryptedData.Length >> 4;
            var keyStack = SharedFunctions.GenerateKeyStack(cryptoVars, bufferSize);

        }
    }
}