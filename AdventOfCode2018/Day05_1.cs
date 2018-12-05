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

                if (currentNode.Value != nextNode.Value && char.ToLower(currentNode.Value) == char.ToLower(nextNode.Value))
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

            Console.WriteLine(inputList.Count);
            Console.ReadKey();
        }
    }
}
 