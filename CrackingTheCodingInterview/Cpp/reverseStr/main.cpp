/*
Language: Visual C
IDE: Code blocks

Source
Book : Cracking the coding interview - 5th Edition
Problem : 1.2

Question: Implement a function void reverse (char* str) in C or C++ which reverses a null-terminated string.
Hints : Must have good knowledge about pointer
*/

#include <iostream>

using namespace std;

void reverseA(char *str)
{
    char* endChar = str;
    char tmp;
    if(str)
    {
        while(*endChar)             //Find end of the string
        {
            ++endChar;
        }
        --endChar;                  //set one character back, since last character is null

        /*
        /* Swap character from start of the string with the end of the string
        /* until pointer meet in the null in the middle
        */
        while (str < endChar)
        {
            tmp = *str;
            *str++ = *endChar;
            *endChar-- = tmp;
        }
    }
}
int main()
{
    char input1[] = "This is a string extend\0 me";
    cout << "reversing the string: " << input1 << endl;
    reverseA(input1);
    cout << "reverse of input string is  " << input1 << endl;

    //Actually [2][10] size array
    char input2[][10] = { "abcde", "cat" };

    for (int i = 0; i < 2; i++)
    {
        cout << "reversing the string: " << input2[i] << endl;
        reverseA(input2[i]);
        cout << "reverse of input string is " << input2[i] << endl;
    }
    return 0;
}
