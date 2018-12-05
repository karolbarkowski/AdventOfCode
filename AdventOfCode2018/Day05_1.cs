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

            var pattern = BuildRegexPattern();

            while (Regex.IsMatch(input, pattern))
            {
                input = Regex.Replace(input, pattern, "");
            }

            Console.WriteLine(input.Length);
            Console.ReadKey();
        }

        private static string BuildRegexPattern()
        {
            StringBuilder sbPattern = new StringBuilder();
            for (var i = (int) 'A'; i <= 'Z'; i++)
            {
                var up = ((char) i).ToString();
                var low = ((char) i).ToString().ToLower();

                sbPattern.Append("(").Append(up).Append(low).Append("|").Append(low).Append(up).Append(")|");
            }

            return sbPattern.Remove(sbPattern.Length - 1, 1).ToString();
        }
    }
}
 