using System;
using System.Collections.Generic;

namespace SearchingMethods
{
    // Vertex class
    class Vertex
    {
        public Vertex(int id)
        {
            // Set vertex's id
            this.Id = id;

            // Initialize predecessors list
            this.Predecessors = new List<int>();

            // Initialize successors' list
            this.Successors = new List<int>();
        }

        public void AddPredecessor(int id)
        {
            this.Predecessors.Add(id);
        }

        // Add new successor of this vertex
        public void AddSuccessor(int id)
        {
            this.Successors.Add(id);
        }

        // Print all vertex's predecessors
        public void PrintPredecessors()
        {
            Console.Write(this.Id + ": ");
            foreach (int predecessor in this.Predecessors)
            {
                Console.Write(Convert.ToString(predecessor) + ' ');
            }
            Console.WriteLine();
        }

        // Print all vertex's successors
        public void PrintSuccessors()
        {
            Console.Write(this.Id + ": ");
            foreach (int successor in this.Successors)
            {
                Console.Write(Convert.ToString(successor) + ' ');
            }
            Console.WriteLine();
        }

        // Vertex's id
        public int Id;

        // Vertex's predecessors
        public List<int> Predecessors;

        // Vertex's successors
        public List<int> Successors;
    }

    // Main class
    class Program
    {
        static List<Vertex> vertices = new List<Vertex>();

        static int adjacencyMatrixDimension = 11;

        static int[,] adjacencyMatrix =
        {
            { 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        };

        static bool BreadthFirstSearch(Vertex start, int end)
        {
            // OPEN array
            List<Vertex> open = new List<Vertex>();
            // CLOSED array
            List<Vertex> closed = new List<Vertex>();

            // Add start to OPEN
            open.Add(start);

            // Iterate through every vertex in OPEN
            while (open.Count > 0)
            {
                // Extract vertex from OPEN
                Console.Write(Convert.ToString(open[0].Id) + " -> ");
                Vertex currentVertex = open[0];
                open.RemoveAt(0);

                // Check if we found target vertex
                if (currentVertex.Id == end)
                {
                    return true;
                }

                // Check all successors of current vertex
                foreach (int successor in currentVertex.Successors)
                {
                    // Get current successor
                    Vertex currentSuccessor = vertices[successor];

                    // If it hasn't been closed yet add it to OPEN
                    if (!open.Contains(currentSuccessor) && !closed.Contains(currentSuccessor))
                    {
                        // Add vertex to OPEN's end
                        open.Add(currentSuccessor);
                    }
                }

                // Move current vertex to CLOSED
                closed.Add(currentVertex);
            }

            return false;
        }

        static bool DepthFirstSearch(Vertex start, int end)
        {
            // OPEN array
            List<Vertex> open = new List<Vertex>();
            // CLOSED array
            List<Vertex> closed = new List<Vertex>();

            // Add start to OPEN
            open.Add(start);

            // Iterate through every vertex in OPEN
            while (open.Count > 0)
            {
                // Extract vertex from OPEN
                Console.Write(Convert.ToString(open[0].Id) + " -> ");
                Vertex currentVertex = open[0];
                open.RemoveAt(0);

                // Check if we found target vertex
                if (currentVertex.Id == end)
                {
                    return true;
                }

                // Check all successors of current vertex
                foreach (int successor in currentVertex.Successors)
                {
                    // Get current successor
                    Vertex currentSuccessor = vertices[successor];

                    // If it hasn't been closed yet add it to OPEN
                    if (!open.Contains(currentSuccessor) && !closed.Contains(currentSuccessor))
                    {
                        open.Insert(0, currentSuccessor);
                    }
                }

                // Move current vertex to CLOSED
                closed.Add(currentVertex);
            }

            return false;
        }

        static HashSet<int> path = new HashSet<int>();

        static void FindPaths(Vertex vertex, int start, int end)
        {
            if (vertex.Id == start)
            {
                return;
            }

            foreach (int predecessor in vertex.Predecessors)
            {
                if (vertex.Id == end)
                {
                    path.Add(vertices[end].Id);
                }

                if (vertex.Id == start)
                {
                    path.Add(vertices[start].Id);
                }

                if (!path.Contains(predecessor))
                {
                    path.Add(predecessor);
                }

                if (predecessor == start)
                {
                    foreach (int v in path)
                    {
                        if (v != vertices[end].Id)
                        {
                            Console.Write(" -> " + v);
                        }
                        else
                        {
                            Console.Write(v);
                        }
                    }
                    Console.WriteLine();

                    path.Clear();
                    path.Add(vertices[end].Id);
                }

                FindPaths(vertices[predecessor], start, end);
            }
        }

        /* int[] shortestPath = paths[0].ToArray();
            foreach (List<int> path in paths)
            {
                if (path.Count < shortestPath.Length)
                {
                    shortestPath = path.ToArray();
                }
            }

            foreach (int vertex in shortestPath)
            {
                Console.WriteLine(vertex);
            }*/

        static void Main(string[] args)
        {
            // Initialize vertices list
            for (int i = 0; i < adjacencyMatrixDimension; ++i)
            {
                vertices.Add(new Vertex(i));
            }

            // Parse vertices' predecessors
            for (int j = 0; j < adjacencyMatrixDimension; ++j)
            {
                for (int i = 0; i < adjacencyMatrixDimension; ++i)
                {
                    if (adjacencyMatrix[i, j] == 1)
                    {
                        vertices[j].AddPredecessor(i);
                    }
                }
            }

            // Parse vertices' successors
            for (int i = 0; i < adjacencyMatrixDimension; ++i)
            {
                for (int j = 0; j < adjacencyMatrixDimension; ++j)
                {
                    if (adjacencyMatrix[i, j] == 1)
                    {
                        vertices[i].AddSuccessor(j);
                    }
                }
            }

            Console.WriteLine("IDB-19-07. Afanasyev Vadim. Searching methods.");
            Console.WriteLine();

            int start = 0;
            Console.Write("Enter start: ");
            start = Convert.ToInt32(Console.ReadLine());

            int end = 0;
            Console.Write("Enter end: ");
            end = Convert.ToInt32(Console.ReadLine());

            bool isEndFound = false;

            bool inputFlag = true;
            while (inputFlag)
            {
                int method = 0;
                Console.Write("Enter preferred method (1 – BFS, 2 – DFS): ");
                method = Convert.ToInt32(Console.ReadLine());

                switch (method)
                {
                    case 1:
                        inputFlag = false;

                        isEndFound = BreadthFirstSearch(vertices[start], end);
                        Console.WriteLine(isEndFound);

                        if (isEndFound)
                        {
                            Console.WriteLine();
                            Vertex targetVertex = vertices[end];
                            FindPaths(targetVertex, start, end);
                        }

                        break;
                    case 2:
                        inputFlag = false;

                        isEndFound = DepthFirstSearch(vertices[start], end);
                        Console.WriteLine(isEndFound);

                        if (isEndFound)
                        {
                            
                        }

                        break;
                    default:
                        Console.WriteLine("Invalid input. Choose another option.");
                        break;
                }
            }
        }
    }
}
