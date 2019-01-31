using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{

    internal class Node
    {
        public string Name { get; set; }
        public int TimeNeeded { get; set; }
        public Dictionary<string, Node> Next { get; set; }
        public Dictionary<string, Node> Previous { get; set; }

        public Node(string name)
        {
            Name = name;
            Next = new Dictionary<string, Node>();
            Previous = new Dictionary<string, Node>();
            TimeNeeded = (char) name.First() - 64;
        }
    }

    internal class Program
    {
        private static void Main()
        {
            var nodesRef = ParseInput(File.ReadAllLines("input\\Day7.txt"));
            var resultLength = nodesRef.Count;

            //get the first node, the one that doesn't have any previous nodes
            var availableNodes = nodesRef.Where(n => n.Value.Previous.Count == 0).OrderBy(n => n.Key).ToDictionary(n => n.Key, n=>  n.Value);
            const int workers = 2;

            for (var i = 0; i < workers; i++)
            {
                if (availableNodes.Count == 0)
                    break;

                availableNodes[i].TimeNeeded--;

            }
            
        //    var result = new StringBuilder(nodePointer.Key);

            //RemoveNode(nodePointer, nodesRef);

            //while (result.Length < resultLength)
            //{
            //    nodePointer = availableNodes.OrderBy(n => n.Value.Name).First();
            //    result.Append(nodePointer.Value.Name);

            //    availableNodes.Remove(nodePointer.Key);
            //    RemoveNode(nodePointer, nodesRef);

            //    var newAvailable = nodePointer.Value.Next.Where(n => n.Value.Previous.Count == 0).ToList();
            //    foreach (var n in newAvailable)
            //    {
            //        if (!availableNodes.ContainsKey(n.Key))
            //        {
            //            availableNodes.Add(n.Key, n.Value);
            //        }
            //    }
            //}

           //Console.WriteLine(result);
            Console.ReadKey();
        }

        private static void RemoveNode(KeyValuePair<string, Node> nodePointer, Dictionary<string, Node> nodesRef)
        {
            foreach (var nextNode in nodePointer.Value.Next)
            {
                if (nextNode.Value.Previous.ContainsKey(nodePointer.Key))
                    nextNode.Value.Previous.Remove(nodePointer.Key);
            }

            nodesRef.Remove(nodePointer.Key);
        }

        private static Dictionary<string, Node> ParseInput(string[] lines)
        {
            var nodesRef = new Dictionary<string, Node>();
            foreach (var line in lines)
            {
                var prevNodeName = line.Substring(5, 1);
                var nextNodeName = line.Substring(36, 1);

                var prevNode = GetNode(nodesRef, prevNodeName);
                var nextNode = GetNode(nodesRef, nextNodeName);

                if (!prevNode.Next.ContainsKey(nextNodeName))
                {
                    prevNode.Next.Add(nextNode.Name, nextNode);
                }

                if (!nextNode.Previous.ContainsKey(prevNode.Name))
                {
                    nextNode.Previous.Add(prevNode.Name, prevNode);
                }
            }

            return nodesRef;
        }

        private static Node GetNode(IDictionary<string, Node> nodesRef, string nodeName)
        {
            Node node;
            if (!nodesRef.ContainsKey(nodeName))
            {
                node = new Node(nodeName);
                nodesRef.Add(nodeName, node);
            }
            else
            {
                node = nodesRef[nodeName];
            }

            return node;
        }
    }
}
 