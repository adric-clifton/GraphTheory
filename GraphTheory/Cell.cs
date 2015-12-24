using System;
using System.Collections.Generic;
using System.Text;

namespace GraphTheory
{
	public class Cell
	{
		public readonly uint X, Y, Z;
		private List<Direction> passages = new List<Direction>();
		public List<Direction> Pass { get { return new List<Direction>(passages.ToArray()); } }

		public Cell() : this(0, 0, 0) { }

		public Cell(uint x, uint y) : this(x, y, 0) { }

		public Cell(uint x, uint y, uint z)
		{
			X = x;
			Y = y;
			Z = z;

			Isolate();
		}

		public void Isolate()
		{
			passages.Clear();
		}

		public void Connect(Direction dir)
		{
			if (!IsConnected(dir))
				passages.Add(dir);
		}

		public void Disconnect(Direction dir)
		{
			passages.Remove(dir);
		}

		public bool IsConnected(Direction dir)
		{
			return passages.Contains(dir);
		}
	}
}
