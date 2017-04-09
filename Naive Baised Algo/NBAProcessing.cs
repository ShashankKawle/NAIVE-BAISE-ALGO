using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naive_Baised_Algo
{ 
    class NBAProcessing
    {
        public static int ColNumbers { get; set; }
        protected static int totalLength { get; set; }
        protected static string[] lastColumn;
        protected static int[] lastColumnValueCount;
        public static string[] userInput;
        protected static string answer { get; set; }
        protected static double probabilityOfAnswer { get; set; }

        internal void AssignValues(List<Entity> table, int ColNum)
        {
            totalLength = table.Count();
            ColNumbers = ColNum;
        }

        /*
        * #######################################################################################################################################################################################
        * THE FOLLOWING CODE GENERATES LIST OF FREQUENCY TABLES.
        * FOLLOWING CODE ALSO INCLUDES DISPLAY FUNCTION TO DISPLAY FREQUENCY TABLES.
        * #######################################################################################################################################################################################
        */

        internal List<List<string[]>> GetFreqTable(List<Entity> table, List<string[]> colHolder)
        {
            Console.WriteLine("The Frequency tables are as follows:\n--------------------------------------------");
            List<string[]> OneFreqTable;
            List<List<string[]>> GrpOfFreqTable = new List<List<string[]>>();
            int count;
            string[] FreqTableRow;
            lastColumn = colHolder[(ColNumbers - 1)];
            GetLastColumnValueCount(table);
            string[] probabilityHolder = new string[lastColumn.Length];
            double temp;

            for (int i = 0; i < (colHolder.Count() - 1); i++)   //to point to the column in colHolder
            {
                string[] s = colHolder[i];
                OneFreqTable = new List<string[]>();

                for (int j = 0; j < s.Length; j++)     //to point to the element in 's'
                {
                    for (int k = 0; k<lastColumn.Length; k++)    //to point to the element in last column
                    {
                        count = GetRepeatingCount(table, i, s[j], lastColumn[k]);
                        temp = (Double)count / lastColumnValueCount[k];
                        probabilityHolder[k] = temp.ToString();
                    }
                    FreqTableRow = GetStringRow(s[j], probabilityHolder);
                    OneFreqTable.Add(FreqTableRow);
                }
                GrpOfFreqTable.Add(OneFreqTable);
            }
            return GrpOfFreqTable;
        }

        private void GetLastColumnValueCount(List<Entity> table)
        {
            lastColumnValueCount = new int[lastColumn.Length];
            int count = 0;
            for (int i = 0; i < lastColumn.Length; i++)
            {
                foreach (var item in table)
                {
                    if(lastColumn[i].Equals(item.Row[ColNumbers-1]))
                    {
                        count++;
                    }
                }
                lastColumnValueCount[i] = count;
            }
        }

        private int GetRepeatingCount(List<Entity> table, int i, string v1, string v2)
        {
            int count = 0;
            foreach (Entity entity in table)    
            {
                if (GetComparisonResult(entity.Row[i], v1) && GetComparisonResult(entity.Row[(ColNumbers - 1)], v2))
                {
                    count++;
                }
            }
            return count;
        }

        private bool GetComparisonResult(string v1, string v2)
        {
       
            if(v1.Equals(v2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static string[] GetStringRow(string v, string[] probabilityHolder)
        {
            int sizeCount = (probabilityHolder.Length + 1);
            string[] r = new string[sizeCount];
            r[0] = v;
            for (int i = 0; i < (probabilityHolder.Length); i++)
            {
                r[i+1] = probabilityHolder[i];
            }
            return r;
        }

        internal void DisplayAllFreqTables(List<List<string[]>> grpFrqTable)
        {
            foreach (List<string[]> list in grpFrqTable)
            {
                foreach (string[] s in list)
                {
                    foreach (string content in s)
                    {
                        Console.Write($"{content} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("\n\n");
            }
        }

        /*
         * #######################################################################################################################################################################################
         * THE FOLLOWING CODE CALCULATES NBA PROBABILITIES AND ANSWER.
         * #######################################################################################################################################################################################
         */

        internal void GetLargestProbability(double[] finalValues)
        {
            int index = 0;
            double indexPointer = finalValues[0];
            for (int i = 0; i < finalValues.Length; i++)
            {
                if (indexPointer < finalValues[i])
                {
                    index = i;
                    indexPointer = finalValues[i];
                }
            }
            answer = lastColumn[index];
            probabilityOfAnswer = indexPointer;
        }

        internal double[] CalculateProbabilities(List<List<string[]>> grpFreqTables)
        {
            List<double[]> dataHolder;
            dataHolder = GetQueryData(grpFreqTables, userInput, lastColumn);
            double[] finalValueHolder = GetAllFinalValues(dataHolder);
            return finalValueHolder;
        }

        private static double[] GetAllFinalValues(List<double[]> dataHolder)    //Calculate All Final Probabilities
        {

            double[] finalValueHolder = new double[lastColumn.Length];
            double sum = 1, result;

            for (int i = 0; i < lastColumn.Length; i++)
            {
                foreach (var item in dataHolder)
                {
                    sum = sum * item[i];
                }
                result = (sum * lastColumnValueCount[i])/totalLength;
                finalValueHolder[i] = result;
            }
            return finalValueHolder;
        }

        private static List<double[]> GetQueryData(List<List<string[]>> grpFreqTables, string[] userInput, string[] lastColumn)
        {
            List<double[]> dataHolder = new List<double[]>();

            double[] valueHolder = new double[lastColumn.Length];
            for (int i = 0; i < userInput.Length; i++)
            {
                List<string[]> oneFreqTable = grpFreqTables[i];

                foreach (var item in oneFreqTable)
                {
                    if (userInput[i].Equals(item[0]))
                    {
                        valueHolder = GetValueFromFreqTable(item);
                    }
                }
                dataHolder.Add(valueHolder);
            }
            return dataHolder;
        }


        private static double[] GetValueFromFreqTable(string[] item)
        {
            double[] valueHolder = new double[lastColumn.Length];
            for (int i = 1; i < item.Length; i++)
            {
                valueHolder[i - 1] = Double.Parse(item[i]);
            }
            return valueHolder;
        }

        /*
         *  ############################################################################################################################################
         *  PRINT THE ANSWER
         *  ############################################################################################################################################
         */

        internal void PrintResult(double[] finalValues)
        {
            Console.WriteLine("--------------------------------------------\nResult is as follows\n--------------------------------------------");
            for (int i = 0; i < lastColumn.Length; i++)
            {
                Console.WriteLine($"\nProbability for {lastColumn[i]} is  {finalValues[i]}");
            }
            Console.WriteLine($"Hence the Answer is {answer} with the highest probability of {probabilityOfAnswer}");
        }
    }
        
    
}