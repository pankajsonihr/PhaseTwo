using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhaseTwo
{
    static class ConsoleHelper
    {
        /// <summary>
        /// this method take two integers, one is minimum and the other is maximum integer values entered by user it also have 
        /// a string error message that will be shown when input is not in the given range
        /// </summary>
        /// <param name="min">minimum allowed userinput int value</param>
        /// <param name="max">maximum allowed userinput int value</param>
        /// <param name="errorMsg">Error message to be shown when user input is not valid</param>
        /// <returns>returns the sanitized int input value</returns>
        public static int GetSelection(int min, int max, string errorMsg)
        {
            int? selection = SanitizeInput(Console.ReadLine(), min, max);
            while (selection == null)
            {
                Console.WriteLine(errorMsg);
                selection = SanitizeInput(Console.ReadLine(), min, max);
            }
            return (int)selection; // 'selection' cannot be null here
        }
        /// <summary>
        /// it will convert the string input value into integer input value and also checks that value is b/w min and max int value we have given
        /// </summary>
        /// <param name="input">string input to convert int value</param>
        /// <param name="min">minimum allowed userinput int value</param>
        /// <param name="max">maximum allowed userinput int value</param>
        /// <returns>returns the filtered int values that we need for specific task</returns>
        public static int? SanitizeInput(string input, int min, int max)
        {
            int result;
            if (int.TryParse(input, out result) && result >= min && result <= max)
            {
                return result;
            }
            return null;
        }
    }
}
