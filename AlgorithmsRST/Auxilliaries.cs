﻿using System;
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

    public class Rectangle
    {
        /// <summary>
        /// First coordinate of the top left corner
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Second coordinate of the top left corner
        /// </summary>
        public int Y { get; set; }

        public int LengthX { get; set; }

        public int LengthY { get; set; }

        public int Area { get => this.LengthX * this.LengthY; }
    }



    public static class TestCasesGenerator
    {
        /// <summary>
        /// Generates an m x n matrix with random entries between min and max (both inclusive)
        /// </summary>
        public static int[,] Generate2DMatrix(int m, int n, int rndMin, int rndMax, int? seed = null)
        {
            int[,] matrix = new int[m, n];
            Random rnd = seed == null ? new Random() : new Random(seed.Value);

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    matrix[i, j] = rnd.Next(rndMin, rndMax + 1);

            return matrix;
        }
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

        public static T[] GetColumn<T>(this T[,] matrix, int column)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, column])
                    .ToArray();
        }

        public static T[] GetRow<T>(this T[,] matrix, int row)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[row, x])
                    .ToArray();
        }

        /// <summary>
        /// Given the value, all matrix entries with the value are
        /// replaced with ones, the others are replaced by zeros.
        /// </summary>
        public static int[,] SetOnesInMatrix(this int[,] matrix, int valueToReplace)
        {
            int[,] newMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == valueToReplace)
                        newMatrix[i, j] = 1;
                    else
                        newMatrix[i, j] = 0;
                }
            }
            return newMatrix;
        }
    }
}
