using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Block_Cipher_Modes_WPF
{
    public static class BlockCipherModesFunctions
    {
        public static void ElectronicCodebookModeEncypt(string plaintext, byte[] key)
        {
            List<byte> listByteCiphertext = new List<byte>();

            byte[] bytesPlaintext = Encoding.UTF8.GetBytes(plaintext);

            for (int i = 0; i < bytesPlaintext.Length / 128; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(bytesPlaintext, i * 128, 128);
                AESFunctions.AES_encrypt_block(bytesBlock, key);
            }

        }

    }
}
