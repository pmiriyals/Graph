using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
    class Dijkstras
    {
        private static void swapNode(List<Node<int>> nodeset, int i, int j)
        {
            Node<int> temp = nodeset[i];
            nodeset[i] = nodeset[j];
            nodeset[j] = temp;
        }

        private static void Minheapify(List<Node<int>> nodeset)
        {
            int end = nodeset.Count - 2;
            int start = (end) / 2; //get last parent, that is for node at index 'k', its child will be 2k+1, 2k+2
            while (start >= 0)
            {
                siftdown(nodeset, start, end);
                start--;
            }
        }

        private static void siftdown(List<Node<int>> nodeset, int start, int end)
        {
            int root = start;
            while (((root * 2) + 1) <= end)
            {
                int swap = root;
                int child = 2 * root + 1;
                if (nodeset[swap].key > nodeset[child].key)
                    swap = child;
                if (child + 1 <= end && nodeset[swap].key > nodeset[child + 1].key)
                    swap = child + 1;
                if (swap != root)
                {
                    swapNode(nodeset, swap, root);
                    root = swap;
                }
                else
                    return;

            }
        }

        public static Graph<int> DijkstrasAlg(Graph<int> graph)
        {
            List<Node<int>> nodeset = graph.nodeset.ToList<Node<int>>();
            Graph<int> sptGraph = new Graph<int>();

            while (nodeset.Count > 0)
            {
                Minheapify(nodeset);
                Node<int> root = nodeset[0];
                nodeset.RemoveAt(0);
                Node<int> sptNode = new Node<int>(root.data);

                if (root.parent != null)
                {
                    sptGraph.AddEdge(sptNode, root.parent, root.key);
                    root.parent = null;
                }

                for (int i = 0; i < root.neighbors.Count; i++)
                {
                    if (root.neighbors[i].key > (root.cost[i] + root.key))
                    {
                        root.neighbors[i].key = root.cost[i] + root.key;
                        root.neighbors[i].parent = sptNode;
                    }
                }
            }

            sptGraph.printGraph();
            return sptGraph;
        }

        public static void driver()
        {
            Graph<int> graph = Graph<int>.buildTree();
            DijkstrasAlg(graph);
        }
    }
}
