using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphss
{
    //http://community.topcoder.com/tc?module=Static&d1=tutorials&d2=graphsDataStrucs1
    //We need to use Dijkstras or A* alg, but also need to track predecessor info to find the shortest path.
    //Using a bottom up approach, we can only calculate the shortest between 2 vertices
    public class _2DArrReachHome
    {
        private struct point
        {
            public int x;
            public int y;
            
            public point(int xa, int ya)
            {
                this.x = xa;
                this.y = ya;
            }
        }        

        private static bool validate(int[, ] grid, point p)
        {
            if (p.x < 0 || p.x >= grid.GetLength(0) || p.y < 0 || p.y >= grid.GetLength(1) || grid[p.x, p.y] == -1)
                return false;
            return true;
        }

        private static bool ishome(int[,] grid, point p)
        {
            return (grid[p.x, p.y] == 1);
        }               

        private static int FindMin(int[,] grid, point p)
        {
            Queue<point> queue = new Queue<point>();
            queue.Enqueue(p);
            int mincost = 0;
            grid[p.x, p.y] = -1;
            while (queue.Count > 0)
            {
                p = queue.Dequeue();
                Console.Write(" [{0}, {1}]", p.x, p.y);
                if (grid[p.x, p.y] == 1)
                {
                    return mincost;
                }

                point pr = new point(p.x + 1, p.y);
                point pl = new point(p.x - 1, p.y);
                point pu = new point(p.x, p.y - 1);
                point pd = new point(p.x, p.y + 1);

                if (validate(grid, pr))
                {
                    if(grid[pr.x, pr.y] != 1)
                        grid[pr.x, pr.y] = -1;
                    queue.Enqueue(pr);
                }

                if (validate(grid, pl))
                {
                    if(grid[pl.x, pl.y] != 1)
                        grid[pl.x, pl.y] = -1;
                    queue.Enqueue(pl);
                }

                if (validate(grid, pu))
                {
                    if(grid[pu.x, pu.y] != 1)
                        grid[pu.x, pu.y] = -1;
                    queue.Enqueue(pu);
                }

                if (validate(grid, pd))
                {
                    if(grid[pd.x, pd.y] != 1)
                        grid[pd.x, pd.y] = -1;
                    queue.Enqueue(pd);
                }
            }

            return -1;
        }
        
        private static void FindPath(int[,] arr)
        {
            List<point> curPath = new List<point>();
            List<point> minPath = new List<point>();
            point p;
            p.x = 0;
            p.y = arr.GetLength(1)-1;
            FindMin(arr, p);
            //int minCost = 0;
            //minCost = FindMinPath(arr, curPath, minPath, p);
            //Console.WriteLine("min cost = " + minCost);
            //Console.WriteLine("Min path = ");

            //foreach (point pt in minPath)
            //{
            //    Console.Write(" [{0}, {1}]", pt.x, pt.y);
            //}
            //Console.WriteLine("Min count = " + minPath.Count);
        }

        public static void driver()
        {
            int[,] arr = { {0, 0, 0, -1, 0, 0, 0},
                           {0, -1, 0, -1, 0, -1, 0},
                           {0, 1, 0, 0, 0, 0, 0},
                           {0, 0, 0, -1, 0, 0, 0},
                           {0, 0, 0, 0, 0, -1, 0}};

            FindPath(arr);
        }
    }
}
