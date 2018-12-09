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
                bytesBlock = HelpfulFunctions.ExclusiveOR(bytesBlock, tmp);
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

                listBytePlaintext.AddRange(HelpfulFunctions.ExclusiveOR(bytesFromDecryptor, tmp));
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
                byte[] xorBlock = HelpfulFunctions.ExclusiveOR(bytesBlock, tmp);
                byte[] bytesFromAES = AESFunctions.AES_encrypt_block(xorBlock, key);
                listByteCiphertext.AddRange(bytesFromAES);
                tmp = HelpfulFunctions.ExclusiveOR(bytesBlock, bytesFromAES);
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
                byte[] plaintextBlock = HelpfulFunctions.ExclusiveOR(bytesFromDecryptor, tmp);
                listBytePlaintext.AddRange(plaintextBlock);
                tmp = HelpfulFunctions.ExclusiveOR(bytesBlock,plaintextBlock);
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
                byte[] bytesBeforXOR = HelpfulFunctions.ExclusiveOR(bytesBlock, bytesFromAES);
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
                listBytePlaintext.AddRange(HelpfulFunctions.ExclusiveOR(bytesFromEncrypt, bytesBlock));
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
                bytesBlock = HelpfulFunctions.ExclusiveOR(bytesBlock, tmp);
                listByteCiphertext.AddRange(HelpfulFunctions.ExclusiveOR(bytesBlock, bytesFromAES));
                tmp = bytesFromAES;
            }

            return listByteCiphertext.ToArray();
        }

        public static byte[] CounterModeEncypt(byte[] plaintext, byte[] key, byte[] nonce)
        {
            List<byte> listByteCiphertext = new List<byte>();
            byte[] counter = { 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 0; i < plaintext.Length / 16; i++)
            {
                List<byte> tmp = new List<byte>();
                tmp.AddRange(nonce);
                tmp.AddRange(counter);

                byte[] bytesFromAES = AESFunctions.AES_encrypt_block(tmp.ToArray(), key);
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(plaintext, i * 16, 16);
                listByteCiphertext.AddRange(HelpfulFunctions.ExclusiveOR(bytesBlock, bytesFromAES));

                counter = HelpfulFunctions.AddOneToByteArray(counter);
            }

            return listByteCiphertext.ToArray();
        }


        public static byte[] PlaintextBlockChainingModeEncypt(byte[] plaintext, byte[] key, byte[] IV)
        {
            List<byte> listByteCiphertext = new List<byte>();

            byte[] tmp = IV;
            for (int i = 0; i < plaintext.Length / 16; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(plaintext, i * 16, 16);
                byte[] xorBlock = HelpfulFunctions.ExclusiveOR(bytesBlock, tmp);
                listByteCiphertext.AddRange(AESFunctions.AES_encrypt_block(xorBlock, key));
                tmp = bytesBlock;
            }

            return listByteCiphertext.ToArray();
        }

        public static byte[] PlaintextBlockChainingModeDecrypt(byte[] ciphertext, byte[] key, byte[] IV)
        {
            List<byte> listBytePlaintext = new List<byte>();

            byte[] tmp = IV;
            for (int i = 0; i < ciphertext.Length / 16; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(ciphertext, i * 16, 16);
                byte[] bytesFromDecryptor = AESFunctions.AES_Decrypt_block(bytesBlock, key);

                tmp = HelpfulFunctions.ExclusiveOR(bytesFromDecryptor, tmp);
                listBytePlaintext.AddRange(tmp);
            }

            return listBytePlaintext.ToArray();
        }


        public static byte[] CipherBlockChainingwithChecksumModeEncypt(byte[] plaintext, byte[] key, byte[] IV)
        {
            List<byte> listByteCiphertext = new List<byte>();

            byte[] tmp = IV;
            for (int i = 0; i < plaintext.Length / 16; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(plaintext, i * 16, 16);
                byte[] xorBlock = HelpfulFunctions.ExclusiveOR(bytesBlock, tmp);
                listByteCiphertext.AddRange(AESFunctions.AES_encrypt_block(xorBlock, key));
                tmp = bytesBlock;
            }

            return listByteCiphertext.ToArray();
        }

        public static byte[] CipherBlockChainingwithChecksumModeDecrypt(byte[] ciphertext, byte[] key, byte[] IV)
        {
            List<byte> listBytePlaintext = new List<byte>();

            byte[] tmp = IV;
            for (int i = 0; i < ciphertext.Length / 16; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(ciphertext, i * 16, 16);
                byte[] bytesFromDecryptor = AESFunctions.AES_Decrypt_block(bytesBlock, key);

                tmp = HelpfulFunctions.ExclusiveOR(bytesFromDecryptor, tmp);
                listBytePlaintext.AddRange(tmp);
            }

            return listBytePlaintext.ToArray();
        }


        public static byte[] BlockChainingModeEncypt(byte[] plaintext, byte[] key, byte[] IV)
        {
            List<byte> listByteCiphertext = new List<byte>();

            byte[] tmp = IV;
            for (int i = 0; i < plaintext.Length / 16; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(plaintext, i * 16, 16);
                bytesBlock = HelpfulFunctions.ExclusiveOR(bytesBlock, tmp);
                byte[] bytesFromAES = AESFunctions.AES_encrypt_block(bytesBlock, key);
                listByteCiphertext.AddRange(bytesFromAES);
                tmp = HelpfulFunctions.ExclusiveOR(tmp, bytesFromAES);
            }

            return listByteCiphertext.ToArray();
        }

        public static byte[] BlockChainingModeDecrypt(byte[] ciphertext, byte[] key, byte[] IV)
        {
            List<byte> listBytePlaintext = new List<byte>();

            byte[] tmp = IV;
            for (int i = 0; i < ciphertext.Length / 16; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(ciphertext, i * 16, 16);
                byte[] bytesFromDecryptor = AESFunctions.AES_Decrypt_block(bytesBlock, key);
                byte[] plaintextBlock = HelpfulFunctions.ExclusiveOR(bytesFromDecryptor, tmp);
                listBytePlaintext.AddRange(plaintextBlock);
                tmp = HelpfulFunctions.ExclusiveOR(bytesBlock, tmp);
            }

            return listBytePlaintext.ToArray();
        }


        public static byte[] PropagatingCipherBlockChainingMode2Encypt(byte[] plaintext, byte[] key, byte[] IV)
        {
            List<byte> listByteCiphertext = new List<byte>();

            byte[] tmp = IV;
            byte[] tmp2 = { 1 };
            for (int i = 0; i < plaintext.Length / 16; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(plaintext, i * 16, 16);
                byte[] tmp2Copy = tmp2;
                tmp2 = bytesBlock;
                if (i != 0)
                {
                    bytesBlock = HelpfulFunctions.ExclusiveOR(bytesBlock, tmp2Copy);
                }
                bytesBlock = HelpfulFunctions.ExclusiveOR(bytesBlock, tmp);
                tmp = AESFunctions.AES_encrypt_block(bytesBlock, key);
                listByteCiphertext.AddRange(tmp);
            }

            return listByteCiphertext.ToArray();
        }

        public static byte[] PropagatingCipherBlockChainingMode2Decrypt(byte[] ciphertext, byte[] key, byte[] IV)
        {
            List<byte> listBytePlaintext = new List<byte>();

            byte[] tmp = IV;
            byte[] tmp2 = { 1 };
            for (int i = 0; i < ciphertext.Length / 16; i++)
            {
                byte[] bytesBlock = HelpfulFunctions.SubArrayDeepClone(ciphertext, i * 16, 16);
                byte[] tmp2Copy = tmp2;
                tmp2 = bytesBlock;
                bytesBlock = AESFunctions.AES_Decrypt_block(bytesBlock, key);
                if (i != 0)
                {
                    bytesBlock = HelpfulFunctions.ExclusiveOR(bytesBlock, tmp2Copy);
                }
                tmp = HelpfulFunctions.ExclusiveOR(bytesBlock, tmp);
                listBytePlaintext.AddRange(tmp);
            }

            return listBytePlaintext.ToArray();
        }


        // public static byte[] 

    }
}
