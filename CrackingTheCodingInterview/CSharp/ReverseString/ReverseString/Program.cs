using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseString
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new string[] { "vxyz", "abcde", "cat" };

            foreach (var str in input)
            {
                Console.WriteLine("reversing the string: {0} => {1} / {2}", str, Reverse(str), Reverse2(str));
            }

            Console.ReadKey();
        }

        public static string Reverse(string str)
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0, j = str.Length - 1; i < j; i++, j--)
            {
                stringBuilder.Insert(stringBuilder.Length - i, str[i]);
                stringBuilder.Insert(i, str[j]);
            }

            if (str.Length % 2 != 0)
            {
                stringBuilder.Insert(stringBuilder.Length / 2, str[str.Length / 2]);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// For sake of principle
        /// </summary>
        public static string Reverse2(string str)
        {
            int beginIndex = 0;
            int endIndex = str.Length - 1;

            StringBuilder stringBuilder = new StringBuilder(str);

            //First half is reverse and insert in the end one by one
            //second half is reverse and keep in the first one by one
            //Suppose If there are 7 characters, last 3 reverse and keep in first
            //then first 3 reverse and keep at the end, 4th character will be in the same position
            while (beginIndex < endIndex)
            {
                //begin character will be replaced in the following statment
                //thats why it must be keep in temp to keep in the end 
                char temp = stringBuilder[beginIndex];

                //stringBuilder.Replace(char oldChar,char newChar, int start index, int count)
                stringBuilder.Replace(stringBuilder[beginIndex], stringBuilder[endIndex], beginIndex, 1);
                stringBuilder.Replace(stringBuilder[endIndex], temp, endIndex, 1);

                beginIndex++;
                endIndex--;
            }

            return stringBuilder.ToString();
        }

    }
}
