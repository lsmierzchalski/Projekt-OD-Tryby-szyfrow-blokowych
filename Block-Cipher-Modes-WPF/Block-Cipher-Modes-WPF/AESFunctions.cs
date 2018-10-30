using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Block_Cipher_Modes_WPF
{
    public static class AESFunctions
    {
        public static byte[] AES_encrypt_block(byte[] plainText, byte[] Key)
        {
            byte[] output_buffer = new byte[plainText.Length];

            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.BlockSize = 128;
                aesAlg.KeySize = 128;
                aesAlg.Padding = PaddingMode.Zeros;
                aesAlg.Key = Key;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                encryptor.TransformBlock(plainText, 0, plainText.Length, output_buffer, 0);
            }

            return output_buffer;
        }

        public static byte[] AES_Decrypt_block(byte[] cipherText, byte[] Key)
        {
            // Declare the string used to hold the decrypted text. 
            byte[] output_buffer = new byte[cipherText.Length];

            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.BlockSize = 128;
                aesAlg.KeySize = 128;
                aesAlg.Padding = PaddingMode.None;
                aesAlg.Key = Key;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                decryptor.TransformBlock(cipherText, 0, cipherText.Length, output_buffer, 0);
            }

            return output_buffer;
        }
    }

}
