using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Channels;

namespace Borut.Lectures.AlgorithmsRST
{
    /// <summary>
    /// In this class we collect algorithms using backtracking.
    /// </summary>
    public static class Backtracking
    {
        public enum Algorithm
        {
            DFS,
            NQueens,
            NKnights,
            Exercise1,
            Knapsack,
            Opinion,
            SubsetSum,
            SubsetSumLargeScale,
            CommonSubsequence
        }

        /// <summary>
        /// Non-recursive implementation of depth first search with a stack.
        /// </summary>
        public static void DepthFirstSearch(BinaryTree tree)
        {
            var currentNode = tree.Root;
            Stack<BinaryNode> stackNodes = new Stack<BinaryNode>();
            stackNodes.Push(currentNode);

            int countVisited = 0;
            while (stackNodes.Count > 0)
            {
                currentNode = stackNodes.Pop();
                Console.WriteLine($"Visiting node #{++countVisited} with value {currentNode.Value}");

                // We start with right son since stack...
                if (currentNode.RightSon != null)
                    stackNodes.Push(currentNode.RightSon);
                if (currentNode.LeftSon != null)
                    stackNodes.Push(currentNode.LeftSon);
            }
        }


        /// <summary>
        /// We are solving task n queens problem, i.e., 
        /// counting how many configurations of n queens can we set in an n x n chessboard
        /// without queens attacking each other.
        /// </summary>
        public static List<int> NQueensOnePosition(int n, List<int> solution)
        {
            List<int> position = new List<int>();

            // When we arrive at a solution, we send info back up
            if (solution.Count == n)
                return new List<int>(solution) { };

            // Otherwise try to place k-th queen in the row
            int k = solution.Count;
            for (int i = 0; i < n; i++)
            {
                if (solution.Count == 0)
                {
                    position = NQueensOnePosition(n, new List<int>(solution) { i });
                    if (position.Count == n)
                        return position;
                }
                else
                {
                    bool canPlace = true;
                    for (int j = 0; j < k; j++)
                    {
                        if (solution[k - j - 1] == i || solution[k - j - 1] == i - j - 1 || solution[k - j - 1] == i + (j + 1))
                        {
                            canPlace = false;
                            break; // This break improves the performance by a factor of 2.5
                        }
                    }
                    if (canPlace)
                    {
                        // Make a recursive call to set up the rest of the queens
                        position = NQueensOnePosition(n, new List<int>(solution) { i });
                        if (position.Count == n)
                            return position;
                    }
                }
            }
            return position;
        }

        /// <summary>
        /// We are solving task n queens problem, i.e., 
        /// counting how many configurations of n queens can we set in an n x n chessboard
        /// without queens attacking each other.
        /// </summary>
        public static int NQueens(int n, List<int> solution)
        {
            int numPossibilities = 0;

            // When we arrive at a solution, we send info back up
            if (solution.Count == n)
                return 1;

            // Otherwise try to place k-th queen in the row
            int k = solution.Count;
            for (int i = 0; i < n; i++)
            {
                if (solution.Count == 0)
                    numPossibilities += NQueens(n, new List<int>(solution) { i });
                else
                {
                    bool canPlace = true;
                    for (int j = 0; j < k; j++)
                    {
                        if (solution[k - j - 1] == i || solution[k - j - 1] == i - j - 1 || solution[k - j - 1] == i + (j + 1))
                        {
                            canPlace = false;
                            break; // This break improves the performance by a factor of 2.5
                        }
                    }
                    if (canPlace)
                        // Make a recursive call to set up the rest of the queens
                        numPossibilities += NQueens(n, new List<int>(solution) { i });
                }
            }
            return numPossibilities;
        }

        /// <summary>
        /// Given a number n and a list of integers,
        /// the function determines if n can be written as a sum of 
        /// some subset of the given numbers.        
        /// </summary>
        public static (bool Exists, List<int> Sumands) SubsetSum(int n, List<int> lstCandidates)
        {
            // First, order candidates
            List<int> lstCandidatesCopy = new List<int>(lstCandidates);
            //lstCandidatesCopy.Sort();

            long totalSum = lstCandidatesCopy.Sum(x => (long)x);

            if (totalSum < n)
                throw new ArithmeticException($"The candidates do not sum up to {n}!");

            int[] vecSumands = SubsetSumInternal(n, lstCandidatesCopy);

            List<int> lstSumands = new List<int>();
            for (int i = 0; i < vecSumands.Length; i++)
                if (vecSumands[i] == 1) lstSumands.Add(lstCandidatesCopy[i]);
            return lstSumands.Count > 0 ? (Exists: true, Sumands: lstSumands) : (Exists: false, Sumands: lstSumands);
        }

        /// <summary>
        /// Internal method for handling sum factorization - using loop not recursive calls!
        /// </summary>
        private static int[] SubsetSumInternal(int n, List<int> lstCandidates)
        {
            int[] vecSumands = new int[lstCandidates.Count];
            Dictionary<int, List<int>> dicTriedValues = new Dictionary<int, List<int>>();
            for (int i = 0; i < lstCandidates.Count; i++) dicTriedValues.Add(i, new List<int>());

            int sumCurrent = 0;
            long sumRemaining = lstCandidates.Sum(x => (long)x);

            for (int i = 0; i < lstCandidates.Count && i > -1; i++)
            {
                // Check if i-th item reaches the goal
                if (!dicTriedValues[i].Contains(1) && lstCandidates[i] + sumCurrent == n)
                {
                    vecSumands[i] = 1;
                    break;
                }

                // Cut the tree of choices in no solution is possible in this branch
                if (sumCurrent + sumRemaining < n || i == lstCandidates.Count - 1)
                {
                    i -= 2; // Decrease index by 2, since it gets increased by 1
                    continue;
                }

                // First try adding i-th element
                if (!dicTriedValues[i].Contains(1))
                {
                    dicTriedValues[i].Add(1);
                    sumCurrent += lstCandidates[i];
                    sumRemaining -= lstCandidates[i];

                    if (sumCurrent < n)
                    {
                        vecSumands[i] = 1;
                    }
                    else if (sumCurrent > n)
                    {
                        i -= 1;
                        continue;
                    }
                }
                else if (!dicTriedValues[i].Contains(0))
                {
                    dicTriedValues[i].Add(0);
                    vecSumands[i] = 0;
                    sumCurrent -= lstCandidates[i];
                    sumRemaining += lstCandidates[i];
                }
                else
                {
                    if (i == 0) return vecSumands;

                    dicTriedValues[i].Clear();
                    i -= 2;
                    continue;
                }
            }

            return vecSumands;
        }

        /// <summary>
        /// Recursive function to determine whether a list of integers
        /// contains a sequence whose sum equals n.
        /// Recursion is fast, but we quickly obtain StackOverflowException (having list of 100 000 candidates).
        /// https://docs.microsoft.com/en-us/dotnet/api/system.stackoverflowexception?view=net-5.0
        /// </summary>
        public static bool SubsetSumRec(int n, List<int> lstCandidates, int i)
        {
            if (n == 0)
                return true;
            else if (n < 0 || i == 0)
                return false;
            else
            {
                return SubsetSumRec(n - lstCandidates[i - 1], lstCandidates, i - 1)
                            ||
                       SubsetSumRec(n, lstCandidates, i - 1);
            }
        }

        /// <summary>
        /// TODO.
        /// </summary>
        public static bool SubsetSumRecWithSolution(int n, List<int> lstCandidates, int i)
        {
            if (n == 0)
                return true;
            else if (n < 0 || i == 0)
                return false;
            else
            {
                return SubsetSumRec(n - lstCandidates[i - 1], lstCandidates, i - 1)
                            ||
                       SubsetSumRec(n, lstCandidates, i - 1);
            }
        }

        /// <summary>
        /// Solving the 0/1 knapsack problem recursively, NOT paying special attention
        /// to implicit restrictions.
        /// The third parameter denotes the 1-based index of element being considered.
        /// </summary>
        public static int KnapsackProblemRec(int volume, List<Item> lstItems, int i)
        {
            if (i == 1)
            {
                if (volume < lstItems[i - 1].Volume)
                    return 0;
                else
                    return lstItems[i - 1].Value;
            }

            int max1 = 0;
            if (lstItems[i - 1].Volume <= volume)
            {
                max1 = KnapsackProblemRec(volume - lstItems[i - 1].Volume, lstItems, i - 1) + lstItems[i - 1].Value;
            }
            int max0 = KnapsackProblemRec(volume, lstItems, i - 1);

            return Math.Max(max1, max0);
        }

        /// <summary>
        /// Solving the 0/1 knapsack problem with backtracking, 
        /// using fractional knapsack problem solution for the upper bound.
        /// </summary>
        public static int KnapsackProblem(int volume, List<Item> lstItems)
        {
            // Order items by relative value
            lstItems = lstItems.OrderByDescending(x => (double)x.Value / x.Volume).ToList();

            // A "global" variable to carry max found value
            int maxValue = 0;
            // Call recursive function
            KnapsackInternal(volume, lstItems, 0, 0, 0, ref maxValue);
            return maxValue;
        }

        /// <summary>
        /// Traversing the state tree in depth
        /// </summary>
        private static void KnapsackInternal(int volume, List<Item> lstItems, int i, int currentValue, int currentVolume, ref int maxValue)
        {
            if (currentVolume <= volume && currentValue > maxValue)
            {
                maxValue = currentValue;
            }

            if (IsPromising(i, volume, currentValue, currentVolume, maxValue, lstItems))
            {
                KnapsackInternal(volume, lstItems, i + 1, currentValue + lstItems[i].Value, currentVolume + lstItems[i].Volume, ref maxValue);
                KnapsackInternal(volume, lstItems, i + 1, currentValue, currentVolume, ref maxValue);
            }
        }

        /// <summary>
        /// Auxilliary function to compute fractional constraint for the node
        /// </summary>
        private static bool IsPromising(int i, int volume, int currentValue, int currentVolume, int maxValue, List<Item> lstItems)
        {
            // Node is promising only if we should expand to its children. 
            // There must be some capacity left for the children.
            if (currentVolume >= volume)
                return false;
            else
            {
                int j = i;
                double bound = currentValue;
                int totalValue = currentVolume;

                // Compute fractional solution
                while (j < lstItems.Count && totalValue + lstItems[j].Volume <= volume)
                {
                    // Grab as many items as possible.
                    totalValue += lstItems[j].Volume;
                    bound += lstItems[j].Value;
                    j++;
                }
                if (j < lstItems.Count)
                    bound += (volume - totalValue) * ((double)lstItems[j].Value / lstItems[j].Volume);

                return bound > maxValue;
            }
        }

        public static int NKnights(int dimension, int numKnights, bool printBoards = false)
        {
            int positionedKnights = 0;

            // Backtracking stack
            Stack<Board> stDFS = new Stack<Board>();
            stDFS.Push(new Board(new List<Position>(), 0, 0));

            while (stDFS.Count > 0)
            {
                var board = stDFS.Pop();

                if (board.Knights.Count == numKnights)
                {
                    positionedKnights++;
                    if (printBoards)
                        board.Print(dimension);
                    continue;
                }

                for (int row = board.StartPosition.X; row < dimension; row++)
                {
                    for (int col = (row == board.StartPosition.X ? board.StartPosition.Y : 0); col < dimension; col++)
                    {
                        if (!board.IsAttacked(row, col))
                        {
                            var newKnights = new List<Position>(board.Knights);
                            newKnights.Add(new Position(row, col));

                            stDFS.Push(new Board(newKnights, row, col + 1));
                        }
                    }
                }
            }

            return positionedKnights;
        }

        private class Board
        {
            static Position[] attacks = {   new(1, 2), new(2, 1),
                                            new(-1, 2), new(-2, 1),
                                            new(-2, -1), new(-1, -2),
                                            new(1, -2), new(2, -1) };

            public List<Position> Knights { get; }
            public Position StartPosition;

            public Board(List<Position> knights, int i, int j)
            {
                Knights = new List<Position>(knights);
                StartPosition = new(i, j);
            }

            public bool IsAttacked(int i, int j)
            {
                foreach (var pos in this.Knights)
                {
                    for (int t = 0; t < attacks.Length; t++)
                    {
                        if (pos.X + attacks[t].X == i && pos.Y + attacks[t].Y == j)
                            return true;
                    }
                }
                return false;
            }

            public void Print(int n)
            {
                char[,] board = new char[n, n];
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        board[i, j] = '.';

                foreach (var pos in this.Knights)
                    board[pos.X, pos.Y] = 'K';

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                        Console.Write(board[i, j] + " ");
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        public static List<int> LongestCommonSubsequence(List<int> A, List<int> B)
        {
            return LongestCommonSubsequenceRec(A, B, 0, 0);
        }

        private static List<int> LongestCommonSubsequenceRec(List<int> A, List<int> B, int i, int j)
        {
            if (i >= A.Count || j >= B.Count)
                return new List<int>();

            if (A[i] == B[j])
            {
                var res = LongestCommonSubsequenceRec(A, B, i + 1, j + 1);
                res.Insert(0, A[i]);
                return res;
            }
            else
            {
                var skipA = LongestCommonSubsequenceRec(A, B, i + 1, j);
                var skipB = LongestCommonSubsequenceRec(A, B, i, j + 1);
                return skipA.Count > skipB.Count ? skipA : skipB;
            }
        }

        public class OpinionChange
        {
            static int[] dx = { -1, 1, 0, 0 }; // Directions for moving up, down, left, right
            static int[] dy = { 0, 0, -1, 1 };

            // Function to check if the cell is within the bounds of the grid
            public static bool IsValid(int x, int y, int n)
            {
                return x >= 0 && y >= 0 && x < n && y < n;
            }

            public static int globalDepth = int.MaxValue;

            public static void SpreadOpinion(int[,] grid, int depth)
            {
                // Get neighbors different from 0,0                
                int n = grid.GetLength(0);

                Queue<(int X, int Y)> qArea = new Queue<(int, int)>();
                HashSet<(int X, int Y)> hshArea = new HashSet<(int X, int Y)>();
                HashSet<int> hshBoundaryValues = new HashSet<int>();
                hshArea.Add((0, 0));
                qArea.Enqueue((0, 0));

                while (qArea.Count > 0)
                {
                    (int X, int Y) cell = qArea.Dequeue();
                    int targetOpinion = grid[cell.X, cell.Y];

                    // Check all 4 neighboring cells
                    for (int i = 0; i < 4; i++)
                    {
                        int nx = cell.X + dx[i];
                        int ny = cell.Y + dy[i];

                        if (IsValid(nx, ny, n) && !hshArea.Contains((nx, ny)))
                        {
                            // If the neighbor has the same opinion as the target, propagate
                            if (grid[nx, ny] == targetOpinion)
                            {
                                hshArea.Add((nx, ny));
                                qArea.Enqueue((nx, ny));
                            }
                            // Otherwise, change the opinion and count it as a change
                            else
                            {
                                hshBoundaryValues.Add(grid[nx, ny]);                                                                
                            }
                        }
                    }
                }

                if (hshArea.Count == grid.Length)
                {
                    if(globalDepth > depth)
                        globalDepth = depth;
                    return;
                }

                if (globalDepth <= depth)
                    return;

                int min = int.MaxValue;
                foreach(int recolor in hshBoundaryValues)
                {
                    int[,] gridNew = (int[,])grid.Clone();
                    foreach(var pos in hshArea)
                    {
                        gridNew[pos.X, pos.Y] = recolor;
                    }

                    SpreadOpinion(gridNew, depth + 1);
                }
                return;
            }

            public static void PrintGrid(int[,] grid, int n)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write(grid[i, j] + " ");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
