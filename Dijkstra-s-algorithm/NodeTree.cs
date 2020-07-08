using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra_s_algorithm
{
    internal class NodeTree
    {
        internal readonly static int DEFAULT_DISTANCE = 6;

        public NodeTree(Dictionary<int, Node> nodes)
        {
            Nodes = nodes;
        }

        private int StartNode { get; set; }

        //Dictionary necessary to speed up node retrieval
        private Dictionary<int, Node> Nodes { get; set; }

        private Node GetNodeWithValue(int value)
        {
            return Nodes[value];
        }

        public void SetStartingNode(int startNodeValue)
        {
            var startNode = GetNodeWithValue(startNodeValue);
            startNode.Distance = 0;
            StartNode = startNodeValue;
        }

        public void SetDistancesFromStartingNode()
        {
            Nodes[StartNode].SetConnectionsDistance();

            while (Nodes.Any(n => !n.Value.Visited))
            {
                var currentNode = (from n in Nodes where !n.Value.Visited orderby n.Value.Distance select n).FirstOrDefault();
                currentNode.Value.SetConnectionsDistance();
            }
        }

        public void AddChildToNode(int nodeAValue, int nodeBValue)
        {
            Node nodeA = GetNodeWithValue(nodeAValue), nodeB = GetNodeWithValue(nodeBValue);

            nodeA.AddConnection(nodeB);
            nodeB.AddConnection(nodeA);
        }

        /// <summary>
        /// Return distances from each node, sequentially from the starting Node
        /// </summary>
        /// <returns>Distances for each node, sequentially, separated by a space</returns>
        public string GetDistances()
        {
            StringBuilder distances = new StringBuilder();

            foreach (var node in (from n in Nodes where n.Key != StartNode orderby n.Key select n))
            {
                if (node.Value.Distance == int.MaxValue)
                {
                    distances.Append("-1 ");
                }
                else
                {
                    distances.Append(string.Concat(node.Value.Distance.ToString(), " "));
                }
            }

            return distances.ToString();
        }
    }

    internal class Node
    {
        public Node()
        {
            ConnectedNodes = new List<Node>();
            Distance = int.MaxValue;
        }

        public int Distance { get; set; }

        public List<Node> ConnectedNodes { get; private set; }

        public bool Visited { get; private set; }

        public void AddConnection(Node node)
        {
            ConnectedNodes.Add(node);
        }

        public void SetConnectionsDistance()
        {
            Visited = true;

            int distance = Distance;

            if (Distance < int.MaxValue)
            {
                distance += NodeTree.DEFAULT_DISTANCE;
            }

            foreach (var node in (from n in ConnectedNodes where n.Distance > distance select n))
            {
                node.Distance = distance;
            }
        }
    }
}
