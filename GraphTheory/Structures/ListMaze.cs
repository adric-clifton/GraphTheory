using System.Collections.Generic;

namespace GraphTheory.Structures
{
	public class ListMaze : IMaze
	{
		private uint xSize, ySize, zSize;

		private List<Direction>[, ,] connections;

		public uint xLength { get { return xSize; } }

		public uint yLength { get { return ySize; } }

		public uint zLength { get { return zSize; } }

		public void Setup(uint width, uint height, uint depth)
		{
			xSize = width;
			ySize = height;
			zSize = depth;

			connections = new List<Direction>[xSize, ySize, zSize];
		}

		public void Reset()
		{
			Setup(xSize, ySize, zSize);
		}

		public bool IsPassable(Direction dir, uint x, uint y, uint z = 0)
		{
			List<Direction> cell = connections[x, y, z];
			return (cell == null) ? false : cell.Contains(dir);
		}

		public bool Connect(Direction dir, uint x, uint y, uint z = 0)
		{
			List<Direction> cell = connections[x, y, z];
			if (cell == null) connections[x, y, z] = new List<Direction>();
			connections[x, y, z].Add(dir);

			if (this.NextCell(dir, ref x, ref y, ref z))
			{
				List<Direction> next = connections[x, y, z];
				if (next == null) connections[x, y, z] = new List<Direction>();
				connections[x, y, z].Add(dir.Reverse());
			}
			else Disconnect(dir, x, y, z);

			return true;
		}

		public bool Disconnect(Direction dir, uint x, uint y, uint z = 0)
		{
			List<Direction> cell = connections[x, y, z];
			if (cell == null) connections[x, y, z] = new List<Direction>();
			connections[x, y, z].Remove(dir);

			if (this.NextCell(dir, ref x, ref y, ref z))
			{
				List<Direction> next = connections[x, y, z];
				if (next == null) connections[x, y, z] = new List<Direction>();
				connections[x, y, z].Remove(dir.Reverse());
			}

			return true;
		}

		public List<Direction> this[uint x, uint y, uint z] { get { return connections[x, y, z]; } }
	}
}
