using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borut.Lectures.AlgorithmsRST
{
    /// <summary>
    /// In this class we collect algorithms using branch and bound.
    /// </summary>
    public static class BranchAndBound
    {
        public enum Algorithm
        {
            General,
            BreadthFirstSearch,
            KnapsackProblem
        }

        public static void BreadthFirstSearch(BinaryTree tree)
        {
            var currentNode = tree.Root;
            Queue<BinaryNode> queueNodes = new Queue<BinaryNode>();
            queueNodes.Enqueue(currentNode);

            int countVisited = 0;
            while (queueNodes.Count > 0)
            {
                currentNode = queueNodes.Dequeue();
                Console.WriteLine($"Visiting node #{++countVisited} with value {currentNode.Value}");

                if (currentNode.LeftSon != null)
                    queueNodes.Enqueue(currentNode.LeftSon);
                if (currentNode.RightSon != null)
                    queueNodes.Enqueue(currentNode.RightSon);
            }
        }

        /// <summary>
        /// Solving 0/1 Knapsack problem with branch and bound approach
        /// following the algorithm in 
        /// Foundations of Algorithms using C++ Pseudocode
        /// </summary>
        public static int KnapsackProblem(int volume, List<Item> lstItems)
        {
            // Order items by relative value
            lstItems = lstItems.OrderByDescending(x => (double)x.Value / x.Volume).ToList();

            // Initiate root node
            Item parentItem = new Item() { Value = 0, Volume = 0 };
            BinaryNode parentNode = new BinaryNode(parentItem) { Level = 0 };
            Queue<BinaryNode> queStateTreeNodes = new Queue<BinaryNode>();
            queStateTreeNodes.Enqueue(parentNode);
            int maxValue = 0;

            BinaryNode childNodeTakeItem;
            BinaryNode childNodeSkipItem;
            // Traverse all nodes in queue
            while (queStateTreeNodes.Count > 0)
            {
                parentNode = queStateTreeNodes.Dequeue();
                parentItem = (Item)parentNode.Value;

                // Insert child nodes if possible
                if (parentNode.Level < lstItems.Count)
                {
                    // Child node with next item taken
                    Item itemChildTake = new Item()
                    {
                        Volume = parentItem.Volume + lstItems[parentNode.Level].Volume,
                        Value = parentItem.Value + lstItems[parentNode.Level].Value
                    };
                    childNodeTakeItem = new BinaryNode(itemChildTake) { Level = parentNode.Level + 1 };

                    if (itemChildTake.Volume <= volume && itemChildTake.Value > maxValue)
                        maxValue = itemChildTake.Value;
                    if (Bound(childNodeTakeItem, volume, lstItems) > maxValue)
                        queStateTreeNodes.Enqueue(childNodeTakeItem);

                    // Child node with next item not taken
                    Item itemChildSkip = new Item()
                    {
                        Volume = parentItem.Volume,
                        Value = parentItem.Value
                    };
                    childNodeSkipItem = new BinaryNode(itemChildSkip) { Level = parentNode.Level + 1 };

                    if (Bound(childNodeSkipItem, volume, lstItems) > maxValue)
                        queStateTreeNodes.Enqueue(childNodeSkipItem);
                }
            }
            return maxValue;
        }

        /// <summary>
        /// Auxilliary function for establishing bound in state tree's node
        /// </summary>
        private static double Bound(BinaryNode node, int volume, List<Item> lstItems)
        {
            int j, k;
            int totalVolume;
            double bound;
            Item nodeItem = (Item)node.Value;

            if (nodeItem.Volume >= volume)
                return 0;
            else
            {
                bound = nodeItem.Value;
                j = node.Level + 1;
                totalVolume = nodeItem.Volume;

                // Take as many whole items as possible.
                while (j <= lstItems.Count && totalVolume + lstItems[j - 1].Volume <= volume)
                {
                    totalVolume += lstItems[j - 1].Volume;
                    bound += lstItems[j - 1].Value;
                    j++;
                }
                k = j;
                // Take a fraction of k-th item.
                if (k <= lstItems.Count)
                    bound += (volume - totalVolume) * 
                        ((double)lstItems[k - 1].Value / lstItems[k - 1].Volume);

                return bound;
            }
        }
    }
}
