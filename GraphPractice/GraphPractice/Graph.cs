using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphPractice
{
    public class Node
    {
        public int data { get; set; }
        public Dictionary<Node, int> edgesTo = new Dictionary<Node, int>();
        public bool isVisted { get; set; }
        public int key { get; set; }
        public Node parent { get; set; }

        public Node(int data)
        {
            this.data = data;
            isVisted = false;
            parent = null;
            key = Int32.MaxValue;
        }

        public void AddNeighbor(Node n, int cost)
        {
            if (!edgesTo.ContainsKey(n))
                edgesTo.Add(n, cost);
        }
    }
    
    public class Graph
    {
        public Node root { get; set; }
        public List<Node> nodeset = new List<Node>();

        public Graph(Node root)
        {
            this.root = root;
            if (root != null)
                root.key = 0;
        }

        public void AddEdge(Node n1, Node n2, int cost)
        {
            n1.AddNeighbor(n2, cost);
            n2.AddNeighbor(n1, cost);
            if (!nodeset.Contains(n1))
                nodeset.Add(n1);
            if (!nodeset.Contains(n2))
                nodeset.Add(n2);
        }

        private void heapify(List<Node> nodes)
        {
            int end = nodes.Count - 1;
            int start = (end - 1) / 2;

            while (start >= 0)
            {
                siftDown(nodes, start, end);
                start--;
            }
        }

        private void siftDown(List<Node> nodes, int start, int end)
        {
            int root = start;
            while ((root * 2 + 1) <= end)
            {
                int swap = root;
                int child = (2*root+1);

                if (nodes[swap].key > nodes[child].key)
                    swap = child;
                if ((child + 1) <= end && nodes[swap].key > nodes[child + 1].key)
                    swap = child + 1;

                if (root != swap)
                {
                    Node n = nodes[root];
                    nodes[root] = nodes[swap];
                    nodes[swap] = n;
                    root = swap;
                }
                else
                    return;
            }
        }

        public Graph Prims()
        {
            Graph mst = new Graph(null);
            List<Node> temp = new List<Node>();
            Node cur;
            heapify(nodeset);

            while (nodeset.Count > 0)
            {
                cur = nodeset[0];   //extract min
                nodeset.RemoveAt(0);
                temp.Add(cur);
                Node n = new Node(cur.data);
                if (cur.parent != null)
                {
                    mst.AddEdge(cur.parent, n, cur.key);
                    cur.parent = null;
                }
                else
                {
                    mst.nodeset.Add(n);
                    mst.root = n;
                }

                foreach (KeyValuePair<Node, int> kv in cur.edgesTo)
                {
                    if (kv.Key.key > kv.Value)
                    {
                        kv.Key.key = kv.Value;
                        kv.Key.parent = n;
                    }
                }
                heapify(nodeset);
                //siftDown(nodeset, 0, nodeset.Count - 1);
            }
            this.nodeset = temp;
            return mst;
        }

        public void PrintGraphBFS()
        {
            Queue<Node> queue = new Queue<Node>();
            Node cur = null;
            queue.Enqueue(root);
            root.isVisted = true;
            Console.WriteLine("\nBreadth First Search Traversal of Graph: ");
            while (queue.Count > 0)
            {
                cur = queue.Dequeue();
                Console.Write("{0} ", cur.data);

                foreach(Node n in cur.edgesTo.Keys)
                {
                    if (!n.isVisted)
                    {
                        n.isVisted = true;
                        queue.Enqueue(n);
                    }
                }
            }
            ResetVisitedFlags();
        }

        public void PrintGraphDFS()
        {
            Stack<Node> stk = new Stack<Node>();
            Node cur = null;
            stk.Push(root);
            root.isVisted = true;
            Console.WriteLine("\n\nDepth First Search Traversal of Graph: ");
            while (stk.Count > 0)
            {
                cur = stk.Pop();
                Console.Write("{0} ", cur.data);

                foreach (Node n in cur.edgesTo.Keys)
                {
                    if (!n.isVisted)
                    {
                        n.isVisted = true;
                        stk.Push(n);
                    }
                }
            }
            ResetVisitedFlags();
        }

        public void PrintEdges()
        {
            Node cur = root;
            Console.WriteLine("\nEdges of current graph:\n ");
            foreach (Node n in nodeset)
            {
                n.isVisted = true;
                foreach (KeyValuePair<Node, int> kv in n.edgesTo)
                    if(!kv.Key.isVisted)
                        Console.WriteLine("Edge from {0} to {1} and cost = {2}", n.data, kv.Key.data, kv.Value);
                Console.WriteLine();
            }
            ResetVisitedFlags();
        }

        private void ResetVisitedFlags()
        {
            foreach (Node n in nodeset)
                n.isVisted = false;
        }
    }

    public class Driver
    {
        public static void driver()
        {
            Graph graph = buildTree();
            graph.PrintGraphBFS();
            graph.PrintGraphDFS();
            //graph.PrintEdges();

            Graph mst = graph.Prims();
            Console.WriteLine("\nPrims MST: ");
            mst.PrintGraphBFS();
            mst.PrintEdges();
        }

        public static Graph buildTree()
        {
            Node n = new Node(0);
            Graph graph = new Graph(n);            
            graph.root = n;
            graph.root.key = 0;
            Node n1 = new Node(1);
            Node n2 = new Node(2);
            Node n3 = new Node(3);
            Node n4 = new Node(4);
            Node n5 = new Node(5);
            Node n6 = new Node(6);
            Node n7 = new Node(7);
            Node n8 = new Node(8);

            graph.AddEdge(n, n1, 4);
            graph.AddEdge(n, n7, 8);
                        
            graph.AddEdge(n1, n, 4);
            graph.AddEdge(n1, n7, 11);
            graph.AddEdge(n1, n2, 8);

            graph.AddEdge(n2, n1, 8);
            graph.AddEdge(n2, n8, 2);
            graph.AddEdge(n2, n3, 7);
            graph.AddEdge(n2, n5, 4);

            graph.AddEdge(n3, n2, 7);
            graph.AddEdge(n3, n5, 14);
            graph.AddEdge(n3, n4, 9);

            graph.AddEdge(n7, n, 8);
            graph.AddEdge(n7, n1, 11);
            graph.AddEdge(n7, n8, 7);
            graph.AddEdge(n7, n6, 1);

            graph.AddEdge(n4, n3, 9);
            graph.AddEdge(n4, n5, 10);

            graph.AddEdge(n5, n2, 4);
            graph.AddEdge(n5, n6, 2);
            graph.AddEdge(n5, n3, 14);
            graph.AddEdge(n5, n4, 10);

            graph.AddEdge(n6, n7, 1);
            graph.AddEdge(n6, n8, 6);
            graph.AddEdge(n6, n5, 2);

            graph.AddEdge(n8, n7, 7);
            graph.AddEdge(n8, n2, 2);
            graph.AddEdge(n8, n6, 6);
            
            return graph;
        }
    }
}
