using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Draw
{
	[Serializable]
	class CircleShape : BaseShape
	{
		public override BaseShape copySelf()
		{
			CircleShape copyCircleShape = new CircleShape();
			copyCircleShape.setP1(this.getP1());
			copyCircleShape.setP2(this.getP2());
			copyCircleShape.penColor = this.penColor;
			copyCircleShape.penWidth = this.penWidth;
			return copyCircleShape;
		}

		public override bool catchShape(Point p3)
		{
			Point p1 = this.getP1();
			Point p2 = this.getP2();
			int r = (int)Math.Pow(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2), 0.5);
			if (Math.Abs(Math.Pow(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p1.Y - p3.Y, 2), 0.5) - r) < 3)
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
