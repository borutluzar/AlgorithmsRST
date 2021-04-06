using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borut.Lectures.AlgorithmsRST
{
    /// <summary>
    /// The main enumeration for selecting tests.
    /// </summary>
    public enum Chapter
    {
        GreedyAlgorithm,
        DivideAndConquer,
        DynamicProgramming,
        BranchAndBound,
        Backtracking,
        NetworkFlows
    }

    /// <summary>
    /// The item class will often be used.
    /// </summary>
    public class Item
    {
        public int Volume { get; set; }

        public int Value { get; set; }
    }

    public class Task
    {
        public int Start { get; set; }

        public int End { get; set; }
    }

    public static class ExtensionMethods
    {
        public static string ToString<T>(this T[] array, int round = -1)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("(");
            for (int i = 0; i < array.Length; i++)
            {
                if (i > 0)
                    sb.Append(", ");

                if (round < 0)
                    sb.Append(array[i]);
                else
                {
                    if (array is double[])
                    {
                        var arrD = array as double[];
                        sb.Append(Math.Round(arrD[i], round, MidpointRounding.AwayFromZero));
                    }
                    if (array is int[])
                    {
                        var arrD = array as int[];
                        sb.Append(arrD[i]);
                    }
                }
            }
            sb.Append(")");

            return sb.ToString();
        }
    }
}
