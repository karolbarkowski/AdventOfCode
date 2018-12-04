using System;
using System.IO;

namespace AdventOfCode2018
{
    class Program
    {
        static void Main()
        {
            var lines = File.ReadAllLines("input\\Day1.txt");
            var result = 0;

            foreach (var line in lines)
            {
                result += int.Parse(line);
            }

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
