using System.Collections.Generic;

namespace GraphTheory.Structures
{
	public interface IMaze
	{
		uint xLength { get; }
		uint yLength { get; }
		uint zLength { get; }

		void Setup(uint width, uint height, uint depth);
		void Reset();

		bool IsPassable(Direction dir, uint x, uint y, uint z = 0);
		bool Connect(Direction dir, uint x, uint y, uint z = 0);
		bool Disconnect(Direction dir, uint x, uint y, uint z = 0);

		List<Direction> this[uint x, uint y, uint z] { get; }
	}
}
