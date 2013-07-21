using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{    
    class Prims
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
            int start = (end)/2; //get last parent, that is for node at index 'k', its child will be 2k+1, 2k+2
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
                int child = 2*root + 1;
                if (nodeset[swap].key > nodeset[child].key)
                    swap = child;
                if (child+1 <= end && nodeset[swap].key > nodeset[child + 1].key)
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

        //Time: O(E logV) using adjacency list rep (using BFS on keys)
        //Find minimum spanning tree (connected nodes with min cost edges, E = V-1)
        //1. Place the graph in min heap (using keys)
        //2. Extract the min key node and add to MST graph
        //3. Update the keys of adjacent nodes (if key > cost then update)
        //4. Minheapify the remaining nodes in the graph and repeat from step 2 until the entire list is empty
        private static Graph<int> PrimsAlg(Graph<int> graph)
        {
            Graph<int> mstGraph = new Graph<int>(); //Generate a new MST graph
            List<Node<int>> nodeset = graph.nodeset.ToList<Node<int>>();    //Create a new list of nodes from existing graph, so that we don't modify the given graph
            while (nodeset.Count > 0)
            {
                Minheapify(nodeset);    //Place the list in MinHeap
                Node<int> root = nodeset[0];    //Extract Min node
                Node<int> mstNode = new Node<int>(root.data);   //Create a new node for MST graph
                if (root.parent != null)    //Add nodes and edges between them
                {
                    mstGraph.AddEdge(mstNode, root.parent, root.key);   
                    root.parent = null; //reset the parent node
                }

                nodeset.RemoveAt(0);    //Extract root node (remove from list)

                for(int i = 0; i< root.neighbors.Count; i++)
                {
                    if (root.neighbors[i].key > root.cost[i])   //Update keys of neighboring nodes if the min node
                    {
                        root.neighbors[i].key = root.cost[i];
                        root.neighbors[i].parent = mstNode;
                    }
                }
            }
            mstGraph.printGraph();
            return mstGraph;
        }
        
        public static void driver()
        {
            Graph<int> graph = Graph<int>.buildTree();
            PrimsAlg(graph);
        }
    }
}
