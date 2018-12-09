using Block_Cipher_Modes_WPF.ViewModels;
using System.Windows.Controls;

namespace Block_Cipher_Modes_WPF.Pages
{
    /// <summary>
    /// Interaction logic for HMACPage.xaml
    /// </summary>
    public partial class HMACPage : Page
    {
        public HMACPage()
        {
            InitializeComponent();
            DataContext = new HMACViewModel();
        }
    }
}
