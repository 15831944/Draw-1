using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Draw
{
	[Serializable]
	class EllipseShape : BaseShape
	{
		public override BaseShape copySelf()
		{
			EllipseShape copyEllipseShape = new EllipseShape();
			copyEllipseShape.setP1(this.getP1());
			copyEllipseShape.setP2(this.getP2());
			copyEllipseShape.penColor = this.penColor;
			copyEllipseShape.penWidth = this.penWidth;
			return copyEllipseShape;
		}

		public override bool catchShape(Point p3)
		{
			Point p1 = this.getP1();
			Point p2 = this.getP2();
			Point center = new Point((p1.X+p2.X)/2,(p1.Y+p2.Y)/2);
			double a = Math.Abs(p2.X - p1.X) / 2;
			double b = Math.Abs(p2.Y - p1.Y) / 2;
			int c = (int)(Math.Pow(Math.Abs(Math.Pow(a, 2) - Math.Pow(b, 2)), 0.5));
			Point c1 = new Point();
			Point c2 = new Point();
			if (a > b)
			{
				c1.X = center.X - c ;
				c1.Y = center.Y;
				c2.X = center.X + c;
				c2.Y = center.Y;
			}
			else
			{
				c1.X = center.X;
				c1.Y = center.Y + c;
				c2.X = center.X;
				c2.Y = center.Y - c;
			}
			double l1 = Math.Pow(Math.Pow(p3.X - c1.X, 2) + Math.Pow(p3.Y - c1.Y, 2), 0.5);
			double l2 = Math.Pow(Math.Pow(p3.X - c2.X, 2) + Math.Pow(p3.Y - c2.Y, 2), 0.5);
			double A = (a > b ? a : b);
			if (l1+l2-2*A<3 && l1+l2-2*A>-3)
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
