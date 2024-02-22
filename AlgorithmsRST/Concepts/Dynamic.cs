using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borut.Lectures.AlgorithmsRST
{
    /// <summary>
    /// In this class we collect algorithms using dynamic programming.
    /// </summary>
    public static class Dynamic
    {
        public enum Algorithm
        {
            FibonacciRec,
            FibonacciList,
            FibonacciDirect,
            IsomorphicTrees,
            KnapsackRec,
            KnapsackRecVec,
            KnapsackDyn,
            ShuffleString,
            VankinsMile,
            MaxSolidBlock,
            MaxSolidSquareBlock,
            MinPalindroms,
            MinBankNotes
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
                a[indA] == c[indA + indB + 1] && ShuffleStringRec(a, b, c, indA - 1, indB)
                ||
                b[indB] == c[indA + indB + 1] && ShuffleStringRec(a, b, c, indA, indB - 1);
        }

        /// <summary>
        /// A greedy approach which DOES NOT WORK!
        /// Given three strings, it greedily decides whether the third is
        /// ordered combination of the former two.
        /// BANANA, ANANAS => BAANNAANNAAS
        /// </summary>
        public static bool ShuffleStringLin(string a, string b, string c)
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

        /// <summary>
        /// Vankin's mile is a game on a square matrix where we start in a given cell
        /// (we assume cell (0,0), otherwise we have cells that are irrelevant for the problem)
        /// and by stepping one step right or one step down, we search for the path with maximum sum of values.
        /// The game stops when the position reaches the edge of the board.
        /// Recursive algorithm.
        /// </summary>
        public static int VankinsMileRec(int[,] matrix, int indX, int indY)
        {
            // Maximum reached value will contain the value of current cell
            int max = matrix[indX, indY];

            // Stop if at the border
            if (indX + 1 == matrix.GetLength(0) && indY + 1 == matrix.GetLength(1))
                return max;

            int maxDown = 0;
            if (indX + 1 < matrix.GetLength(0))
                maxDown = VankinsMileRec(matrix, indX + 1, indY);

            int maxRight = 0;
            if (indY + 1 < matrix.GetLength(1))
                maxRight = VankinsMileRec(matrix, indX, indY + 1);

            return new List<int>()
                {
                    max,
                    max + maxDown,
                    max + maxRight
                }.Max();
        }

        /// <summary>
        /// Vankin's mile is a game on a square matrix where we start in a given cell
        /// (we assume cell (0,0), otherwise we have cells that are irrelevant for the problem)
        /// and by stepping one step right or one step down, we search for the path with maximum sum of values.
        /// The game stops when the position reaches the edge of the board.
        /// Recursive algorithm with memoization.
        /// </summary>
        public static int VankinsMileDyn(int[,] matrix, int indX, int indY, Dictionary<(int X, int Y), int> dicStore)
        {
            // Maximum reached value will contain the value of current cell
            int max = matrix[indX, indY];

            // Stop if at the border
            if (indX + 1 == matrix.GetLength(0) && indY + 1 == matrix.GetLength(1))
            {
                if (!dicStore.ContainsKey((X: indX, Y: indY)))
                    dicStore.Add((X: indX, Y: indY), max);
                return max;
            }

            int maxDown = 0;
            if (indX + 1 < matrix.GetLength(0))
            {
                if (dicStore.ContainsKey((X: indX + 1, Y: indY)))
                    maxDown = dicStore[(X: indX + 1, Y: indY)];
                else
                    maxDown = VankinsMileDyn(matrix, indX + 1, indY, dicStore);
            }

            int maxRight = 0;
            if (indY + 1 < matrix.GetLength(1))
            {
                if (dicStore.ContainsKey((X: indX, Y: indY + 1)))
                    maxRight = dicStore[(X: indX, Y: indY + 1)];
                else
                    maxRight = VankinsMileDyn(matrix, indX, indY + 1, dicStore);
            }

            max = new List<int>()
                {
                    max,
                    max + maxDown,
                    max + maxRight
                }.Max();
            //if (!dicStore.ContainsKey((X: indX, Y: indY)))
            dicStore.Add((X: indX, Y: indY), max);

            return max;
        }


        /// <summary>
        /// Computes the maximum subtree which can be found in both given trees.
        /// </summary>
        public static (int MaxNodes, string Signature) MaxIsomorphicSubtree(BinaryTree treeA, BinaryTree treeB)
        {
            // For each tree make a list of subtrees, defined by signatures
            HashSet<string> hsSubsA = new HashSet<string>();
            HashSet<string> hsSubsB = new HashSet<string>();

            // Traverse both trees recursively
            TraverseNodesForSubtrees(treeA.Root, ref hsSubsA);
            TraverseNodesForSubtrees(treeB.Root, ref hsSubsB);

            // Compare the sets for the max signature
            int maxNodes = 0;
            string maxSign = "";
            foreach (var sign in hsSubsA)
            {
                if (hsSubsB.Contains(sign) && sign.Length > maxNodes)
                {
                    maxNodes = sign.Replace("(", "").Replace(")", "").Length;
                    maxSign = sign;
                }
            }
            return (MaxNodes: maxNodes + 1, Signature: "R" + maxSign); // +1 to count the root node
        }

        private static string TraverseNodesForSubtrees(BinaryNode root, ref HashSet<string> hsSubs)
        {
            StringBuilder signature = new StringBuilder();

            // Get signature of left son
            if (root.LeftSon != null)
            {
                signature.Append("(");
                signature.Append("L");
                signature.Append(TraverseNodesForSubtrees(root.LeftSon, ref hsSubs));
                signature.Append(")");
            }

            // Get signature of right son
            if (root.RightSon != null)
            {
                signature.Append("(");
                signature.Append("D");
                signature.Append(TraverseNodesForSubtrees(root.RightSon, ref hsSubs));
                signature.Append(")");
            }

            string strSign = signature.ToString();
            hsSubs.Add(strSign);

            return strSign;
        }


        /// <summary>
        /// In a binary matrix it finds the maximum area of a homogene square submatrix
        /// (comprised only of 1's or 0's - in fact for any constant number of entries).
        /// </summary>
        public static int MaxHomogeneSquareBlockRec(int[,] matrix)
        {
            int[,] maxAreas = new int[matrix.GetLength(0), matrix.GetLength(1)];
            MaxHomogeneSquareBlockForCellRec(matrix, 0, 0, ref maxAreas);

            int maxArea = 0;
            for (int i = 0; i < maxAreas.GetLength(0); i++)
            {
                for (int j = 0; j < maxAreas.GetLength(1); j++)
                {
                    if (maxAreas[i, j] > maxArea)
                        maxArea = maxAreas[i, j];
                }
            }
            return maxArea;
        }

        /// <summary>
        /// For each cell we determine max square block that the cell is its left top corner.
        /// NOTE: this approach does not work for rectangular blocks, 
        /// since we do not store all maximal rectangles for each cell 
        /// (that would increase time complexity for an order).
        /// </summary>
        private static Rectangle MaxHomogeneSquareBlockForCellRec(int[,] matrix, int indX, int indY, ref int[,] maxArea)
        {
            bool canMoveDown = indX + 1 < matrix.GetLength(0);
            bool canMoveRight = indY + 1 < matrix.GetLength(1);

            // Get max block for the cell right-below
            Rectangle maxRightDown = new Rectangle();
            if (canMoveDown && canMoveRight) // We check all cells (regardless the cell values)
            {
                maxRightDown = MaxHomogeneSquareBlockForCellRec(matrix, indX + 1, indY + 1, ref maxArea);
            }

            // Get max block for the cell below
            Rectangle maxDown = new Rectangle();
            if (canMoveDown) // We check all cells
            {
                maxDown = MaxHomogeneSquareBlockForCellRec(matrix, indX + 1, indY, ref maxArea);
            }

            // Get max block for the right cell
            Rectangle maxRight = new Rectangle();
            if (canMoveRight) // We check all cells
            {
                maxRight = MaxHomogeneSquareBlockForCellRec(matrix, indX, indY + 1, ref maxArea);
            }

            Rectangle rect = new Rectangle()
            {
                X = indX,
                Y = indY,
                LengthX = 1,
                LengthY = 1
            };

            // Check if the current cell is a left-top corner of some other block            
            if (canMoveDown && canMoveRight && matrix[indX, indY] == matrix[indX + 1, indY + 1]
                    && matrix[indX, indY] == matrix[indX + 1, indY] && matrix[indX, indY] == matrix[indX, indY + 1])
            {
                int maxX = Math.Min(maxDown.LengthX, maxRightDown.LengthX);
                int maxY = Math.Min(maxRight.LengthY, maxRightDown.LengthY);
                int maxSquare = Math.Min(maxX, maxY) + 1;
                rect.LengthX = maxSquare;
                rect.LengthY = maxSquare;
            }

            maxArea[indX, indY] = rect.Area;
            return rect;
        }

        /// <summary>
        /// In a binary matrix it finds the maximum area of a homogene square submatrix
        /// (comprised only of 1's or 0's - in fact for any constant number of entries).
        /// </summary>
        public static int MaxHomogeneSquareBlockDyn(int[,] matrix)
        {
            int m = matrix.GetLength(0), n = matrix.GetLength(1);

            int[,] maxAreas = new int[m, n];

            // Fill the last row and last column first 
            for (int j = n - 1; j >= 0; j--)
            {
                maxAreas[m - 1, j] = 1;
            }
            for (int i = m - 1; i >= 0; i--)
            {
                maxAreas[i, n - 1] = 1;
            }

            //int maxArea = 0;
            for (int i = m - 2; i >= 0; i--)
            {
                for (int j = n - 2; j >= 0; j--)
                {
                    if (matrix[i, j] == matrix[i + 1, j] && matrix[i, j] == matrix[i, j + 1] && matrix[i, j] == matrix[i + 1, j + 1])
                    {
                        int maxX = Math.Min(maxAreas[i + 1, j], maxAreas[i + 1, j + 1]);
                        int maxY = Math.Min(maxAreas[i, j + 1], maxAreas[i + 1, j + 1]);
                        maxAreas[i, j] = Math.Min(maxX, maxY) + 1;
                    }
                    else
                        maxAreas[i, j] = 1;
                }
            }

            int maxArea = 0;
            for (int i = m - 1; i >= 0; i--)
            {
                for (int j = n - 1; j >= 0; j--)
                {
                    if (maxAreas[i, j] > maxArea)
                        maxArea = maxAreas[i, j];
                }
            }
            return maxArea * maxArea;
        }


        /// <summary>
        /// Returns maximum rectangular area in a given histogram.
        /// E.g., for 2 1 3 2 2 1 3 => 7 x 1
        /// Source: https://www.geeksforgeeks.org/largest-rectangle-under-histogram/
        /// </summary>
        public static int MaxRectangleInHistogram(int[] histValues)
        {
            // Create an empty stack. The stack holds indexes of hist[] array.
            // The bars stored in stack are always in increasing order of their heights.
            Stack<int> stack = new Stack<int>();

            int stackTop; // Top of stack

            int maxArea = 0;
            int area;

            // Run through all bars of given histogram
            int i = 0;
            while (i < histValues.Length)
            {
                // If this bar is higher than the bar on top of stack, push it to stack
                if (stack.Count == 0 || histValues[stack.Peek()] <= histValues[i])
                {
                    stack.Push(i++);
                }
                else
                {
                    // If this bar is lower than top of stack, then calculate area of
                    // rectangle with stack top as the smallest (or minimum height) bar.
                    // 'i' is 'right index' for the top and element before top in stack is 'left index'
                    stackTop = histValues[stack.Peek()];
                    stack.Pop();

                    if (stack.Count > 0)
                        area = stackTop * (i - stack.Peek() - 1);
                    else
                        area = stackTop * i;

                    maxArea = Math.Max(area, maxArea);
                }
            }

            // Now pop the remaining bars from stack and calculate area with
            // every popped bar as the smallest bar
            while (stack.Count > 0)
            {
                stackTop = histValues[stack.Peek()];
                stack.Pop();

                if (stack.Count > 0)
                    area = stackTop * (i - stack.Peek() - 1);
                else
                    area = stackTop * i;

                maxArea = Math.Max(area, maxArea);
            }
            return maxArea;
        }

        // Returns area of the largest rectangle with all same values in the matrix.
        // It uses max histogram rectangle approach.
        public static int MaxHomogeneBlock(int[,] matrix)
        {
            // We check rectangles for all possible values in the matrix
            HashSet<int> lstValues = new HashSet<int>();
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (!lstValues.Contains(matrix[i, j]))
                        lstValues.Add(matrix[i, j]);
                }
            }

            int maxArea = 0;
            foreach (var val in lstValues)
            {
                var tmpMatrix = matrix.SetOnesInMatrix(val);
                // Calculate area for first row and initialize it as result
                int[] row = tmpMatrix.GetRow(0);
                int maxAreaForVal = MaxRectangleInHistogram(row);

                // Iterate over row to find maximum rectangular area considering each row as histogram
                for (int i = 1; i < tmpMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < tmpMatrix.GetLength(1); j++)
                    {
                        if (tmpMatrix[i, j] == 1)
                        {
                            tmpMatrix[i, j] += tmpMatrix[i - 1, j];
                        }
                    }

                    // Update result if area with current row (as last row of rectangle) is more
                    row = tmpMatrix.GetRow(i);
                    maxAreaForVal = Math.Max(maxAreaForVal, MaxRectangleInHistogram(row));
                }
                maxArea = Math.Max(maxArea, maxAreaForVal);
            }

            return maxArea;
        }

        /// <summary>
        /// Computes the minimum number of palindroms into which a given word (list of characters)
        /// can be decomposed.
        /// </summary>
        /// <param name="word">List of word's characters</param>
        /// <param name="k">Last index of the first palindrom's character</param>
        /// <returns>The minimum number of palindroms into which the word can be decomposed</returns>
        public static int MinPalindromsRec(List<char> word)
        {
            if (word.Count == 1)
                return 1;

            if (IsPalindrom(word))
                return 1;

            int min = int.MaxValue;
            for (int i = 0; i < word.Count - 1; i++)
            {
                if (IsPalindrom(word.GetRange(0, i + 1)))
                {
                    var innerMin = MinPalindromsRec(word.GetRange(i + 1, word.Count - i - 1));
                    if (min > innerMin)
                        min = innerMin;
                }
            }
            return min + 1;
        }

        private static bool IsPalindrom(List<char> word)
        {
            for (int i = 0; i < word.Count / 2; i++)
                if (word[i] != word[word.Count - i - 1]) return false;
            return true;
        }

        public static int MinPalindromsRecMemo(string word, ref Dictionary<string, int> dicMemo)
        {
            if (word.Length == 1)
                return 1;

            if (IsPalindrom(word.ToList()))
                return 1;

            int min = int.MaxValue;
            for (int i = 0; i < word.Length - 1; i++)
            {
                if (IsPalindrom(word.Substring(0, i + 1).ToList()))
                {
                    var testWord = word.Substring(i + 1, word.Length - i - 1);
                    int innerMin;
                    if (dicMemo.ContainsKey(testWord))
                        innerMin = dicMemo[testWord];
                    else
                        innerMin = MinPalindromsRecMemo(testWord, ref dicMemo);
                    if (min > innerMin)
                        min = innerMin;
                }
            }
            dicMemo.Add(word, min + 1);
            return min + 1;
        }

        /// <summary>
        /// Computes the minimum number of bank notes needed to pay out a given amount,
        /// provided that the values of banknotes are known and for every value we have an unlimited amount of banknotes.
        /// </summary>
        public static int MinBankNotesRec(int totalAmount, ref List<int> lstValues)
        {
            if (totalAmount == 0)
                return 0;

            int min = int.MaxValue;

            for (int i = 0; i < lstValues.Count; i++)
            {
                if (totalAmount - lstValues[i] < 0)
                    continue;
                var minI = MinBankNotesRec(totalAmount - lstValues[i], ref lstValues);
                if (min > minI)
                    min = minI;
            }

            return min == int.MaxValue ? int.MaxValue :  min + 1;
        }

        /// <summary>
        /// Computes the minimum number of bank notes needed to pay out a given amount,
        /// provided that the values of banknotes are known and for every value we have an unlimited amount of banknotes.
        /// Here, we also use memoization.
        /// </summary>
        public static int MinBankNotesDyn(int totalAmount, ref List<int> lstValues, ref Dictionary<int, int> dicMemo)
        {
            if (totalAmount == 0)
                return 0;

            int min = int.MaxValue;

            for (int i = lstValues.Count - 1; i >= 0; i--)
            {
                if (totalAmount - lstValues[i] < 0)
                    continue;

                int minI;
                if (!dicMemo.ContainsKey(totalAmount - lstValues[i]))
                {
                    minI = MinBankNotesDyn(totalAmount - lstValues[i], ref lstValues, ref dicMemo);
                    dicMemo.Add(totalAmount - lstValues[i], minI);
                }
                else
                    minI = dicMemo[totalAmount - lstValues[i]];

                if (min > minI)
                    min = minI;
            }

            return min == int.MaxValue ? int.MaxValue : min + 1;
        }

        /// <summary>
        /// Computes the minimum number of bank notes needed to pay out a given amount,
        /// provided that the values of banknotes are known and for every value we have an unlimited amount of banknotes.
        /// Here, we apart from memoization, we also do not try in a recursive call a value which is higher than the value from the previous call.
        /// </summary>
        public static int MinBankNotesDynOptimized(int totalAmount, ref List<int> lstValues, int upToValue, ref Dictionary<int, int> dicMemo)
        {
            if (totalAmount == 0)
                return 0;

            int min = int.MaxValue;

            for (int i = upToValue; i >= 0; i--)
            {
                if (totalAmount - lstValues[i] < 0)
                    continue;

                int minI;
                if (!dicMemo.ContainsKey(totalAmount - lstValues[i]))
                {
                    minI = MinBankNotesDynOptimized(totalAmount - lstValues[i], ref lstValues, i, ref dicMemo);
                    dicMemo.Add(totalAmount - lstValues[i], minI);
                }
                else
                    minI = dicMemo[totalAmount - lstValues[i]];

                if (min > minI)
                    min = minI;
            }

            return min == int.MaxValue ? int.MaxValue : min + 1;
        }
    }
}
