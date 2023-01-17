using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
	internal static class Field
	{
		private static int _width = 20;
		private static int _height = 20;

		public static int Width
		{
			get
			{
				return _width;
			}
			
		}

		public static int Height
		{
			get
			{
				return _height;
			}

		}


		private static bool[][] _heap;

		static Field()
		{
			_heap = new bool[Height][];
			for(int i = 0; i < Height; i++)
			{
				_heap[i] = new bool[Width];
			}
		}

		public static void TryDeleteLines()
		{
			for(int i = 0; i < Height; i++)
			{
				int counter = 0;
				for(int j = 0; j < Width; j++)
				{
					if(_heap[i][j])
						counter++;
				}
				if(counter == Width)
				{
					DeleteLines(i);
					Redraw();
				}
			}
		}

		private static void Redraw()
		{
			for(int i = 0; i < Height; i++)
			{
				for(int j = 0; j < Width; j++)
				{
					if(_heap[i][j])
						DrawerProvider.Drawer.DrawPoint(j, i);
					else
						DrawerProvider.Drawer.HidePoint(j, i);
				}
			}
		}

		private static void DeleteLines(int line)
		{
			for(int i = line; i >= 0; i--)
			{
				for(int k = 0; k < Width; k++)
				{
					if(i == 0)
					{
						_heap[i][k] = false;
					}
					else
					{
						_heap[i][k] = _heap[i - 1][k];
					}
				}
			}
		}

		public static bool CheckStrike(Point p)
		{
			return _heap[p.Y][p.X];
		}

		public static void AddFigure(Figure fig)
		{
			foreach(var p in fig.Points)
			{
				_heap[p.Y][p.X] = true;
			}
		}
	}
}
