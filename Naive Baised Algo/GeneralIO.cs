using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naive_Baised_Algo
{
    class GeneralIO
    {
        internal static void GetNumberOfColumns()
        {
            Console.Write("Please enter the number of columns : ");
            string input = Console.ReadLine();
            Program.ColNumbers = int.Parse(input);
        }

        internal static void GetUserInput()
        {
            NBAProcessing.userInput = new string[(NBAProcessing.ColNumbers - 1)];
            Console.WriteLine("Enter your Query :");
            for (int i = 0; i < (NBAProcessing.ColNumbers - 1); i++)
            {
                NBAProcessing.userInput[i] = Console.ReadLine();
            }
        }
    }
}
