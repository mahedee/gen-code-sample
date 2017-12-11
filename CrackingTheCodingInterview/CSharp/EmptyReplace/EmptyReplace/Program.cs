using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyReplace
{

    class Program
    {
        static void Main(string[] args)
        {
            string input = "Mr. John Smith";

            //Consider size for 5 extra space
            var characterArray = new char[input.Length + 5 * 2 + 1]; 

            for (var i = 0; i < input.Length; i++)
            {
                characterArray[i] = input[i];
            }

            ReplaceSpaces(characterArray, input.Length);
            Console.WriteLine("{0} -> {1}", input, new string(characterArray));
            Console.ReadKey();
        }



        static void ReplaceSpaces(char[] input, int length)
        {
            int spaceCount = 0;

            // count the number of spaces
            for (int i = 0; i<length; i++)
            {
                if(input[i] == ' ')
                {
                    spaceCount++;
                }
            }

            // calculate new string size
            int index = length + spaceCount * 2;

            // copying the characters backwards and inserting % 20
            for (int i = length-1; i>=0; i--)
            {
                if(input[i] == ' ')
                {
                    input[index - 1] = '0';
                    input[index - 2] = '2';
                    input[index - 3] = '%';
                    index -= 3;
                }
                else
                {
                    input[index - 1] = input[i];
                    index--;
                }
            }
        }
    }
}
