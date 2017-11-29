using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw
{
	[Serializable]
	class RectangleShape : BaseShape
	{
		public override BaseShape copySelf()
		{
			RectangleShape copyRectangleShape = new RectangleShape();
			copyRectangleShape.setP1(this.getP1());
			copyRectangleShape.setP2(this.getP2());
			copyRectangleShape.penColor = this.penColor;
			copyRectangleShape.penWidth = this.penWidth;
			return copyRectangleShape;
		}

		public override bool catchShape(Point p3)
		{
			Point p1 = this.getP1();
			Point p2 = this.getP2();
			int x1 = p1.X < p2.X ? p1.X : p2.X;
			int x2 = p1.X > p2.X ? p1.X : p2.X;
			int y1 = p1.Y < p2.Y ? p1.Y : p2.Y;
			int y2 = p1.Y > p2.Y ? p1.Y : p2.Y;
			if (((Math.Abs(p3.Y - y1)<3 || Math.Abs(p3.Y - y2) < 3) && p3.X>x1- 3&& p3.X<x2+3)|| ((Math.Abs(p3.X - x1) < 3 || Math.Abs(p3.X - x2) < 3) && p3.Y > y1 - 3 && p3.Y < y2 + 3))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
