﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Block_Cipher_Modes_WPF.ViewModels
{
    class CreatorKeysViewModel : ViewModelBase
    {
        Random rnd = new Random();

        private List<byte> _listByteKey = new List<byte>();

        public List<byte> ListByteKey
        {
            get => _listByteKey;
            set
            {
                _listByteKey = value;
                OnPropertyChanged();
            }
        }

        private string _keyBase64 = string.Empty;

        public string KeyBase64 {
            get
            {
                return _keyBase64;
            }
            set
            {
                _keyBase64 = value;
                OnPropertyChanged();
            }
        }

        private string _keyBase64_64bit = string.Empty;

        public string KeyBase64_64bit
        {
            get
            {
                return _keyBase64_64bit;
            }
            set
            {
                _keyBase64_64bit = value;
                OnPropertyChanged();
            }
        }


        public ICommand RandomKey_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        RandomKeyValue();
                    });
            }
        }

        public ICommand CreatorKey_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        KeyBase64 = Convert.ToBase64String(ListByteKey.ToArray());
                        KeyBase64_64bit = Convert.ToBase64String(HelpfulFunctions.SubArrayDeepClone(ListByteKey.ToArray(), 0, 8));
                    });
            }
        }

        public CreatorKeysViewModel()
        {
            RandomKeyValue();
        }

        private void RandomKeyValue()
        {
            List<byte> newListByteKey = new List<byte>();
            for (int i = 0; i < 16; i++)
            {
                byte[] intBytes = BitConverter.GetBytes(rnd.Next(0, 256));
                newListByteKey.Add(intBytes[0]);
            }
            ListByteKey = newListByteKey;
            KeyBase64 = Convert.ToBase64String(ListByteKey.ToArray());
            KeyBase64_64bit = Convert.ToBase64String(HelpfulFunctions.SubArrayDeepClone(ListByteKey.ToArray(), 0, 8));
        }
    }
}
