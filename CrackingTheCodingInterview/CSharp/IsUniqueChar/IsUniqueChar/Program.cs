using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsUniqueChar
{
    /*
     * Question: Implement an algorithm to determine if a string has all unique characterrs. What if you cannot use additional data structure?
     * Hints : Total extended ASCII characters are 256
     */

    class Program
    {
        static void Main(string[] args)
        {
            //Input string
            string input = "PleaseCheckUniqueKey";
            if (IsUniqueChar(input))
            {
                Console.WriteLine("Yes! It's unique character string");
            }
            else
            {
                Console.WriteLine("Duplicate character exists in the string.");
            }
            Console.ReadKey();
        }

        //Algorithm to determine unique character
        public static bool IsUniqueChar(string str)
        {
            //Because total extended characters are 256
            //Any string with more than 256 character must have duplicate characters
            if (str.Length > 256)
                return false;

            bool[] CharSet = new bool[256];

            for (int i = 0; i < str.Length; i++)
            {
                //ElementAt(index) return character. Since it assigned to an integer variable so its ASCII value will be assigned
                int val = str.ElementAt(i);

                //Already found this character in the string
                if (CharSet[val])
                {
                    return false;
                }
                else
                {
                    CharSet[val] = true;
                }
            }

            return true;
        }
    }
}
