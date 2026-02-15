using System;
using System.Collections.Generic;
using System.Linq;

namespace AttendanceAggregator
{
    class Interval
    {
        public long Start;
        public long End;

        public Interval(long start, long end)
        {
            Start = start;
            End = end;
        }
    }

    class AttendanceAggregator
    {
        static void Run()
        {
            // Sample Data (UTC milliseconds)
            var events = new List<(string DelegateId, long Start, long End)>
            {
                ("D1", 1700000000000, 1700001800000), // 30 min
                ("D1", 1700000900000, 1700002700000), // overlaps

                ("D2", 1700000000000, 1700000600000), // 10 min
                ("D2", 1700001200000, 1700001800000)  // 10 min
            };

            var grouped = events.GroupBy(e => e.DelegateId);

            foreach (var group in grouped)
            {
                long minutes = CalculateUniqueMinutes(group.ToList());

                Console.WriteLine($"Delegate {group.Key}");
                Console.WriteLine($"Unique Minutes: {minutes}");
                Console.WriteLine();
            }
        }

        // Core Logic
        static long CalculateUniqueMinutes(List<(string Id, long Start, long End)> events)
        {
            var intervals = new List<Interval>();

            // Convert to intervals
            foreach (var e in events)
            {
                intervals.Add(new Interval(e.Start, e.End));
            }

            // Sort by start time
            intervals = intervals
                .OrderBy(i => i.Start)
                .ToList();

            long total = 0;

            long curStart = intervals[0].Start;
            long curEnd = intervals[0].End;

            for (int i = 1; i < intervals.Count; i++)
            {
                var next = intervals[i];

                // Overlap
                if (next.Start <= curEnd)
                {
                    curEnd = Math.Max(curEnd, next.End);
                }
                else
                {
                    // No overlap → close interval
                    total += (curEnd - curStart);

                    curStart = next.Start;
                    curEnd = next.End;
                }
            }

            // Add last
            total += (curEnd - curStart);

            // Convert ms → minutes
            return total / (1000 * 60);
        }
    }
}
