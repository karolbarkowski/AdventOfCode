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
            var input = File.ReadAllText("input\\Day9.txt");
           // _data = Array.ConvertAll(input.Split(' '), int.Parse);

            Console.WriteLine(ParseNode());
            Console.ReadKey();
        }
    }
}
 