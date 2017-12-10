/*
Language: Visual C++
IDE: Code blocks

Source
Book : Cracking the coding interview - 5th Edition
Problem : 1.2

Question: Implement a function void reverse (char* str) in C or C++ which reverses a null-terminated string.
Hints : Must have good knowledge about pointer
*/

#include <iostream>

using namespace std;

void reverseA(char *str);

//Int main
int main()
{

    //Actually [2][10] size array
    //For two input2[10] string
    char input2[][10] = { "abcde", "cat" };
    for (int i = 0; i < 2; i++)
    {
        cout << "reversing the string: " << input2[i] << endl;
        reverseA(input2[i]);
        cout << "reverse of input string is " << input2[i] << endl;
    }

    //character array for string
    char input1[] = "This is a string extend\0 me";
    cout << "reversing the string: " << input1 << endl;
    reverseA(input1);
    cout << "reverse of input string is  " << input1 << endl;


    return 0;
}

void reverseA(char *str)
{
    //*str works like a character array
    //*str point the memory address of the array input
    char *endChar = str;
    //Now value of endChar and str are same
    //for example endChar = str = abcde
    //And the memory address of endChar and str are same. i,e : Ox6afee8

    char tmp;

    //if str != null
    if(str)
    {
        //While *endChar is not null
        // *endChar is the array of character
        // endChar
        while(*endChar)             //Find end of the string
        {
            //Increment index by 1 means increment memory address by one
            //For example first time endChar = abcde then bcde then cde then de etc
            //Pointing the next index
            ++endChar;
        }
        --endChar;                  //set one character back, since last character is null

        /*
        /* Swap character from start of the string with the end of the string
        /* until pointer meet in the null in the middle
        */

        //Memory address of str < Memory address of endChar
        while (str < endChar)
        {
            //Here str reversing from beginning to middle and endChar reversing from end to middle
            //If number of characters in string is odd then first half is reversed by str and second half by endChar
            //and middle character remain unchanged

            //Since tmp is a character variable it holds the first index of the str
            //tmp holds the current pointing character of str
            tmp = *str;

            //current pointing character of str is replaced by *endChar
            *str = *endChar;

            //Now point next index
            *str++;

            //current pointing character of endChar is replaced by tmp
            *endChar = tmp;

            //Now point to the previous index
            *endChar--;

            //The above 4 statement can be written as
            //*str++ = *endChar;
            //*endChar-- = tmp;

        }
    }
}
