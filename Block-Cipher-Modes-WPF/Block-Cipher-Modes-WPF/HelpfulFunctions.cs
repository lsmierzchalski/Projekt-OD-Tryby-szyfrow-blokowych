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
            bool first = true;
            for (int i=listData.Capacity; i < listData.Capacity/128+1 * 128 - 1; i++)
            {
                listData.Add(1);
            }
            return listData.ToArray();
        }
    }
}
