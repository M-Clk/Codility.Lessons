using System;
using System.Linq;
using System.Collections.Generic;

namespace BinaryGap
{
class Program
    {
        /// <summary>
        /// Test values. [Key: Test value], [Value: Expected result].
        /// </summary>
        static readonly Dictionary<int, int> testValueResultPairs = new Dictionary<int, int>{
            {1041, 5},
            {15, 0},
            {32, 0},
            {1, 0},
            {5, 1},
            {2147483647, 0},
            {1376796946, 5},
            {1073741825, 29},
            {51712, 2},
            {19, 2}
        };
        static void Main(string[] args)
        {
            if (Solve())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("All tests PASSED.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Some tests FAILED.");
            }
            Console.ResetColor();
        }
        /// <summary>
        /// Solving the Binary Gap task via best method.
        /// </summary>
        /// <returns>Returns true if all test cases are passed, otherwise false.</returns>
        static bool Solve()
        {
            var isCorrect = true;
            foreach (var pair in testValueResultPairs)
            {
                var result = Solution1(pair.Key);
                var resultString = $"{Convert.ToString(pair.Key,2)} => solution({pair.Key}) = {pair.Value}.";
                if (result == pair.Value)
                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{resultString} PASSED.");
                }
                else
                {
                    isCorrect = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{resultString} FAILED. (Expected {result})");
                }
            }
            return isCorrect;
        }
        /// <summary>
        /// Solves the problem in a single iteration using mathematical operations. Longest and fastest solution.
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        static int Solution1(int N)
        {
            if (N <= 4)
                return 0;
            var count = 0;
            var maxCount = 0;
            var isStarted = false;
            var lastResult = N;
            while (lastResult > 0)
            {
                if (lastResult % 2 == 0)
                {
                    if (isStarted)
                        count++;
                }
                else
                {
                    if (isStarted && count > 0)
                    {
                        isStarted = false;
                        if (count > maxCount)
                            maxCount = count;
                        count = 0;
                    }
                    isStarted = true;
                }
                lastResult /= 2;
            }
            return maxCount;
        }
        /// <summary>
        /// Solves the problem after converting value from integer to string.
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        static int solution2(int N)
        {
            if (N <= 0)
                return 0;
            var binary = Convert.ToString(N, 2);
            var count = 0;
            var maxCount = 0;
            var isGapStarted = false;
            foreach (var item in binary)
            {
                if (item == '0')
                {
                    if (isGapStarted)
                        count++;
                }
                else
                {
                    if (isGapStarted && count > 0)
                    {
                        isGapStarted = false;
                        maxCount = count;
                        count = 0;
                    }
                    else
                        isGapStarted = true;
                }
            }
            return maxCount;
        }
        /// <summary>
        /// Solves the problem by other methods. Shortest and slowest solution.
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        static int solution3(int N)
        {
            if (N <= 0)
                return 0;
            var binary = Convert.ToString(N, 2);
            var gaps = binary.Split('1');
            var max = gaps.Max(g => g.Length);
            return max;
        }
    }
}
