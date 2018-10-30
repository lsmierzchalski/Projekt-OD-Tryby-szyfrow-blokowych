using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Block_Cipher_Modes_WPF
{
    public static class BlockCipherModesFunctions
    {
        public static byte[] ElectronicCodebookModeEncypt(byte[] plaintext, byte[] key)
        {
            List<byte> listByteCiphertext = new List<byte>();

            
            for (int i = 0; i < plaintext.Length / 128; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(plaintext, i * 128, 128);
                listByteCiphertext.AddRange(AESFunctions.AES_encrypt_block(bytesBlock, key));
            }

            return listByteCiphertext.ToArray();
        }
        
        public static byte[] ElectronicCodebookModeDecrypt(byte[] ciphertext, byte[] key)
        {
            List<byte> listBytePlaintext = new List<byte>();
            
            for (int i = 0; i < ciphertext.Length / 128; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(ciphertext, i * 128, 128);
                listBytePlaintext.AddRange(AESFunctions.AES_Decrypt_block(bytesBlock, key));
            }

            return listBytePlaintext.ToArray();
        }

        public static byte[] CipherBlockChainingModeEncypt(byte[] plaintext, byte[] key, byte[] IV)
        {
            List<byte> listByteCiphertext = new List<byte>();

            //TODO

            for (int i = 0; i < plaintext.Length / 128; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(plaintext, i * 128, 128);
                listByteCiphertext.AddRange(AESFunctions.AES_encrypt_block(bytesBlock, key));
            }

            return listByteCiphertext.ToArray();
        }

        public static byte[] CipherBlockChainingModeDecrypt(byte[] ciphertext, byte[] key, byte[] IV)
        {
            List<byte> listBytePlaintext = new List<byte>();

            //TODO

            for (int i = 0; i < ciphertext.Length / 128; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(ciphertext, i * 128, 128);
                listBytePlaintext.AddRange(AESFunctions.AES_Decrypt_block(bytesBlock, key));
            }

            return listBytePlaintext.ToArray();
        }
    }
}
