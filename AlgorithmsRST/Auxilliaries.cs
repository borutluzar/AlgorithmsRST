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
    public struct Item
    {
        public int Volume { get; set; }

        public int Value { get; set; }

        public override string ToString()
        {
            return $"Vol: {Volume}, Val: {Value}";
        }
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

    public class BinaryTree
    {
        public BinaryNode Root { get; set; }

        public int CountNodes()
        {
            int numnNodes = 0;
            if (this.Root != null)
            {
                Queue<BinaryNode> qNodes = new Queue<BinaryNode>();
                qNodes.Enqueue(this.Root);
                while (qNodes.Count > 0)
                {
                    numnNodes++;
                    var node = qNodes.Dequeue();
                    if (node.LeftSon != null) qNodes.Enqueue(node.LeftSon);
                    if (node.RightSon != null) qNodes.Enqueue(node.RightSon);
                }
            }
            return numnNodes;
        }
    }

    public class BinaryNode
    {
        public BinaryNode() : this(null) { }

        public BinaryNode(object value)
        {
            this.Value = value;
        }

        public object Value { get; set; }

        public int Level { get; set; }

        public BinaryNode LeftSon { get; set; }

        public BinaryNode RightSon { get; set; }

        public override string ToString()
        {
            return $"[{Value}], Level: {Level}";
        }
    }

    public class Graph
    {
        public Graph(HashSet<int> verts) : this(verts, new HashSet<Edge>()) { }

        public Graph(HashSet<int> verts, HashSet<Edge> edges)
        {
            this.Vertices = verts;
            this.Edges = edges;
            this.Neighbors = new Dictionary<int, HashSet<int>>();

            foreach (var v in this.Vertices)
                this.Neighbors.Add(v, new HashSet<int>());
        }

        public HashSet<int> Vertices { get; }

        public HashSet<Edge> Edges { get; }

        public Dictionary<int, HashSet<int>> Neighbors { get; }

        public void AddEdge(int v1, int v2)
        {
            if (this.Vertices.Contains(v1) && this.Vertices.Contains(v2))
            {
                this.Edges.Add(new Edge(v1, v2));
                this.Neighbors[v1].Add(v2);
                this.Neighbors[v2].Add(v1);
            }
            else
                throw new Exception("No such vertices in graph!");
        }
    }

    public struct Edge
    {
        public Edge(int v1, int v2)
        {
            this.Start = Math.Min(v1, v2);
            this.End = Math.Max(v1, v2);
        }

        public int Start { get; }

        public int End { get; }

        public override string ToString()
        {
            return $"({Start}, {End})";
        }
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

        /// <summary>
        /// Generates a random binary tree with the given number of nodes.
        /// </summary>
        /// <param name="numNodes">Number of nodes in the tree</param>
        /// <param name="maxValue">Maximum node value (randomly selected between 0 and maxValue). If maxValue = 0, then null is set</param>
        /// <param name="seed">Seed for random generator</param>
        /// <returns>The generated tree.</returns>
        public static BinaryTree GenerateRandomBinaryTree(int numNodes, int maxValue = 0, int? seed = null)
        {
            Random rnd = seed == null ? new Random() : new Random(seed.Value);
            int rootValue = rnd.Next(0, maxValue + 1);
            BinaryTree tree = new BinaryTree() { Root = new BinaryNode(maxValue == 0 ? null : rootValue) };
            
            BinaryNode currentNode = tree.Root;
            List<BinaryNode> lstNodes = new List<BinaryNode>() { currentNode };

            while (lstNodes.Count < numNodes)
            {
                currentNode = lstNodes[rnd.Next(0, lstNodes.Count)];
                int addNode = rnd.Next(0, 2);
                int nodeValue = rnd.Next(0, maxValue + 1);

                if (addNode == 0 && currentNode.LeftSon == null)
                {
                    currentNode.LeftSon = new BinaryNode(maxValue == 0 ? null : nodeValue);
                    lstNodes.Add(currentNode.LeftSon);
                }
                else if (addNode == 1 && currentNode.RightSon == null)
                {
                    currentNode.RightSon = new BinaryNode(maxValue == 0 ? null : nodeValue);
                    lstNodes.Add(currentNode.RightSon);
                }
            }
            return tree;
        }

        public static List<int> GenerateRandomListOfIntegers(int numNumbers, int min, int max, bool distinct, int? seed = null)
        {
            HashSet<int> lstNums = new HashSet<int>();
            Random rnd = seed == null ? new Random() : new Random(seed.Value);

            while (lstNums.Count < numNumbers)
            {
                int num = rnd.Next(min, max + 1);

                if (distinct && !lstNums.Contains(num) || !distinct)
                    lstNums.Add(num);
            }

            return lstNums.ToList();
        }

        /// <summary>
        /// Generates a graph with a given number of vertices and edges.
        /// </summary>
        public static Graph GenerateRandomGraph(int numVertices, int numEdges, int? seed = null)
        {
            if (numEdges > (numVertices * (numVertices - 1)) / 2)
                throw new Exception("Too many edges!");

            Random rnd = seed == null ? new Random() : new Random(seed.Value);

            Graph g = new Graph(Enumerable.Range(1, numVertices).ToHashSet());
            int edgCount = 0;
            while (edgCount < numEdges)
            {
                int v1 = rnd.Next(1, numVertices + 1);
                int v2 = rnd.Next(1, numVertices + 1);
                if (v1 != v2 && !g.Neighbors[v1].Contains(v2))
                {
                    g.AddEdge(v1, v2);
                    edgCount++;
                }
            }
            return g;
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

        public static string ToString<T>(this List<T> lst, int round = -1)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("(");
            for (int i = 0; i < lst.Count; i++)
            {
                if (i > 0)
                    sb.Append(", ");

                if (round < 0)
                    sb.Append(lst[i]);
                else
                {
                    if (lst is double[])
                    {
                        var arrD = lst as double[];
                        sb.Append(Math.Round(arrD[i], round, MidpointRounding.AwayFromZero));
                    }
                    if (lst is int[])
                    {
                        var arrD = lst as int[];
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

    public static class InterfaceFunctions
    {
        /// <summary>
        /// Funkcija izpiše možne sekcije in prebere izbiro uporabnika.
        /// Tip enumeracije ji podamo preko parametra generičnega tipa T.
        /// </summary>
        public static T ChooseSection<T>()
        {
            // Izpis sekcij za izbiro 
            int i = 1;
            Console.WriteLine("--\t--\t--\t--");
            Console.WriteLine($"{typeof(T).Name}:\n");
            foreach (var section in Enum.GetValues(typeof(T)))
            {
                var value = Convert.ChangeType(section, Type.GetTypeCode(typeof(T)));
                Console.WriteLine($"{value}. {section}");
                i++;
            }
            Console.WriteLine("\n--\t--\t--\t--");
            Console.Write($"Choose {typeof(T).Name} to run: ");

            string input = Console.ReadLine();
            bool isFormatCorrect = int.TryParse(input, out int chosen);
            if (!isFormatCorrect)
            {
                Console.WriteLine($"\n The input {input} is not an integer! The execution is stopped.");
                return default;
            }
            else if (chosen < 0 || chosen > i)
            {
                Console.WriteLine($"\n There is no Section {input}! The execution is stopped.");
                return default;
            }

            Console.Write("\n");
            Console.WriteLine($"Running {typeof(T).Name} {(T)(object)chosen}...");
            Console.Write("\n\n");

            // Pretvorba (cast) iz int nazaj v enumeracijo ni možna 
            // neposredno iz int (saj je enumeracija lahko kakega drugega
            // celoštevilskega tipa), zato chosen najprej pretvorimo v
            // object in šele nato v T.
            return (T)(object)chosen;
        }
    }
}
