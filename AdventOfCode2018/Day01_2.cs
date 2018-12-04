using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2018
{
    internal class Program
    {
        private static void Main()
        {
            var lines = File.ReadAllLines("input\\Day1.txt");

            var result = 0;
            var fUsed = new HashSet<int>() {0};

            do
            {
                foreach (var line in lines)
                {
                    result += int.Parse(line);

                    if (fUsed.Add(result)) continue;

                    Console.WriteLine(result);
                    Console.ReadKey();
                    return;
                }
            } while (true);
        }
    }
}
