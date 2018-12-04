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
            var boxesIds = File.ReadAllLines("input\\Day2.txt");
            var result = new List<char>();

            for (var i = 0; i < boxesIds.Length; i++)
            {
                for (var j = i + 1; j < boxesIds.Length; j++)
                {
                    var comparisonResult = boxesIds[i].Where((t, u) => t == boxesIds[j][u]).ToList();
                    if (comparisonResult.Count > result.Count)
                    {
                        result = comparisonResult;
                    }
                }
            }

            Console.WriteLine(new string(result.ToArray()));
            Console.ReadKey();
        }
    }
}
