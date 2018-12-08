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
        public Dictionary<string, Node> Next { get; set; }
        public Dictionary<string, Node> Previous { get; set; }

        public Node(string name)
        {
            Name = name;
            Next = new Dictionary<string, Node>();
            Previous = new Dictionary<string, Node>();
        }
    }

    internal class Program
    {

        private static void Main()
        {
            var lines = File.ReadAllLines("input\\Day7.txt");

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

            //get the first node, the one that doesn't have any previous nodes
            var nodePointer = nodesRef.First(n => n.Value.Previous.Count == 0);
            var availableNodes = new Dictionary<string, Node>(nodePointer.Value.Next);

            var result = new StringBuilder(nodePointer.Key);
            while (result.Length < nodesRef.Count)
            {
                nodePointer = availableNodes.OrderBy(n => n.Value.Name).First();
                result.Append(nodePointer.Value.Name);

                availableNodes.Remove(nodePointer.Key);
                foreach (var n in nodePointer.Value.Next)
                {
                    if (!availableNodes.ContainsKey(n.Key))
                    {
                        availableNodes.Add(n.Key, n.Value);
                    }
                }
            }

      
            Console.ReadKey();
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
 