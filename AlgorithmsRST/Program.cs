using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Borut.Lectures.AlgorithmsRST
{
    public class Program
    {
        static void Main(string[] args)
        {
            Chapter chTests = Chapter.Backtracking;

            switch (chTests)
            {
                case Chapter.GreedyAlgorithm:
                    {
                        Console.WriteLine("Testing greedy method");
                        Greedy.Algorithm algType = Greedy.Algorithm.FractionalKnapsack;

                        switch (algType)
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
                        DivideAndConquer.Algorithm algType = DivideAndConquer.Algorithm.MaxSubsequenceSumLin;

                        switch (algType)
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
                                    var max = DivideAndConquer.MaxSubsequenceSumLinear(list);
                                    Console.WriteLine($"The maximum sum is {max}.");
                                }
                                break;
                        }
                    }
                    break;
                case Chapter.DynamicProgramming:
                    {
                        Console.WriteLine("Testing dynamic programming");
                        Dynamic.Algorithm algType = Dynamic.Algorithm.MaxSolidBlock;

                        switch (algType)
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
                            case Dynamic.Algorithm.FibonacciList:
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

                                    int volume = 10;
                                    List<Item> lstItems = new List<Item>()
                                    {
                                        new Item(){ Volume=3, Value=5},
                                        new Item(){ Volume=4, Value=6},
                                        new Item(){ Volume=5, Value=7},
                                        new Item(){ Volume=6, Value=8},
                                    };

                                    int[] x = new int[lstItems.Count];

                                    Stopwatch sw = Stopwatch.StartNew();
                                    (int result, int[] xOut) = Dynamic.KnapsackProblemRecVec(volume, lstItems, lstItems.Count - 1, x);
                                    //int result  = Dynamic.KnapsackProblemRec(volume, lstItems, lstItems.Count - 1);
                                    Console.WriteLine($"The maximum value of the knapsack is {result}.");
                                    Console.WriteLine($"Vector of selected elements is {xOut.ToString<int>()}.");
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
                                    int k = 1000;
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

                                    /*sw = Stopwatch.StartNew();
                                    result = Dynamic.KnapsackProblemRec(volume, lstItems, lstItems.Count - 1);
                                    Console.WriteLine($"RECURSIVE: The maximum value of the knapsack is {result}.");                                    
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");*/
                                }
                                break;
                            case Dynamic.Algorithm.ShuffleString:
                                {
                                    //string a = "borut", b = "lužar", c = "blouržuatr";
                                    //string a = "1234", b = "test", c = "t123e4st";
                                    string a = "abababababaababababababa", b = "ababaababababababacababaaba", c = "ababaabababababababababaababaababababacaabababababa";

                                    Stopwatch sw = Stopwatch.StartNew();
                                    bool result = Dynamic.ShuffleStringRec(a, b, c, a.Length - 1, b.Length - 1);
                                    Console.WriteLine($"String \"{c}\" is{(result ? "" : " NOT")} a shuffle of \"{a}\" and \"{b}\".");
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");
                                    sw = Stopwatch.StartNew();
                                    bool result2 = Dynamic.ShuffleStringDyn(a, b, c);
                                    Console.WriteLine($"String \"{c}\" is{(result2 ? "" : " NOT")} a shuffle of \"{a}\" and \"{b}\".");
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");
                                }
                                break;
                            case Dynamic.Algorithm.VankinsMile:
                                {
                                    int[,] matrix = new int[,]
                                        {
                                            { 1, 3, -2, 3 },
                                            { 2, 6, -2, 1 },
                                            {-1,-2,  1, -2 }
                                        };

                                    //int[,] matrix = TestCasesGenerator.Generate2DMatrix(1000, 1000, 0, 10, 5);

                                    Stopwatch sw = Stopwatch.StartNew();
                                    /*int result = Dynamic.VankinsMileRec(matrix, 0, 0);
                                    Console.WriteLine($"RECURSIVE: The optimal path has value {result}.");*/
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");

                                    sw = Stopwatch.StartNew();
                                    int result2 = Dynamic.VankinsMileDyn(matrix, 0, 0, new Dictionary<(int X, int Y), int>());
                                    Console.WriteLine($"DYNAMIC: The optimal path has value {result2}.");
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");
                                }
                                break;
                            case Dynamic.Algorithm.IsomorphicTrees:
                                {
                                    BinaryTree treeA = TestCasesGenerator.GenerateRandomBinaryTree(500, 0);
                                    BinaryTree treeB = TestCasesGenerator.GenerateRandomBinaryTree(500, 1);

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

                                    int[,] matrix = TestCasesGenerator.Generate2DMatrix(10000, 10000, 0, 1, 5);

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
                        }
                    }
                    break;
                case Chapter.Backtracking:
                    {
                        Console.WriteLine("Testing backtracking");
                        Backtracking.Algorithm algType = Backtracking.Algorithm.NQueens;

                        switch (algType)
                        {
                            case Backtracking.Algorithm.NQueens:
                                {
                                    int n = 15;
                                    Stopwatch sw;
                                    for (int i = 1; i <= n; i++)
                                    {
                                        sw = Stopwatch.StartNew();
                                        int numPositions = Backtracking.NQueens(i, new List<int>());
                                        Console.WriteLine($"Number of solutions for {i} queens is {numPositions}. \t (Time: {sw.Elapsed.TotalSeconds} s)");
                                    }
                                }
                                break;
                            case Backtracking.Algorithm.SubsetSum:
                                {
                                    //int n = 10;
                                    //List<int> lstCandidates = new List<int>() { 3, 4, 5 };

                                    for (int i = 0; i < 1; i++)
                                    {
                                        int n = 50_000_000;
                                        List<int> lstCandidates = TestCasesGenerator.GenerateRandomListOfIntegers(3_000, 1, 100_000, true, i);


                                        Stopwatch sw = Stopwatch.StartNew();
                                        (bool hasSum, List<int> lstSumands) = Backtracking.SubsetSum(n, lstCandidates);
                                        //bool hasSumRec = Backtracking.SubsetSumRec(n, lstCandidates, lstCandidates.Count);

                                        //if (hasSum != hasSumRec) throw new Exception();
                                            //Console.WriteLine($"NAPAKA!!");

                                        if (hasSum)
                                            Console.WriteLine($"The number {n} IS sum of numbers {lstSumands.ToString<int>()}. \t (Time: {sw.Elapsed.TotalSeconds} s)");
                                        else
                                            Console.WriteLine($"The number {n} IS NOT sum of any given numbers! \t (Time: {sw.Elapsed.TotalSeconds} s)");
                                    }
                                }
                                break;
                            case Backtracking.Algorithm.Exercise1:
                                {
                                    int n = 30;
                                    Stopwatch sw;

                                    sw = Stopwatch.StartNew();
                                    var position = Backtracking.NQueensOnePosition(n, new List<int>());
                                    if (position.Count == n)
                                        Console.WriteLine($"A solution for {n} queens is {position.ToString<int>()}. \t (Time: {sw.Elapsed.TotalSeconds} s)");
                                    else
                                        Console.WriteLine($"A solution for {n} queens does not exist. \t (Time: {sw.Elapsed.TotalSeconds} s)");

                                }
                                break;
                        }
                    }
                    break;
            }
            Console.WriteLine("\nTesting done.");
            Console.Read();
        }
    }
}
