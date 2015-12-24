using GraphTheory.Searches;
using GraphTheory.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GraphTheory
{
	public partial class CreatorForm : Form
	{
		uint width = 6;
		uint height = 5;
		IMaze maze;

		uint x = 3;
		uint y = 2;
		uint z = 0;

		Stack<Tuple<uint, uint, uint>> depth = new Stack<Tuple<uint, uint, uint>>();

		bool[, ,] visited;

		public CreatorForm()
		{
			InitializeComponent();

			maze = new ListMaze();
			maze.Setup(width, height, 1);

			visited = new bool[maze.xLength, maze.yLength, maze.zLength];
			DFSGenerator.Self.Step(maze, ref x, ref y, ref z, 0, visited);

			depth.Push(new Tuple<uint, uint, uint>(x, y, z));
			Draw();
		}

		private void Draw()
		{
			for (uint i = 0; i < width; i++)
			{
				for (uint j = 0; j < height; j++)
				{
					int index = (int)(i + j * width);
					((PictureBox)layoutGrid.Controls[index]).Image = maze[i, j, 0].ToImage();
				}
			}
		}

		private void cmdGenerate_Click(object sender, EventArgs e)
		{
		}

		private void cmdStep_Click(object sender, EventArgs e)
		{
			if (DFSGenerator.Self.Step(maze, ref x, ref y, ref z, 5, visited))
				depth.Push(new Tuple<uint, uint, uint>(x, y, z));
			else
			{
				if (depth.Count == 0) return;

				var state = depth.Pop();
				x = state.Item1; y = state.Item2; z = state.Item3;
			}

			Draw();
		}
	}
}
