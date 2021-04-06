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
    public static class Greedy
    {
        public enum Algorithm
        {
            FractionalKnapsack,
            SchedulingTasks
        }

        /// <summary>
        /// We are solving the simple knapsack problem (fractional knapsack).
        /// We are given a list of items, together with their volumes and values,
        /// and the knapsack volume.
        /// The algorithm returns the maximum value of items that can be put in 
        /// the knapsack.
        /// </summary>
        public static (double, double[]) FractionalKnapsack(int volumeKS, List<Item> lstItems)
        {
            // Order items by relative value
            lstItems = lstItems.OrderByDescending(x => (double)x.Value / x.Volume).ToList();
            double[] arrX = new double[lstItems.Count];

            // Add elements until knapsack is full
            double currentVolume = 0;
            int i = 0;
            double totalValue = 0;
            while (currentVolume < volumeKS && i < lstItems.Count)
            {
                // Add whole item if possible
                if (currentVolume + lstItems[i].Volume <= volumeKS)
                {
                    arrX[i] = 1;
                    currentVolume += lstItems[i].Volume;
                }
                // Or just its part
                else
                {
                    arrX[i] = (double)(volumeKS - currentVolume) / lstItems[i].Volume;
                    currentVolume += arrX[i] * lstItems[i].Volume;
                }
                totalValue += arrX[i] * lstItems[i].Value;
                i++;
            }
            // Return value and the vector x
            return (totalValue, arrX);
        }

        /// <summary>
        /// We are solving task scheduling, where we want to maximize the number of tasks
        /// in our solution set so that no pair of tasks overlap in time.
        /// The algorithm returns the maximum number of tasks we can select from a given set.
        /// </summary>
        public static int ScheduleTasks(List<Task> lstTasks)
        {
            int numTasks = 0;

            lstTasks = lstTasks.OrderBy(x => x.End).ToList();
            int currentEnd = 0;
            for (int i = 0; i < lstTasks.Count; i++)
            {
                // Take task if feasible
                if (lstTasks[i].Start >= currentEnd)
                {
                    numTasks++;
                    // Increase current finishing time
                    currentEnd = lstTasks[i].End;
                }
            }

            return numTasks;
        }
    }
}
