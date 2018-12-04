using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    class Program
    {
        static void Main()
        {
            var lines = File.ReadAllLines("input\\Day1.txt");
            var result = lines.Select(int.Parse).Sum();

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
