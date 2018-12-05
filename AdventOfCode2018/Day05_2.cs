using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2018
{
    internal class Program
    {
        private static void Main()
        {
            var input = File.ReadAllText("input\\Day5.txt");
            var globalPattern = BuildGlobalPattern();

            var results = new Dictionary<char, int>();
            for (var i = (int)'A'; i <= 'Z'; i++)
            {
                var up = ((char)i).ToString();
                var low = ((char)i).ToString().ToLower();

                var groupPattern = $"[{low}{up}]";
                var inputShortened = Regex.Replace(input, groupPattern, "");

                while (Regex.IsMatch(inputShortened, globalPattern))
                {
                    inputShortened = Regex.Replace(inputShortened, globalPattern, "");
                }

                results.Add((char)i, inputShortened.Length);
            }

            foreach (var r in results)
            {
                Console.WriteLine($"{r.Key} - {r.Value}");
            }
            Console.ReadKey();
        }

        private static string BuildGlobalPattern()
        {
            StringBuilder sbPattern = new StringBuilder();
            for (var i = (int)'A'; i <= 'Z'; i++)
            {
                var up = ((char)i).ToString();
                var low = ((char)i).ToString().ToLower();

                sbPattern.Append("(").Append(up).Append(low).Append("|").Append(low).Append(up).Append(")|");
            }

            return sbPattern.Remove(sbPattern.Length - 1, 1).ToString();
        }
    }
}
 