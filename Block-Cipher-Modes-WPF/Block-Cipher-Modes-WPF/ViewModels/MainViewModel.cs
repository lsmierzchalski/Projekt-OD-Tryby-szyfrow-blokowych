using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Block_Cipher_Modes_WPF.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private Page BasicBlockCipherModesPage;
        private Page AboutPage;
        private Page CreatorKeysPage;
        private Page HMACPage;
        private Page TimeTestPage;

        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private double _frameOpacity;
        public double FrameOpacity { get => _frameOpacity; set => _frameOpacity = value; }

        public MainViewModel()
        {
            BasicBlockCipherModesPage = new Pages.BasicBlockCipherModesPage();
            AboutPage = new Pages.AboutPage();
            CreatorKeysPage = new Pages.CreatorKeysPage();
            HMACPage = new Pages.HMACPage();
            TimeTestPage = new Pages.TimeTestPage();

            FrameOpacity = 1;
            CurrentPage = BasicBlockCipherModesPage;
        }

        public ICommand BasicBlockCipherModes_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        SlowOpacity(BasicBlockCipherModesPage);
                    });
            }
        }

        public ICommand About_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        SlowOpacity(AboutPage);
                    });
            }
        }

        public ICommand CreatorKeys_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        SlowOpacity(CreatorKeysPage);
                    });
            }
        }
        
        public ICommand HMAC_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        SlowOpacity(HMACPage);
                    });
            }
        }

        public ICommand TimeTestPage_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        SlowOpacity(TimeTestPage);
                    });
            }
        }

        private async void SlowOpacity(Page page)
        {
            await Task.Factory.StartNew(() =>
            {
                for (double i = 1.0; i > 0.0; i -= 0.1)
                {
                    FrameOpacity = i;
                    Thread.Sleep(10);
                }
                CurrentPage = page;
                for (double i = 0.0; i < 1.1; i += 0.1)
                {
                    FrameOpacity = i;
                    Thread.Sleep(10);
                }
            });
        }
    }
}
