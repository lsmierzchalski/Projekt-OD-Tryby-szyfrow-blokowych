using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Block_Cipher_Modes_WPF.ViewModels
{
    public class HMACViewModel : ViewModelBase
    {
        private string _key = string.Empty;
        public string Key { get => _key; set => _key = value; }

        private string _message = string.Empty;
        public string Message { get => _message; set => _message = value; }

        private string _hash = "MD5";// string.Empty;
        public string Hash { get => _hash; set => _hash = value; }

        private ObservableCollection<string> _hashs = new ObservableCollection<string>() { "MD5", "SHA-384", "SHA-512", "SHA-224", "SHA-1" };
        public ObservableCollection<string> Hashs { get => _hashs; set => _hashs = value; }

        private int _blockSize = 0;
        public int BlockSize { get => _blockSize; set => _blockSize = value; }

        private int _outputSize = 0;
        public int OutputSize { get => _outputSize; set => _outputSize = value; }

        private string _resultHMAC = string.Empty;
        public string ResultHMAC { get => _resultHMAC; set => _resultHMAC = value; }

        private string _verifiedHMAC = string.Empty;
        public string VerifiedHMAC { get => _verifiedHMAC; set => _verifiedHMAC = value; }

        public HMACViewModel()
        {

        }

        public ICommand CreateHMAC_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        try
                        {

                        }catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString()); 
                        }
                    });
            }
        }


        private string createHMAC(string key, string message, string hash, int blockSize, int outputSize)
        {
            if(key.Length > blockSize)
            {
                key = GetHash(hash, key);
            }

            if (key.Length < blockSize)
            {
                key = Pad(key, blockSize);
            }

           // byte[] o_key_pad = HelpfulFunctions.ExclusiveOR(key)

            return "";
        }

        private string GetHash(string hash, string input)
        {
            byte[] byte_hash = { 0 };
            switch (hash)
            {
                case "MD5":
                    {
                        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                        byte[] preHash = Encoding.UTF32.GetBytes(input);
                        byte_hash = md5.ComputeHash(preHash);
                        break;
                    }
                case "SHA-1":
                    {
                        System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
                        byte[] preHash = Encoding.UTF32.GetBytes(input);
                        byte_hash = sha1.ComputeHash(preHash);
                        break;
                    }
                case "SHA256":
                    {
                        System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create();
                        byte[] preHash = Encoding.UTF32.GetBytes(input);
                        byte_hash = sha256.ComputeHash(preHash);
                        break;
                    }
                case "SHA-384":
                    {
                        System.Security.Cryptography.SHA384 sha384 = System.Security.Cryptography.SHA384.Create();
                        byte[] preHash = Encoding.UTF32.GetBytes(input);
                        byte_hash = sha384.ComputeHash(preHash);
                        break;
                    }
                case "SHA-512":
                    {
                        System.Security.Cryptography.SHA512 sha512 = System.Security.Cryptography.SHA512.Create();
                        byte[] preHash = Encoding.UTF32.GetBytes(input);
                        byte_hash = sha512.ComputeHash(preHash);
                        break;
                    }
            }

            return Convert.ToBase64String(byte_hash);
        }


        public static string Pad(string key, int blockSize)
        {
            List<byte> listData = new List<Byte>(Encoding.UTF32.GetBytes(key));
            for (int i = listData.Count; i < blockSize; i++)
            {
                listData.Add(0);
            }
            return Encoding.Default.GetString(listData.ToArray());
        }

    }
}
