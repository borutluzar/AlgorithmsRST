using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace Borut.Lectures.AlgorithmsRST
{
    public class Program
    {
        static void Main(string[] args)
        {
            switch (InterfaceFunctions.ChooseSection<Chapter>())
            {
                case Chapter.GreedyAlgorithm:
                    {
                        Console.WriteLine("Testing greedy method");

                        switch (InterfaceFunctions.ChooseSection<Greedy.Algorithm>())
                        {
                            case Greedy.Algorithm.FractionalKnapsack:
                                {
                                    List<Item> items1 = new List<Item>()
                                        {
                                            new Item(){ Volume=8, Value=8 },
                                            new Item(){ Volume=5, Value=6 },
                                            new Item(){ Volume=5, Value=6 }
                                        };

                                    (var val1, var x1) = Greedy.FractionalKnapsack(10, items1);
                                    Console.WriteLine($"Optimal solution to task #1 is {val1} with the vector {x1.ToString(2)}");

                                    List<Item> items2 = new List<Item>()
                                        {
                                            new Item(){ Volume=4, Value=4 },
                                            new Item(){ Volume=6, Value=9 },
                                            new Item(){ Volume=6, Value=9 }
                                        };

                                    (var val2, var x2) = Greedy.FractionalKnapsack(10, items2);
                                    Console.WriteLine($"Optimal solution to task #2 is {val2} with the vector {x2.ToString(2)}");
                                }
                                break;
                            case Greedy.Algorithm.SchedulingTasks:
                                {
                                    List<Task> tasks = new List<Task>()
                                        {
                                            new Task(){ Start=0, End=8 },
                                            new Task(){ Start=1, End=4 },
                                            new Task(){ Start=2, End=3 },
                                            new Task(){ Start=4, End=5 },
                                            new Task(){ Start=1, End=6 },
                                        };
                                    var numTasks = Greedy.ScheduleTasks(tasks);
                                    Console.WriteLine($"Optimal solution contains {numTasks} tasks.");
                                }
                                break;
                        }
                    }
                    break;
                case Chapter.DivideAndConquer:
                    {
                        Console.WriteLine("Testing divide and conquer");

                        switch (InterfaceFunctions.ChooseSection<DivideAndConquer.Algorithm>())
                        {
                            case DivideAndConquer.Algorithm.Bisection:
                                {
                                    var list = new SortedSet<int>() { 1, 4, 6, 9, 11, 12, 13, 23, 24, 25, 27, 29, 31, 33, 35, 59 };
                                    int searchedElement = 23;
                                    bool contains = DivideAndConquer.FindElementInList(searchedElement, list, 0, list.Count - 1);
                                    Console.WriteLine($"The list {(contains ? "contains" : "does not contain")} element {searchedElement}.");
                                }
                                break;
                            case DivideAndConquer.Algorithm.NaiveMaxSubsequenceSum:
                                {
                                    var list = new List<int>() { -41, -53, -58, -93, -23, -84 };
                                    //var list = new List<int>() { 31, -41, 59, 26, -53, 58, 97, -93, -23, 84 };
                                    var max = DivideAndConquer.NaiveMaxSubsequenceSum(list);
                                    Console.WriteLine($"The maximum sum is {max}.");
                                }
                                break;
                            case DivideAndConquer.Algorithm.MaxSubsequenceSumDC:
                                {
                                    var list = new List<int>() { -41, -53, -58, -93, -23, -84 };
                                    //var list = new List<int>() { 31, -41, 59, 26, -53, 58, 97, -93, -23, 84 };
                                    var max = DivideAndConquer.MaxSubsequenceSum_DivAndCon(list, 0, list.Count - 1);
                                    Console.WriteLine($"The maximum sum is {max}.");
                                }
                                break;
                            case DivideAndConquer.Algorithm.MaxSubsequenceSumLin:
                                {
                                    var list = new List<int>() { -41, -53, -58, -93, -23, -84 };
                                    //var list = new List<int>() { 31, -41, 59, 26, -53, 58, 97, -93, -23, 84 };
                                    //var list = new List<int>() { 5, -4, -2, 3, 2, -2, 1 };
                                    var max = DivideAndConquer.MaxSubsequenceSumLinearWorkingForAllNegative(list);
                                    Console.WriteLine($"The maximum sum is {max}.");
                                }
                                break;
                            case DivideAndConquer.Algorithm.MaxSubsequenceCompareTimes:
                                {
                                    int size = 100_000;
                                    var list = TestCasesGenerator.GenerateRandomListOfIntegers(size, -size, 5 * size, true);

                                    Stopwatch sw = Stopwatch.StartNew();
                                    var max = DivideAndConquer.NaiveMaxSubsequenceSum(list);
                                    Console.WriteLine($"Naiven pristop se izvede v {sw.Elapsed.TotalSeconds:0.0000} - Rezultat: {max}");

                                    sw = Stopwatch.StartNew();
                                    max = DivideAndConquer.MaxSubsequenceSum_DivAndCon(list, 0, list.Count - 1);
                                    Console.WriteLine($"Pristop deli in vladaj se izvede v {sw.Elapsed.TotalSeconds:0.0000} - Rezultat: {max}");

                                    sw = Stopwatch.StartNew();
                                    max = DivideAndConquer.MaxSubsequenceSumLinearWorkingForAllNegative(list);
                                    Console.WriteLine($"Linearen pristop se izvede v {sw.Elapsed.TotalSeconds:0.0000} - Rezultat: {max}");
                                }
                                break;
                            case DivideAndConquer.Algorithm.LargestIncreasingSubsequence:
                                {
                                    //var list = new List<int>() { 31, -41, 59, 26, -53, 58, 97, -93, -23, 84 };
                                    var list = new List<int>();
                                    Random rnd = new();
                                    for (int i = 0; i < 100_000_000; i++)
                                        list.Add(rnd.Next(0, 1000));

                                    Stopwatch sw = Stopwatch.StartNew();
                                    var max = DivideAndConquer.LargestIncreasingSubsequence(list);
                                    Console.WriteLine($"Largest increasing subsequence has length {max}. [Time: {sw.Elapsed.TotalSeconds}]");
                                }
                                break;
                            case DivideAndConquer.Algorithm.LargestIncreasingSubsequence_DivAndCon:
                                {
                                    //var list = new List<int>() { 31, -41, 59, 26, -53, 58, 97, -93, -23, 84 };
                                    var list = new List<int>();
                                    Random rnd = new();
                                    for (int i = 0; i < 100_000_000; i++)
                                        list.Add(rnd.Next(0, 1000));

                                    Stopwatch sw = Stopwatch.StartNew();
                                    var max = DivideAndConquer.LargestIncreasingSubsequence_DivAndCon(list, 0, list.Count - 1);
                                    Console.WriteLine($"Largest increasing subsequence with divide and conquer has length {max}. [Time: {sw.Elapsed.TotalSeconds}]");
                                }
                                break;
                            case DivideAndConquer.Algorithm.CountingInversionsExhaustive:
                                {
                                    var list = new List<int>() { 2, 4, 3, 5, 1, 6 };
                                    /*var list = new List<int>();
                                    Random rnd = new();
                                    for (int i = 0; i < 100_000_000; i++)
                                        list.Add(rnd.Next(0, 1000));
                                    */
                                    Stopwatch sw = Stopwatch.StartNew();
                                    var inversions = DivideAndConquer.CountInversionsExhaustive(list, out List<(int, int)> lstInversions);
                                    Console.WriteLine($"The nuber of inversions in the list is {inversions}. " +
                                        $" The inversions are: {lstInversions.ToString<(int, int)>()}" +
                                        $" [Time: {sw.Elapsed.TotalSeconds}]");
                                }
                                break;
                            case DivideAndConquer.Algorithm.CountingInversions_DivAndCon:
                                {
                                    var list = new List<int>() { 2, 4, 3, 5, 1, 6, -1 };
                                    /*var list = new List<int>();
                                    Random rnd = new();
                                    for (int i = 0; i < 100_000_000; i++)
                                        list.Add(rnd.Next(0, 1000));
                                    */
                                    Stopwatch sw = Stopwatch.StartNew();
                                    (var lstOrdered, var numInv) = DivideAndConquer.CountInversionsDivideAndConquer(list);
                                    Console.WriteLine($"The number of inversions in the list is {numInv}. " +
                                        $" [Time: {sw.Elapsed.TotalSeconds}]");
                                }
                                break;
                        }
                    }
                    break;
                case Chapter.DynamicProgramming:
                    {
                        Console.WriteLine("Testing dynamic programming");

                        switch (InterfaceFunctions.ChooseSection<Dynamic.Algorithm>())
                        {
                            case Dynamic.Algorithm.FibonacciRec:
                                {
                                    int k = 45;
                                    Stopwatch sw = Stopwatch.StartNew();
                                    long result = Dynamic.FibonacciRec(k);
                                    Console.WriteLine($"The {k}-th element of the Fibonacci sequence is {result}.");
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");
                                }
                                break;
                            case Dynamic.Algorithm.FibonacciMemo:
                                {
                                    int k = 1000;
                                    Stopwatch sw = Stopwatch.StartNew();
                                    Dictionary<int, long> dicStore = new();
                                    long result = Dynamic.FibonacciRecMemo(k, dicStore);
                                    Console.WriteLine($"The {k}-th element of the Fibonacci sequence is {result}.");
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");
                                }
                                break;
                            case Dynamic.Algorithm.FibonacciTabul:
                                {
                                    int k = 1000;
                                    Stopwatch sw = Stopwatch.StartNew();
                                    long result = Dynamic.FibonacciList(k);
                                    Console.WriteLine($"The {k}-th element of the Fibonacci sequence is {result}.");
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");
                                }
                                break;
                            case Dynamic.Algorithm.FibonacciDirect:
                                {
                                    int k = 1000;
                                    Stopwatch sw = Stopwatch.StartNew();
                                    long result = Dynamic.FibonacciDirect(k);
                                    Console.WriteLine($"The {k}-th element of the Fibonacci sequence is {result}.");
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");
                                }
                                break;
                            case Dynamic.Algorithm.KnapsackRec:
                                {
                                    /*int volume = 9;
                                    List<Item> lstItems = new List<Item>()
                                    {
                                        new Item(){ Volume=2, Value=3},
                                        new Item(){ Volume=4, Value=5},
                                        new Item(){ Volume=4, Value=7},
                                        new Item(){ Volume=6, Value=8}
                                    };*/

                                    /*
                                    int volume = 10;
                                    List<Item> lstItems = new List<Item>()
                                    {
                                        new Item(){ Volume=3, Value=5},
                                        new Item(){ Volume=4, Value=6},
                                        new Item(){ Volume=5, Value=7},
                                        new Item(){ Volume=6, Value=8},
                                    };
                                    */

                                    int volume = 200;
                                    List<Item> lstItems = new List<Item>();
                                    Random rnd = new Random(1);
                                    int k = 35;
                                    for (int i = 0; i < k; i++)
                                    {
                                        lstItems.Add(new Item() { Volume = rnd.Next(10, 40), Value = rnd.Next(5, 10) });
                                    }

                                    int[] x = new int[lstItems.Count];

                                    Stopwatch sw = Stopwatch.StartNew();
                                    //(int result, int[] xOut) = Dynamic.KnapsackProblemRecVec(volume, lstItems, lstItems.Count - 1, x);
                                    int result = Dynamic.KnapsackProblemRec(volume, lstItems, lstItems.Count - 1);
                                    Console.WriteLine($"The maximum value of the knapsack is {result}.");
                                    //Console.WriteLine($"Vector of selected elements is {xOut.ToString<int>()}.");
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");
                                }
                                break;
                            case Dynamic.Algorithm.KnapsackDyn:
                                {
                                    /*int volume = 9;
                                    List<Item> lstItems = new List<Item>()
                                    {
                                        new Item(){ Volume=2, Value=3},
                                        new Item(){ Volume=4, Value=5},
                                        new Item(){ Volume=4, Value=7},
                                        new Item(){ Volume=6, Value=8}
                                    };*/

                                    int volume = 100;
                                    List<Item> lstItems = new List<Item>();
                                    Random rnd = new Random(1);
                                    int k = 100; // 1000
                                    for (int i = 0; i < k; i++)
                                    {
                                        lstItems.Add(new Item() { Volume = rnd.Next(10, 40), Value = rnd.Next(5, 10) });
                                    }

                                    int[] x = new int[lstItems.Count];

                                    Stopwatch sw = Stopwatch.StartNew();
                                    //(int result, int[] xOut) = Dynamic.KnapsackProblemRecVec(volume, lstItems, lstItems.Count - 1, x);
                                    int result = Dynamic.KnapsackProblemDyn(volume, lstItems, lstItems.Count - 1, new Dictionary<(int, int), int>());
                                    Console.WriteLine($"DYNAMIC: The maximum value of the knapsack is {result}.");
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");

                                    sw = Stopwatch.StartNew();
                                    result = Dynamic.KnapsackProblemRec(volume, lstItems, lstItems.Count - 1);
                                    Console.WriteLine($"RECURSIVE: The maximum value of the knapsack is {result}.");
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");
                                }
                                break;
                            case Dynamic.Algorithm.ShuffleString:
                                {
                                    //string a = "borut", b = "lužar", c = "blouržuatr";
                                    string a = "1234mskdffgnbsddjsddsfkvnjnfvbdfssldosnfkdkdksdfi",
                                        b = "testjdsksldnvnvjsjeefjsjsjkvlvkfshenf",
                                        c = "1234mskdftestjdsksfgnbsddjsddsfkvnjnfldnvnvjsjeefvbdfssldosnfkdkdkjsjsjkvlvkfshenfsdfi";
                                    //string a = "ananas", b = "mango", c = "amanangnaos";

                                    Stopwatch sw = Stopwatch.StartNew();
                                    bool result = Dynamic.ShuffleStringRec(a, b, c, a.Length - 1, b.Length - 1);
                                    Console.WriteLine($"String \"{c}\" is{(result ? "" : " NOT")} a shuffle of \"{a}\" and \"{b}\".");
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");
                                    sw = Stopwatch.StartNew();
                                    bool result2 = Dynamic.ShuffleStringLin(a, b, c);
                                    Console.WriteLine($"String \"{c}\" is{(result2 ? "" : " NOT")} a shuffle of \"{a}\" and \"{b}\".");
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");
                                }
                                break;
                            case Dynamic.Algorithm.VankinsMile:
                                {
                                    /*
                                    int[,] matrix = new int[,]
                                        {
                                            { 1,  3, -2, 3 },
                                            { 2,  6,  2, -1 },
                                            {-1, -2, -1, -2 }
                                        };
                                    */

                                    int[,] matrix = TestCasesGenerator.Generate2DMatrix(1000, 1000, 0, 10, 5);

                                    Stopwatch sw = Stopwatch.StartNew();
                                    /*
                                    int result = Dynamic.VankinsMileRec(matrix, 0, 0);
                                    Console.WriteLine($"RECURSIVE: The optimal path has value {result}.");
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");
                                    
                                    sw = Stopwatch.StartNew();
                                    */
                                    int result2 = Dynamic.VankinsMileDyn(matrix, 0, 0, new Dictionary<(int X, int Y), int>());
                                    Console.WriteLine($"DYNAMIC: The optimal path has value {result2}.");
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");
                                }
                                break;
                            case Dynamic.Algorithm.LongestCommonSubsequence:
                                {
                                    /*
                                    var lst1 = new List<int>() { 1, 2, 3, 2, 5 };
                                    var lst2 = new List<int>() { 1, 2, 4, 3, 1, 2 };                                    
                                    */

                                    var lst1 = TestCasesGenerator.GenerateRandomListOfIntegers(10000, 1, 100, false);
                                    var lst2 = TestCasesGenerator.GenerateRandomListOfIntegers(10000, 1, 100, false);

                                    Stopwatch sw = Stopwatch.StartNew();
                                    int lcs = Dynamic.LongestCommonSubsequence(lst1, lst2);
                                    Console.WriteLine($"The length of the longest common sequence between:\n" +
                                        $"{lst1.ToString<int>()}\n" +
                                        $"{lst2.ToString<int>()}\n" +
                                        $"is {lcs}\n" +
                                        $"Computed in {sw.Elapsed.TotalSeconds:0.00}");
                                }
                                break;
                            case Dynamic.Algorithm.IsomorphicTrees:
                                {
                                    BinaryTree treeA = TestCasesGenerator.GenerateRandomBinaryTree(500, 0, 0);
                                    BinaryTree treeB = TestCasesGenerator.GenerateRandomBinaryTree(500, 0, 1);

                                    /*BinaryNode rootA = new BinaryNode()
                                    {
                                        LeftSon = new BinaryNode(),
                                        RightSon = new BinaryNode()
                                        {
                                            LeftSon = new BinaryNode()
                                            {
                                                LeftSon = new BinaryNode(),
                                                RightSon = new BinaryNode()
                                            },
                                            RightSon = new BinaryNode()
                                        }
                                    };
                                    BinaryTree treeA = new BinaryTree() { Root = rootA};

                                    BinaryNode rootB = new BinaryNode()
                                    {
                                        LeftSon = new BinaryNode()
                                        {
                                            RightSon = new BinaryNode()
                                        },
                                        RightSon = new BinaryNode()
                                        {
                                            LeftSon = new BinaryNode(),
                                            RightSon = new BinaryNode()
                                            {
                                                LeftSon = new BinaryNode()
                                            }
                                        }
                                    };
                                    BinaryTree treeB = new BinaryTree() { Root = rootB };*/

                                    Stopwatch sw = Stopwatch.StartNew();
                                    (int maxSubtreeNodes, string maxSign) = Dynamic.MaxIsomorphicSubtree(treeA, treeB);
                                    Console.WriteLine($"The maximum subtree has {maxSubtreeNodes} nodes.");
                                    Console.WriteLine($"The maximum subtree has signature {maxSign}.");
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");
                                }
                                break;
                            case Dynamic.Algorithm.MaxSolidSquareBlock:
                                {
                                    /*int[,] matrix = new int[,]
                                        {
                                            { 0, 0, 0 },
                                            { 0, 0, 0 },
                                            { 0, 0, 0 },
                                        };*/

                                    int[,] matrix = TestCasesGenerator.Generate2DMatrix(10000, 10000, 0, 1, 10);

                                    Stopwatch sw = Stopwatch.StartNew();
                                    //int result = Dynamic.MaxHomogeneSquareBlockRec(matrix);
                                    int result = Dynamic.MaxHomogeneSquareBlockDyn(matrix);
                                    Console.WriteLine($"RECURSIVE: Maximum solid block has area {result}.");
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");

                                    sw = Stopwatch.StartNew();
                                    //Dynamic.PrintMaxSubSquare(matrix);

                                    //Console.WriteLine($"DYNAMIC: Maximum solid block has area {result2} vs {result2}.");
                                    //Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");
                                }
                                break;
                            case Dynamic.Algorithm.MaxSolidBlock:
                                {
                                    /*int[,] matrix = new int[,]
                                        {
                                            { 1, 1, 0 },
                                            { 1, 0, 1 },
                                            { 1, 1, 1 },
                                            { 0, 0, 1 },
                                            { 0, 0, 1 },
                                            { 0, 0, 1 },
                                            { 1, 0, 0 }
                                        };*/

                                    int[,] matrix = TestCasesGenerator.Generate2DMatrix(100, 100, 0, 1, 5);

                                    Stopwatch sw = Stopwatch.StartNew();
                                    /*int result = Dynamic.VankinsMileRec(matrix, 0, 0);
                                    Console.WriteLine($"RECURSIVE: The optimal path has value {result}.");*/
                                    //Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");

                                    sw = Stopwatch.StartNew();
                                    //int result2 = Dynamic.MaxHomogeneBlockRec(matrix);
                                    //Dynamic.PrintMaxSubSquare(matrix);

                                    int[] histo = new int[] { 1, 2, 2, 0, 3, 2, 2 };
                                    int hist = Dynamic.MaxRectangleInHistogram(histo);
                                    //Console.WriteLine($"DYNAMIC: Hist area: {hist}.");

                                    int result3 = Dynamic.MaxHomogeneBlock(matrix);
                                    Console.WriteLine($"DYNAMIC: Maximum solid block has area {result3}.");
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");
                                }
                                break;
                            case Dynamic.Algorithm.MinPalindroms:
                                {
                                    var word = "ribarezeracirep";
                                    word = "pericarezeracirep";
                                    word = "anabanana";

                                    //var input = word.ToList();
                                    var input = ExtensionMethods.RandomWord(4000);
                                    var dic = new Dictionary<string, int>();

                                    Stopwatch sw = Stopwatch.StartNew();
                                    var result = Dynamic.MinPalindromsRecMemo(input, ref dic);
                                    Console.WriteLine($"DYNAMIC: Minimum number of palindroms for {word} is {result} in {sw.Elapsed.TotalSeconds}.");
                                }
                                break;
                            case Dynamic.Algorithm.MinBankNotes:
                                {
                                    Dictionary<int, int> dicMemo = new Dictionary<int, int>();
                                    List<int> lstValues = new List<int>() { 5, 20, 30, 47 };
                                    int amount = 273;

                                    Stopwatch sw = Stopwatch.StartNew();
                                    int result = Dynamic.MinBankNotesRec(amount, ref lstValues);
                                    Console.WriteLine($"Recursive: Minimum amount of banknotes is {result} in {sw.Elapsed.TotalSeconds}.");

                                    sw = Stopwatch.StartNew();
                                    result = Dynamic.MinBankNotesDyn(amount, ref lstValues, ref dicMemo);
                                    Console.WriteLine($"DYNAMIC: Minimum amount of banknotes is {result} in {sw.Elapsed.TotalSeconds}.");

                                    sw = Stopwatch.StartNew();
                                    dicMemo = new Dictionary<int, int>();
                                    result = Dynamic.MinBankNotesDynOptimized(amount, ref lstValues, lstValues.Count - 1, ref dicMemo);
                                    Console.WriteLine($"DYNAMIC optimized: Minimum amount of banknotes is {result} in {sw.Elapsed.TotalSeconds}.");
                                }
                                break;
                        }
                    }
                    break;
                case Chapter.Backtracking:
                    {
                        Console.WriteLine("Testing backtracking");

                        switch (InterfaceFunctions.ChooseSection<Backtracking.Algorithm>())
                        {
                            case Backtracking.Algorithm.NQueens:
                                {
                                    int n = 15;
                                    Stopwatch sw;
                                    for (int i = 4; i <= n; i++)
                                    {
                                        sw = Stopwatch.StartNew();
                                        int numPositions = Backtracking.NQueens(i, new List<int>());
                                        Console.WriteLine($"Number of solutions for {i} queens is {numPositions}. \t (Time: {sw.Elapsed.TotalSeconds} s)");
                                    }
                                }
                                break;
                            case Backtracking.Algorithm.NKnights:
                                {
                                    int n = 3;
                                    int k = 2;
                                    Stopwatch sw = Stopwatch.StartNew();
                                    int numPositions = Backtracking.NKnights(n, k, true);
                                    Console.WriteLine($"Number of solutions for {k} knights is {numPositions}. \t (Time: {sw.Elapsed.TotalSeconds} s)");
                                }
                                break;
                            case Backtracking.Algorithm.SubsetSum:
                                {
                                    int n = 31;
                                    List<int> lstCandidates = new List<int>() { 3, 7, 8, 13, 17, 21, 34, 36, 42, 54 };
                                    lstCandidates = new List<int>() { 3, 3 };
                                    lstCandidates = new List<int>() { 4, 6, 9, 13, 16 };

                                    bool hasSumRec = Backtracking.SubsetSumRec(n, lstCandidates, lstCandidates.Count);

                                    Stopwatch sw = Stopwatch.StartNew();
                                    (bool hasSum, List<int> lstSumands) = Backtracking.SubsetSum(n, lstCandidates);

                                    if (hasSum)
                                        Console.WriteLine($"The number {n} IS sum of numbers {lstSumands.ToString<int>()}. \t (Time: {sw.Elapsed.TotalSeconds} s)");
                                    else
                                        Console.WriteLine($"The number {n} IS NOT sum of any given numbers! \t (Time: {sw.Elapsed.TotalSeconds} s)");
                                }
                                break;
                            case Backtracking.Algorithm.SubsetSumLargeScale:
                                {
                                    for (int i = 0; i < 1; i++)
                                    {
                                        int n = 20_000;
                                        List<int> lstCandidates = TestCasesGenerator.GenerateRandomListOfIntegers(10_000, 1, 50_000, true, i);
                                        //List<int> lstCandidates = TestCasesGenerator.GenerateRandomListOfIntegers(10_000, 1, 1000, true, i);

                                        Stopwatch sw = Stopwatch.StartNew();
                                        (bool hasSum, List<int> lstSumands) = Backtracking.SubsetSum(n, lstCandidates);
                                        //bool hasSum = Backtracking.SubsetSumRec(n, lstCandidates, lstCandidates.Count);

                                        if (hasSum)
                                            Console.WriteLine($"The number {n} IS sum of numbers {lstSumands.ToString<int>()}. \t (Time: {sw.Elapsed.TotalSeconds} s)");
                                        else
                                            Console.WriteLine($"The number {n} IS NOT sum of any given numbers! \t (Time: {sw.Elapsed.TotalSeconds} s)");
                                    }
                                }
                                break;
                            case Backtracking.Algorithm.Exercise1:
                                {
                                    int n = 40;
                                    Stopwatch sw;

                                    sw = Stopwatch.StartNew();
                                    var position = Backtracking.NQueensOnePosition(n, new List<int>());
                                    if (position.Count == n)
                                        Console.WriteLine($"A solution for {n} queens is {position.ToString<int>()}. \t (Time: {sw.Elapsed.TotalSeconds} s)");
                                    else
                                        Console.WriteLine($"A solution for {n} queens does not exist. \t (Time: {sw.Elapsed.TotalSeconds} s)");

                                }
                                break;
                            case Backtracking.Algorithm.CommonSubsequence:
                                {
                                    List<int> A = new() { 1, 2, 6, 4, 8, 7 };
                                    List<int> B = new() { 2, 4, 6, 1, 8 };

                                    var result = Backtracking.LongestCommonSubsequence(A, B);

                                    Console.WriteLine("Najdaljše skupno podzaporedje (z backtrackingom): " + string.Join(", ", result));
                                }
                                break;
                            case Backtracking.Algorithm.Opinion:
                                {
                                    const int n = 4; // Example grid size (5x5)
                                    /*
                                    int[,] grid = new int[n, n]
                                    {
                                        {3, 9, 3, 9},
                                        {2, 2, 4, 9},
                                        {9, 3, 9, 8},
                                        {8, 9, 7, 8},
                                    };
                                    */
                                    int[,] grid = new int[6, 6]
                                    {
                                        { 5, 4, 5, 4, 1, 8 },
                                        { 1, 9, 5, 5, 4, 2 },
                                        { 8, 8, 8, 6, 9, 1 },
                                        { 4, 1, 4, 1, 1, 6 },
                                        { 1, 4, 9, 3, 9, 9 },
                                        { 8, 3, 9, 4, 8, 9 }
                                    };
                                    
                                    Console.WriteLine("Initial Grid:");
                                    Backtracking.OpinionChange.PrintGrid(grid, grid.GetLength(0));

                                    Backtracking.OpinionChange.SpreadOpinion(grid, 0);

                                    Console.WriteLine($"Number of opinion changes: {Backtracking.OpinionChange.globalDepth}");
                                }
                                break;
                            case Backtracking.Algorithm.Knapsack:
                                {
                                    /*int volume = 9;
                                    List<Item> lstItems = new List<Item>()
                                    {
                                        new Item(){ Volume=2, Value=3},
                                        new Item(){ Volume=4, Value=5},
                                        new Item(){ Volume=4, Value=7},
                                        new Item(){ Volume=6, Value=8}
                                    };*/

                                    for (int k = 50; k <= 70; k++)
                                    {
                                        int volume = 100;
                                        List<Item> lstItems = new List<Item>();
                                        Random rnd = new Random(k);
                                        //int k = 20; // For n=120, we need 23 seconds for recursive backtracking
                                        for (int i = 0; i < k; i++)
                                        {
                                            lstItems.Add(new Item() { Volume = rnd.Next(10, 40), Value = rnd.Next(5, 10) });
                                        }

                                        Stopwatch sw = Stopwatch.StartNew();
                                        int resultDyn = Dynamic.KnapsackProblemDyn(volume, lstItems, lstItems.Count - 1, new Dictionary<(int, int), int>());
                                        Console.WriteLine($"DYNAMIC: The maximum value of the knapsack is {resultDyn}.");
                                        Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");

                                        sw = Stopwatch.StartNew();
                                        int resultBackRec = Backtracking.KnapsackProblemRec(volume, lstItems, lstItems.Count);
                                        Console.WriteLine($"Recursive: The maximum value of the knapsack is {resultBackRec}.");
                                        Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");

                                        sw = Stopwatch.StartNew();
                                        //int resultBack = Backtracking.KnapsackProblemNonRec(volume, lstItems);
                                        //Console.WriteLine($"Backtracking: The maximum value of the knapsack is {resultBack}.");
                                        Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");

                                        sw = Stopwatch.StartNew();
                                        int resultBackOpt = Backtracking.KnapsackProblem(volume, lstItems);
                                        Console.WriteLine($"Backtracking: The maximum value of the knapsack is {resultBackOpt}.");
                                        Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");

                                        if (resultBackOpt != resultBackRec || resultBackOpt != resultDyn)
                                        {

                                        }

                                        /*sw = Stopwatch.StartNew();
                                        result = Dynamic.KnapsackProblemRec(volume, lstItems, lstItems.Count - 1);
                                        Console.WriteLine($"RECURSIVE: The maximum value of the knapsack is {result}.");                                    
                                        Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");*/
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case Chapter.BranchAndBound:
                    {
                        Console.WriteLine("Testing branch and bound");

                        switch (InterfaceFunctions.ChooseSection<BranchAndBound.Algorithm>())
                        {
                            case BranchAndBound.Algorithm.General:
                                {
                                    int n = 50;
                                    int m = 100;
                                    Graph g = TestCasesGenerator.GenerateRandomGraph(n, m, 0);
                                }
                                break;
                            case BranchAndBound.Algorithm.BreadthFirstSearch:
                                {
                                    BinaryTree tree = TestCasesGenerator.GenerateRandomBinaryTree(10, 20, 1);

                                    Console.WriteLine("BFS");
                                    BranchAndBound.BreadthFirstSearch(tree);

                                    Console.WriteLine("DFS");
                                    Backtracking.DepthFirstSearch(tree);
                                }
                                break;
                            case BranchAndBound.Algorithm.KnapsackProblem:
                                {
                                    /*int volume = 9;
                                    List<Item> lstItems = new List<Item>()
                                    {
                                        new Item(){ Volume=2, Value=3},
                                        new Item(){ Volume=4, Value=5},
                                        new Item(){ Volume=4, Value=7},
                                        new Item(){ Volume=6, Value=8}
                                    };*/

                                    int volume = 1000;
                                    List<Item> lstItems = new List<Item>();
                                    Random rnd = new Random(1);
                                    int k = 35; // If volume is 1000, then for k = 50, B&B takes 25 seconds to execute (also queue has many items), dynamic prog. executes within a second
                                    Console.WriteLine($"Knapsack volume: {volume}");
                                    for (int i = 0; i < k; i++)
                                    {
                                        Item item = new Item() { Volume = rnd.Next(5, 10), Value = rnd.Next(10, 20) }; // k=40 takes 45 seconds
                                        //Item item = new Item() { Volume = rnd.Next(5, 10), Value = rnd.Next(1, 10) }; // k=40 takes 1 second
                                        lstItems.Add(item);
                                        Console.WriteLine($"Item: {item}");
                                    }


                                    // Branch and bound
                                    Stopwatch sw = Stopwatch.StartNew();
                                    int max = BranchAndBound.KnapsackProblem(volume, lstItems);
                                    Console.WriteLine($"B&B: Optimal solution is {max}. \t (Time: {sw.Elapsed.TotalSeconds} s)");

                                    // Dynamic
                                    sw = Stopwatch.StartNew();
                                    int maxDyn = Dynamic.KnapsackProblemDyn(volume, lstItems, lstItems.Count - 1, new Dictionary<(int MaxIndex, int Volume), int>());
                                    Console.WriteLine($"DYN: Optimal solution is {maxDyn}. \t (Time: {sw.Elapsed.TotalSeconds} s)");

                                    // Backtracking
                                    sw = Stopwatch.StartNew();
                                    int maxBack = Backtracking.KnapsackProblem(volume, lstItems);
                                    Console.WriteLine($"BACK: Optimal solution is {maxBack}. \t (Time: {sw.Elapsed.TotalSeconds} s)");

                                    if (max != maxDyn)
                                        throw new Exception("Error in one of the algs!");
                                }
                                break;
                        }
                    }
                    break;
                default:
                    {
                        var matrix = TestCasesGenerator.Generate2DMatrix(50, 50, 0, 100);
                        TestCasesGenerator.Write2DMatrixToFile(matrix, "matrixBig.txt");
                        break;
                    }
            }
            Console.WriteLine("\nTesting done.");
            Console.Read();
        }
    }
}
