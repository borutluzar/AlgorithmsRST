using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Borut.Lectures.AlgorithmsRST
{
    public class Program
    {
        static void Main(string[] args)
        {
            Chapter chTests = Chapter.DynamicProgramming;

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
                        Dynamic.Algorithm algType = Dynamic.Algorithm.ShuffleString;

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

                                    /*int volume = 35;
                                    List<Item> lstItems = new List<Item>()
                                    {
                                        new Item(){ Volume=2, Value=3},
                                        new Item(){ Volume=3, Value=5},
                                        new Item(){ Volume=4, Value=7},
                                        new Item(){ Volume=4, Value=3},
                                        new Item(){ Volume=3, Value=4},
                                        new Item(){ Volume=5, Value=4},
                                        new Item(){ Volume=4, Value=2},
                                        new Item(){ Volume=6, Value=7},
                                        new Item(){ Volume=3, Value=4},
                                        new Item(){ Volume=2, Value=4},
                                        new Item(){ Volume=6, Value=5},
                                        new Item(){ Volume=6, Value=8},
                                        new Item(){ Volume=5, Value=6},
                                        new Item(){ Volume=4, Value=5},
                                        new Item(){ Volume=3, Value=4},
                                        new Item(){ Volume=2, Value=5},
                                        new Item(){ Volume=3, Value=6},
                                        new Item(){ Volume=4, Value=7},
                                        new Item(){ Volume=5, Value=8},
                                        new Item(){ Volume=9, Value=8},
                                        new Item(){ Volume=8, Value=8},
                                        new Item(){ Volume=7, Value=7},
                                        new Item(){ Volume=6, Value=6},
                                        new Item(){ Volume=5, Value=5},
                                        new Item(){ Volume=4, Value=4},
                                        new Item(){ Volume=3, Value=5},
                                        new Item(){ Volume=7, Value=6},
                                        new Item(){ Volume=8, Value=7},
                                        new Item(){ Volume=9, Value=8}
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
                                    string a = "1234", b = "test", c = "t123e4st";
                                    //string a = "abababababaababababababa", b = "ababaababababababacababaaba", c = "ababaabababababababababaababaababababacaabababababa";
                                    //string a = "abababababaababababababaabababababaababababababa", b = "abababababaababababababaababaababababababacababaaba", c = "abababababaababababababaabababababaababababababaababaabababababababababaababaababababacaabababababa";
                                    Stopwatch sw = Stopwatch.StartNew();
                                    //bool result = Dynamic.ShuffleStringRec(a, b, c, a.Length - 1, b.Length - 1);
                                    bool result = Dynamic.ShuffleStringDyn(a, b, c);
                                    Console.WriteLine($"String \"{c}\" is{(result ? "" : " NOT")} a shuffle of \"{a}\" and \"{b}\".");
                                    Console.WriteLine($"Result computed in {sw.Elapsed.TotalSeconds} seconds.");
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
