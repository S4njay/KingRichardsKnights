



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
            var matrix = new int[n * n];
            var locationMatrix = new int[n * n];

            InitMatrix(n, matrix);

            var originalMatrix = matrix;
            Array.Copy(originalMatrix, locationMatrix, n * n);

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
                //|InRange|             // |InRange  

                matrix = originalMatrix
                            .Select((v,i) => new {v,i})
                            .Where(x => InRange(x.i, a, b, d, n))
                            .Select(x => x.v)
                            .ToArray();

                RotateMatrix(matrix, command[0], command[1], command[2], ref originalMatrix, n, ref locationMatrix);
                //Console.WriteLine($"{a} {b} {d}");
                //PrintMatrix(n,originalMatrix);
            }


            var result = new List<int[]>();
            foreach (var knight in knights)
            {
                result.Add(new List<int> {
                    (int)locationMatrix[knight] / n + 1,
                    (int)locationMatrix[knight] % n + 1}
                    .ToArray());

            }

            return result.ToArray();

        }
        static bool InRange(int idx, int a, int b, int d, int n)
        {
            var row =  idx / n;
            var rowInRange = row >= a - 1 && row <= a + d - 1;
            

            var col = idx % n;
            var colInRange = col >= b - 1 && col <= b + d - 1;
            var inRange =  rowInRange && colInRange;
            return inRange;
        }

        static void Main(string[] args)
        {
            //TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            bool readfromFile = false;

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

                //textWriter.WriteLine(String.Join("\n", result.Select(idx => String.Join(" ", idx))));

                //textWriter.Flush();
                //textWriter.Close();
            }


        }

        private static void RotateMatrix(int[] matrix, int a, int b, int d, ref int[] originalMatrix, int n,
            ref int[] locationMatrix)
        {
            var nmatrix = new int[matrix.Length];

            for (var key = 0; key < matrix.Length; key++)
            {
                var row = key / (d + 1);
                var col = key % (d + 1);

                var temp = col;
                col = (d + 1) - row - 1;
                row = temp;
                nmatrix[row * (d + 1) + col] = matrix[key];
                row = row + (a - 1);
                col = col + b - 1;

                originalMatrix[row * n + col] = matrix[key];
                locationMatrix[matrix[key]] = row * n + col;
            }
        }

        private static void InitMatrix(int N, int[] matrix)
        {
            for (int row = 1; row <= N; row++)
            {
                for (int col = 1; col <= N; col++)
                {
                    matrix[(row - 1) * N + col - 1] = (row - 1) * N + col - 1;
                }
            }
        }

        private static void PrintMatrix(int N, int[] matrix)
        {
            for (int row = 0; row < N; row++)
            {
                for (int col = 0; col < N; col++)
                {
                    //var key = InRange * N + colInRange;
                    //Console.WriteLine($"{key} -> Row : " +
                    //              $"{matrix[key].InRange} Col : " +
                    //              $"{matrix[key].colInRange}");

                    try
                    {
                        Console.Write(matrix[row * N + col] + " ");
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
