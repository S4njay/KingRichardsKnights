



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

            var groupedCommands = commands
                .Select(c => new {a = c[0], b = c[1], d = c[2]})
                .GroupBy(x => new
                {
                    x.a, x.b, x.d
                })
                .Select(x => x.First()).ToList();

            foreach (var knight in locations)
            {
                int a = 1;
                int b = 1;
                int d = n - 1;

                var row = knight.location[0];
                var col = knight.location[1];

                foreach (var command in groupedCommands)
                {
                    a = command.a;
                    b = command.b;
                    d = command.d;

                    if (row - (a - 1) < 1 || col - (b - 1) < 1)
                        break;
                    if (row - (a - 1) > d + 1 || col - (b - 1) > d + 1)
                        break;

                    var oldRow = row;
                    row = col - (b - 1) + a - 1;
                    col = (d + 1) - (oldRow - (a - 1)) + b;
                }

                knight.location[0] = row;
                knight.location[1] = col;
            }

            var result = locations
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
    }
}
