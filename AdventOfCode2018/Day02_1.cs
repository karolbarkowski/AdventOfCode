using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    internal class Program
    {
        private static void Main()
        {
            var lines = File.ReadAllLines("input\\Day2.txt");
            var twoLetterCount = 0;
            var threeLetterCount = 0;

            foreach (var line in lines)
            {
                var group = line.ToCharArray().GroupBy(x => x);
                twoLetterCount += group.Where(y => y.Count() == 2).FirstOrDefault() != null ? 1 : 0;
                threeLetterCount += group.Where(y => y.Count() == 3).FirstOrDefault() != null ? 1 : 0;
            }

            Console.WriteLine(twoLetterCount * threeLetterCount);
            Console.ReadKey();
        }
    }
}
