using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Draw
{
	[Serializable]
	class LineShape : BaseShape
	{
		public override BaseShape copySelf()
		{
			LineShape copyLineShape = new LineShape();
			copyLineShape.setP1(this.getP1());
			copyLineShape.setP2(this.getP2());
			copyLineShape.penColor = this.penColor;
			copyLineShape.penWidth = this.penWidth;
			return copyLineShape;
		}
		public override bool catchShape(Point p3)
		{
			Point p1 = this.getP1();
			Point p2 = this.getP2();
			double l1 = Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2);
			double l2 = Math.Pow(p1.X - p3.X, 2) + Math.Pow(p1.Y - p3.Y, 2);
			double l3 = Math.Pow(p3.X - p2.X, 2) + Math.Pow(p3.Y - p2.Y, 2);
			if (Math.Pow(l2, 0.5) + Math.Pow(l3, 0.5) - Math.Pow(l1, 0.5) < 0.1)
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
