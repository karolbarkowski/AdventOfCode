using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    internal class Program
    {
        private static void Main()
        {
            var coords = ParseInput(File.ReadAllLines("input\\Day6.txt"));
            var xMin = coords.Min(c => c.Item1);
            var xMax = coords.Max(c => c.Item1);
            var yMin = coords.Min(c => c.Item2);
            var yMax = coords.Max(c => c.Item2);

            var results = new Dictionary<Tuple<int, int>, int>();
            var infiniteOrigins = new HashSet<Tuple<int, int>>();

            for (var x = xMin; x <= xMax; x++)
            {
                for (var y = yMin; y <= yMax; y++)
                {
                    //check if this x,y is not one of existing coordinates
                    if (coords.FirstOrDefault(c => c.Item1 == x && c.Item2 == y) != null) continue;

                    var closestCoords = FindClosestCoords(coords, x, y);

                    //do nothing if there's more than one closest coordinate
                    if (closestCoords == null || closestCoords.Count != 1) continue;

                    var cord = closestCoords.First();
                    if (results.ContainsKey(cord))
                    {
                        results[cord]++;
                    }
                    else
                    {
                        results.Add(closestCoords.First(), 1);
                    }

                    //if we're on the edge, mark current origin point as the one that exceeds to infinity
                    if (x == xMin || x == xMax || y == yMin || y == yMax)
                    {
                        infiniteOrigins.Add(cord);
                    }
                }
            }

            //get rid of all infinite areas
            foreach (var inf in infiniteOrigins)
            {
                results.Remove(inf);
            }

            var biggestArea = results
                .Where(r => r.Key.Item1 != xMin)
                .Where(r => r.Key.Item1 != xMax)
                .Where(r => r.Key.Item2 != yMin)
                .Where(r => r.Key.Item2 != yMax)
                .Max(r => r.Value);

            Console.WriteLine($"Max area: {biggestArea + 1}");  //ass one as we also need to count the point itself
            Console.ReadKey();
        }

        private static List<Tuple<int, int>> FindClosestCoords(List<Tuple<int, int>> coords, int x, int y)
        {
            var closestCoords = new List<Tuple<int, int>>();
            var minDistance = int.MaxValue;

            foreach (var cord in coords)
            {
                var cordDistance = Dist(cord.Item1, cord.Item2, x, y);
                if (cordDistance > 0 && cordDistance == minDistance)
                {
                    minDistance = cordDistance;
                    closestCoords.Add(cord);
                }
                else if (cordDistance > 0 && cordDistance < minDistance)
                {
                    minDistance = cordDistance;
                    closestCoords = new List<Tuple<int, int>> {cord};
                }
            }

            return closestCoords;
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
 