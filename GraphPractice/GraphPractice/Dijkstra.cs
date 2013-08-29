using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphPractice
{
    class Dijkstra
    {
        public static void driver()
        {
            Graph graph = Driver.buildTree();
            Graph mst = graph.Dijkstras();
            mst.PrintEdges();
        }
    }
}
