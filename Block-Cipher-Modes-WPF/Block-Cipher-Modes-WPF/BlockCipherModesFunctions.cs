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

            
            for (int i = 0; i < plaintext.Length / 16; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(plaintext, i * 16, 16);
                listByteCiphertext.AddRange(AESFunctions.AES_encrypt_block(bytesBlock, key));
            }

            return listByteCiphertext.ToArray();
        }
        
        public static byte[] ElectronicCodebookModeDecrypt(byte[] ciphertext, byte[] key)
        {
            List<byte> listBytePlaintext = new List<byte>();
            
            for (int i = 0; i < ciphertext.Length / 16; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(ciphertext, i * 16, 16);
                listBytePlaintext.AddRange(AESFunctions.AES_Decrypt_block(bytesBlock, key));
            }

            return listBytePlaintext.ToArray();
        }

        public static byte[] CipherBlockChainingModeEncypt(byte[] plaintext, byte[] key, byte[] IV)
        {
            List<byte> listByteCiphertext = new List<byte>();

            byte[] tmp = IV;
            for (int i = 0; i < plaintext.Length / 16; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(plaintext, i * 16, 16);
                bytesBlock = HelpfulFunctions.exclusiveOR(bytesBlock, tmp);
                tmp = AESFunctions.AES_encrypt_block(bytesBlock, key);
                listByteCiphertext.AddRange(tmp);
            }

            return listByteCiphertext.ToArray();
        }

        public static byte[] CipherBlockChainingModeDecrypt(byte[] ciphertext, byte[] key, byte[] IV)
        {
            List<byte> listBytePlaintext = new List<byte>();

            byte[] tmp = IV;
            for (int i = 0; i < ciphertext.Length / 16; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(ciphertext, i * 16, 16);
                byte[] bytesFromDecryptor = AESFunctions.AES_Decrypt_block(bytesBlock, key);

                listBytePlaintext.AddRange(HelpfulFunctions.exclusiveOR(bytesFromDecryptor, tmp));
                tmp = bytesBlock;
            }

            return listBytePlaintext.ToArray();
        }

        public static byte[] PropagatingCipherBlockChainingModeEncypt(byte[] plaintext, byte[] key, byte[] IV)
        {
            List<byte> listByteCiphertext = new List<byte>();

            byte[] tmp = IV;
            for (int i = 0; i < plaintext.Length / 16; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(plaintext, i * 16, 16);
                bytesBlock = HelpfulFunctions.exclusiveOR(bytesBlock, tmp);
                byte[] bytesFromAES = AESFunctions.AES_encrypt_block(bytesBlock, key);
                listByteCiphertext.AddRange(bytesFromAES);
                tmp = HelpfulFunctions.exclusiveOR(bytesBlock, bytesFromAES);
            }

            return listByteCiphertext.ToArray();
        }

        public static byte[] PropagatingCipherBlockChainingModeDecrypt(byte[] ciphertext, byte[] key, byte[] IV)
        {
            List<byte> listBytePlaintext = new List<byte>();

            byte[] tmp = IV;
            for (int i = 0; i < ciphertext.Length / 16; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(ciphertext, i * 16, 16);
                byte[] bytesFromDecryptor = AESFunctions.AES_Decrypt_block(bytesBlock, key);
                byte[] plaintextBlock = HelpfulFunctions.exclusiveOR(bytesFromDecryptor, tmp);
                listBytePlaintext.AddRange(plaintextBlock);
                tmp = HelpfulFunctions.exclusiveOR(bytesBlock,plaintextBlock);
            }

            return listBytePlaintext.ToArray();
        }

        public static byte[] CipherFeedbackModeEncypt(byte[] plaintext, byte[] key, byte[] IV)
        {
            List<byte> listByteCiphertext = new List<byte>();
            //TODO
            byte[] tmp = IV;
            for (int i = 0; i < plaintext.Length / 16; i++)
            {
                byte[] bytesFromAES = AESFunctions.AES_encrypt_block(tmp, key);
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(plaintext, i * 16, 16);
                byte[] bytesBeforXOR = HelpfulFunctions.exclusiveOR(bytesBlock, bytesFromAES);
                listByteCiphertext.AddRange(bytesBeforXOR);
                tmp = bytesBeforXOR;
            }

            return listByteCiphertext.ToArray();
        }

        public static byte[] CipherFeedbackModeDecrypt(byte[] ciphertext, byte[] key, byte[] IV)
        {
            List<byte> listBytePlaintext = new List<byte>();
            //TODO
            byte[] tmp = IV;
            for (int i = 0; i < ciphertext.Length / 16; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(ciphertext, i * 16, 16);
                byte[] bytesFromEncrypt = AESFunctions.AES_encrypt_block(tmp, key);
                listBytePlaintext.AddRange(HelpfulFunctions.exclusiveOR(bytesFromEncrypt, bytesBlock));
                tmp = bytesBlock;
            }

            return listBytePlaintext.ToArray();
        }

        public static byte[] OutputFeedbackModeEncypt(byte[] plaintext, byte[] key, byte[] IV)
        {
            List<byte> listByteCiphertext = new List<byte>();

            byte[] tmp = IV;
            for (int i = 0; i < plaintext.Length / 16; i++)
            {
                byte[] bytesFromAES = AESFunctions.AES_encrypt_block(tmp, key);
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(plaintext, i * 16, 16);
                bytesBlock = HelpfulFunctions.exclusiveOR(bytesBlock, tmp);
                listByteCiphertext.AddRange(HelpfulFunctions.exclusiveOR(bytesBlock, bytesFromAES));
                tmp = bytesFromAES;
            }

            return listByteCiphertext.ToArray();
        }
        
    }
}
