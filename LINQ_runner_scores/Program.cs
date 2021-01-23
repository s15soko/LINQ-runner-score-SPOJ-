using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_runner_scores
{
    class Program
    {
        public static void Main(string[] args)
        {
            string testCase = Console.ReadLine();
            string data = Console.ReadLine();

            switch (testCase)
            {
                case "test1":
                    Test1(data);
                    break;
                case "test2":
                    Test2(data);
                    break;
                case "test3":
                    Test3(data);
                    break;
                case "test4":
                    Test4(data);
                    break;
                case "test5":
                    Test5(data);
                    break;
            }
        }

        public static void Test1(string data)
        {
            Console.WriteLine(data.Split(",").Count());
        }

        public static void Test2(string data)
        {
            List<int> loopTimes = GetLoopsTimes(data);

            var times = loopTimes.Select(time => $"{((time / 60) % 60):D2}:{(time % 60):D2}");

            Console.WriteLine(String.Join(" ", times.ToArray()));
        }

        public static void Test3(string data)
        {
            var times = GetTimesWithIndex(data);
            var minNumber = times.Min(t => (t.Item1, t.Item2));
            var minPosition = times.Where(t => t.Item1 == minNumber.Item1).Select(t => (t.Item2));

            Console.WriteLine($"{((minNumber.Item1 / 60) % 60):D2}:{(minNumber.Item1 % 60):D2} {String.Join(" ", minPosition)}");
        }

        public static void Test4(string data)
        {
            var times = GetTimesWithIndex(data);
            var maxNumber = times.Max(t => (t.Item1, t.Item2));
            var maxPosition = times.Where(t => t.Item1 == maxNumber.Item1).Select(t => (t.Item2));

            Console.WriteLine($"{((maxNumber.Item1 / 60) % 60):D2}:{(maxNumber.Item1 % 60):D2} {String.Join(" ", maxPosition)}");
        }

        public static void Test5(string data)
        {
            double avg = GetLoopsTimes(data).Average();

            sbyte averageMinutes = (sbyte) Math.Floor(avg / 60);
            sbyte averageSeconds = (sbyte) Math.Ceiling(avg % 60);

            Console.WriteLine($"{averageMinutes:D2}:{averageSeconds:D2}");
        }

        //

        private static List<int> GetLoopsTimes(string data)
        {
            int[] timesInSeconds = GetTimestamps(data);

            List<int> loopTimes = timesInSeconds.Skip(1).Zip(timesInSeconds, (curr, prev) => curr - prev).Prepend(timesInSeconds.First()).ToList();

            return loopTimes;
        }

        private static IEnumerable<Tuple<int, int>> GetTimesWithIndex(string data)
        {
            List<int> loopTimes = GetLoopsTimes(data);

            return loopTimes.Select((t, index) => new Tuple<int, int>(item1: t, item2: index + 1));
        }

        private static int[] GetTimestamps(string data)
        {
            string[] splittedTimes = data.Split(",");

            int[] timesInSeconds = splittedTimes.Select(t => t.Split(":")).Select(t => (int.Parse(t[0]) * 60) + int.Parse(t[1])).ToArray();

            return timesInSeconds;
        }
    }

}
