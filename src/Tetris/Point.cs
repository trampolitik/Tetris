using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
	internal class Point
	{
		public int X { get; set; }
		public int Y { get; set; }
		public char C { get; set; }
		public void Draw()
		{
			DrawerProvider.Drawer.DrawPoint(X, Y);
		}
		public Point(Point p)
		{
			X = p.X;
			Y = p.Y;
			C = p.C;
		}
		public Point(int a, int b, char sym)
		{
			X = a;
			Y = b;
			C = sym;
		}

		internal void Move(Direction dir)
		{
			switch(dir)
			{
				case Direction.DOWN:
					Y += 1;
					break;
				case Direction.LEFT:
					X -= 1;
					break;
				case Direction.RIGHT:
					X += 1;
					break;
			}
		}

		internal void Hide()
		{
			DrawerProvider.Drawer.HidePoint(X, Y);
		}
	}
}
