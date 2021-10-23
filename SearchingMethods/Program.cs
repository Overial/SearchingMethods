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

            // Set vertex's status
            this.IsClosed = false;

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

        // Vertex's status
        public bool IsClosed;

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

        static void FillAdjacencyMatrix()
        {
            for (int i = 0; i < adjacencyMatrixDimension; ++i)
            {
                for (int j = 0; j < adjacencyMatrixDimension; ++j)
                {
                    Console.Write("Enter [{0}][{1}] element: ", i, j);
                    adjacencyMatrix[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }
        }

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
                currentVertex.IsClosed = true;
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
                currentVertex.IsClosed = true;
            }

            return false;
        }

        static List<Vertex> rClosed = new List<Vertex>();

        static bool RecursiveDepthFirstSearch(Vertex vertex, int end)
        {
            if (vertex.Id == end)
            {
                return true;
            }
            else
            {
                Console.Write(vertex.Id + " -> ");
                rClosed.Add(vertex);
                vertex.IsClosed = true;

                foreach (int successor in vertex.Successors)
                {
                    Vertex currentSuccessor = vertices[successor];

                    if (RecursiveDepthFirstSearch(currentSuccessor, end) == true)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static bool RecursiveDepthFirstSearchWithSolvingPath(Vertex vertex, int start, int end, List<Vertex> solvingPath)
        {
            if (vertex.Id == end)
            {
                Console.WriteLine();
                Console.Write(vertices[start].Id + " -> ");
                foreach (Vertex v in solvingPath)
                {
                    Console.Write(v.Id);
                    if (v != solvingPath[solvingPath.Count - 1])
                    {
                        Console.Write(" -> ");
                    }
                }
                Console.WriteLine(" -> True");

                return true;
            }
            else
            {
                rClosed.Add(vertex);
                vertex.IsClosed = true;

                foreach (int successor in vertex.Successors)
                {
                    Vertex currentSuccessor = vertices[successor];
                    solvingPath.Add(currentSuccessor);

                    if (RecursiveDepthFirstSearchWithSolvingPath(currentSuccessor, start, end, solvingPath) == true)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static HashSet<int> path = new HashSet<int>();

        static void FindPaths(Vertex vertex, int start, int end)
        {
            foreach (int predecessor in vertex.Predecessors)
            {
                if (!vertices[predecessor].IsClosed)
                {
                    break;
                }

                if (vertex.IsClosed)
                {
                    path.Add(vertex.Id);
                }

                if (vertex.Id == end)
                {
                    path.Add(vertices[end].Id);
                }

                if (!path.Contains(predecessor) && vertices[predecessor].IsClosed)
                {
                    path.Add(predecessor);
                }

                if (predecessor == start)
                {
                    int[] currentPath = new int[path.Count];
                    path.CopyTo(currentPath);
                    Array reversedPath = Array.CreateInstance(typeof(int), currentPath.Length);
                    currentPath.CopyTo(reversedPath, 0);
                    Array.Reverse(reversedPath);

                    foreach (int v in reversedPath)
                    {
                        Console.Write(v);
                        if (v != vertices[end].Id)
                        {
                            Console.Write(" -> ");
                        }
                    }
                    Console.WriteLine();

                    path.Clear();
                    path.Add(vertices[end].Id);
                }

                FindPaths(vertices[predecessor], start, end);
            }
        }

        static void Main(string[] args)
        {
            // FillAdjacencyMatrix();

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
                Console.Write("Enter preferred method (1 – BFS, 2 – DFS, 3 – RDFS, 4 – RDFSP): ");
                method = Convert.ToInt32(Console.ReadLine());

                switch (method)
                {
                    case 1:
                        inputFlag = false;

                        Console.WriteLine("\nProcessing:");
                        isEndFound = BreadthFirstSearch(vertices[start], end);
                        Console.WriteLine(isEndFound);

                        break;
                    case 2:
                        inputFlag = false;

                        Console.WriteLine("\nProcessing:");
                        isEndFound = DepthFirstSearch(vertices[start], end);
                        Console.WriteLine(isEndFound);

                        break;
                    case 3:
                        inputFlag = false;

                        Console.WriteLine("\nProcessing:");
                        isEndFound = RecursiveDepthFirstSearch(vertices[start], end);
                        Console.Write(vertices[end].Id + " -> ");
                        Console.WriteLine(isEndFound);

                        break;
                    case 4:
                        inputFlag = false;

                        List<Vertex> solvingPath = new List<Vertex>();
                        isEndFound = RecursiveDepthFirstSearchWithSolvingPath(vertices[start], start, end, solvingPath);
                        if (!isEndFound)
                        {
                            Console.WriteLine();
                            foreach (Vertex v in solvingPath)
                            {
                                Console.Write(v.Id);
                                if (v != solvingPath[solvingPath.Count - 1])
                                {
                                    Console.Write(" -> ");
                                }
                            }
                            Console.WriteLine(" -> False");
                        }

                        break;
                    default:
                        Console.WriteLine("Invalid input. Choose another option.");
                        break;
                }

                if (isEndFound)
                {
                    Console.WriteLine();
                    Vertex targetVertex = vertices[end];
                    Console.WriteLine("Result: ");
                    FindPaths(targetVertex, start, end);
                }
            }
        }
    }
}
