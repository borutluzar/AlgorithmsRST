using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borut.Lectures.AlgorithmsRST
{
    /// <summary>
    /// In this class we collect algorithms using divide and conquer.
    /// </summary>
    public static class DivideAndConquer
    {
        public enum Algorithm
        {
            Bisection,
            NaiveMaxSubsequenceSum,
            MaxSubsequenceSumDC,
            MaxSubsequenceSumLin,
            MaxSubsequenceCompareTimes,
            LargestIncreasingSubsequence,
            LargestIncreasingSubsequence_DivAndCon,
            CountingInversionsExhaustive,
            CountingInversions_DivAndCon
        }

        /// <summary>
        /// Given an ordered list and an element e, 
        /// it determines if the list contains the given element.
        /// </summary>
        public static bool FindElementInList(int elt, SortedSet<int> orderedSet, int start, int end)
        {
            // We divide only if there are at least two elements in the set
            if (start < end)
            {
                int halfIndex = (start + end) / 2;

                // We check only one half of the remaining elements
                if (elt <= orderedSet.ElementAt(halfIndex))
                    return FindElementInList(elt, orderedSet, start, halfIndex);
                else
                    return FindElementInList(elt, orderedSet, halfIndex + 1, end);
            }
            // If the element exists return true
            else if (orderedSet.ElementAt(start) == elt)
                return true;
            // Otherwise false
            return false;
        }


        /// <summary>
        /// Given a list of integers, it finds the maximum sum of contiguous elements
        /// in O(n^2) time.
        /// </summary>
        public static int NaiveMaxSubsequenceSum(List<int> lst)
        {
            int max = int.MinValue;
            for (int i = 0; i < lst.Count; i++)
            {
                int sumI = 0;
                for (int j = i; j < lst.Count; j++)
                {
                    sumI += lst[j];
                    if (sumI > max)
                        max = sumI;
                }
            }
            return max;
        }


        /// <summary>
        /// Given a list of integers, it finds the maximum sum of contiguous elements
        /// in O(n log(n)) time using divide and conquer approach.
        /// </summary>
        public static int MaxSubsequenceSum_DivAndCon(List<int> lst, int start, int end)
        {
            // Check if we only have one element
            if (start == end)
                return lst[start];

            int half = (start + end) / 2;

            // Check left side from the break
            int leftMax = int.MinValue, leftSum = 0;
            for (int i = half; i >= start; i--)
            {
                leftSum += lst[i];
                if (leftMax < leftSum)
                {
                    leftMax = leftSum;
                }
            }

            // Check right side from the break
            int rightMax = int.MinValue, rightSum = 0;
            for (int i = half + 1; i <= end; i++)
            {
                rightSum += lst[i];
                if (rightMax < rightSum) rightMax = rightSum;
            }

            return new List<int>()
                {
                    leftMax + rightMax,
                    MaxSubsequenceSum_DivAndCon(lst, start, half),
                    MaxSubsequenceSum_DivAndCon(lst, half + 1, end)
                }.Max();
        }


        /// <summary>
        /// Given a list of integers, it finds the maximum sum of contiguous elements
        /// in linear time.
        /// DOES NOT WORK FOR ALL NEGATIVE VALUES!
        /// </summary>
        public static int MaxSubsequenceSumLinear(List<int> lst)
        {
            int max = int.MinValue;
            int currentMax = int.MinValue;
            for (int i = 0; i < lst.Count; i++)
            {                
                currentMax = Math.Max(currentMax > int.MinValue ? currentMax + lst[i] : lst[i], int.MinValue);
                max = Math.Max(max, currentMax);
            }
            return max;
        }

        /// <summary>
        /// Given a list of integers, it finds the maximum sum of contiguous elements
        /// in linear time.
        /// </summary>
        public static int MaxSubsequenceSumLinearWorkingForAllNegative(List<int> lst)
        {
            int max = int.MinValue;
            int currentMax = 0;
            bool allNegativeValues = true;
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i] >= 0)
                    allNegativeValues = false;

                currentMax = Math.Max(currentMax + lst[i], 0);
                max = Math.Max(max, currentMax);
            }

            if (allNegativeValues)
                return lst.Max();

            return max;
        }

        public static int LargestIncreasingSubsequence(List<int> lst)
        {
            int max = 1; // Always at least one
            int currentMax = 1;
            for (int i = 1; i < lst.Count; i++)
            {
                currentMax = lst[i] >= lst[i - 1] ? currentMax + 1 : 1;
                max = Math.Max(max, currentMax);
            }
            return max;
        }

        public static int LargestIncreasingSubsequence_DivAndCon(List<int> lst, int start, int end)
        {
            // Check if we only have one element
            if (start == end)
                return 1;

            int half = (start + end) / 2;

            // Check left side from the break
            int leftMax = 1;
            for (int i = half - 1; i >= start; i--)
            {
                if (lst[i] < lst[i + 1]) leftMax++;
                else break;
            }

            // Check right side from the break
            int rightMax = 0;
            for (int i = half + 1; i <= end; i++)
            {
                if (lst[i] > lst[i - 1]) rightMax++;
                else break;
            }

            return new List<int>()
                {
                    leftMax + rightMax,
                    LargestIncreasingSubsequence_DivAndCon(lst, start, half),
                    LargestIncreasingSubsequence_DivAndCon(lst, half + 1, end)
                }.Max();
        }

        /// <summary>
        /// For a given list {a_i} counts the number of elements 
        /// with i &lt; j and a_i &gt; a_j.
        /// </summary>
        public static int CountInversionsExhaustive(List<int> sequence, out List<(int, int)> inversions)
        {
            int countInversions = 0;
            inversions = new List<(int, int)>();
            for (int i = 0; i < sequence.Count; i++)
            {
                for (int j = i + 1; j < sequence.Count; j++)
                {
                    if (sequence[i] > sequence[j])
                    {
                        countInversions++;
                        inversions.Add((sequence[i], sequence[j]));
                    }
                }
            }
            return countInversions;
        }

        public static (List<int> OrderedList, int Inversions) CountInversionsDivideAndConquer(List<int> sequence)
        {
            // Handle the trivial cases
            if (sequence.Count <= 1)
                return (sequence, 0);

            int half = sequence.Count / 2;
            var lstLeft = sequence.GetRange(0, half);
            var lstRight = sequence.GetRange(half, sequence.Count - half);
            // Recursive calls on the left and right part
            (var lstLeftOrd, var numInvLeft) = CountInversionsDivideAndConquer(lstLeft);
            (var lstRightOrd, var numInvRight) = CountInversionsDivideAndConquer(lstRight);

            // Counting split inversions
            (var lstOrdered, var numSplitInv) = MergeAndCountSplitInversions(lstLeftOrd, lstRightOrd);
            return (lstOrdered, numInvLeft + numInvRight + numSplitInv);
        }

        private static (List<int> OrderedList, int Inversions) MergeAndCountSplitInversions(List<int> seqLeft, List<int> seqRight)
        {
            int lft = 0,
                rgt = 0;
            int splitInversions = 0;
            List<int> lstOrdered = new List<int>();

            bool lftExhausted = false;
            bool rgtExhausted = false;
            for (int i = 0; i < seqLeft.Count + seqRight.Count; i++)
            {
                if (!lftExhausted &&  (rgtExhausted || seqLeft[lft] < seqRight[rgt]))
                {
                    lstOrdered.Add(seqLeft[lft]);
                    lft++;
                }
                else
                {
                    lstOrdered.Add(seqRight[rgt]);
                    rgt++;
                    splitInversions += seqLeft.Count - lft;
                }

                if (lft == seqLeft.Count)
                    lftExhausted = true;
                if (rgt == seqRight.Count)
                    rgtExhausted = true;
            }

            return (lstOrdered, splitInversions);
        }
    }
}
