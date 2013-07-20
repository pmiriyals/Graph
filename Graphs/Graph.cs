using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
    public class Node<T>
    {
        public T data { get; set; }
        public List<Node<T>> neighbors = new List<Node<T>>();
        public List<int> cost = new List<int>();
        public bool isVisited { get; set; }

        public Node(T data)
        {
            this.data = data;
            isVisited = false;
        }

        public void AddNeightbors(Node<T> node, int cst)
        {
            this.neighbors.Add(node);
            this.cost.Add(cst);
        }

    }

    public class Graph<T>
    {
        public Node<T> root { get; set; }
        public List<Node<T>> nodeset { get; set; }

        public Graph() : this(null) { }
        public Graph(Node<T> node)
        {
            this.root = node;
            nodeset = new List<Node<T>>();
        }

        public bool BFSIter(T val)
        {
            if (root == null || val == null)
                return false;

            Queue<Node<T>> queue = new Queue<Node<T>>();
            queue.Enqueue(root);
            root.isVisited = true;
            Node<T> node = null;
            Console.Write("Iterative bfs traversal = ");
            while (queue.Count > 0)
            {
                node = queue.Dequeue();
                Console.Write(" {0} ", node.data.ToString());
                if (node.data.ToString() == val.ToString())
                    return true;

                foreach (Node<T> n in node.neighbors)
                {
                    if (n.isVisited == false)
                    {
                        n.isVisited = true;
                        queue.Enqueue(n);
                    }
                }
            }
            clearIsVistedFlag();
            return false;
        }

        public bool DFSIter(T val)
        {            
            if (root == null || val == null)
                return false;

            Stack<Node<T>> stk = new Stack<Node<T>>();
            stk.Push(root);
            root.isVisited = true;
            Node<T> node = null;
            Console.Write("Iterative dfs traversal = ");
            while (stk.Count > 0)
            {
                node = stk.Pop();
                Console.Write(" {0} ", node.data.ToString());
                if (node.data.ToString() == val.ToString())
                    return true;

                foreach (Node<T> n in node.neighbors)
                {
                    if (n.isVisited == false)
                    {
                        n.isVisited = true;
                        stk.Push(n);
                    }
                }
            }
            clearIsVistedFlag();
            return false;
        }

        public bool DFSRecur(T val)
        {
            Console.Write("\nDFSRecur traversal = ");
            root.isVisited = true;
            bool isExists = DFSRecur(root, val);
            Console.WriteLine("\nDFSRecur element " + (isExists ? "found" : "not found"));
            clearIsVistedFlag();
            return isExists;
        }

        private bool DFSRecur(Node<T> node, T val)
        {
            if (node == null)
                return false;

            Console.Write(" {0} ", node.data.ToString());

            if (node.data.ToString() == val.ToString())
                return true;

            foreach (Node<T> n in node.neighbors)
            {
                if (!n.isVisited)
                {
                    n.isVisited = true;
                    return DFSRecur(n, val);
                }
            }            
            return false;
        }

        public void clearIsVistedFlag()
        {
            foreach (Node<T> node in nodeset)
                node.isVisited = false;
        }
        
    }

    public class GraphDriver
    {
        public static void driver()
        {
            Graph<int> graph = buildTree();
            Console.WriteLine("\nDFS Iter: element " + (graph.DFSIter(1) ? "found" : "not found"));            
            Console.WriteLine("\nBFS Iter: element " + (graph.BFSIter(1) ? "found" : "not found"));
            graph.DFSRecur(8);
        }

        public static Graph<int> buildTree()
        {
            Graph<int> graph = new Graph<int>();
            Node<int> n = new Node<int>(0);
            graph.root = n;
            Node<int> n1 = new Node<int>(1);
            Node<int> n2 = new Node<int>(2);
            Node<int> n3 = new Node<int>(3);
            Node<int> n4 = new Node<int>(4);
            Node<int> n5 = new Node<int>(5);
            Node<int> n6 = new Node<int>(6);
            Node<int> n7 = new Node<int>(7);
            Node<int> n8 = new Node<int>(8);

            graph.nodeset.Add(n);
            graph.nodeset.Add(n1);
            graph.nodeset.Add(n2);
            graph.nodeset.Add(n3);
            graph.nodeset.Add(n4);
            graph.nodeset.Add(n5);
            graph.nodeset.Add(n6);
            graph.nodeset.Add(n7);
            graph.nodeset.Add(n8);

            n.AddNeightbors(n1, 4);
            n.AddNeightbors(n7, 8);

            n1.AddNeightbors(n, 4);
            n1.AddNeightbors(n7, 11);
            n1.AddNeightbors(n2, 8);

            n7.AddNeightbors(n, 8);
            n7.AddNeightbors(n1, 11);
            n7.AddNeightbors(n8, 7);
            n7.AddNeightbors(n6, 1);

            n2.AddNeightbors(n1, 8);
            n2.AddNeightbors(n8, 2);
            n2.AddNeightbors(n3, 7);
            n2.AddNeightbors(n5, 4);

            n8.AddNeightbors(n7, 7);
            n8.AddNeightbors(n2, 2);
            n8.AddNeightbors(n6, 6);

            n6.AddNeightbors(n7, 1);
            n6.AddNeightbors(n8, 6);
            n6.AddNeightbors(n5, 2);

            n5.AddNeightbors(n2, 4);
            n5.AddNeightbors(n6, 2);
            n5.AddNeightbors(n3, 14);
            n5.AddNeightbors(n4, 10);

            n3.AddNeightbors(n2, 7);
            n3.AddNeightbors(n5, 14);
            n3.AddNeightbors(n4, 9);

            n4.AddNeightbors(n3, 9);
            n4.AddNeightbors(n5, 10);

            return graph;
        }
    }
}
