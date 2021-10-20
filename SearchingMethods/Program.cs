using System;

namespace SearchingMethods
{
    class Program
    {
        static void BreadthFirstSearch()
        {

        }

        static void DepthFirstSearch()
        {

        }

        static void Main(string[] args)
        {
            // User input
            Console.Write("Enter matrix dimension: ");
            int dimension = Convert.ToInt32(Console.ReadLine());

            // Creating adjacency matrix
            Random rnd = new Random();
            int[,] adjacencyMatrix = new int[dimension, dimension];
            for (int i = 0; i < dimension; ++i)
            {
                for (int j = 0; j < dimension; ++j)
                {
                    // Filling matrix with random integers for debugging
                    adjacencyMatrix[i, j] = rnd.Next(0, 9);
                }
            }

            // Printing adjacency matrix
            for (int i = 0; i < dimension; ++i)
            {
                for (int j = 0; j < dimension; ++j)
                {
                    Console.Write(adjacencyMatrix[i, j] + "|");
                }
                Console.WriteLine();
            }
        }
    }
}
