using GraphTheory.Structures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SecureRand = System.Security.Cryptography.RNGCryptoServiceProvider;

namespace GraphTheory
{
	[Flags]
	public enum Direction
    {
		Zero = 0,
		North = 1 << 0,	// 1
		South = 1 << 1,	// 2
        East = 1 << 2,	// 4
        West = 1 << 3,	// 8
        Over = 1 << 4,	// 16
        Under = 1 << 5,	// 32
    }

	public static class Util
	{
		private static SecureRand provider = new SecureRand();

        private static int NextInt(int max)
        {
            byte[] box = new byte[1];
			do { provider.GetBytes(box); }
			while (!(box[0] < max * (Byte.MaxValue / max)));
            return (box[0] % max);
        }

		public static bool NextCell(this IMaze maze, 
			Direction dir, ref uint x, ref uint y, ref uint z)
		{
			switch (dir)
			{
				case Direction.East:  if (++x < maze.xLength) return true; x--; return false;
				case Direction.West:  if (--x >= 0) return true; x++; return false;
				case Direction.South: if (++y < maze.yLength) return true; y--; return false;
				case Direction.North: if (--y >= 0) return true; y++; return false;
				case Direction.Over:  if (++z < maze.zLength) return true; z--; return false;
				case Direction.Under: if (--z >= 0) return true; z++; return false;
				default: return true;
			}
		}

		public static List<T> Shuffle<T>(this List<T> toShuffle)
		{
			int n = toShuffle.Count;
			while (n > 1)
			{
				int k = NextInt(n);
				n--;
				T value = toShuffle[k];
				toShuffle[k] = toShuffle[n];
				toShuffle[n] = value;
			}
			return toShuffle;
		}

		public static Direction RandDirection { get { return (Direction)(1 << NextInt(5)); } }

		public static Direction Reverse(this Direction initial)
		{
			if (initial > Direction.Zero)
			{	//return (Direction)(((int)initial % 2 == 0) ? initial - 1 : initial + 1);
				if (initial.HasFlag(Direction.North) ||
						initial.HasFlag(Direction.East) || initial.HasFlag(Direction.Over))
					return (Direction)((int)initial << 1);
				else return (Direction)((int)initial >> 1);
			}

			return initial;
		}

		public static string ToPrintString(this List<Direction> cell)
		{
			StringBuilder result = new StringBuilder();

			if (cell == null) return "blank";

			if (cell.Contains(Direction.North)) result.Append("N");
			if (cell.Contains(Direction.East))  result.Append("E");
			if (cell.Contains(Direction.South)) result.Append("S");
			if (cell.Contains(Direction.West))  result.Append("W");

			if (result.Length == 0) result.Append("blank");

			return result.ToString();
		}

		public static Image ToImage(this List<Direction> cell)
		{
			string filename = "Icons\\" + cell.ToPrintString() + ".png";
			return Image.FromFile(filename);
		}
	}
}
