using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Block_Cipher_Modes_WPF.ViewModels
{
    class BasicBlockCipherModesViewModel : ViewModelBase
    {
        private string _plainText = string.Empty;
        public string PlainText
        {
            get
            {
                _plainText = new TextRange(RichTextBoxPlainText.Document.ContentStart, RichTextBoxPlainText.Document.ContentEnd).Text;
                return _plainText;
            }
            set
            {
                RichTextBoxPlainText.Document.Blocks.Clear();
                RichTextBoxPlainText.Document.Blocks.Add(new Paragraph(new Run(value)));
                _plainText = value;
            }
        }

        private string _cipherText = string.Empty;
        public string CipherText
        {
            get
            {
                _cipherText = new TextRange(RichTetBoxCipherText.Document.ContentStart, RichTetBoxCipherText.Document.ContentEnd).Text;
                return _cipherText;
            }
            set
            {
                RichTetBoxCipherText.Document.Blocks.Clear();
                RichTetBoxCipherText.Document.Blocks.Add(new Paragraph(new Run(value)));
                _cipherText = value;
            }
        }

        private string _key = string.Empty;
        public string Key { get => _key; set => _key = value; }

        private string _iv = string.Empty;
        public string IV { get => _iv; set => _iv = value; }

        private string _encryptTime = "0 ms";
        public string EncryptTime
        {
            get => _encryptTime;
            set
            {
                _encryptTime = value;
                OnPropertyChanged();
            }
        }

        private string _decryptTime = "0 ms";
        public string DecryptTime
        {
            get => _encryptTime;
            set
            {
                _encryptTime = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _listModes = new ObservableCollection<string>()
        {
            "ECB", "CBC", "PCBC", "CFB", "OFB"
        };
        public ObservableCollection<string> ListModes { get => _listModes; set => _listModes = value; }

        private string _selectMode = "ECB";
        public string SelectMode { get => _selectMode; set => _selectMode = value; }

        private RichTextBox RichTextBoxPlainText;
        private RichTextBox RichTetBoxCipherText;

        public BasicBlockCipherModesViewModel(RichTextBox richTextBoxPlainText, RichTextBox richTetBoxCipherText)
        {
            RichTextBoxPlainText = richTextBoxPlainText;
            RichTetBoxCipherText = richTetBoxCipherText;
        }

        public ICommand ReadPlainTextFromFile_Click
        {
            get
            {
                return new RelayCommand(
                    ()=>
                    {
                        PlainText = FileHelpfulFunctions.SelectFile();
                    });
            }
        }

        public ICommand ReadCipherTextFromFile_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        CipherText = FileHelpfulFunctions.SelectFile();
                    });
            }
        }

        public ICommand Encrypt_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        Encrypt();
                    });
            }
        }

        public ICommand Decrypt_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        Decrypt();
                    });
            }
        }

        public void Encrypt()
        {
            try
            {
                byte[] bytesPlaintext = Encoding.Default.GetBytes(PlainText);
                bytesPlaintext = HelpfulFunctions.AddPaddingZero(bytesPlaintext);

                byte[] byteArrayCipherText = { 1 };
                var watch = System.Diagnostics.Stopwatch.StartNew();
                switch (SelectMode)
                {
                    case "ECB":
                        byteArrayCipherText = BlockCipherModesFunctions.ElectronicCodebookModeEncypt(bytesPlaintext, Convert.FromBase64String(Key));
                        break;
                    case "CBC":
                        byteArrayCipherText = BlockCipherModesFunctions.CipherBlockChainingModeEncypt(bytesPlaintext, Convert.FromBase64String(Key), Convert.FromBase64String(IV));
                        break;
                    case "PCBC":
                        byteArrayCipherText = BlockCipherModesFunctions.PropagatingCipherBlockChainingModeEncypt(bytesPlaintext, Convert.FromBase64String(Key), Convert.FromBase64String(IV));
                        break;
                    case "CFB":
                        byteArrayCipherText = BlockCipherModesFunctions.CipherFeedbackModeEncypt(bytesPlaintext, Convert.FromBase64String(Key), Convert.FromBase64String(IV));
                        break;
                    case "OFB":
                        byteArrayCipherText = BlockCipherModesFunctions.OutputFeedbackModeEncypt(bytesPlaintext, Convert.FromBase64String(Key), Convert.FromBase64String(IV));
                        break; 
                }
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                EncryptTime = elapsedMs.ToString() + " ms";

                CipherText = Convert.ToBase64String(byteArrayCipherText);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Decrypt()
        {
            try
            {
                byte[] bytesCipherText = Convert.FromBase64String(CipherText);
                bytesCipherText = HelpfulFunctions.AddPaddingZero(bytesCipherText);

                var watch = System.Diagnostics.Stopwatch.StartNew();
                byte[] byteArrayPlainText = { 1 };
                switch (SelectMode)
                {
                    case "ECB":
                        byteArrayPlainText = BlockCipherModesFunctions.ElectronicCodebookModeDecrypt(bytesCipherText, Convert.FromBase64String(Key));
                        break;
                    case "CBC":
                        byteArrayPlainText = BlockCipherModesFunctions.CipherBlockChainingModeDecrypt(bytesCipherText, Convert.FromBase64String(Key), Convert.FromBase64String(IV));
                        break;
                    case "PCBC":
                        byteArrayPlainText = BlockCipherModesFunctions.PropagatingCipherBlockChainingModeDecrypt(bytesCipherText, Convert.FromBase64String(Key), Convert.FromBase64String(IV));
                        break;
                    case "CFB":
                        byteArrayPlainText = BlockCipherModesFunctions.CipherFeedbackModeDecrypt(bytesCipherText, Convert.FromBase64String(Key), Convert.FromBase64String(IV));
                        break;
                    case "OFB":
                        byteArrayPlainText = BlockCipherModesFunctions.OutputFeedbackModeEncypt(bytesCipherText, Convert.FromBase64String(Key), Convert.FromBase64String(IV));
                        break;
                }
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                DecryptTime = elapsedMs.ToString() + " ms";

                PlainText = System.Text.Encoding.Default.GetString(byteArrayPlainText);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
