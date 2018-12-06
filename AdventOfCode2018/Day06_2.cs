using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    internal class Program
    {
        private const int MAX_DIST = 10000;

        private static void Main()
        {
            var coords = ParseInput(File.ReadAllLines("input\\Day6.txt"));
            var xMin = coords.Min(c => c.Item1);
            var xMax = coords.Max(c => c.Item1);
            var yMin = coords.Min(c => c.Item2);
            var yMax = coords.Max(c => c.Item2);

            var result = 0;

            for (var x = xMin; x <= xMax; x++)
            {
                for (var y = yMin; y <= yMax; y++)
                {
                    var distanceSum = 0;
                    foreach (var cord in coords)
                    {
                       distanceSum += Dist(cord.Item1, cord.Item2, x, y);
                    }

                    if (distanceSum < MAX_DIST)
                        result++;
                }
            }

            Console.WriteLine($"Max area: {result}");
            Console.ReadKey();
        }


        private static List<Tuple<int, int>> ParseInput(string[] lines)
        {
            return lines.Select(l =>
                            new Tuple<int, int>(
                                int.Parse(l.Substring(0, l.IndexOf(",")))
                                , int.Parse(l.Substring(l.IndexOf(",") + 2, l.Length - (l.IndexOf(",") + 2))))
                        ).ToList();
        }

        private static int Dist(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }
    }
}
 