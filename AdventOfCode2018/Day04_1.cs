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
            minutesBreakdown = new Dictionary<int, int>();
        }

        public int Id { get; set; }
        public int TotalMinutesAsleep { get; set; }
        public Dictionary<int, int> minutesBreakdown { get; set; }
    }

    internal class Program
    {
        private static void Main()
        {
            using (var sr = new StreamReader("input\\Day4.txt"))
            {
                var file = sr.ReadToEnd();
                var lines = file.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

                List<Input> inputs = ReadInput(lines);
                Dictionary<int, Guard> guards = new Dictionary<int, Guard>();
                Guard currentGuard = null;

                for (var i = 0; i < inputs.Count - 1; i++)
                {
                    var currentInput = inputs[i];
                    var nextInput = inputs[i + 1];

                    if (currentInput.Activity == Actions.Begins)
                    {
                        if (guards.ContainsKey(currentInput.Id.Value))
                        {
                            currentGuard = guards[currentInput.Id.Value];
                        }
                        else
                        {
                            currentGuard = new Guard(currentInput.Id.Value);
                            guards.Add(currentGuard.Id, currentGuard);
                        }
                    }



                    if (currentInput.Activity == Actions.FallsAsleep)
                    {
                        TimeSpan sleepTime = nextInput.Date - currentInput.Date;
                        currentGuard.TotalMinutesAsleep += sleepTime.Minutes;

                        for (int j = currentInput.Date.Minute; j < nextInput.Date.Minute; j++)
                        {
                            if (currentGuard.minutesBreakdown.ContainsKey(j))
                            {
                                currentGuard.minutesBreakdown[j]++;
                            }
                            else
                            {
                                currentGuard.minutesBreakdown.Add(j, 1);
                            }
                        }
                    }
                }

                var ordered = guards.OrderByDescending(g => g.Value.TotalMinutesAsleep).ToList();
                var sleepyGuard = ordered.First();
                var id = sleepyGuard.Value.Id;
                var minute = sleepyGuard.Value.minutesBreakdown.OrderByDescending(m => m.Value).First().Key;

                var result = id * minute;
            }
        }

        private static List<Input> ReadInput(string[] lines)
        {
            List<Input> input = new List<Input>();
            foreach (var line in lines)
            {
                DateTime date = DateTime.Parse(line.Substring(0, 19).Replace("[", "").Replace("]", ""));
                string activity = line.Substring(19, line.Length - 19);

                Actions? activityParsed = null;
                int? id = null;
                if (activity == "falls asleep")
                    activityParsed = Actions.FallsAsleep;
                if (activity == "wakes up")
                    activityParsed = Actions.WakesUp;
                if (activity.Contains("begins shift"))
                {
                    activityParsed = Actions.Begins;
                    id = int.Parse(activity.Split(new[] {' '})[1].Replace("#", ""));
                }

                input.Add(new Input() {Id = id, Date = date, Activity = activityParsed.Value});
            }

            input = input.OrderBy(i => i.Date).ToList();
            return input;
        }
    }
}
 