using System;
using System.IO;

namespace AdventOfCode2018
{
    internal class Program
    {
        private static int[] _data;
        private static int _index;
        private static int _sum;

        private static void ParseNode()
        {
            var childNodes = _data[_index++];
            var metaNodes = _data[_index++];

            for (var i = 0; i < childNodes; i++)
            {
                ParseNode();
            }

            for (var i = 0; i < metaNodes; i++)
            {
                _sum += _data[_index++];
            }
        }

        private static void Main()
        {
            var input = File.ReadAllText("input\\Day8.txt");
            _data = Array.ConvertAll(input.Split(' '), int.Parse);

            ParseNode();

            Console.WriteLine(_sum);
            Console.ReadKey();
        }
    }
}
 