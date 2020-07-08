using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra_s_algorithm
{
    class Program
    {
        /// <summary>
        /// This is the solution to the following hackerrank problem, solved implementing Dijkstra's algorithm:
        /// https://www.hackerrank.com/challenges/ctci-bfs-shortest-reach/problem
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            List<NodeTree> nodeTrees = new List<NodeTree>();

            using (var reader = new System.IO.StreamReader("../../../inputTestCase2.txt"))
            {
                int queries = int.Parse(reader.ReadLine().TrimEnd());

                for (int i = 0; i < queries; i++)
                {
                    var inputData = reader.ReadLine().Split(' ');
                    int nodeCount = int.Parse(inputData[0]), edges = int.Parse(inputData[1]);

                    Dictionary<int, Node> nodes = new Dictionary<int, Node>();

                    for (int j = 1; j <= nodeCount; j++)
                    {
                        nodes.Add(j, new Node());
                    }

                    var currentNodeTree = new NodeTree(nodes);

                    for (int j = 0; j < edges; j++)
                    {
                        inputData = reader.ReadLine().Split(' ');
                        int parentNodeValue = int.Parse(inputData[0]), childNodeValue = int.Parse(inputData[1]);
                        currentNodeTree.AddChildToNode(parentNodeValue, childNodeValue);
                    }

                    int startingNode = int.Parse(reader.ReadLine());
                    currentNodeTree.SetStartingNode(startingNode);

                    currentNodeTree.SetDistancesFromStartingNode();
                    nodeTrees.Add(currentNodeTree);
                }
            }

            foreach (var tree in nodeTrees)
            {
                Console.WriteLine(tree.GetDistances());
            }

            Console.ReadKey();
        }
    }
}
