using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCompression
{
    class Program
    {
        static void Main(string[] args)
        {
            const string original = "abbccccccde";
            var compressed = CompressBetter(original);
            Console.WriteLine("Original  : {0}", original);
            Console.WriteLine("Compressed: {0}", compressed);
            Console.ReadKey();
        }

        static int CountCompression(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }

            var last = str[0];
            var size = 0;
            var count = 0;

            for (var i = 1; i < str.Length; i++)
            {
                if (str[i] == last)
                {
                    count++;
                }
                else
                {
                    last = str[i];
                    size += 1 + string.Format("{0}", count).Length;
                    count = 1;
                }
            }

            //This is for the last character
            //Because last character is in variable last which size is not added
            size += 1 + string.Format("{0}", count).Length;

            return size;
        }

        static string CompressBetter(string str)
        {
            int size = CountCompression(str);

            if (size >= str.Length)
            {
                return str;
            }

            StringBuilder sb = new StringBuilder();
            char last = str[0];
            int count = 1;

            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == last)
                {
                    count++;
                }
                else
                {
                    sb.Append(last);
                    sb.Append(count);
                    last = str[i];
                    count = 1;
                }
            }
            sb.Append(last);
            sb.Append(count);

            return sb.ToString();
        }
    }
}
