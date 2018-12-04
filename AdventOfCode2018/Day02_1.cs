using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    internal class Program
    {
        private static Dictionary<char, int> Count(string stringToCount)
        {
            var characterCount = new Dictionary<char, int>();

            foreach (var character in stringToCount)
            {
                if (!characterCount.ContainsKey(character))
                {
                    characterCount.Add(character, 1);
                }
                else
                {
                    characterCount[character]++;
                }
            }

            return characterCount.OrderByDescending(c => c.Value).ToDictionary(z => z.Key, y => y.Value);
        }

        private static void Main()
        {
            var lines = File.ReadAllLines("input\\Day2.txt");

            var twoLetterCount = 0;
            var threeLetterCount = 0;

            foreach (var line in lines)
            {
                var countData = Count(line);
                twoLetterCount = countData.Count(c => c.Value == 2) > 0 ? twoLetterCount + 1 : twoLetterCount;
                threeLetterCount = countData.Count(c => c.Value == 3) > 0 ? threeLetterCount + 1 : threeLetterCount;
            }

            Console.WriteLine(twoLetterCount * threeLetterCount);
            Console.ReadKey();
        }
    }
}
