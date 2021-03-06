﻿using Block_Cipher_Modes_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Block_Cipher_Modes_WPF.Pages
{
    /// <summary>
    /// Interaction logic for BasicBlockCipherModesPage.xaml
    /// </summary>
    public partial class BasicBlockCipherModesPage : Page
    {
        public BasicBlockCipherModesPage()
        {
            InitializeComponent();
            DataContext = new BasicBlockCipherModesViewModel(RichTextBoxPlainText, RichTetBoxCipherText);
        }
    }
}
