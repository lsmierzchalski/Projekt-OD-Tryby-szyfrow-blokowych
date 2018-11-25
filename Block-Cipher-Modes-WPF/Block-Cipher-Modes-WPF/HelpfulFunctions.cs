using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Block_Cipher_Modes_WPF
{
    public static class HelpfulFunctions
    {
        public static T[] SubArrayDeepClone<T>(this T[] data, int index, int length)
        {
            T[] arrCopy = new T[length];
            Array.Copy(data, index, arrCopy, 0, length);
            using (MemoryStream ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, arrCopy);
                ms.Position = 0;
                return (T[])bf.Deserialize(ms);
            }
        }

        public static byte[] AddPaddingZero(byte[] data)
        {
            List<byte> listData = new List<Byte>(data);
            int plus_one_or_not = 1;
            if (listData.Count % 16 == 0) { plus_one_or_not = 0; }
            int size_16 = (listData.Count / 16 + plus_one_or_not) * 16;
            for (int i=listData.Count; i < size_16; i++)
            {
                listData.Add(1);
            }
            return listData.ToArray();
        }

        public static byte[] ExclusiveOR(byte[] arr1, byte[] arr2)
        {
            if (arr1.Length != arr2.Length)
                throw new ArgumentException("arr1 and arr2 are not the same length");

            byte[] result = new byte[arr1.Length];

            for (int i = 0; i < arr1.Length; ++i)
                result[i] = (byte)(arr1[i] ^ arr2[i]);

            return result;
        }

        public static byte[] AddOneToByteArray(byte[] array)
        {
            for(int i=array.Length-1; i>=0; i--)
            {
                if (array[i] < 255)
                {
                    array[i]++;
                    break;
                }
            }
            return array;
        }
    }
}
