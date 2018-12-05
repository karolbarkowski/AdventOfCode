using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{
    internal class Program
    {
        private static void Main()
        {
            var results = new Dictionary<char, int>();
            for (var i = (int) 'A'; i <= 'Z'; i++)
            {
                var low = char.ToLower((char) i).ToString();
                var up = ((char) i).ToString();

                var inputList = CollapsePolymer(low, up);

                results.Add((char) i, inputList.Count);
            }

            foreach (var r in results)
            {
                Console.WriteLine($"{r.Key} - {r.Value}");
            }

            Console.WriteLine($"Minimum: {results.Min(r => r.Value)}");
            Console.ReadKey();
        }

        private static LinkedList<char> CollapsePolymer(string charLow, string charUp)
        {
            StringBuilder input = new StringBuilder(File.ReadAllText("input\\Day5.txt"));
            input = input.Replace(charLow, "").Replace(charUp, "");
            var inputList = new LinkedList<char>(input.ToString());

            var currentNode = inputList.First;
            while (currentNode?.Next != null)
            {
                var nextNode = currentNode.Next;

                if (currentNode.Value != nextNode.Value &&
                    char.ToLower(currentNode.Value) == char.ToLower(nextNode.Value))
                {
                    if (currentNode.Previous != null)
                    {
                        var previousRef = currentNode.Previous;
                        inputList.Remove(currentNode);
                        inputList.Remove(nextNode);

                        currentNode = previousRef;
                    }
                    else if (nextNode.Next != null)
                    {
                        var nextRef = nextNode.Next;

                        inputList.Remove(currentNode);
                        inputList.Remove(nextNode);

                        currentNode = nextRef;
                    }
                    else
                    {
                        inputList.Remove(currentNode);
                        inputList.Remove(nextNode);
                    }
                }
                else
                {
                    currentNode = currentNode.Next;
                }
            }

            return inputList;
        }
    }
}
 