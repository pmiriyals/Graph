using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
    class Edge
    {
        public int v1;
        public int v2;
        public int cost;

        public Edge(int v1, int v2, int cost)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.cost = cost;
        }
    }

    //Assume each vertex value is unqiue
    class GraphKruskal
    {
        public List<int> Vertices = new List<int>();
        public List<Edge> Edges = new List<Edge>();

        public void AddEdge(int v1, int v2, int cost)
        {
            Edges.Add(new Edge(v1, v2, cost));
            if (!Vertices.Contains(v1))
                Vertices.Add(v1);
            if (!Vertices.Contains(v2))
                Vertices.Add(v2);
        }
    }
    
    //Not working:
    //Time: O(E log V)
    //Need to implement union-find algorithm, determine if adding an edge to mst forms a cycle or not
    //1. Sort edges in a graph by weight/cost
    //2. Take the min/first edge from the sorted list and ensure that adding this edge to MST will not form a cycle, if yes then add the edge
    class Kruskals
    {
        private static int find(int[] parent, int i)
        {
            if (parent[i] == -1)
                return i;
            return find(parent, parent[i]);
        }

        private static void union(int[] parent, int src, int dest, int x, int y)
        {
            if (x == -1 && y == -1)
            {
                parent[src] = dest;
            }
            else
            { 
                
            }
        }
        
        public static void KurskalsAlg(GraphKruskal gk)
        {
            List<Edge> edges = gk.Edges.OrderBy(o => o.cost).ToList<Edge>();    //sort edges by weight/cost
            GraphKruskal mstGraph = new GraphKruskal();
            HashSet<int> verticesInMST = new HashSet<int>();

            int[] parent = new int[gk.Vertices.Count];
            for (int i = 0; i < parent.Length; i++)
                parent[i] = -1;

            foreach (Edge e in edges)
            {
                if(!(verticesInMST.Contains(e.v1) && verticesInMST.Contains(e.v2)))
                {
                    mstGraph.AddEdge(e.v1, e.v2, e.cost);
                    if (!verticesInMST.Contains(e.v1))
                        verticesInMST.Add(e.v1);

                    if (!verticesInMST.Contains(e.v2))
                        verticesInMST.Add(e.v2);
                }
            }
        }

        public static void driver()
        {
            GraphKruskal gk = new GraphKruskal();
            buildgraph(gk);

        }

        private static void buildgraph(GraphKruskal gk)
        {
            gk.AddEdge(0, 1, 4);
            gk.AddEdge(0, 7, 8);
            gk.AddEdge(1, 7, 11);
            gk.AddEdge(1, 2, 8);
            gk.AddEdge(7, 8, 7);
            gk.AddEdge(7, 6, 1);
            gk.AddEdge(6, 8, 6);
            gk.AddEdge(6, 5, 2);
            gk.AddEdge(2, 8, 2);
            gk.AddEdge(2, 3, 7);
            gk.AddEdge(2, 5, 4);
            gk.AddEdge(5, 4, 10);
            gk.AddEdge(5, 3, 14);
            gk.AddEdge(3, 4, 9);
        }
    }
}
