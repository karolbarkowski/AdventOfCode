using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    internal enum Actions
    {
        FallsAsleep,
        WakesUp,
        Begins
    }

    internal class Input
    {
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public Actions Activity { get; set; }
    }

    internal class Guard
    {
        public Guard(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public int TotalMinutesAsleep { get; set; }
    }



    internal class Program
    {
        private static void Main()
        {
            var inputs = ReadInput(File.ReadAllLines("input\\Day4.txt"));
            var minutes = new Dictionary<string, int>();    //minute#guardId, totalSleepTime
           
            var currentGuardId = inputs.First().Id.Value;
            for (var i = 0; i < inputs.Count - 1; i++)
            {
                var currentInput = inputs[i];
                currentGuardId = currentInput.Id.HasValue ? currentInput.Id.Value : currentGuardId;

                if (currentInput.Activity != Actions.FallsAsleep) continue;

                for (var j = currentInput.Date.Minute; j < inputs[i + 1].Date.Minute; j++)
                {
                    var key = $"{j}#{currentGuardId}";

                    if (minutes.ContainsKey(key))
                    {
                        minutes[key]++;
                    }
                    else
                    {
                        minutes.Add(key, 1);
                    }
                }
            }

            var maxEntry = minutes.OrderByDescending(m => m.Value).First();

            var data = maxEntry.Key.Split('#');
            var minute = int.Parse(data[0]);
            var id = int.Parse(data[1]);

            var result = id * minute;

            Console.WriteLine($"{result}");
            Console.ReadKey();
        }

        private static List<Input> ReadInput(IEnumerable<string> lines)
        {
            var input = new List<Input>();
            foreach (var line in lines)
            {
                var date = DateTime.Parse(line.Substring(0, 19).Replace("[", "").Replace("]", ""));
                var activity = line.Substring(19, line.Length - 19);

                Actions activityParsed;
                int? id = null;
                switch (activity)
                {
                    case "falls asleep":
                        activityParsed = Actions.FallsAsleep;
                        break;
                    case "wakes up":
                        activityParsed = Actions.WakesUp;
                        break;
                    default:
                        activityParsed = Actions.Begins;
                        id = int.Parse(activity.Split(new[] {' '})[1].Replace("#", ""));
                        break;
                }

                input.Add(new Input() {Id = id, Date = date, Activity = activityParsed});
            }

            return input.OrderBy(i => i.Date).ToList();
        }
    }
}
 