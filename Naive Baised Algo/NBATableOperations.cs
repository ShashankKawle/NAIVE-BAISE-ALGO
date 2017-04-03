using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naive_Baised_Algo
{
    struct Entity
    {
        public string[] Row;
    }
    class NBATableOperations
    {
        protected Entity OneRow = new Entity();
        protected static int ColNumbers { get; set; }
        protected static int TotalLength { get; set; }

        internal bool GetData(List<Entity> table, int colNumbers)
        {
            Boolean exit = false;
            Console.WriteLine("Enter 'quit' to exit.");
            string[] data = new string[colNumbers];
            for (int i = 0; i < colNumbers; i++)
            {
                Console.Write($"Enter the value for Column {(i+1)} : ");
                string input = Console.ReadLine();
                if (Equals(input, "quit"))
                {
                    exit = true;
                    break;
                }
                else
                {
                    data[i] = input;
                }
            }
            if (exit == false)
            {
                OneRow.Row = data;
                table.Add(OneRow);
                return false;
            }
            else
            {
                return true;
            }
        }

        internal void AssignValues(List<Entity> table, int colNumbers)
        {
            TotalLength = table.Count();
            ColNumbers = colNumbers;
        }

        internal void DisplayData(List<Entity> table, int colNumbers)
        {
            foreach (Entity entity in table)
            {
                for (int i = 0; i < colNumbers; i++)
                {
                    Console.Write($"{entity.Row[i]} ");
                }
                Console.WriteLine();
            }
        }

        internal List<Entity> GetTable(int colNumbers)
        {
            List<Entity> Table = new List<Entity>();
            return Table;
        }


        /*
         * #######################################################################################################################################################################################
         * THE FOLLOWING CODE GENERATES ARRAY OF COLUMNS SAPERATED AND REMOVING CONTENT REPEATATION.
         * #######################################################################################################################################################################################
         */

        internal List<string[]> GetColumnArray(List<Entity> table)
        {
            Boolean res;
            List<string[]> colHolder = new List<string[]>();
            int ColNum = 0;
            string[] finalArray;
            string[] OneRow = new string[ColNumbers];

            while (ColNum < ColNumbers)
            {
                string[] array1 = new string[TotalLength];
                int index = 0;
                foreach (Entity entity in table)
                {
                    OneRow = entity.Row;
                    res = CheckIfExist(OneRow[ColNum], array1);
                    if (res == false)
                    {
                        array1[index] = OneRow[ColNum];
                        index++;
                    }
                }
                finalArray = FilterArray(array1);
                colHolder.Add(finalArray);
                ColNum++;
            }

            return colHolder;
        }

        private string[] FilterArray(string[] array1)
        {
            int actualSize = array1.Length;
            int counter = 0;
            int index = 0;
            while (index < actualSize)
            {
                if (!String.IsNullOrEmpty(array1[index]))
                {
                    counter++;
                }
                index++;
            }
            string[] ResultArray = new string[counter];
            for (int i = 0; i < counter; i++)
            {
                ResultArray[i] = array1[i];
            }
            return ResultArray;
        }

        private static bool CheckIfExist(string v, string[] array1)
        {
            int flag = 0;
            for (int i = 0; i < TotalLength; i++)
            {
                if (Equals(v, array1[i]))
                {
                    flag = 1;
                    break;
                }
            }
            if (flag == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}