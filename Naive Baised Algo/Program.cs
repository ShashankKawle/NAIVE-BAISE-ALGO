using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naive_Baised_Algo
{

    class Program
    {
        static NBATableOperations nbaTable;
        static NBAProcessing nbaProcess;
        static List<Entity> Table;
        static List<string[]> ColHolder;
        public static int ColNumbers { get; set; }
        static Boolean res;
        static List<List<string[]>> GrpFreqTable;

        static void Main(string[] args)
        {
            Initializer();
            GeneralIO.GetNumberOfColumns();
            Table = nbaTable.GetTable(ColNumbers);

            do
            {
                res = nbaTable.GetData(Table, ColNumbers);
            } while (res == false);

            Console.WriteLine("\n Table is as follows:\n--------------------------------------------");
            nbaTable.DisplayData(Table, ColNumbers);

            nbaProcess.AssignValues(Table, ColNumbers);
            nbaTable.AssignValues(Table, ColNumbers);
            ColHolder = nbaTable.GetColumnArray(Table);

            Console.WriteLine("\n--------------------------------------------");

            GrpFreqTable = nbaProcess.GetFreqTable(Table, ColHolder);
            nbaProcess.DisplayAllFreqTables(GrpFreqTable);

            GeneralIO.GetUserInput();
            double[] finalValues = nbaProcess.CalculateProbabilities(GrpFreqTable);
            nbaProcess.GetLargestProbability(finalValues);
            nbaProcess.PrintResult(finalValues);
            Console.Read();
        }

        private static void Initializer()
        {
            nbaTable = new NBATableOperations();
            nbaProcess = new NBAProcessing();
            ColHolder = new List<string[]>();
        }

        
    }
}