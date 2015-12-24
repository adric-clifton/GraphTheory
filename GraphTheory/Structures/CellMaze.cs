
using System.Collections.Generic;
namespace GraphTheory.Structures
{
	public class CellMaze : IMaze
	{
		private uint xSize, ySize, zSize;

		private Cell[, ,] cells;

		public uint xLength { get { return xSize; } }

		public uint yLength { get { return ySize; } }

		public uint zLength { get { return zSize; } }

		public void Setup(uint width, uint height, uint depth)
		{
			xSize = width;
			ySize = height;
			zSize = depth;

			cells = new Cell[xSize, ySize, zSize];

            for (uint x = 0; x < xSize; x++)
            {
                for (uint y = 0; y < ySize; y++)
                {
                    for (uint z = 0; z < zSize; z++)
                    {
                        cells[x, y, z] = new Cell(x, y, z);
                    }
                }
            }
        }

		public void Reset()
		{
			Setup(xSize, ySize, zSize);
		}

		public bool IsPassable(Direction dir, uint x, uint y, uint z = 0)
		{
			return cells[x, y, z].IsConnected(dir);
		}

		public bool Connect(Direction dir, uint x, uint y, uint z = 0)
		{
			cells[x, y, z].Connect(dir);

			switch (dir)
			{
				case Direction.East:
					cells[x + 1, y, z].Connect(Direction.West);
					break;
				case Direction.West:
					cells[x - 1, y, z].Connect(Direction.East);
					break;
				case Direction.South:
					cells[x, y + 1, z].Connect(Direction.North);
					break;
				case Direction.North:
					cells[x, y - 1, z].Connect(Direction.South);
					break;
				case Direction.Over:
					cells[x, y, z + 1].Connect(Direction.Under);
					break;
				case Direction.Under:
					cells[x, y, z - 1].Connect(Direction.Over);
					break;
			}

			return true;
		}

		public bool Disconnect(Direction dir, uint x, uint y, uint z = 0)
		{
			cells[x, y, z].Disconnect(dir);

			switch (dir)
			{
				case Direction.East:
					cells[x + 1, y, z].Disconnect(Direction.West);
					break;
				case Direction.West:
					cells[x - 1, y, z].Disconnect(Direction.East);
					break;
				case Direction.South:
					cells[x, y + 1, z].Disconnect(Direction.North);
					break;
				case Direction.North:
					cells[x, y - 1, z].Disconnect(Direction.South);
					break;
				case Direction.Over:
					cells[x, y, z + 1].Disconnect(Direction.Under);
					break;
				case Direction.Under:
					cells[x, y, z - 1].Disconnect(Direction.Over);
					break;
			}

			return true;
		}

		public List<Direction> this[uint x, uint y, uint z] { get { return cells[x, y, z].Pass; } }
	}
}
