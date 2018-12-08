using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    internal class Program
    {
        private static int[] _data;
        private static int _index;

        private static int ParseNode()
        {
            var childNodesCount = _data[_index++];
            var metaNodesCount = _data[_index++];

            var childValues = new List<int>();
            for (var i = 0; i < childNodesCount; i++)
            {
                childValues.Add(ParseNode());
            }

            var meta = new List<int>();
            for (var i = 0; i < metaNodesCount; i++)
            {
                meta.Add(_data[_index++]);
            }


            if (childNodesCount == 0)
                return meta.Sum();

            var nodeValue = 0;
            foreach (var childNode in meta)
            {
                if (childValues.Count > childNode - 1)
                {
                    nodeValue += childValues[childNode - 1];
                }
            }

            return nodeValue;
        }

        private static void Main()
        {
            var input = File.ReadAllText("input\\Day8.txt");
            _data = Array.ConvertAll(input.Split(' '), int.Parse);

            Console.WriteLine(ParseNode());
            Console.ReadKey();
        }
    }
}
 