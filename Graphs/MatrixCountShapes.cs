using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
    class MatrixCountShapes
    {
        private static bool validate(int[,] grid, int x, int y)
        {
            if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1) || grid[x, y] == 0)
                return false;
            return true;
        }

        private static void flipShape(int[,] grid, int x, int y)
        {
            if (!validate(grid, x, y))
                return;

            grid[x, y] = 0;

            flipShape(grid, x + 1, y);
            flipShape(grid, x - 1, y);
            flipShape(grid, x, y + 1);
            flipShape(grid, x, y - 1);
        }
        
        private static int CountShapes(int[,] grid)
        {
            int shapes = 0;

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 1)
                    {
                        shapes++;
                        flipShape(grid, i, j);
                    }
                }
            }
            return shapes;
        }

        private class point
        {
            public int x, y;
            public point(int xx, int yy)
            {
                x = xx;
                y = yy;
            }
        }

        private static int CountShapesBFS(int[,] grid)
        {
            int shapes = 0;

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 1)
                    {
                        grid[i, j] = 0;
                        shapes++;
                        Queue<point> queue = new Queue<point>();
                        queue.Enqueue(new point(i, j));
                        while (queue.Count > 0)
                        {
                            point cur = queue.Dequeue();
                            if (validate(grid, cur.x + 1, cur.y))
                            {
                                grid[cur.x + 1, cur.y] = 0;
                                queue.Enqueue(new point(cur.x + 1, cur.y));
                            }
                            if (validate(grid, cur.x - 1, cur.y))
                            {
                                grid[cur.x - 1, cur.y] = 0;
                                queue.Enqueue(new point(cur.x - 1, cur.y));
                            }
                            if (validate(grid, cur.x, cur.y + 1))
                            {
                                grid[cur.x, cur.y + 1] = 0;
                                queue.Enqueue(new point(cur.x, cur.y + 1));
                            }
                            if (validate(grid, cur.x, cur.y - 1))
                            {
                                grid[cur.x, cur.y - 1] = 0;
                                queue.Enqueue(new point(cur.x, cur.y - 1));
                            }
                        }
                    }
                }
            }
            return shapes;
        }

        public static void driver()
        {
            int[, ] grid = {{0, 0, 1, 1, 0},
                            {0, 0, 1, 0, 1},
                            {1, 0, 0, 0, 0},
                            {0, 0, 0, 0, 1}};

            //Console.WriteLine("Total shapes = {0}", CountShapes(grid));
            Console.WriteLine("Total shapes = {0}", CountShapesBFS(grid));
        }
    }
}
