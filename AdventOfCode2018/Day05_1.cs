using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2018
{
    internal class Program
    {
        private static void Main()
        {
            var inputList = new LinkedList<char>(File.ReadAllText("input\\Day5.txt"));

            var currentNode = inputList.First;
            while (currentNode?.Next != null)
            {
                var nextNode = currentNode.Next;
                var refForNextIteration = currentNode.Next;

                if (currentNode.Value != nextNode.Value &&
                    char.ToLower(currentNode.Value) == char.ToLower(nextNode.Value))
                {
                    refForNextIteration = currentNode.Previous ?? nextNode.Next;
                    inputList.Remove(currentNode);
                    inputList.Remove(nextNode);
                }

                currentNode = refForNextIteration;
            }

            Console.WriteLine(inputList.Count);
            Console.ReadKey();
        }
    }
}
 