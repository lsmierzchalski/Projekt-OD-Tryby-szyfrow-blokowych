using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Block_Cipher_Modes_WPF.ViewModels
{
    class BasicBlockCipherModesViewModel
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

        private RichTextBox RichTextBoxPlainText;
        private RichTextBox RichTetBoxCipherText;

        public BasicBlockCipherModesViewModel(RichTextBox richTextBoxPlainText, RichTextBox richTetBoxCipherText)
        {
            RichTextBoxPlainText = richTextBoxPlainText;
            RichTetBoxCipherText = richTetBoxCipherText;
        }

        public ICommand Encrypt_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        MessageBox.Show(PlainText);
                    });
            }
        }

        public void Encrypt()
        {

        }

    }
}
