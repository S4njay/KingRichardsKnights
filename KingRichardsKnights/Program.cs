



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
        internal class KnightLocation
        {
            public int knight;
            public int[] location;
            public int counter;
        }

        /*
         * Complete the kingRichardKnights function below.
         */
        static int[][] kingRichardKnights(int n, int s, int[][] commands, int l, int[] knights)
        {
            int a = 1;
            int b = 1;
            int d = n - 1;
            var counter = 1;
            var locations = new List<KnightLocation>();
            locations.AddRange(
                    knights
                        .Select(x => new KnightLocation
                        {
                            knight = x,
                            location = new[] { (x / n) + 1, (x % n) + 1 },
                            counter = 0
                        }));

            var allLocations = new List<KnightLocation>();
            allLocations.AddRange(locations);

            var groupedCommands = commands
                .Select(c => new {a = c[0], b = c[1], d = c[2]})
                .GroupBy(x => new
                {
                    x.a, x.b, x.d
                })
                .Select(x => x.First()).ToList();
                


            foreach (var command in groupedCommands)
            {
                //if (command.a == a && command.b == b && command.d == d)
                //{
                //    continue;
                //}

                if (command.a < a || command.b < b || command.d > d ||
                    command.a + command.d > a + d || command.b + command.d > b + d)
                {
                    continue;
                }

                a = command.a;
                b = command.b;
                d = command.d;

                locations = locations
                    .GroupBy(x => x.knight)
                    .Select(x => x.OrderByDescending(g => g.counter).First())
                    .Where(x => InRange(x.location[0], x.location[1], a, b, d, n))
                    .Select(ckl => new KnightLocation
                    {
                        knight = ckl.knight,
                        location = new[]
                        {
                            //subRow + a - 1,
                            //subCol + b
                            ckl.location[1] - (b - 1) + a - 1,
                            (d + 1) - ckl.location[0] + (a - 1) + b
                        },
                        counter = counter
                    }).ToList();

                counter++;

                allLocations.AddRange(locations);




               //foreach (var ckl in commandknightLocations)
               //{
               //    var row = ckl.location[0];
               //    var col = ckl.location[1];
               //    var subRow = row - (a - 1);
               //    var subCol = col - (b - 1);

                //    var temp = subCol;
                //    subCol = (d + 1) - subRow;
                //    subRow = temp;

                //    knightLocations.Single(k => k.knight == ckl.knight)
                //            .location[0] = subRow + a - 1;
                //    knightLocations.Single(k => k.knight == ckl.knight)
                //            .location[1] = subCol + b;

                //}
            }

            allLocations = allLocations
                .GroupBy(x => x.knight)
                .Select(x => x.OrderByDescending(g => g.counter).First())
                .ToList();
            
            

            var result = allLocations
                .Select(x => x.location).ToArray();
            return result;
        }

        static bool InRange(int row, int col, int a, int b, int d, int n)
        {
            var rowInRange = row >= a && row <= a + d;
            var colInRange = col >= b && col <= b + d;
            var inRange =  rowInRange && colInRange;
            return inRange;
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

                //textWriter.WriteLine(String.Join("\n", result.Select(idx => String.Join(" ", idx))));

                //textWriter.Flush();
                //textWriter.Close();
            }


        }

        private static void RotateMatrix(int[] matrix, int a, int b, int d, ref int[] originalMatrix, int n,
            ref int[] locationMatrix, int l, int[] knights)
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
