using Block_Cipher_Modes_WPF.Models;
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
    public class TimeTestViewModel : ViewModelBase
    {

        private int _number_of_samples = 5;
        public int Number_of_samples { get => _number_of_samples; set => _number_of_samples = value; }
        private int _number_of_max_bytes = 100;
        public int Number_of_max_bytes { get => _number_of_max_bytes; set => _number_of_max_bytes = value; }

        private ObservableCollection<SampleTimeTest> _test_result = new ObservableCollection<SampleTimeTest>();
        public ObservableCollection<SampleTimeTest> Test_result { get => _test_result; set => _test_result = value; }

        
        public ICommand StartTest_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        try
                        {
                            Test_result = TimeTest();
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    });
            }
        }

        public string[] TestedModes = { "ECB", "CBC", "PCBC", "CFB", "OFB", "CTR", "PBC", "BC" };

        public ObservableCollection<SampleTimeTest> TimeTest()
        {
            ObservableCollection<SampleTimeTest> result = new ObservableCollection<SampleTimeTest>();

            int step = Number_of_max_bytes / Number_of_samples;

            for(int i=1; i< Number_of_samples; i++)
            {

            }

            return result;
        }
    }
}
