using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    internal class Box
    {
        public Box(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
        public List<char> ClosestBoxSimilarLetters { get; set; } = new List<char>();
    }

    internal class DiffResult
    {
        public int DiffCharactersCount { get; set; }
        public List<char> SimilarChars { get; set; } = new List<char>();
    }

    internal class Program
    {
        private static DiffResult CountDiff(string s1, string s2)
        {
            if (s1.Length != s2.Length)
                throw new ArgumentException();

            var result = new DiffResult();

            for (var i = 0; i < s1.Length; i++)
            {
                if (s1[i] != s2[i])
                {
                    result.DiffCharactersCount++;
                }
                else
                {
                    result.SimilarChars.Add(s1[i]);
                }
            }

            return result;
        }

        private static void Main()
        {
            var boxes = File.ReadAllLines("input\\Day2.txt")
                .Select(l => new Box(l))
                .ToList();

            foreach (var box in boxes)
            {
                var leastCharactersDiff = box.Id.Length;

                foreach (var boxToCompare in boxes)
                {
                    if (boxToCompare == box) continue;

                    var comparisonResult = CountDiff(box.Id, boxToCompare.Id);
                    if (comparisonResult.DiffCharactersCount < leastCharactersDiff)
                    {
                        box.ClosestBoxSimilarLetters = comparisonResult.SimilarChars;
                        leastCharactersDiff = comparisonResult.DiffCharactersCount;
                    }
                }
            }

            boxes = boxes.OrderByDescending(b => b.ClosestBoxSimilarLetters.Count).ToList();
            var result = new string(boxes.First().ClosestBoxSimilarLetters.ToArray());

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
