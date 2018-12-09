using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Block_Cipher_Modes_WPF.Models
{
    public class SampleTimeTest
    {
        private int _index;
        public int Index { get => _index; set => _index = value; }

        private int _number_of_bytes;
        public int Number_of_bytes { get => _number_of_bytes; set => _number_of_bytes = value; }

        private string _mode_name = string.Empty;
        public string Mode_name { get => _mode_name; set => _mode_name = value; }

        private int _time;
        public int Time { get => _time; set => _time = value; }
    }
}
