using System;

namespace SearchingMethods
{
    class Program
    {
        static void DepthFirstSearch()
        {

        }

        static void Main(string[] args)
        {
            // User input
            Console.Write("Enter rows amount: ");
            int rows = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter cols amount: ");
            int cols = Convert.ToInt32(Console.ReadLine());

            // Creating adjacency matrix
            Random rnd = new Random();
            int[,] iAdjacencyMatrix = new int[rows, cols];
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    iAdjacencyMatrix[i, j] = rnd.Next(0, 9);
                }
            }

            // Printing adjancency matrix
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    Console.Write(iAdjacencyMatrix[i, j] + "|");
                }
                Console.WriteLine();
            }
        }
    }
}
