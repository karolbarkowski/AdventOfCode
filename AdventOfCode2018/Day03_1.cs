using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    internal class Claim
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    internal class Program
    {
        private static void Main()
        {
            using (var sr = new StreamReader("input\\Day3.txt"))
            {
                var file = sr.ReadToEnd();
                var lines = file.Split(new []{ Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                //parse all claims
                List<Claim> claims = new List<Claim>();
                foreach (var line in lines)
                {
                    var claimData = line.Split(new[] {'@'}, StringSplitOptions.RemoveEmptyEntries)[1];
                    string[] claimDataValues = claimData.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                    string[] position = claimDataValues[0].Split((new[] {','}), StringSplitOptions.RemoveEmptyEntries);
                    string[] size = claimDataValues[1].Split((new[] {'x'}), StringSplitOptions.RemoveEmptyEntries);
                    claims.Add(new Claim()
                    {
                        Height =  int.Parse(size[1]),
                        Width = int.Parse(size[0]),
                        Left = int.Parse(position[0]),
                        Top = int.Parse(position[1])
                    });
                }

                //count maximum canvas size based on claims data
                int maxCanvasWidth = claims.Max(c => c.Width + c.Left);
                int maxCanvasHeight = claims.Max(c => c.Top + c.Height);

                //mark all claim positions and check how many positions are overlapping
                int[,] canvas = new int[maxCanvasWidth,maxCanvasHeight];
                int counter = 0;
                foreach (var claim in claims)
                {
                    for (var x = claim.Left; x < claim.Left + claim.Width; x++)
                    {
                        for (var y = claim.Top; y < claim.Top + claim.Height; y++)
                        {
                            if (canvas[x, y] == 1)
                            {
                                counter++;
                            }

                            canvas[x, y]++;
                        }
                    }
                }

                Console.WriteLine(counter);
                Console.ReadLine();
            }
        }
    }
}
