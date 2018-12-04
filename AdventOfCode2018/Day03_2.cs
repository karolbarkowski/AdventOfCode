using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    internal class Claim
    {
        public int Id { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Overlaps { get; set; }

        public override string ToString()
        {
            return $"#{Id} - Overlaps: {Overlaps}";
        }
    }

    internal class CanvasItem
    {
        public List<Claim> Claims { get; set; }
        public int Occurencies { get; set; }

        public CanvasItem()
        {
            Claims = new List<Claim>();
        }
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
                    string[] claimData = line.Split(new[] {'@'}, StringSplitOptions.RemoveEmptyEntries);
                    string id = claimData[0].Replace("#", "");
                    string[] claimDataValues = claimData[1].Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                    string[] position = claimDataValues[0].Split((new[] {','}), StringSplitOptions.RemoveEmptyEntries);
                    string[] size = claimDataValues[1].Split((new[] {'x'}), StringSplitOptions.RemoveEmptyEntries);
                    claims.Add(new Claim()
                    {
                        Id = int.Parse(id),
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
                CanvasItem[,] canvas = new CanvasItem[maxCanvasWidth,maxCanvasHeight];
                foreach (var claim in claims)
                {
                    for (var x = claim.Left; x < claim.Left + claim.Width; x++)
                    {
                        for (var y = claim.Top; y < claim.Top + claim.Height; y++)
                        {
                            if (canvas[x, y] == null)
                            {
                                canvas[x, y] = new CanvasItem();
                            }

                            if (canvas[x, y].Occurencies > 0)
                            {
                                foreach (var prevClaim in canvas[x, y].Claims)
                                {
                                    if (prevClaim != claim) prevClaim.Overlaps++;
                                }

                                claim.Overlaps++;
                            }

                            canvas[x, y].Occurencies++;
                            canvas[x,y].Claims.Add(claim);
                        }
                    }
                }

                var nonOverlappingClaims = claims.Where(c => c.Overlaps == 0).ToList();
            }
        }
    }
}
