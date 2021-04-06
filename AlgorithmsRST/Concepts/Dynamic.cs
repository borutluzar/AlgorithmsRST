using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borut.Lectures.AlgorithmsRST
{
    /// <summary>
    /// In this class we collect algorithms using greedy approach.
    /// </summary>
    public static class Dynamic
    {
        public enum Algorithm
        {
            FibonacciRec,
            FibonacciList,
            FibonacciDirect,
            KnapsackRec,
            KnapsackRecVec,
            KnapsackDyn,
            ShuffleString
        }

        /// <summary>
        /// We computing k-th Fibonacci number using recursion.
        /// </summary>
        public static long FibonacciRec(int k)
        {
            // For small k, we output the result directly
            if (k == 1 || k == 2)
                return 1;
            // Otherwise make recursive call
            return FibonacciRec(k - 2) + FibonacciRec(k - 1);
        }

        public static long FibonacciList(int k)
        {
            // For small k, we output the result directly
            if (k == 1 || k == 2)
                return 1;

            // Otherwise compute all values up to k and store them
            List<long> lstVals = new List<long>() { 1, 1 };
            for (int i = 2; i < k; i++)
            {
                long nextVal = lstVals[i - 2] + lstVals[i - 1];
                lstVals.Add(nextVal);
            }
            return lstVals[k - 1]; // Zero based list
        }

        public static long FibonacciDirect(int k)
        {
            // For small k, we output the result directly
            if (k == 1 || k == 2)
                return 1;

            // Otherwise compute all values up to k subsequently
            long fib_minus2 = 1;
            long fib_minus1 = 1;
            long current = -1;
            for (int i = 2; i < k; i++)
            {
                current = fib_minus2 + fib_minus1;
                fib_minus2 = fib_minus1;
                fib_minus1 = current;
            }
            return current;
        }

        /// <summary>
        /// We are solving the 0/1 knapsack problem without storing values
        /// of recursive calls.
        /// </summary>
        public static int KnapsackProblemRec(int volume, List<Item> lstItems, int maxIndex)
        {
            // Handle boundary cases
            // Negative volume means that the items do not fit
            if (volume < 0)
            {
                return int.MinValue;
            }
            // If one item, there is no recursion
            if (maxIndex == 0)
            {
                if (lstItems[0].Volume <= volume)
                    return lstItems[0].Value;
                else
                    return 0;
            }

            // Otherwise make recursive call
            return Math.Max(
              KnapsackProblemRec(volume, lstItems, maxIndex - 1),
              KnapsackProblemRec(volume - lstItems[maxIndex].Volume, lstItems, maxIndex - 1) + lstItems[maxIndex].Value);
        }

        /// <summary>
        /// We are solving the 0/1 knapsack problem without storing values
        /// of recursive calls.
        /// The vector x is also output.
        /// </summary>
        public static (int, int[]) KnapsackProblemRecVec(int volume, List<Item> lstItems, int maxIndex, int[] x)
        {
            // Handle boundary cases
            int[] tmpX = x.Clone() as int[];
            // Negative volume means that the items do not fit
            if (volume < 0)
            {
                return (int.MinValue, tmpX);
            }
            // If one item, there is no recursion
            if (maxIndex == 0)
            {
                if (lstItems[0].Volume <= volume)
                {
                    tmpX[0] = 1;
                    return (lstItems[0].Value, tmpX);
                }
                else
                {
                    tmpX[0] = 0;
                    return (0, tmpX);
                }
            }

            // Otherwise make recursive call
            (int result1, int[] x1) = KnapsackProblemRecVec(volume, lstItems, maxIndex - 1, tmpX);
            (int result2, int[] x2) = KnapsackProblemRecVec(volume - lstItems[maxIndex].Volume, lstItems, maxIndex - 1, tmpX);
            result2 += lstItems[maxIndex].Value;

            if (result1 >= result2)
            {
                x1[maxIndex] = 0;
                return (result1, x1);
            }
            else
            {
                x2[maxIndex] = 1;
                return (result2, x2);
            }
        }

        /// <summary>
        /// We are solving the 0/1 knapsack problem with dynamic programming by storing values
        /// of recursive calls.
        /// </summary>
        public static int KnapsackProblemDyn(int volume, List<Item> lstItems, int maxIndex, Dictionary<(int MaxIndex, int Volume), int> dicStore)
        {
            // Handle boundary cases
            // Negative volume means that the items do not fit
            if (volume < 0)
            {
                return int.MinValue;
            }
            // If one item, there is no recursion
            if (maxIndex == 0)
            {
                if (dicStore.ContainsKey((MaxIndex: maxIndex, Volume: volume)))
                    return dicStore[(MaxIndex: maxIndex, Volume: volume)];
                else
                {
                    if (lstItems[0].Volume <= volume)
                    {
                        dicStore.Add((MaxIndex: maxIndex, Volume: volume), lstItems[0].Value);
                        return lstItems[0].Value;
                    }
                    else
                    {
                        dicStore.Add((MaxIndex: maxIndex, Volume: volume), 0);
                        return 0;
                    }
                }
            }

            // Otherwise make recursive calls and store results
            if (!dicStore.ContainsKey((MaxIndex: maxIndex - 1, Volume: volume)))
                KnapsackProblemDyn(volume, lstItems, maxIndex - 1, dicStore);
            int result1 = dicStore[(MaxIndex: maxIndex - 1, Volume: volume)];

            if (volume - lstItems[maxIndex].Volume < 0)
            {
                dicStore.Add((MaxIndex: maxIndex, Volume: volume), result1);
                return result1;
            }
            else if (!dicStore.ContainsKey((MaxIndex: maxIndex - 1, Volume: volume - lstItems[maxIndex].Volume)))
                KnapsackProblemDyn(volume - lstItems[maxIndex].Volume, lstItems, maxIndex - 1, dicStore);
            int result2 = dicStore[(MaxIndex: maxIndex - 1, Volume: volume - lstItems[maxIndex].Volume)] + lstItems[maxIndex].Value;

            int result = Math.Max(result1, result2);
            dicStore.Add((MaxIndex: maxIndex, Volume: volume), result);
            return result;
        }


        /// <summary>
        /// Given three strings, it recursively decides whether the third is
        /// ordered combination of the former two.
        /// BANANA, ANANAS => BAANNAANNAAS
        /// </summary>
        public static bool ShuffleStringRec(string a, string b, string c, int indA, int indB)
        {
            if (a.Length + b.Length > c.Length) throw new Exception("Incorrect input");

            // Boundary cases
            if (indA == -1)
                return c.Substring(0, indB + 1) == b.Substring(0, indB + 1);
            if (indB == -1)
                return c.Substring(0, indA + 1) == a.Substring(0, indA + 1);

            // Bellman's equation
            return
                ShuffleStringRec(a, b, c, indA - 1, indB) && a[indA] == c[indA + indB + 1]
                ||
                ShuffleStringRec(a, b, c, indA, indB - 1) && b[indB] == c[indA + indB + 1];
        }

        /// <summary>
        /// Given three strings, it greedily decides whether the third is
        /// ordered combination of the former two.
        /// BANANA, ANANAS => BAANNAANNAAS
        /// </summary>
        public static bool ShuffleStringDyn(string a, string b, string c)
        {
            if (a.Length + b.Length > c.Length) throw new Exception("Incorrect input");

            if (a.Length == 0 || b.Length == 0)
            {
                if (b == c || a == c) return true;
                else return false;
            }

            int indA = 0;
            int indB = 0;
            for (int i = 0; i < c.Length; i++)
            {
                if (indA < a.Length && c[i] == a[indA])
                    indA++;
                else if (indB < b.Length && c[i] == b[indB])
                    indB++;
                else
                    return false;
            }
            return true;
        }
    }
}
