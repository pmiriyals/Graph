using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
    class FloydWarshall
    {
        public static void FloydWarshallAlg(int[,] grid)
        {
            int n = (grid.GetLength(0) > grid.GetLength(1) ? grid.GetLength(0) : grid.GetLength(1));
            int[,] dist = new int[n, n];

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    dist[i, j] = grid[i, j];
                }
            }

            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (dist[i, k] != Int32.MaxValue && dist[k, j] != Int32.MaxValue && dist[i, j] > (dist[i, k] + dist[k, j]))
                            dist[i, j] = dist[i, k] + dist[k, j];
                    }
                }
            }
            printArr(dist);
        }

        private static void printArr(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                    Console.Write(" {0} ", arr[i, j]);
                Console.WriteLine();
            }
        }

        public static void driver()
        {
            int[,] grid = {    {0, 5, Int32.MaxValue, 10},
                               {Int32.MaxValue, 0, 3, Int32.MaxValue},
                               {Int32.MaxValue, Int32.MaxValue, 0, 1},
                               {Int32.MaxValue, Int32.MaxValue, Int32.MaxValue, 0}
                           };

            FloydWarshallAlg(grid);
        }
    }
}
