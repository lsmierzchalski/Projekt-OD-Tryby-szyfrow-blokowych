﻿using System;
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

        private string _key = string.Empty;
        public string Key { get => _key; set => _key = value; }
        

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

                byte[] bytesPlaintext = Encoding.UTF8.GetBytes(PlainText);
                bytesPlaintext = HelpfulFunctions.AddPaddingZero(bytesPlaintext);
                byte[] byteArrayCipherText = BlockCipherModesFunctions.ElectronicCodebookModeEncypt(bytesPlaintext, Convert.FromBase64String(Key));
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
                byte[] byteArrayPlainText = BlockCipherModesFunctions.ElectronicCodebookModeDecrypt(bytesCipherText, Convert.FromBase64String(Key));
                PlainText = System.Text.Encoding.Default.GetString(byteArrayPlainText);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
