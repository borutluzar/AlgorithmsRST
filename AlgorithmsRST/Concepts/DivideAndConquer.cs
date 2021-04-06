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
    public static class DivideAndConquer
    {
        public enum Algorithm
        {
            Bisection,
            NaiveMaxSubsequenceSum,
            MaxSubsequenceSumDC,
            MaxSubsequenceSumLin
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
        /// DOES NOT WORK FOR ALL NEGATIVE NUMBERS
        /// </summary>
        public static int MaxSubsequenceSum_DivAndCon(List<int> lst, int start, int end)
        {
            // Check if we only have one element
            if (start == end)
                return lst[start];

            int half = (start + end) / 2;

            // Check left side from the break
            int leftMax = 0, leftSum = 0;
            for (int i = half; i >= start; i--)
            {
                leftSum += lst[i];
                if (leftMax < leftSum) leftMax = leftSum;
            }

            // Check right side from the break
            int rightMax = 0, rightSum = 0;
            for (int i = half+1; i <= end; i++)
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

        public static int FindLargestIncreasingSubsequence(List<int> lst)
        {
            return 0;
        }
    }
}
