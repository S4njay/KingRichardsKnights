



using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace KingRichardsKnights
{
    class Solution
    {
        internal class Location
        {
            public long row;
            public long col;
        }

        /*
         * Complete the kingRichardKnights function below.
         */
        static int[][] kingRichardKnights(int n, int s, int[][] commands, int l, int[] knights)
        {
            var matrix = new Dictionary<long, Location>();

            InitMatrix(n, matrix);

            var originalMatrix = matrix;
           
            int a = 1;
            int b = 1;
            int d = n - 1;

            foreach (var command in commands)
            {
                if (command[0] == a && command[1] == b && command[2] == d)
                {
                    continue;
                }

                if (command[0] < a || command[1] < b || command[2] > d ||
                    command[0] + command[2] > a + d || command[1] + command[2] > b + d)
                {
                    continue;
                }

                a = command[0];
                b = command[1];
                d = command[2];

                matrix = matrix.Where(x => x.Value.row >= a - 1 && x.Value.row <= (a + d)
                                                                && x.Value.col >= b - 1 && x.Value.col <= (b + d))
                    .ToDictionary(i => i.Key, i => i.Value);

                RotateMatrix(matrix, command[0], command[1], command[2]);
            }


            var result = new List<int[]>();
            foreach (var knight in knights)
            {
                result.Add(new List<int> {
                    (int)originalMatrix[knight].row + 1,
                    (int)originalMatrix[knight].col + 1}
                    .ToArray());

            }

            return result.ToArray();

        }

        static void Main(string[] args)
        {
            //TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            bool readfromFile = true;

            if (readfromFile)
            {

                System.IO.StreamReader file =
                    new System.IO.StreamReader(@"input01.txt");

                int n = Convert.ToInt32(file.ReadLine());

                int s = Convert.ToInt32(file.ReadLine());

                int[][] commands = new int[s][];

                for (int commandsRowItr = 0; commandsRowItr < s; commandsRowItr++)
                {
                    commands[commandsRowItr] = Array.ConvertAll(file.ReadLine().Split(' '),
                        commandsTemp => Convert.ToInt32(commandsTemp));
                }

                int l = Convert.ToInt32(file.ReadLine());

                int[] knights = new int[l];

                for (int knightsRowItr = 0; knightsRowItr < l; knightsRowItr++)
                {
                    knights[knightsRowItr] = Convert.ToInt32(file.ReadLine());
                }


                int[][] result = kingRichardKnights(n, s, commands, l, knights);

                file.Close();
                
                // Suspend the screen.  
                Console.ReadLine();
            }
            else
            {
                int n = Convert.ToInt32(Console.ReadLine());

                int s = Convert.ToInt32(Console.ReadLine());

                int[][] commands = new int[s][];

                for (int commandsRowItr = 0; commandsRowItr < s; commandsRowItr++)
                {
                    commands[commandsRowItr] = Array.ConvertAll(Console.ReadLine().Split(' '),
                        commandsTemp => Convert.ToInt32(commandsTemp));
                }

                int l = Convert.ToInt32(Console.ReadLine());

                int[] knights = new int[l];

                for (int knightsRowItr = 0; knightsRowItr < l; knightsRowItr++)
                {
                    knights[knightsRowItr] = Convert.ToInt32(Console.ReadLine());
                }


                int[][] result = kingRichardKnights(n, s, commands, l, knights);

                //textWriter.WriteLine(String.Join("\n", result.Select(x => String.Join(" ", x))));

                //textWriter.Flush();
                //textWriter.Close();
            }


        }

        private static void RotateMatrix(Dictionary<long, Location> matrix, int a, int b, int d)
        {
            foreach (var key in matrix.Keys)
            {
               
                var row = matrix[key].row;
                var col = matrix[key].col;


                if (row < a - 1
                    || row > (a - 1) + d
                    || col < (b - 1)
                    || col > (b - 1) + d)
                {

                    continue;
                }

                row = row - (a - 1);
                col = col - (b - 1);



                var temp = col;
                col = (d + 1) - row - 1;
                row = temp;


                row = row + (a - 1);
                col = col + b - 1;

                matrix[key].row = row;
                matrix[key].col = col;



            }
        }

        private static void InitMatrix(int N, Dictionary<long, Location> matrix)
        {
            for (int row = 0; row < N; row++)
            {
                for (int col = 0; col < N; col++)
                {
                    matrix.Add(row * N + col,
                        new Location { row = row, col = col });
                }
            }
        }

        private static void PrintMatrix(int N, Dictionary<int, Location> matrix)
        {
            for (int row = 0; row < N; row++)
            {
                for (int col = 0; col < N; col++)
                {
                    //var key = row * N + col;
                    //Console.WriteLine($"{key} -> Row : " +
                    //              $"{matrix[key].row} Col : " +
                    //              $"{matrix[key].col}");

                    try
                    {
                        Console.Write(matrix.SingleOrDefault(
                                          m => m.Value.row == row && m.Value.col == col).Key + "  ");
                    }
                    catch (Exception e)
                    {
                        Console.Write("E ");

                    }

                }
                Console.WriteLine();
            }


            Console.ReadKey();
        }
    }

    

    
}
