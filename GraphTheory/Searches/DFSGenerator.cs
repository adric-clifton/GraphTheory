using GraphTheory.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphTheory.Searches
{
	public class DFSGenerator
	{
		private static readonly DFSGenerator instance = new DFSGenerator();
		private DFSGenerator() { }
		public static DFSGenerator Self { get { return instance; } }

		private uint counter = 0;

		public bool Generate(IMaze maze, uint startX, uint startY, uint startZ, uint maxPathLength = 0)
		{
			counter = 0;
			maze.Reset();

			lock (this)
			{
				bool[, ,] visited = new bool[maze.xLength, maze.yLength, maze.zLength];
				return GenerateDFSStep(maze, startX, startY, startZ, maxPathLength, visited);
			}
		}

		public bool Step(IMaze maze, ref uint x, ref uint y, ref uint z,
			uint maxLen, bool[, ,] visited)
		{
			// increment path length counter
			counter++;

			// set this cell as visited
			visited[x, y, z] = true;

			// check counter against maximum path length
			if (maxLen != 0 && counter > maxLen)
			{
				counter = 0;
				return false;
			}

			// get a list of this cell's visitable neighbours
			List<Direction> region = GetNeighbours(maze, x, y, z);

			// for each neighbour,
			foreach (Direction neighbour in region)
			{
				uint nextX = x;
				uint nextY = y;
				uint nextZ = z;

				maze.NextCell(neighbour, ref nextX, ref nextY, ref nextZ);

				// if it hasn't been visited,
				if (!visited[nextX, nextY, nextZ])
				{
					// remove the wall between this cell and that neighbour
					maze.Connect(neighbour, x, y, z);

					x = nextX; y = nextY; z = nextZ;
					return true;
				}
			}
			return false;
		}

		private bool GenerateDFSStep(IMaze maze, uint x, uint y, uint z,
			uint maxLen, bool[, ,] visited)
		{
			// increment path length counter
			counter++;

			// check counter against maximum path length
			if (maxLen != 0 && counter > maxLen)
			{
				counter = 0;
				return true;
			}

			// set this cell as visited
			visited[x, y, z] = true;

			// get a list of this cell's visitable neighbours
			List<Direction> region = GetNeighbours(maze, x, y, z);

			// for each neighbour,
			foreach (Direction neighbour in region)
			{
				uint nextX = x;
				uint nextY = y;
				uint nextZ = z;

				maze.NextCell(neighbour, ref nextX, ref nextY, ref nextZ);

				// if it hasn't been visited,
				if (!visited[nextX, nextY, nextZ])
				{
					// remove the wall between this cell and that neighbour
					maze.Connect(neighbour, x, y, z);

					// then recurse on that neighbour
					if (!GenerateDFSStep(maze, nextX, nextY, nextZ, maxLen, visited))
						return false;
				}
			}
			return true;
		}

		private List<Direction> GetNeighbours(IMaze maze, uint x, uint y, uint z)
		{
			List<Direction> passable = new List<Direction>();

			if (x < maze.xLength - 1) passable.Add(Direction.East);
			if (x > 0) passable.Add(Direction.West);

			if (y < maze.yLength - 1) passable.Add(Direction.South);
			if (y > 0) passable.Add(Direction.North);

			if (z < maze.zLength - 1) passable.Add(Direction.Under);
			if (z > 0) passable.Add(Direction.Over);

			return passable.Shuffle();
		}
	}
}
